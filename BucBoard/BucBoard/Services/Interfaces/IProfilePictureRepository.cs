using BucBoard.Models.Entities.Existing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services.Interfaces
{
    public interface IProfilePictureRepository
    {
        ProfilePicture UploadPicture(ProfilePicture data);
        ICollection<ProfilePicture> SeeAllPictures();
        ProfilePicture GetPic(int id);
        void UpdatePicture(int id, ProfilePicture picture);
        void DeletePicture(int id);
    }
}
