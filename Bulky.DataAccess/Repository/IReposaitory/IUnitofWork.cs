using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IReposaitory
{
    public interface IUnitofWork
    {
        public IServiceReposaitory Service { get; }
        public ITeamReposaitory Team { get; }
        public ICompanyReposaitory Company { get;}
        public IBookingCartReposaitory BookingCart { get; }
        public IOrderDetailReposaitory OrderDetail { get; }
        public IOrderHeaderReposaitory OrderHeader { get; }
        public IApplicationUserReposaitory ApplicationUser { get; }
        public ITeamImageReposaitory TeamImage { get; }
        public IFeedbackReposaitory Feedback { get; }
        void Save();
    }
}
