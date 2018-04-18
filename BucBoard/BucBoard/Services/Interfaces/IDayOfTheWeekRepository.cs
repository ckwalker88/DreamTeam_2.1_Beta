using BucBoard.Models.Entities.Existing;
using System.Collections.Generic;

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
