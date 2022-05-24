using HM.Data.DataTransferObjects;
using HM.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM.WebAPI.Controllers
{
    [CustomAuthorize]
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        #region Fields
        private readonly IRoomService _roomService;
        #endregion

        #region Constructor
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        #endregion

        #region Room Methods

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoomDto>))]
        public async Task<IActionResult> GetRoomsAsync()
        {
            var roomDtos = await _roomService.GetRoomsAsync();
            return Ok(roomDtos);
        }

        [HttpGet("summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoomSummaryDto>))]
        public async Task<IActionResult> GetRoomsSummaryAsync()
        {
            var roomDtos = await _roomService.GetRoomsSummaryAsync();
            return Ok(roomDtos);
        }

        [HttpGet("analytics")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomAnalyticsDto))]
        public async Task<IActionResult> GetRoomAnalyticsAsync()
        {
            var roomAnalyticsDto = await _roomService.GetRoomAnalyticsAsync();
            return Ok(roomAnalyticsDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> CreateRoomAsync([FromBody] RoomForCreateDto roomDto)
        {
            var id = await _roomService.CreateRoomAsync(roomDto);
            if (id != null)
                return Ok(id);

            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRoomAsync([FromBody] RoomForUpdateDto roomDto)
        {
            if (await _roomService.UpdateRoomAsync(roomDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRoomAsync([FromBody] RoomForDeleteDto roomDto)
        {
            if (await _roomService.DeleteRoomAsync(roomDto))
                return Ok();

            return BadRequest();
        }

        #endregion

        #region Room Event Methods

        [HttpPost("event")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomEventDto))]
        public async Task<IActionResult> CreateRoomEventAsync([FromBody] RoomEventForCreateDto roomEventDto)
        {
            var roomEventResponseDto = await _roomService.CreateRoomEventAsync(roomEventDto);
            if (roomEventResponseDto != null)
                return Ok(roomEventResponseDto);

            return BadRequest();
        }

        [HttpPut("event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomerEventAsync([FromBody] RoomEventForUpdateDto roomEventDto)
        {
            if (await _roomService.UpdateRoomEventAsync(roomEventDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCustomerEventAsync([FromBody] RoomEventForDeleteDto roomEventDto)
        {
            if (await _roomService.DeleteRoomEventAsync(roomEventDto))
                return Ok();

            return BadRequest();
        }

        #endregion

    }
}
