using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebSovelluksetFinal.Controllers
{
    public class FeaturesController : Controller
    {
        public IActionResult Features()
        {
            return View();
        }
    }
}