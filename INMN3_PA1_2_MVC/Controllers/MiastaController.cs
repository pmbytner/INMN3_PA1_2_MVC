using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INMN3_PA1_2_MVC.Data;
using INMN3_PA1_2_MVC.Models;
using Microsoft.IdentityModel.Tokens;

namespace INMN3_PA1_2_MVC.Controllers
{
    public class MiastaController : Controller
    {
        private readonly INMN3_PA1_2_MVCContext _context;

        public MiastaController(INMN3_PA1_2_MVCContext context)
        {
            _context = context;
        }

        // GET: Miasta
        public async Task<IActionResult> Index(string? id)
        {
            if(_context.Miasta == null)
                Problem("Entity set 'INMN3_PA1_2_MVCContext.Miasto'  is null.");

            string? szukaneWNazwie = id;
            if (szukaneWNazwie.IsNullOrEmpty())
            {
                return View(_context.Miasta);
            }
            else
            {
                var miasta =
                    from miasto
                    in _context.Miasta
                    where miasto.Nazwa.Contains(szukaneWNazwie)
                    select miasto
                    ;
                ViewData["Szukane"] = szukaneWNazwie;
                return View(await miasta.ToListAsync());
            }
        }

        // GET: Miasta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Miasta == null)
            {
                return NotFound();
            }

            var miasto = await _context.Miasta
                .FirstOrDefaultAsync(miasto => miasto.Id == id);
            if (miasto == null)
            {
                return NotFound();
            }

            return View(miasto);
        }

        // GET: Miasta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miasta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Populacja,Powierzchnia")] Miasto miasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miasto);
        }

        // GET: Miasta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Miasta == null)
            {
                return NotFound();
            }

            var miasto = await _context.Miasta.FindAsync(id);
            if (miasto == null)
            {
                return NotFound();
            }
            return View(miasto);
        }

        // POST: Miasta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Populacja,Powierzchnia")] Miasto miasto)
        {
            if (id != miasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiastoExists(miasto.Id))
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
            return View(miasto);
        }

        // GET: Miasta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Miasta == null)
            {
                return NotFound();
            }

            var miasto = await _context.Miasta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miasto == null)
            {
                return NotFound();
            }

            return View(miasto);
        }

        // POST: Miasta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Miasta == null)
            {
                return Problem("Entity set 'INMN3_PA1_2_MVCContext.Miasto'  is null.");
            }
            var miasto = await _context.Miasta.FindAsync(id);
            if (miasto != null)
            {
                _context.Miasta.Remove(miasto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiastoExists(int id)
        {
          return (_context.Miasta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
