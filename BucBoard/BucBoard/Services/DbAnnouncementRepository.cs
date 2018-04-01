using BucBoard.Data;
using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services
{
    public class DbAnnouncementRepository : IAnnouncementRepository
    {

        private BucBoardDBContext _db;
        private AuthenticationDbContext _authenDb;
        private UserManager<ApplicationUser> _users;

        public DbAnnouncementRepository(BucBoardDBContext db, AuthenticationDbContext authenDb, UserManager<ApplicationUser> users)
        {
            _db = db;
            _authenDb = authenDb;
            _users = users;
        }

        public Announcement CreateAnnouncement(Announcement announcement)
        {
            _db.Announcement.Add(announcement);
            _db.SaveChanges();
            return announcement;
        }

        public Announcement ReadAnnouncement(int id)
        {
            return _db.Announcement.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Announcement> ReadAllAnnouncements()
        {
            return _db.Announcement
                .Include(u => u.ApplicationUser)
                .Include(r => r.ApplicationUser.Roles)
                .ToList();
        }

        public void UpdateAnnouncement(int id, Announcement announcement)
        {
            _db.Entry(announcement).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteAnnouncement(int id)
        {
            Announcement announcement = _db.Announcement.Find(id);
            _db.Announcement.Remove(announcement);
            _db.SaveChanges();
        }
    }
}
