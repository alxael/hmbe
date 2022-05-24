using HM.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Models
{
    public class Customer : BaseEntity
    {
        public Guid Id { get; set; }

        [Encrypted]
        public string FirstName { get; set; }

        [Encrypted]
        public string LastName { get; set; }

        [Encrypted]
        public string Address { get; set; }

        [Encrypted]
        public string PhoneNumber { get; set; }

        [Encrypted]
        public string Email { get; set; }

        #region Virtual Collections
        public virtual ICollection<CustomerEvent> CustomerEvents { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        #endregion

        public Customer()
        {
            CustomerEvents = new HashSet<CustomerEvent>();
            Reservations = new HashSet<Reservation>();
        }
    }
}
