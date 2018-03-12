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

        [Authorize(Roles = "Admin")]
        public IActionResult SelectUser()
        {
            //string users = _users.ReadAllUsers().ToString();
            return View(_users.ReadAllUsers());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SelectUserToDelete()
        {
            //string users = _users.ReadAllUsers().ToString();
            return View(_users.ReadAllUsers());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(ApplicationUser user /*string id*/)
        {
            //var roles = _users.ReadAllRoles();
            //var users = _users.ReadAllUsers();

            //var members = await _userManager.GetRolesAsync(userName);


            //var applicationUser = await _userManager.FindByIdAsync(id);
            //var applicationUserRole = await _userManager.GetRolesAsync(applicationUser);


            //var userRole = await _roleManager.FindByNameAsync(applicationUserRole);

            //var applicationUser = await _userManager.GetRolesAsync(userName);
            
            var applicationUser = await _userManager.DeleteAsync(user);

            //var model = new Users_Roles_ViewModel
            //{

            //    //AppUsers = applicationUser,
            //    //RoleName = applicationUserRole.ToString() 

            //    RoleName = applicationUser.ToString()
                

            //};

            return RedirectToAction("Index");
        }

    }
}