using System.Threading.Tasks;
using BucBoard.Data;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{
    public class UsersController : Controller
    {
        private AuthenticationDbContext _context;
        private IAuthenticationRepository _users;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UsersController(AuthenticationDbContext context, IAuthenticationRepository users, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _users = users;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
        
            return View(_users.ReadAllUsers());

        }

        [Authorize(Roles = "Admin")]
        public IActionResult UserRoles()
        {
            return View(_users.ReadAllRoles());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserInRole()
        {
            var model = await _userManager.GetUsersInRoleAsync("Admin");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UsersInSelectedRole(string roleName)
        {
            var model = await _userManager.GetUsersInRoleAsync(roleName);
            return View(model);
        }



    }
}