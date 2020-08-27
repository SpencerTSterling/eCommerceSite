using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eCommerceSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                // map data to user account instance 
                UserAccount acc = new UserAccount()
                {
                    Username = reg.Username,
                    Email = reg.Email,
                    Password = reg.Password,
                    DateOfBirth = reg.DateOfBirth
                };
                // add to database
                _context.UserAccounts.Add(acc);
                await _context.SaveChangesAsync();
                //redirect to home page
                return RedirectToAction("Index", "Home");
            }

            return View(reg);
        }

        public IActionResult Login()
        {
            //check if user is already logged in
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            UserAccount acc =
            await      (from u in _context.UserAccounts
                        where (u.Username == login.UsernameOrEmail
                            || u.Email == login.UsernameOrEmail)
                            && u.Password == login.Password
                       select u).SingleOrDefaultAsync();

            if ( acc == null)
            {
                // credentials did not match
                // custom error message
                ModelState.AddModelError(string.Empty, "Credentials were not found");
                return View(login);
            }

            // log user into website
            HttpContext.Session.SetInt32("UserId", acc.UserId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            // destroy the session
            HttpContext.Session.Clear();

            // redirect to homepage
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
