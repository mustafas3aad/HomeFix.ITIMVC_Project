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
    public class FeedbackReposaitory : Reposaitory<Feedback>, IFeedbackReposaitory
    {
        private readonly ApplicationDbContext db;

        public FeedbackReposaitory(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
       
    }
}
