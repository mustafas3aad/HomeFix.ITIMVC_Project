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
    public class ServiceReposaitory : Reposaitory<Service>, IServiceReposaitory
    {
        private readonly ApplicationDbContext db;

        public ServiceReposaitory(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void update(Service obj)
        {
            db.Services.Update(obj);
        }
    }
}
