using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
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

        public ICollection<ProfilePicture> SeeAllPictures()
        {
            return _db.ProfilePicture
                .Include(u => u.ApplicationUser)
                .Include(r => r.ApplicationUser.Roles)
                .ToList();
        }

        public ProfilePicture UploadPicture(ProfilePicture picture)
        {
            _db.ProfilePicture.Add(picture);
            _db.SaveChanges();
            return picture;
        }
    }
}
