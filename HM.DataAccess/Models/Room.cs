using HM.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Models
{
    public class Room : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public int GuestCount { get; set; }

        public StatusTypeId  Status { get; set; }

        #region Virtual Collections
        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<RoomEvent> RoomEvents { get; set; }
        #endregion

        public Room()
        {
            Reservations = new HashSet<Reservation>();
            RoomEvents = new HashSet<RoomEvent>();
        }

        #region Enums
        public enum StatusTypeId
        {
            InUse,
            Maintenance
        }
        #endregion
    }
}
