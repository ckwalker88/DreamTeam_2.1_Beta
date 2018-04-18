
using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BucBoard.Services.Interfaces
{
    public interface IAuthenticationRepository
    {
        IQueryable<IdentityRole> ReadAllRoles();
        IQueryable<AspNetRoles> ReadAllUserRoles();
        IQueryable<AspNetUsers> ReadAllUsersBB();
        IQueryable<ApplicationUser> ReadAllUsers();
        ApplicationUser Read(string id);
        AspNetUsers ReadBB(string id);
        void Delete(string id);
        
        

    }
}
