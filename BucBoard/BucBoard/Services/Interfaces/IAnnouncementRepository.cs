using BucBoard.Models.Entities.Existing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services.Interfaces
{
    public interface IAnnouncementRepository
    {
        Announcement CreateAnnouncement(Announcement announcement);
        Announcement ReadAnnouncement(int id);
        ICollection<Announcement> ReadAllAnnouncements();
        void UpdateAnnouncement(int id, Announcement announcement);
        void DeleteAnnouncement(int id);
    }
}
