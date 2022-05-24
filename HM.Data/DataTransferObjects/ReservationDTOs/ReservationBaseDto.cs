using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class ReservationBaseDto
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int Status { get; set; }

        public int GuestCount { get; set; }

        public string RoomName { get; set; }
    }
}
