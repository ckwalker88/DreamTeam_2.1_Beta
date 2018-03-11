
using BucBoard.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services.Interfaces
{
    public interface IAuthenticationRepository
    {
        IQueryable<IdentityRole> ReadAllRoles();
        IQueryable<ApplicationUser> ReadAllUsers();
        //IList<ApplicationUser> GetAppUsersInRole(string roleName);
        

    }
}
