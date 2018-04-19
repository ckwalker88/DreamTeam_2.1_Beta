using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services
{
    public class DbProfilePictureRepository : IProfilePictureRepository
    {

        private BucBoardDBContext _db; 

        public DbProfilePictureRepository(BucBoardDBContext db)
        {
            _db = db; 
        }

        public void DeletePicture(int id)
        {
            ProfilePicture picture = _db.ProfilePicture.Find(id);
            _db.ProfilePicture.Remove(picture);
            _db.SaveChanges();
        }

        public ProfilePicture GetPic(int id)
        {
            return _db.ProfilePicture.FirstOrDefault(p => p.Id == id);
        }

        public ICollection<ProfilePicture> SeeAllPictures()
        {
            return _db.ProfilePicture
                .Include(u => u.ApplicationUser)
                .Include(r => r.ApplicationUser.Roles)
                .ToList();
        }

        public void UpdatePicture(int id, ProfilePicture picture)
        {
            _db.Entry(picture).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public ProfilePicture UploadPicture(ProfilePicture picture)
        {
            _db.ProfilePicture.Add(picture);
            _db.SaveChanges();
            return picture;
        }
    }
}
