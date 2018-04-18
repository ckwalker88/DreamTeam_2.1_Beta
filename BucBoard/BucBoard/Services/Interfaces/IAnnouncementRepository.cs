using BucBoard.Models.Entities.Existing;
using System.Collections.Generic;

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
