using BucBoard.Models.Entities.Existing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services.Interfaces
{
    public interface IProfilePictureRepository
    {
        ProfilePicture UploadPicture(ProfilePicture picture);
        ICollection<ProfilePicture> SeeAllPictures();

    }
}
