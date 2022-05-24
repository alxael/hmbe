using HM.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Models
{
    public class EmployeeShift : BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid EmployeeId { get; set; }

        public string Observations { get; set; }

        #region Virtual Properties
        public virtual Employee Employee { get; set; } 
        #endregion
    }
}
