using AutoMapper;
using HM.Data.DataTransferObjects;
using HM.DataAccess.Contracts;
using HM.DataAccess.Models;
using HM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Services.Services
{
    public class RoomService : IRoomService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Room Methods
        public async Task<List<RoomDto>> GetRoomsAsync()
        {
            var rooms = await _unitOfWork.RoomRepository.GetAllAsync();
            return _mapper.Map<List<RoomDto>>(rooms);
        }
     
        public async Task<List<RoomSummaryDto>> GetRoomsSummaryAsync()
        {
            var rooms = await _unitOfWork.RoomRepository.GetAllAsync();
            return _mapper.Map<List<RoomSummaryDto>>(rooms);
        }

        public async Task<RoomAnalyticsDto> GetRoomAnalyticsAsync()
        {
            var rooms = await _unitOfWork.RoomRepository.GetAllAsync();
            var roomOccupancyDtos = _mapper.Map<List<RoomForOccupancyDto>>(rooms);
            var roomRatingDtos = _mapper.Map<List<RoomForRateDto>>(rooms);

            return new RoomAnalyticsDto()
            {
                BestRatedRooms = roomRatingDtos.Where(x => x.Rating.HasValue).OrderByDescending(x => x.Rating).Take(5),
                WorstRatedRooms = roomRatingDtos.Where(x => x.Rating.HasValue).OrderBy(x => x.Rating).Take(5),
                MostOccupiedRooms = roomOccupancyDtos.OrderByDescending(x => x.Occupancy).Take(5),
                LeastOccupiedRooms = roomOccupancyDtos.OrderBy(x => x.Occupancy).Take(5)

            };

        }

        public async Task<Guid?> CreateRoomAsync(RoomForCreateDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);

            await _unitOfWork.RoomRepository.InsertAsync(room);
            await _unitOfWork.SaveAsync();

            return room.Id;
        }

        public async Task<bool> UpdateRoomAsync(RoomForUpdateDto roomDto)
        {
            var room = _unitOfWork.RoomRepository.GetById(roomDto.Id);
            if(room == null)
                return false;

            room.Name = roomDto.Name;
            room.Number = roomDto.Number;
            room.GuestCount = roomDto.GuestCount;
            room.Status = (Room.StatusTypeId)roomDto.Status;

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteRoomAsync(RoomForDeleteDto roomDto)
        {
            var room = _unitOfWork.RoomRepository.GetById(roomDto.Id);
            if (room == null)
                return false;

            _unitOfWork.RoomRepository.Delete(room);
            await _unitOfWork.SaveAsync();

            return true;
        }

        #endregion

        #region Room Event Methods

        public async Task<RoomEventDto> CreateRoomEventAsync(RoomEventForCreateDto roomEventDto)
        {
            var roomEvent = _mapper.Map<RoomEvent>(roomEventDto);

            if(roomEvent.Type == RoomEvent.EventTypeId.CustomerStay)
            {
                if (!roomEvent.ReservationId.HasValue)
                    return null;
            }
            else
            {
                if (!roomEvent.EmployeeId.HasValue)
                    return null;
            }

            await _unitOfWork.RoomEventRepository.InsertAsync(roomEvent);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RoomEventDto>(roomEvent);
        }

        public async Task<bool> UpdateRoomEventAsync(RoomEventForUpdateDto roomEventDto)
        {
            var roomEvent = _unitOfWork.RoomEventRepository.GetById(roomEventDto.Id);
            if (roomEvent == null)
                return false;

            roomEvent.Observations = roomEventDto.Observations;

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteRoomEventAsync(RoomEventForDeleteDto roomEventDto)
        {
            var roomEvent = _unitOfWork.RoomEventRepository.GetById(roomEventDto.Id);
            if (roomEvent == null)
                return false;

            _unitOfWork.RoomEventRepository.Delete(roomEvent);
            await _unitOfWork.SaveAsync();

            return true;
        }

        #endregion
    }
}
