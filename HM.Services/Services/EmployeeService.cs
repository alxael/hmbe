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
    public class EmployeeService : IEmployeeService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Employee Methods
        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }
 
        public async Task<List<EmployeeSummaryDto>> GetEmployeesSummaryAsync()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeSummaryDto>>(employees);
        }

        public List<EmployeeShiftForDashboardDto> GetAvailableEmployees()
        {
            var employeeShifts = _unitOfWork.EmployeeShiftRepository.Get(x => x.StartDate <= DateTime.UtcNow
                                                                              && (!x.EndDate.HasValue || x.EndDate > DateTime.UtcNow));

            return _mapper.Map<List<EmployeeShiftForDashboardDto>>(employeeShifts);


        }

        public async Task<Guid?> CreateEmployeeAsync(EmployeeForCreateDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            await _unitOfWork.EmployeeRepository.InsertAsync(employee);
            await _unitOfWork.SaveAsync();

            return employee.Id;
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeForUpdateDto employeeDto)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(employeeDto.Id);
            if(employee == null)
                return false;

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.JobType = (Employee.JobTypeId)employeeDto.JobType;

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(EmployeeForDeleteDto employeeDto)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(employeeDto.Id);
            if (employee == null)
                return false;

            _unitOfWork.EmployeeRepository.Delete(employee);
            await _unitOfWork.SaveAsync();

            return true;
        }

        #endregion


        #region Employee Shift Methods

        public async Task<EmployeeShiftDto> CreateEmployeeShiftAsync(EmployeeShiftForCreateDto employeeShiftDto)
        {
            var employeeShift = _mapper.Map<EmployeeShift>(employeeShiftDto);

            if (employeeShift.EndDate < employeeShift.StartDate)
                    return null;
            

            await _unitOfWork.EmployeeShiftRepository.InsertAsync(employeeShift);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<EmployeeShiftDto>(employeeShift);
        }

        public async Task<bool> UpdateEmployeeShiftAsync(EmployeeShiftForUpdateDto employeeShiftDto)
        {
            var employeeShift = _unitOfWork.EmployeeShiftRepository.GetById(employeeShiftDto.Id);
            if (employeeShift == null)
                return false;

            employeeShift.StartDate = employeeShiftDto.StartDate;
            employeeShift.EndDate = employeeShiftDto.EndDate;
            employeeShift.Observations = employeeShiftDto.Observations;


            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteEmployeeShiftAsync(EmployeeShiftForDeleteDto employeeShiftDto)
        {
            var employeeShift = _unitOfWork.EmployeeShiftRepository.GetById(employeeShiftDto.Id);
            if (employeeShift == null)
                return false;

            _unitOfWork.EmployeeShiftRepository.Delete(employeeShift);
            await _unitOfWork.SaveAsync();

            return true;
        }

        #endregion
    }
}
