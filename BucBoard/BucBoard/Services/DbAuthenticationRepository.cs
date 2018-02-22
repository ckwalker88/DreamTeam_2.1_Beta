using BucBoard.Data;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services
{
    public class DbAuthenticationRepository : IAuthenticationRepository
    {
        private AuthenticationDbContext _db;

        public DbAuthenticationRepository(AuthenticationDbContext db)
        {
            _db = db; 
        }

        public IQueryable<IdentityRole> ReadAllRoles()
        {
            return _db.Roles;
        }
    }
}
