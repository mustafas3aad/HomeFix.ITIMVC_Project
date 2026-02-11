using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IReposaitory
{
    public interface IServiceReposaitory:IReposaitory<Service>
    {
        void update(Service obj); 
    }
}
