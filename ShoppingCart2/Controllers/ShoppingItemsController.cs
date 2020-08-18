using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart2.Data;
using ShoppingCart2.Models;

namespace ShoppingCart2.Controllers
{
    // this handles users who are not logged in and directs them to the login page
    [Authorize]

    public class ShoppingItemsController : Controller
    {

        //this is a dependancy injection pattern

        private readonly ApplicationDbContext _context;

        // this is added to the dependancy injection so we can get the user see 21min pt on Shopping Cart_intro_p9
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)

        {

            _context = context;
            _userManager = userManager;

        }


        // GET: ShoppingItemsController

        public async Task<ActionResult> Index()
        {
            // this is filtering the items so we only see the items that belong to the signed in user
            // 38 minute point - goes over why we need to use await for items 

            var user = await GetCurrentUserAsync();
            var items = await _context.ShoppingItem
                .Where(si => si.ApplicationUserId == user.Id)
                .ToListAsync();

            return View(items);
        }

        // GET: ShoppingItemsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingItemsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShoppingItem shoppingItem)
        {
            try
            {
                // userManager here to be able to get the user - see one line private helper method 
                // user app at bottom below

                var user = await GetCurrentUserAsync();
                shoppingItem.ApplicationUserId = user.Id;

                _context.ShoppingItem.Add(shoppingItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingItemsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _context.ShoppingItem.FirstOrDefaultAsync(si => si.Id == id);
            var loggedInUser = await GetCurrentUserAsync();

            if (item.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: ShoppingItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ShoppingItem shoppingItem)
        {

            try
            {

                // video Cart_Intro_p9 at 1hr 15 min   Adam explains why he puts in this code to ALWAYS get the current
                // user and set that again - better to trust the cookie that holds the user id info than form field values
                var user = await GetCurrentUserAsync();
                shoppingItem.ApplicationUserId = user.Id;

                _context.ShoppingItem.Update(shoppingItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingItemsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //this is a one line private helper method - gets the entire user object of the person making the request 
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
