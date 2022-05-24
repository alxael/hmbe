using HM.Data.DataTransferObjects;
using HM.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM.WebAPI.Controllers
{
    [CustomAuthorize]
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        #region Fields
        private readonly ICustomerService _customerService;
        #endregion

        #region Constructor
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion

        #region Customer Methods

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var customerDtos = await _customerService.GetCustomersAsync();
            return Ok(customerDtos);
        }

        [HttpGet("summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerSummaryDto>))]
        public async Task<IActionResult> GetCustomersSummaryAsync()
        {
            var customerDtos = await _customerService.GetCustomersSummaryAsync();
            return Ok(customerDtos);
        }

        [HttpGet("analytics")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerAnalyticsDto>))]
        public IActionResult GetCustomerAnalytics()
        {
            var customerAnalyticsDtos = _customerService.GetCustomerAnalytics();
            return Ok(customerAnalyticsDtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerForCreateDto customerDto)
        {
            var id = await _customerService.CreateCustomerAsync(customerDto);
            if (id != null)
                return Ok(id);

            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] CustomerForUpdateDto customerDto)
        {
            if (await _customerService.UpdateCustomerAsync(customerDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCustomerAsync([FromBody] CustomerForDeleteDto customerDto)
        {
            if (await _customerService.DeleteCustomerAsync(customerDto))
                return Ok();

            return BadRequest();
        }

        #endregion

        #region Customer Event Methods

        [HttpPost("event")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerEventDto))]
        public async Task<IActionResult> CreateCustomerEventAsync([FromBody] CustomerEventForCreateDto customerEventDto)
        {
            var customerEventResponseDto = await _customerService.CreateCustomerEventAsync(customerEventDto);
            if (customerEventResponseDto != null)
                return Ok(customerEventResponseDto);

            return BadRequest();
        }

        [HttpPut("event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomerEventAsync([FromBody] CustomerEventForUpdateDto customerEventDto)
        {
            if (await _customerService.UpdateCustomerEventAsync(customerEventDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCustomerEventAsync([FromBody] CustomerEventForDeleteDto customerEventDto)
        {
            if (await _customerService.DeleteCustomerEventAsync(customerEventDto))
                return Ok();

            return BadRequest();
        }

        #endregion

    }
}
