using HM.Data.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Services.Contracts
{
    public interface ICustomerService
    {
        #region Customer Methods
        Task<List<CustomerDto>> GetCustomersAsync();

        Task<List<CustomerSummaryDto>> GetCustomersSummaryAsync();

        List<CustomerAnalyticsDto> GetCustomerAnalytics();

        Task<Guid?> CreateCustomerAsync(CustomerForCreateDto customerDto);

        Task<bool> UpdateCustomerAsync(CustomerForUpdateDto customerDto);

        Task<bool> DeleteCustomerAsync(CustomerForDeleteDto customerDto);

        #endregion

        #region Customer Event Methods

        Task<CustomerEventDto> CreateCustomerEventAsync(CustomerEventForCreateDto customerEventDto);

        Task<bool> UpdateCustomerEventAsync(CustomerEventForUpdateDto customerEventDto);

        Task<bool> DeleteCustomerEventAsync(CustomerEventForDeleteDto customerEventDto);
        #endregion
    }
}
