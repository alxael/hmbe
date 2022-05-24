using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class RoomForManipulateDto
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public int GuestCount { get; set; }

        public int Status { get; set; }
    }
}
