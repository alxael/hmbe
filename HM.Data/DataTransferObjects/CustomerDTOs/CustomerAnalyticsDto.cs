using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class CustomerAnalyticsDto
    {
        public DateTime Date { get; set; }

        public decimal OccupancyRate { get; set; }

        public int CustomerCount { get; set; }
    }
}
