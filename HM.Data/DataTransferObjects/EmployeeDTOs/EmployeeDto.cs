using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int JobType { get; set; }

        public List<RoomEventDto> RoomEvents { get; set; }

        public List<EmployeeShiftDto> EmployeeShifts { get; set; }
    }
}
