using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntelligentTourGuide.Web.Data;
using IntelligentTourGuide.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace IntelligentTourGuide.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class StatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatesController> _logger;

        public StatesController(ApplicationDbContext context, ILogger<StatesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Admin/States
        public async Task<IActionResult> Index()
        {
            return View(await _context.States.ToListAsync());
        }

        // GET: Admin/States/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: Admin/States/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,StateName")] State state)
        {
            if (ModelState.IsValid)
            {
                state.StateName = state.StateName.Trim();

                // Check for Duplicate CategoryName
                bool isDuplicateFound
                    = _context.States.Any(c => c.StateName == state.StateName);
                if (isDuplicateFound)
                {
                    ModelState.AddModelError("StateName", "Duplicate! Another state with same name exists");
                }
                else
                {
                    _context.Add(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(state);
        }

        // GET: Admin/States/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }

        // POST: Admin/States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("StateId,StateName")] State stateInputModel)
        {
            if (id != stateInputModel.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                stateInputModel.StateName = stateInputModel.StateName.Trim();

                // Check for duplicate Category
                bool isDuplicateFound
                    = _context.States.Any(c => c.StateName == stateInputModel.StateName
                                                   && c.StateId != stateInputModel.StateId);
                if (isDuplicateFound)
                {
                    ModelState.AddModelError("StateName", "A Duplicate State was found!");
                }
                else
                {
                    try
                    {
                        _context.Update(stateInputModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StateExists(stateInputModel.StateId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return View(stateInputModel);
        }

        // GET: Admin/States/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: Admin/States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var state = await _context.States.FindAsync(id);
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(short id)
        {
            return _context.States.Any(e => e.StateId == id);
        }
    }
}
