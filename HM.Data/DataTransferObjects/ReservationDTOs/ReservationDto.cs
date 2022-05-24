using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class ReservationDto : ReservationBaseDto
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; }

        public Guid RoomId { get; set; }


    }
}
