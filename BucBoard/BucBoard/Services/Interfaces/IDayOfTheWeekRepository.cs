using BucBoard.Models.Entities.Existing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services.Interfaces
{
    public interface IDayOfTheWeekRepository
    {
        DayOfTheWeek Create(DayOfTheWeek day);
        DayOfTheWeek Read(int id);
        ICollection<DayOfTheWeek> ReadAll();
        void Update(int id, DayOfTheWeek day);
        void Delete(int id);
    }
}
