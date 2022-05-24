using HM.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Models
{
    public class CustomerEvent : BaseEntity
    {
        public Guid Id { get; set; }

        public EventTypeId Type { get; set; }

        public Guid CustomerId { get; set; }

        public Guid? ReservationId { get; set; }

        public string Observations { get; set; }

        #region Virtual Properties
        public virtual Customer Customer { get; set; }

        public virtual Reservation Reservation { get; set; }
        #endregion

        #region Enums
        public enum EventTypeId
        {
            ReservationRequest,
            ReservationCancellation,
            CheckIn,
            CheckOut
        }

        #endregion
    }
}
