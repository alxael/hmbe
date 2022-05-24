using AutoMapper;
using HM.Data.DataTransferObjects;
using HM.DataAccess.Models;

namespace HM.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Customer, CustomerSummaryDto>()
                .ForMember(d=> d.Name, e => e.MapFrom(s=> CustomerNameForCustomerResolver(s)));
            CreateMap<CustomerEvent, CustomerEventDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, EmployeeSummaryDto>()
                .ForMember(d => d.Name, e => e.MapFrom(s => EmployeeNameForEmployeeResolver(s)));
            CreateMap<EmployeeShift, EmployeeShiftDto>();
            CreateMap<EmployeeShift, EmployeeShiftForDashboardDto>()
                .ForMember(d=> d.EmployeeName, e=> e.MapFrom(s=> EmployeeNameForEmployeeResolver(s.Employee)))
                .ForMember(d=> d.JobType, e=> e.MapFrom(s=> s.Employee.JobType));
            CreateMap<Reservation, ReservationBaseDto>()
                .ForMember(d => d.RoomName, e => e.MapFrom(s => RoomNameForRoomResolver(s.Room)));
            CreateMap<Reservation, ReservationDto>()
                .ForMember(d => d.RoomName, e => e.MapFrom(s => RoomNameForRoomResolver(s.Room)))
                .ForMember(d => d.CustomerName, e => e.MapFrom(s => CustomerNameForReservationResolver(s)));
            CreateMap<Reservation, ReservationSummaryDto>()
                .ForMember(d=> d.Description, e=> e.MapFrom(s=> ReservationDescriptionForReservationResolver(s)));
            CreateMap<Reservation, ReservationForDashboardDto>()
                .ForMember(d => d.RoomName, e => e.MapFrom(s => RoomNameForRoomResolver(s.Room)))
                .ForMember(d => d.CustomerName, e => e.MapFrom(s => CustomerNameForReservationResolver(s)));
            CreateMap<Room, RoomDto>();
            CreateMap<Room, RoomSummaryDto>()
                .ForMember(d => d.Name, e => e.MapFrom(s => RoomNameForRoomResolver(s)));
            CreateMap<Room, RoomForOccupancyDto>()
                .ForMember(d => d.Name, e => e.MapFrom(s => RoomNameForRoomResolver(s)))
                .ForMember(d=> d.Occupancy, e=> e.MapFrom(s=> OccupancyForRoomResolver(s)));
            CreateMap<Room, RoomForRateDto>()
                .ForMember(d => d.Name, e => e.MapFrom(s => RoomNameForRoomResolver(s)))
                .ForMember(d => d.Rating, e => e.MapFrom(s => RatingForRoomResolver(s)));
            CreateMap<RoomEvent, RoomEventDto>()
                .ForMember(d => d.EmployeeName, e => e.MapFrom(s => s.Employee != null ? EmployeeNameForEmployeeResolver(s.Employee) : null));

            CreateMap<CustomerForCreateDto, Customer>();
            CreateMap<CustomerEventForCreateDto, CustomerEvent>();
            CreateMap<EmployeeForCreateDto, Employee>();
            CreateMap<EmployeeShiftForCreateDto, EmployeeShift>();
            CreateMap<RoomForCreateDto, Room>();
            CreateMap<RoomEventForCreateDto, RoomEvent>();
            CreateMap<ReservationForCreateDto, Reservation>();

        }


        private string RoomNameForRoomResolver(Room room)
        {
            return room.Name + string.Format(" (#{0})", room.Number);
        }

        private string EmployeeNameForEmployeeResolver(Employee employee)
        {
            return string.Format("{0} {1}", employee.FirstName, employee.LastName);
        }
        
        private string CustomerNameForCustomerResolver(Customer customer)
        {
            return string.Format("{0} {1}", customer.FirstName, customer.LastName);
        }

        private decimal OccupancyForRoomResolver(Room room)
        {
            var endDate = DateTime.Today.AddDays(-1);
            var startDate = endDate.AddMonths(-1);
            var reservations = room.Reservations.Where(x => startDate <= x.CheckIn.Date && x.CheckIn.Date <= endDate && x.Status != Reservation.StatusTypeId.Cancelled);
            var daysOccupied = 0;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var filteredReservations = reservations.Where(x => x.CheckIn.Date <= date && date <= x.CheckOut);
                if (filteredReservations.Any())
                    daysOccupied++;
            }
            return Math.Round(daysOccupied / (decimal)((endDate - startDate).TotalDays) * 100, 2);
        }

        private decimal? RatingForRoomResolver(Room room)
        {
            var endDate = DateTime.Today.AddDays(-1);
            var startDate = endDate.AddMonths(-1);
            var reservations = room.Reservations.Where(x => startDate <= x.CheckIn.Date && x.CheckIn.Date <= endDate && x.Status != Reservation.StatusTypeId.Cancelled);
            var ratings = new List<decimal>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var filteredReservations = reservations.Where(x => x.CheckIn.Date <= date && date <= x.CheckOut && x.Rating.HasValue);
                if (filteredReservations.Any())
                    ratings.AddRange(filteredReservations.Select(x => x.Rating.Value));
            }
            return ratings.Any() ? Math.Round(ratings.Average(),2) : null;
        }

        private string CustomerNameForReservationResolver(Reservation reservation)
        {
            return string.Format("{0} {1}", reservation.Customer.FirstName, reservation.Customer.LastName);
        }

        private string ReservationDescriptionForReservationResolver(Reservation reservation)
        {
            return string.Format("Customer: {0} Date: {1}", CustomerNameForCustomerResolver(reservation.Customer), reservation.CheckIn);
        }
    }
}
