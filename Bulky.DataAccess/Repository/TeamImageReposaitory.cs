using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IReposaitory;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class TeamImageReposaitory : Reposaitory<TeamImages>, ITeamImageReposaitory
    {
        private readonly ApplicationDbContext db;

        public TeamImageReposaitory(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void update(TeamImages obj)
        {
            db.TeamImages.Update(obj);
        }
    }
}
