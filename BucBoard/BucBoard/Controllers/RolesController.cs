using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{
    public class RolesController : Controller
    {
        private IAuthenticationRepository _roles;
        private RoleManager<IdentityRole> _roleManager;

        public RolesController(IAuthenticationRepository roles, RoleManager<IdentityRole> roleManager)
        {
            _roles = roles;
            _roleManager = roleManager;
        }

        [Authorize(Roles="SuperAdmin")]
        public IActionResult Index()
        {
            return View(_roles.ReadAllRoles());
        }

    }
}