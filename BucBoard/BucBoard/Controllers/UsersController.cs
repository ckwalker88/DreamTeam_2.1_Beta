using System.Threading.Tasks;
using BucBoard.Data;
using BucBoard.Models;
using BucBoard.Models.ViewModels;
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

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
        
            return View(_users.ReadAllUsers());

        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DisplayUsers()
        {
            return View(_users.ReadAllUsersBB());
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult UserRoles()
        {
            return View(_users.ReadAllRoles());
        }

        

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UserInRole()
        {
            var model = await _userManager.GetUsersInRoleAsync("Admin");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UsersInSelectedRole(string roleName)
        {
            var model = await _userManager.GetUsersInRoleAsync(roleName);
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult SelectUser()
        {
            return View(_users.ReadAllUsers());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteUser(string id)
        {
            var user = _users.ReadBB(id);
            if (user == null)
            {
                return RedirectToAction("DisplayUsers");
            }
            return View(user);
        }

        [Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = _users.Read(id);
            await _userManager.DeleteAsync(user);
            //_users.Delete(id);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult SelectUserToDelete()
        {
            return View(_users.ReadAllUsers());
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult EditUser()
        {
            return View(_users.ReadAllUsers());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteUserAsync(ApplicationUser user)
        {
            
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

    }
}