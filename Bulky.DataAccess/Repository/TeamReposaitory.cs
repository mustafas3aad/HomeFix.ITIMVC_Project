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
    public class TeamReposaitory : Reposaitory<Team>, ITeamReposaitory
    {
        private readonly ApplicationDbContext db;

        public TeamReposaitory(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void update(Team obj)
        {
            var objFromDb = db.Teams.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.DepositAmount = obj.DepositAmount;
                objFromDb.DepositNote = obj.DepositNote;
                objFromDb.ServiceId = obj.ServiceId;
                objFromDb.YearsOfExperience = obj.YearsOfExperience;
                objFromDb.IsAvailable24Hours = obj.IsAvailable24Hours;
                objFromDb.HourlyRate = obj.HourlyRate;
                objFromDb.TeamImages = obj.TeamImages;
                objFromDb.WorkersCount = obj.WorkersCount;
            }
            
        }
    }
}
