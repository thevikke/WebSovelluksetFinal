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
            newTilaus.OrderDate = DateTime.Now;
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
        public IActionResult EditView(int id)
        {
            var order = _context.Orders.SingleOrDefault(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["HouseTypeID"] = new SelectList(_context.HouseTypes, "ID", "Type");

            return PartialView("Edit", order);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public IActionResult EditOrder(Tilaus order)
        { 
            if (ModelState.IsValid)
            {
                try
                {
                    var dbOrder = _context.Orders.Single(u => u.ID == order.ID);
                    dbOrder.Description = order.Description;
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException){}
                return Json(new
                {
                    msg = "Order edited!"
                });
            }
            return PartialView("Edit", order);
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
            var results = from s in _context.Orders
                         where s.User.UserName == User.Identity.Name
                         select s;

            foreach(var result in results)
            {
                if(result.StartDate != null)
                {
                    result.Status = "ALOITETTU";
                }
                if (result.FinishDate != null)
                {
                    result.Status = "VALMIS";
                }
                if (result.Approved)
                {
                    result.Status = "HYVÄKSYTTY";
                }
                if (result.Discarded)
                {
                    result.Status = "HYLÄTTY";
                }

            }

            return PartialView("OrdersList", results);
        }
        public IActionResult ApproveOrder(int id)
        {
            var r = _context.Orders.Where(t => t.ID == id).SingleOrDefault();
            r.Discarded = false;
            r.Approved = true;
            r.ApproveDate = DateTime.Now;
            _context.SaveChanges();

            // HUOM! ObjectResult sisältää automatic content negotiation:n
            return Json(new
            {
                msg = "Order approved!"
            });
            //return Json(new { status = "OK" });
        }
        public IActionResult DiscardOrder(int id)
        {
            var r = _context.Orders.Where(t => t.ID == id).SingleOrDefault();
            r.Discarded = true;
            r.Approved = false;
            r.DiscardDate = DateTime.Now;
            _context.SaveChanges();

            // HUOM! ObjectResult sisältää automatic content negotiation:n
            return Json(new
            {
                msg = "Order discarded!"
            });
            //return Json(new { status = "OK" });
        }
    }
}