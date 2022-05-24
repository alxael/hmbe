using HM.DataAccess.Contracts;
using HM.DataAccess.Repositories;
using HM.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private HmDbContext _context;
        private ICustomerEventRepository _customerEventRepository;
        private ICustomerRepository _customerRepository;
        private IEmployeeRepository _employeeRepository;
        private IEmployeeShiftRepository _employeeShiftRepository;
        private IReservationRepository _reservationRepository;
        private IRoomRepository _roomRepository;
        private IRoomEventRepository _roomEventRepository;
        #endregion


        public UnitOfWork(HmDbContext context)
        {
            _context = context;
        }

        #region Public Methods
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion


        public ICustomerEventRepository CustomerEventRepository
        {
            get
            {
                return _customerEventRepository ??= new CustomerEventRepository(_context);
            }
        }

        public ICustomerRepository CustomerRepository {
            get
            {
                return _customerRepository ??= new CustomerRepository(_context);
            }
        }

        public IEmployeeRepository EmployeeRepository {
            get
            {
                return _employeeRepository ??= new EmployeeRepository(_context);
            }
        }

        public IEmployeeShiftRepository EmployeeShiftRepository {
            get
            {
                return _employeeShiftRepository ??= new EmployeeShiftRepository(_context);
            }
        }

        public IReservationRepository ReservationRepository {
            get
            {
                return _reservationRepository ??= new ReservationRepository(_context);
            }
        }

        public IRoomRepository RoomRepository
        {
            get
            {
                return _roomRepository ??= new RoomRepository(_context);
            }
        }

        public IRoomEventRepository RoomEventRepository
        {
            get
            {
                return _roomEventRepository ??= new RoomEventRepository(_context);
            }
        }

    }
}
