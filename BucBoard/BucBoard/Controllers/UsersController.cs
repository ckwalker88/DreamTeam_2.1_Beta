using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BucBoard.Controllers
{
    public class UsersController : Controller
    {

        private IAuthenticationRepository _users;
        private UserManager<ApplicationUser> _userManager;

        public UsersController(IAuthenticationRepository users, UserManager<ApplicationUser> userManager)
        {
            _users = users;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
        
            return View(_users.ReadAllUsers());
        }

        
    }
}