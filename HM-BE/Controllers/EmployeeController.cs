using HM.Data.DataTransferObjects;
using HM.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HM.WebAPI.Controllers
{
    [CustomAuthorize]
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Employee Methods

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeDto>))]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var employeeDtos = await _employeeService.GetEmployeesAsync();
            return Ok(employeeDtos);
        }
        [HttpGet("summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeSummaryDto>))]
        public async Task<IActionResult> GetEmployeesSummaryAsync()
        {
            var employeeDtos = await _employeeService.GetEmployeesSummaryAsync();
            return Ok(employeeDtos);
        }

        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeShiftForDashboardDto>))]
        public IActionResult GetAvailableEmployees()
        {
            var employeeShiftDtos =  _employeeService.GetAvailableEmployees();
            return Ok(employeeShiftDtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeForCreateDto employeeDto)
        {
            var id = await _employeeService.CreateEmployeeAsync(employeeDto);
            if (id != null)
                return Ok(id);

            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmployeeAsync([FromBody] EmployeeForUpdateDto employeeDto)
        {
            if (await _employeeService.UpdateEmployeeAsync(employeeDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEmployeeAsync([FromBody] EmployeeForDeleteDto employeeDto)
        {
            if (await _employeeService.DeleteEmployeeAsync(employeeDto))
                return Ok();

            return BadRequest();
        }

        #endregion
      

        #region Employee Shift Methods

        [HttpPost("shift")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeShiftDto))]
        public async Task<IActionResult> CreateEmployeeShiftAsync([FromBody] EmployeeShiftForCreateDto employeeShiftDto)
        {
            var employeeShiftResponseDto = await _employeeService.CreateEmployeeShiftAsync(employeeShiftDto);
            if (employeeShiftResponseDto != null)
                return Ok(employeeShiftResponseDto);

            return BadRequest();
        }

        [HttpPut("shift")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmployeeShiftAsync([FromBody] EmployeeShiftForUpdateDto employeeShiftDto)
        {
            if (await _employeeService.UpdateEmployeeShiftAsync(employeeShiftDto))
                return Ok();

            return BadRequest();
        }

        [HttpDelete("shift")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEmployeeShiftAsync([FromBody] EmployeeShiftForDeleteDto employeeShiftDto)
        {
            if (await _employeeService.DeleteEmployeeShiftAsync(employeeShiftDto))
                return Ok();

            return BadRequest();
        }

        #endregion

    }
}
