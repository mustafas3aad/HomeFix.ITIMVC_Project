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
    public class BookingCartReposaitory : Reposaitory<BookingCart>, IBookingCartReposaitory
    {
        private readonly ApplicationDbContext db;

        public BookingCartReposaitory(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void update(BookingCart obj)
        {
            db.BookingCarts.Update(obj);
        }
    }
}
