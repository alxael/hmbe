using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class ReservationForManipulateDto
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public Guid RoomId { get; set; }

        public string? Observations { get; set; }

        public int GuestCount { get; set; }

        public int Status { get; set; }
    }
}
