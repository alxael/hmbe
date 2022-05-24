using HM.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Models
{
    public class RoomEvent: BaseEntity
    {
        public Guid Id { get; set; }

        public EventTypeId Type { get; set; }

        public Guid RoomId { get; set; }

        public Guid? ReservationId { get; set; }

        public Guid? EmployeeId { get; set; }

        public string Observations { get; set; }

        #region Virtual Properties
        
        public virtual Room Room { get; set; }
        
        public virtual Reservation Reservation { get; set; }

        public virtual Employee Employee { get; set; }


        #endregion

        #region Enums
        public enum EventTypeId
        {
            CustomerStay,
            Maintenance,
            Cleaning
        }
        #endregion
    }
}
