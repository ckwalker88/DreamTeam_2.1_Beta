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
    public class DbAuthenticationRepository : IAuthenticationRepository
    {
        private AuthenticationDbContext _db;
        private BucBoardDBContext _bBDb;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public DbAuthenticationRepository(AuthenticationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, BucBoardDBContext bBDb)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _bBDb = bBDb;
        }

        public void Delete(string id)
        {
            var user = _bBDb.AspNetUsers.FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                _bBDb.AspNetUsers.Remove(user);
            }
        }

        public ApplicationUser Read(string id)
        {
            return _db.Users.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<IdentityRole> ReadAllRoles()
        {
            return _db.Roles;
        }

        public IQueryable<AspNetRoles> ReadAllUserRoles()
        {
            return _bBDb.AspNetRoles.Include(r => r.AspNetUsers);
        }

        public IQueryable<ApplicationUser> ReadAllUsers()
        {
            return _db.Users.Include(r => r.Roles);
         
        }

        public IQueryable<AspNetUsers> ReadAllUsersBB()
        {
            return _bBDb.AspNetUsers.Include(r => r.Roles);
        }

        public AspNetUsers ReadBB(string id)
        {
            return _bBDb.AspNetUsers.FirstOrDefault(p => p.Id == id);
        }
    }
}
