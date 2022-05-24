using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Base
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Guid? CreatorUserId { get; set; }

        public Guid? ModifierUserId { get; set; }
    }
}
