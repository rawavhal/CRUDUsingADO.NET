using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DbOper;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public DbOpers _DbOpers { get; set; }

        public HomeController(ILogger<HomeController> logger, DbOpers dbOpers)
        {            
            _logger = logger;
            _DbOpers = dbOpers;
        }

        public IActionResult Index()
        {           

            List<User> userlist = _DbOpers.GetAllUsers();

            return View(userlist);
        }

        
        public IActionResult Edit(int id)
        {
            User user = _DbOpers.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                int count = _DbOpers.EditUser(user, "EDIT");
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                User userTemp = _DbOpers.GetUser(user.UserId);

                if (userTemp.UserId==0)
                {
                    
                    int count = _DbOpers.EditUser(user, "CREATE");
                    return RedirectToAction("Index", "Home");
                }
                
                else
                {
                    ViewData["UserExistsErrorMessage"] = $"User ID : {user.UserId} already exists.";
                    return View(user);
                }
            }

            return View(user);
        }

       
        public IActionResult Delete (int id)
        {
            int count = _DbOpers.DeleteUser(id);
            return RedirectToAction("Index", "Home");
        }

        public User GetUser(int id)
        {
            User user = _DbOpers.GetUser(id);
            return user;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
