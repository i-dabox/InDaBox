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
    public class SeccionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Secciones
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Seccion.Include(s => s.Pasillo);
            return View(await applicationDbContext.ToListAsync());
        }

      
        // GET: Secciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccion
                .Include(s => s.Pasillo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seccion == null)
            {
                return NotFound();
            }

            return View(seccion);
        }

        // GET: Secciones/Create
        public IActionResult Create()
        {
            ViewData["PasilloId"] = new SelectList(_context.Pasillo, "Id", "Nombre");
            return View();
        }

        // POST: Secciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PasilloId")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PasilloId"] = new SelectList(_context.Pasillo, "Id", "Nombre", seccion.PasilloId);
            return View(seccion);
        }

        // GET: Secciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccion.FindAsync(id);
            if (seccion == null)
            {
                return NotFound();
            }
            ViewData["PasilloId"] = new SelectList(_context.Pasillo, "Id", "Nombre", seccion.PasilloId);
            return View(seccion);
        }

        // POST: Secciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,PasilloId")] Seccion seccion)
        {
            if (id != seccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeccionExists(seccion.Id))
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
            ViewData["PasilloId"] = new SelectList(_context.Pasillo, "Id", "Nombre", seccion.PasilloId);
            return View(seccion);
        }

        // GET: Secciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccion
                .Include(s => s.Pasillo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seccion == null)
            {
                return NotFound();
            }

            return View(seccion);
        }

        // POST: Secciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seccion = await _context.Seccion.FindAsync(id);
            _context.Seccion.Remove(seccion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeccionExists(int id)
        {
            return _context.Seccion.Any(e => e.Id == id);
        }
    }
}
