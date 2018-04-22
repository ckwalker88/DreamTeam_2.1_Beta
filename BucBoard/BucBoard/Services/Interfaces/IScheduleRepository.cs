using BucBoard.Models.Entities.Existing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services.Interfaces
{
    public interface IScheduleRepository
    {
        Schedule CreateSchedule(Schedule schedule);
        Schedule ReadSchedule(int id);
        ICollection<Schedule> ReadAllSchedules();
        void UpdateSchedule(Schedule schedule);
        void DeleteSchedule(int id);
    }
}
