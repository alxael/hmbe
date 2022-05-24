using HM.DataAccess.Models;
using HM.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Repositories
{
    public class CustomerEventRepository : RepositoryBase<CustomerEvent>, ICustomerEventRepository
    {
        public CustomerEventRepository(HmDbContext context) : base(context) { }
    }
}
