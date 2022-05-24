using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class RoomEventForCreateDto : RoomEventForManipulateDto
    {
        public int Type { get; set; }

        public Guid RoomId { get; set; }

        public Guid? ReservationId { get; set; }

        public Guid? EmployeeId { get; set; }
    }
}
