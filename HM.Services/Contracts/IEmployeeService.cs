using HM.Data.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Services.Contracts
{
    public interface IEmployeeService
    {
        #region Employee Methods
        Task<List<EmployeeDto>> GetEmployeesAsync();

        Task<List<EmployeeSummaryDto>> GetEmployeesSummaryAsync();

        List<EmployeeShiftForDashboardDto> GetAvailableEmployees();

        Task<Guid?> CreateEmployeeAsync(EmployeeForCreateDto employeeDto);

        Task<bool> UpdateEmployeeAsync(EmployeeForUpdateDto employeeDto);

        Task<bool> DeleteEmployeeAsync(EmployeeForDeleteDto employeeDto);

        #endregion

        #region Employee Shift Methods

        Task<EmployeeShiftDto> CreateEmployeeShiftAsync(EmployeeShiftForCreateDto employeeShiftDto);

        Task<bool> UpdateEmployeeShiftAsync(EmployeeShiftForUpdateDto employeeShiftDto);

        Task<bool> DeleteEmployeeShiftAsync(EmployeeShiftForDeleteDto employeeShiftDto);
        #endregion
    }
}
