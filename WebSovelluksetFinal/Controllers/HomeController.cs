﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSovelluksetFinal.Data;

namespace WebSovelluksetFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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

            return PartialView("OrdersList", r);
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