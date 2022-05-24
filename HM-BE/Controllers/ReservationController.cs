using HM.Data.DataTransferObjects;
using HM.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM.WebAPI.Controllers
{
    [CustomAuthorize]
    [ApiController]
    [Route("api/reservation")]
    public class ReservationController : ControllerBase
    {
        #region Fields
        private readonly IReservationService _reservationService;
        #endregion

        #region Constructor
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        #endregion

        #region Reservation Methods

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReservationDto>))]
        public async Task<IActionResult> GetReservationsAsync()
        {
            var reservationDtos = await _reservationService.GetReservationsAsync();
            return Ok(reservationDtos);
        }

        [HttpGet("summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReservationSummaryDto>))]
        public async Task<IActionResult> GetReservationsSummaryAsync()
        {
            var reservationDtos = await _reservationService.GetReservationsSummaryAsync();
            return Ok(reservationDtos);
        }

        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReservationForDashboardDto>))]
        public IActionResult GetActiveReservations()
        {
            var reservationDtos =  _reservationService.GetActiveReservations();
            return Ok(reservationDtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> CreateReservationAsync([FromBody] ReservationForCreateDto reservationDto)
        {
            var id = await _reservationService.CreateReservationAsync(reservationDto);
            if (id != null)
                return Ok(id);

            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateReservationAsync([FromBody] ReservationForUpdateDto reservationDto)
        {
            if (await _reservationService.UpdateReservationAsync(reservationDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteReservationAsync([FromBody] ReservationForDeleteDto reservationDto)
        {
            if (await _reservationService.DeleteReservationAsync(reservationDto))
                return Ok();

            return BadRequest();
        }

        #endregion


    }
}
