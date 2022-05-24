using HM.DataAccess.Models;
using HM.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Repositories
{
    public class EmployeeShiftRepository: RepositoryBase<EmployeeShift>, IEmployeeShiftRepository
    {
        public EmployeeShiftRepository(HmDbContext context):base(context) { }
    }
}
