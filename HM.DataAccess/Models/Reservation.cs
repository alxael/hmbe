using HM.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Models
{
    public class Reservation : BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public Guid CustomerId { get; set; }

        public Guid RoomId { get; set; }

        public string Observations { get; set; }

        public int GuestCount { get; set; }

        public StatusTypeId Status { get; set; }

        public decimal? Rating { get; set; }

        #region Virtual Properties
        public virtual Customer Customer { get; set; }

        public virtual Room Room { get; set; }
        #endregion

        #region Virtual Collections
        public virtual ICollection<RoomEvent> RoomEvents { get; set; }

        public virtual ICollection<CustomerEvent> CustomerEvents { get; set; }
        #endregion

        public Reservation()
        {
            CustomerEvents = new HashSet<CustomerEvent>();
            RoomEvents = new HashSet<RoomEvent>();
        }


        #region Enums
        public enum StatusTypeId
        {
            Upcoming,
            Active,
            Ended,
            Cancelled
        }
        #endregion
    }
}
