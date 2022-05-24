using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class ReservationForUpdateDto : ReservationForManipulateDto
    {
        public Guid Id { get; set; }

        public decimal? Rating { get; set; }

    }
}
