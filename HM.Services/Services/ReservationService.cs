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
    public class ReservationService : IReservationService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        public async Task<List<ReservationDto>> GetReservationsAsync()
        {
            var reservations = await _unitOfWork.ReservationRepository.GetAllAsync();
            return _mapper.Map<List<ReservationDto>>(reservations);
        }


        public async Task<List<ReservationSummaryDto>> GetReservationsSummaryAsync()
        {
            var reservations = await _unitOfWork.ReservationRepository.GetAllAsync();
            return _mapper.Map<List<ReservationSummaryDto>>(reservations);
        }

        public List<ReservationForDashboardDto> GetActiveReservations()
        {
            var activeReservations = _unitOfWork.ReservationRepository.Get(x => (x.CheckIn.Date <= DateTime.Today && DateTime.Today <= x.CheckOut)
                                                                                || (DateTime.Today < x.CheckIn && (x.CheckIn - DateTime.Today).TotalDays < 15));

            return _mapper.Map<List<ReservationForDashboardDto>>(activeReservations);
        }

        public async Task<Guid?> CreateReservationAsync(ReservationForCreateDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            await _unitOfWork.ReservationRepository.InsertAsync(reservation);
            await _unitOfWork.SaveAsync();

            return reservation.Id;
        }

        public async Task<bool> UpdateReservationAsync(ReservationForUpdateDto reservationDto)
        {
            var reservation = _unitOfWork.ReservationRepository.GetById(reservationDto.Id);
            if (reservation == null)
                return false;

            reservation.CheckIn = reservationDto.CheckIn;
            reservation.CheckOut = reservationDto.CheckOut;
            reservation.RoomId = reservationDto.RoomId;
            reservation.Observations = reservationDto.Observations;
            reservation.GuestCount = reservationDto.GuestCount;
            reservation.Status = (Reservation.StatusTypeId)reservationDto.Status;
            reservation.Rating = reservationDto.Rating;

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteReservationAsync(ReservationForDeleteDto reservationDto)
        {
            var reservation = _unitOfWork.ReservationRepository.GetById(reservationDto.Id);
            if (reservation == null)
                return false;

            _unitOfWork.ReservationRepository.Delete(reservation);
            await _unitOfWork.SaveAsync();

            return true;
        }


    }
}
