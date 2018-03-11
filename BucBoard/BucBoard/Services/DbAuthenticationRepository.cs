using BucBoard.Data;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services
{
    public class DbAuthenticationRepository : IAuthenticationRepository
    {
        private AuthenticationDbContext _db;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public DbAuthenticationRepository(AuthenticationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IQueryable<IdentityRole> ReadAllRoles()
        {
            return _db.Roles;
        }

        public IQueryable<ApplicationUser> ReadAllUsers()
        {
            return _db.Users;
         
        }



    }
}
