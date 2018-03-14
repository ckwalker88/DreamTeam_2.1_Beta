
using BucBoard.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Models.Entities.Existing;

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
