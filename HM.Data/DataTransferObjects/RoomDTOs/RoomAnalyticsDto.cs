using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class RoomAnalyticsDto
    {
        public IEnumerable<RoomForOccupancyDto> LeastOccupiedRooms { get; set; }

        public IEnumerable<RoomForOccupancyDto> MostOccupiedRooms { get; set; }

        public IEnumerable<RoomForRateDto> WorstRatedRooms { get; set; }
        
        public IEnumerable<RoomForRateDto> BestRatedRooms { get; set; }
    }
}
