using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nickwestertest.Data;
using nickwestertest.Models;

namespace nickwestertest.Controllers
{
    public class NicksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NicksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nicks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nick.ToListAsync());
        }

        // GET: Nicks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nick = await _context.Nick
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nick == null)
            {
                return NotFound();
            }

            return View(nick);
        }

        // GET: Nicks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nicks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,Vriendin")] Nick nick)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nick);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nick);
        }

        // GET: Nicks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nick = await _context.Nick.FindAsync(id);
            if (nick == null)
            {
                return NotFound();
            }
            return View(nick);
        }

        // POST: Nicks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,Vriendin")] Nick nick)
        {
            if (id != nick.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nick);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NickExists(nick.Id))
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
            return View(nick);
        }

        // GET: Nicks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nick = await _context.Nick
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nick == null)
            {
                return NotFound();
            }

            return View(nick);
        }

        // POST: Nicks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nick = await _context.Nick.FindAsync(id);
            if (nick != null)
            {
                _context.Nick.Remove(nick);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NickExists(int id)
        {
            return _context.Nick.Any(e => e.Id == id);
        }
    }
}
