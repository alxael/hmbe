using HM.Data.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Services.Contracts
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetReservationsAsync();

        Task<List<ReservationSummaryDto>> GetReservationsSummaryAsync();
        List<ReservationForDashboardDto> GetActiveReservations();

        Task<Guid?> CreateReservationAsync(ReservationForCreateDto reservationDto);

        Task<bool> UpdateReservationAsync(ReservationForUpdateDto reservationDto);

        Task<bool> DeleteReservationAsync(ReservationForDeleteDto reservationDto);


    }
}
