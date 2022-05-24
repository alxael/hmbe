using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class RoomDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public int GuestCount { get; set; }

        public int Status { get; set; }

        public List<RoomEventDto> RoomEvents { get; set; }
    }
}
