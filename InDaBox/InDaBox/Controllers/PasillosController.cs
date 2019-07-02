using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InDaBox.Data;
using InDaBox.Models;

namespace InDaBox.Controllers
{
    public class PasillosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PasillosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pasillos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pasillo.Include(p => p.Almacen);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pasillos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasillo = await _context.Pasillo
                .Include(p => p.Almacen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasillo == null)
            {
                return NotFound();
            }

            return View(pasillo);
        }

        // GET: Pasillos/Create
        public IActionResult Create()
        {
            ViewData["AlmacenId"] = new SelectList(_context.Almacen, "Id", "Direccion");
            return View();
        }

        // POST: Pasillos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,NumeroDeSecciones,AlmacenId")] Pasillo pasillo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pasillo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlmacenId"] = new SelectList(_context.Almacen, "Id", "Direccion", pasillo.AlmacenId);
            return View(pasillo);
        }

        // GET: Pasillos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasillo = await _context.Pasillo.FindAsync(id);
            if (pasillo == null)
            {
                return NotFound();
            }
            ViewData["AlmacenId"] = new SelectList(_context.Almacen, "Id", "Direccion", pasillo.AlmacenId);
            return View(pasillo);
        }

        // POST: Pasillos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,NumeroDeSecciones,AlmacenId")] Pasillo pasillo)
        {
            if (id != pasillo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasillo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasilloExists(pasillo.Id))
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
            ViewData["AlmacenId"] = new SelectList(_context.Almacen, "Id", "Direccion", pasillo.AlmacenId);
            return View(pasillo);
        }

        // GET: Pasillos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasillo = await _context.Pasillo
                .Include(p => p.Almacen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasillo == null)
            {
                return NotFound();
            }

            return View(pasillo);
        }

        // POST: Pasillos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasillo = await _context.Pasillo.FindAsync(id);
            _context.Pasillo.Remove(pasillo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasilloExists(int id)
        {
            return _context.Pasillo.Any(e => e.Id == id);
        }
    }
}
