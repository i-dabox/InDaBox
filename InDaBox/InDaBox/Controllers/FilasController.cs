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
    public class FilasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fila.Include(f => f.Columna);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Filas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fila = await _context.Fila
                .Include(f => f.Columna)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fila == null)
            {
                return NotFound();
            }

            return View(fila);
        }

        // GET: Filas/Create
        public IActionResult Create()
        {
            ViewData["ColumnaId"] = new SelectList(_context.Columna, "Id", "Nombre");
            return View();
        }

        // POST: Filas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,ColumnaId")] Fila fila)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fila);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColumnaId"] = new SelectList(_context.Columna, "Id", "Nombre", fila.ColumnaId);
            return View(fila);
        }

        // GET: Filas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fila = await _context.Fila.FindAsync(id);
            if (fila == null)
            {
                return NotFound();
            }
            ViewData["ColumnaId"] = new SelectList(_context.Columna, "Id", "Nombre", fila.ColumnaId);
            return View(fila);
        }

        // POST: Filas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,ColumnaId")] Fila fila)
        {
            if (id != fila.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fila);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilaExists(fila.Id))
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
            ViewData["ColumnaId"] = new SelectList(_context.Columna, "Id", "Nombre", fila.ColumnaId);
            return View(fila);
        }

        // GET: Filas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fila = await _context.Fila
                .Include(f => f.Columna)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fila == null)
            {
                return NotFound();
            }

            return View(fila);
        }

        // POST: Filas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fila = await _context.Fila.FindAsync(id);
            _context.Fila.Remove(fila);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilaExists(int id)
        {
            return _context.Fila.Any(e => e.Id == id);
        }
    }
}
