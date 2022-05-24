using HM.Data.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Services.Contracts
{
    public interface IRoomService
    {
        #region Room Methods
        Task<List<RoomDto>> GetRoomsAsync();

        Task<List<RoomSummaryDto>> GetRoomsSummaryAsync();

        Task<RoomAnalyticsDto> GetRoomAnalyticsAsync();

        Task<Guid?> CreateRoomAsync(RoomForCreateDto roomDto);

        Task<bool> UpdateRoomAsync(RoomForUpdateDto roomDto);

        Task<bool> DeleteRoomAsync(RoomForDeleteDto roomDto);

        #endregion

        #region Room Event Methods

        Task<RoomEventDto> CreateRoomEventAsync(RoomEventForCreateDto roomEventDto);

        Task<bool> UpdateRoomEventAsync(RoomEventForUpdateDto roomEventDto);

        Task<bool> DeleteRoomEventAsync(RoomEventForDeleteDto roomEventDto);
        #endregion
    }
}
