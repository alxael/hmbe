using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class RoomEventDto
    {
        public Guid Id { get; set; }

        public int Type { get; set; }

        public string Observations { get; set; }

        public DateTime DateCreated { get; set; }

        public string EmployeeName { get; set; }

        public ReservationBaseDto Reservation { get; set; }
    }
}
