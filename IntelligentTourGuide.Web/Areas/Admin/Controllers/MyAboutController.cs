using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MyAboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
