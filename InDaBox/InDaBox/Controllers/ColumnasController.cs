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
    public class ColumnasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColumnasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Columnas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Columna.Include(c => c.Seccion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Columnas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var columna = await _context.Columna
                .Include(c => c.Seccion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (columna == null)
            {
                return NotFound();
            }

            return View(columna);
        }

        // GET: Columnas/Create
        public IActionResult Create()
        {
            ViewData["SeccionId"] = new SelectList(_context.Seccion, "Id", "NombreSeccion");
            return View();
        }

        // POST: Columnas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,SeccionId")] Columna columna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(columna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeccionId"] = new SelectList(_context.Seccion, "Id", "NombreSeccion", columna.SeccionId);
            return View(columna);
        }

        // GET: Columnas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var columna = await _context.Columna.FindAsync(id);
            if (columna == null)
            {
                return NotFound();
            }
            ViewData["SeccionId"] = new SelectList(_context.Seccion, "Id", "NombreSeccion", columna.SeccionId);
            return View(columna);
        }

        // POST: Columnas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,SeccionId")] Columna columna)
        {
            if (id != columna.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(columna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColumnaExists(columna.Id))
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
            ViewData["SeccionId"] = new SelectList(_context.Seccion, "Id", "NombreSeccion", columna.SeccionId);
            return View(columna);
        }

        // GET: Columnas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var columna = await _context.Columna
                .Include(c => c.Seccion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (columna == null)
            {
                return NotFound();
            }

            return View(columna);
        }

        // POST: Columnas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var columna = await _context.Columna.FindAsync(id);
            _context.Columna.Remove(columna);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColumnaExists(int id)
        {
            return _context.Columna.Any(e => e.Id == id);
        }
    }
}
