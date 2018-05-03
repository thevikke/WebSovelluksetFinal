using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSovelluksetFinal.Data;
using WebSovelluksetFinal.Models;

namespace WebSovelluksetFinal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;
        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> UserManager)
        {
            _context = context;
            _UserManager = UserManager;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public IActionResult CreateView()
        {

            ViewBag.AsiakastyyppiID = new SelectList(_context.HouseTypes, "ID", "Type");
            return PartialView("Create");
        }

        // POST: Home/Create
        [HttpPost]
        public IActionResult CreateOrder(Tilaus newTilaus)
        {
            newTilaus.UserID = _UserManager.GetUserId(User);
            newTilaus.Status = "TILATTU";
            if (ModelState.IsValid)
            {
                _context.Add(newTilaus);
                _context.SaveChanges();
                return Json(new
                {
                    msg = "Order created!"
                });
            }
            return Json(new
            {
                msg = "Failed to create!"
            });
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public IActionResult DeleteView(int id)
        {
            var r = from s in _context.Orders
                    select s;

            r = r.Where(t => t.ID == id);

            return PartialView("DeleteInfo", r);
        }

        // POST: Home/Delete/5

        public IActionResult Delete(int id)
        {
            var r = _context.Orders.Where(t => t.ID == id).SingleOrDefault();

            _context.Orders.Remove(r);
            _context.SaveChanges();

            // HUOM! ObjectResult sisältää automatic content negotiation:n
            return new JsonResult(new { status = "OK" });
            //return Json(new { status = "OK" });
        }

        public IActionResult GetOrders()
        {
            var result = from s in _context.Orders
                         where s.User.UserName == User.Identity.Name
                         select s;

            return PartialView("OrdersList", result);
        }
    }
}