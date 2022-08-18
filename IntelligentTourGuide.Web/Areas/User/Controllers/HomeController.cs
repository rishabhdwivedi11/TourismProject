using IntelligentTourGuide.Web.Areas.User.ViewModels;
using IntelligentTourGuide.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<SelectListItem> states = new List<SelectListItem>();
            states.Add(new SelectListItem { Selected = true, Value = "", Text = "-- select a State or UT --" });
            states.AddRange(new SelectList(_context.States, "StateId", "StateName"));
            ViewData["StateId"] = states.ToArray();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("StateId")] ShowPlaceViewModel model)
        {
            // Retrieve the Places for the selected state
            var items = _context.Places.Where(m => m.StateId == model.StateId);

            // Populate the data into the viewmodel object
            model.Places = items.ToList();

            // Populate the data for the drop-down select list
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", "ImageUrl");

            // Display the View
            return View("Index", model);
        }

        public IActionResult Details(int? id)
        {
            return RedirectToAction("Index");
        }
    }
}
