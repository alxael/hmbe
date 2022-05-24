﻿using HM.DataAccess.Models;
using HM.DataAccess.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.DataAccess.Repositories
{
    public class RoomRepository: RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(HmDbContext context): base(context) { }
    }
}
