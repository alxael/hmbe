using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class CustomerEventForCreateDto : CustomerEventForManipulateDto
    {
        public int Type { get; set; }

        public Guid CustomerId { get; set; }

        public Guid? ReservationId { get; set; }
    }
}
