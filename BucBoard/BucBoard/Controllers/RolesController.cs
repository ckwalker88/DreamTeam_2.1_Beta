using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Models.ViewModels;
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

        [Authorize(Roles="Admin")]
        public IActionResult Index()
        {
            return View(_roles.ReadAllRoles());
        }

        //public IActionResult RoleListDetailed()
        //{
        //    //var role = _roleManager.Roles.First(r => r.Name == roleName);
        //    //var count = _roleManager.Roles.Count(r => r.Id == )

        //    var model = _roleManager.Roles.Select(r => new AppRoleListDetailedViewModel
        //    {
        //        RoleName = r.Name,
        //        Id = r.Id,
        //        Description = r.Name,
        //        NumberOfUsers = _roleManager.Roles.Count()
        //    });       
                
        //}
    }
}