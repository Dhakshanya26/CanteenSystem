﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanteenSystem.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CanteenSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CanteenSystemDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public HomeController(ILogger<HomeController> logger, CanteenSystemDbContext context,
                 UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
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


        [Route("ShowUserProfile/{id}")]
        public async Task<IActionResult> ShowUserProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles
                .Include(u => u.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProfile == null)
            {
                return NotFound();
            }
            var userRole = await userManager.GetRolesAsync(userProfile.ApplicationUser);
            return View(ConvertUserToUserProfile(userProfile, userRole?.First()));
        }


        [Route("EditUserProfile/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditUserProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", userProfile.ApplicationUserId);
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("EditUserProfile/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfile(int id, [Bind("Id,Name,EmailAddress,RollNumber,Department,IsVerified,ApplicationUserId")] UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();

                    var applicationUser = await userManager.FindByIdAsync(userProfile.ApplicationUserId);
                    if (applicationUser != null)
                    {
                        applicationUser.UserName = userProfile.EmailAddress;
                        await userManager.UpdateAsync(applicationUser);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", userProfile.ApplicationUserId);
            return View(userProfile);
        }


        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.Id == id);
        }
        private UserModel ConvertUserToUserProfile(UserProfile user, string role)
        {


            return new UserModel
            {
                ApplicationUserId = user.ApplicationUserId,
                Department = user.Department,
                EmailAddress = user.EmailAddress,
                IsVerified = user.IsVerified,
                Name = user.Name,
                RollNumber = user.RollNumber,
                Role = role,
                Id = user.Id
            };
        }
    }

}
