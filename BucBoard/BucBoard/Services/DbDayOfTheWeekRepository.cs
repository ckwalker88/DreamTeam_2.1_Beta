using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services
{
    public class DbDayOfTheWeekRepository : IDayOfTheWeekRepository
    {
        private BucBoardDBContext _db;

        public DbDayOfTheWeekRepository(BucBoardDBContext db)
        {
            _db = db;
        }

        public DayOfTheWeek Create(DayOfTheWeek day)
        {
            _db.DayOfTheWeek.Add(day);
            _db.SaveChanges();
            return day;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DayOfTheWeek Read(int id)
        {
            return _db.DayOfTheWeek.FirstOrDefault(d => d.Id == id);
        }

        public ICollection<DayOfTheWeek> ReadAll()
        {
            return _db.DayOfTheWeek.
                Include(u => u.ApplicationUser)
                .Include(r => r.ApplicationUser.Roles)
                .ToList();
        }

        public void Update(int id, DayOfTheWeek day)
        {
            throw new NotImplementedException();
        }
    }
}
