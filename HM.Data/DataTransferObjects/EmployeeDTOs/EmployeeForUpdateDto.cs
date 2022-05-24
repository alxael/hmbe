using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Data.DataTransferObjects
{
    public class EmployeeForUpdateDto : EmployeeForManipulateDto
    {
        public Guid Id { get; set; }
    }
}
