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

        public DbAuthenticationRepository(AuthenticationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
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
