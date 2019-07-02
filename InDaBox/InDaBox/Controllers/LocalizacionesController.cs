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
    public class LocalizacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocalizacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Localizaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Localizacion.Include(l => l.Fila).Include(l => l.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Localizaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizacion = await _context.Localizacion
                .Include(l => l.Fila)
                .Include(l => l.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localizacion == null)
            {
                return NotFound();
            }

            return View(localizacion);
        }

        // GET: Localizaciones/Create
        public IActionResult Create()
        {
            ViewData["FilaId"] = new SelectList(_context.Fila, "Id", "Nombre");
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Nombre");
            return View();
        }

        // POST: Localizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilaId,ProductoId")] Localizacion localizacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(localizacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilaId"] = new SelectList(_context.Fila, "Id", "Nombre", localizacion.FilaId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Nombre", localizacion.ProductoId);
            return View(localizacion);
        }

        // GET: Localizaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizacion = await _context.Localizacion.FindAsync(id);
            if (localizacion == null)
            {
                return NotFound();
            }
            ViewData["FilaId"] = new SelectList(_context.Fila, "Id", "Nombre", localizacion.FilaId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Nombre", localizacion.ProductoId);
            return View(localizacion);
        }

        // POST: Localizaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FilaId,ProductoId")] Localizacion localizacion)
        {
            if (id != localizacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localizacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalizacionExists(localizacion.Id))
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
            ViewData["FilaId"] = new SelectList(_context.Fila, "Id", "Nombre", localizacion.FilaId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Nombre", localizacion.ProductoId);
            return View(localizacion);
        }

        // GET: Localizaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizacion = await _context.Localizacion
                .Include(l => l.Fila)
                .Include(l => l.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localizacion == null)
            {
                return NotFound();
            }

            return View(localizacion);
        }

        // POST: Localizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var localizacion = await _context.Localizacion.FindAsync(id);
            _context.Localizacion.Remove(localizacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalizacionExists(int id)
        {
            return _context.Localizacion.Any(e => e.Id == id);
        }
    }
}
