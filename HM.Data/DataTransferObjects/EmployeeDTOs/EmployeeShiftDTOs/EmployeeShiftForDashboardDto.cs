using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class EmployeeShiftForDashboardDto
    {
        public string EmployeeName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int JobType { get; set; }

        public string Observations { get; set; }
    }
}
