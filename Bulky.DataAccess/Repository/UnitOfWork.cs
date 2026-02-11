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
    public class UnitOfWork : IUnitofWork
    {
        private readonly ApplicationDbContext db;
      
        public IServiceReposaitory Service { get; private set; }
        public ITeamReposaitory Team { get; private set; }
        public ICompanyReposaitory Company { get; private set; }
        public IBookingCartReposaitory BookingCart { get; private set; }
        public IOrderDetailReposaitory OrderDetail { get; private set; }
        public IOrderHeaderReposaitory OrderHeader { get; private set;  }
        public IApplicationUserReposaitory ApplicationUser { get; private set; }
        public ITeamImageReposaitory TeamImage { get; private set; }
        public IFeedbackReposaitory Feedback { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            Service=new ServiceReposaitory(db);
            Team = new TeamReposaitory(db);
            Company = new CompanyReposaitory(db);
            BookingCart = new BookingCartReposaitory(db);
            OrderHeader = new OrderHeaderReposaitory(db);
            OrderDetail = new OrderDetailReposaitory(db);
            ApplicationUser = new ApplicationUserReposaitory(db);
            TeamImage = new TeamImageReposaitory(db);
            Feedback = new FeedbackReposaitory(db);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
