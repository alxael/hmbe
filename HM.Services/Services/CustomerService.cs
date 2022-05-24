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
    public class CustomerService : ICustomerService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Customer Methods
        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public async Task<List<CustomerSummaryDto>> GetCustomersSummaryAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerSummaryDto>>(customers);
        }

        public List<CustomerAnalyticsDto> GetCustomerAnalytics()
        {
            var roomCount =  _unitOfWork.RoomRepository.Count();
            if (roomCount == 0)
                return new List<CustomerAnalyticsDto>();
            var endDate = DateTime.Today.AddDays(-1);
            var startDate = endDate.AddMonths(-1);
            var reservations = _unitOfWork.ReservationRepository.Get(x => startDate <= x.CheckIn.Date && x.CheckIn.Date <= endDate && x.Status != Reservation.StatusTypeId.Cancelled);
            var customerAnalyticsDtos = new List<CustomerAnalyticsDto>();
            for(var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var filteredReservations = reservations.Where(x => x.CheckIn.Date <= date && date <= x.CheckOut );
                var customerAnalyticsDto = new CustomerAnalyticsDto()
                {
                    CustomerCount = filteredReservations.Sum(x => x.GuestCount),
                    OccupancyRate = Math.Round(filteredReservations.Select(x => x.Room).Distinct().Count() / (decimal)roomCount * 100, 2),
                    Date = date

                };
                customerAnalyticsDtos.Add(customerAnalyticsDto);
            }

            return customerAnalyticsDtos;
        }

        public async Task<Guid?> CreateCustomerAsync(CustomerForCreateDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            await _unitOfWork.CustomerRepository.InsertAsync(customer);
            await _unitOfWork.SaveAsync();

            return customer.Id;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerForUpdateDto customerDto)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(customerDto.Id);
            if(customer == null)
                return false;

            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.Address = customerDto.Address;
            customer.Email = customerDto.Email;
            customer.PhoneNumber = customerDto.PhoneNumber;

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteCustomerAsync(CustomerForDeleteDto customerDto)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(customerDto.Id);
            if (customer == null)
                return false;

            _unitOfWork.CustomerRepository.Delete(customer);
            await _unitOfWork.SaveAsync();

            return true;
        }

        #endregion

        #region Customer Event Methods

        public async Task<CustomerEventDto> CreateCustomerEventAsync(CustomerEventForCreateDto customerEventDto)
        {
            var customerEvent = _mapper.Map<CustomerEvent>(customerEventDto);

            if (customerEvent.Type != CustomerEvent.EventTypeId.ReservationRequest && !customerEvent.ReservationId.HasValue)
                return null;

            await _unitOfWork.CustomerEventRepository.InsertAsync(customerEvent);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CustomerEventDto>(customerEvent);
        }

        public async Task<bool> UpdateCustomerEventAsync(CustomerEventForUpdateDto customerEventDto)
        {
            var customerEvent = _unitOfWork.CustomerEventRepository.GetById(customerEventDto.Id);
            if (customerEvent == null)
                return false;

            customerEvent.Observations = customerEventDto.Observations;

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteCustomerEventAsync(CustomerEventForDeleteDto customerEventDto)
        {
            var customerEvent = _unitOfWork.CustomerEventRepository.GetById(customerEventDto.Id);
            if (customerEvent == null)
                return false;

            _unitOfWork.CustomerEventRepository.Delete(customerEvent);
            await _unitOfWork.SaveAsync();

            return true;
        }

        #endregion
    }
}
