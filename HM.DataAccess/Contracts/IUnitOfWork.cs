using HM.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Contracts
{
    public interface IUnitOfWork
    {

        ICustomerEventRepository CustomerEventRepository { get; }

        ICustomerRepository CustomerRepository { get;  }

        IEmployeeRepository EmployeeRepository { get; }

        IEmployeeShiftRepository EmployeeShiftRepository { get; }

        IReservationRepository ReservationRepository { get; }

        IRoomRepository RoomRepository { get; }

        IRoomEventRepository RoomEventRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
