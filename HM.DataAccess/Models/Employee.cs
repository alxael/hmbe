using HM.DataAccess.Base;
using System.ComponentModel.DataAnnotations;

namespace HM.DataAccess.Models
{
    public class Employee : BaseEntity
    {
        public Guid Id { get; set; }

        [Encrypted]
        public string FirstName { get; set; }

        [Encrypted]
        public string LastName { get; set; }

        public JobTypeId JobType { get; set; }

        #region Virtual Collections
        public virtual ICollection<RoomEvent> RoomEvents { get; set; }

        public virtual ICollection<EmployeeShift> EmployeeShifts { get; set; }

        #endregion

        public Employee()
        {
            RoomEvents = new HashSet<RoomEvent>();
            EmployeeShifts = new HashSet<EmployeeShift>();
        }

        #region Enums
        public enum JobTypeId
        {
            Concierge,
            Receptionist,
            Cleaning,
            Cooking
        }
        #endregion
    }
}
