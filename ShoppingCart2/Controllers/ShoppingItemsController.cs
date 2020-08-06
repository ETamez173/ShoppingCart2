using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart2.Data;
using ShoppingCart2.Models;

namespace ShoppingCart2.Controllers
{


    public class ShoppingItemsController : Controller
    {

        //this is dependancy injection

        private readonly ApplicationDbContext _context;

        // this is added to the dependancy injection so we can get the user see 21min pt on Cart_intro_p9
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingItemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)

        {

            _context = context;
            _userManager = userManager;

        }


        // GET: ShoppingItemsController
        public ActionResult Index()
        {
            var items = _context.ShoppingItem.Include(si => si.ApplicationUser);
            return View();
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
                // userManager here to be able to get the user - see user app at bottom below
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
