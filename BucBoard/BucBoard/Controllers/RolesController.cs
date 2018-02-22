using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{
    public class RolesController : Controller
    {
        private IAuthenticationRepository _roles;

        public RolesController(IAuthenticationRepository roles)
        {
            _roles = roles;
        }

        public IActionResult Index()
        {
            return View(_roles.ReadAllRoles());
        }
    }
}