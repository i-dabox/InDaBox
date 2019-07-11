using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InDaBox.Data;
using InDaBox.Models;
using InDaBox.Services;

namespace InDaBox.Controllers
{
    public class AlmacenesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlmacen _almacenServices;

        public AlmacenesController(ApplicationDbContext context, IAlmacen almacenServices)
        {
            _context = context;
            _almacenServices = almacenServices;
        }

        // GET: Almacenes
        public async Task<IActionResult> Index()
        {
            List<Almacen> almacen = await _context.Almacen.Include(a => a.Pasillos).ThenInclude(s => s.Secciones).ThenInclude(c => c.Columnas).ThenInclude(r => r.Filas).ToListAsync();
            return View(await _context.Almacen.ToListAsync());
        }

        // GET: Almacenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Almacen almacen = await _context.Almacen.Include(a => a.Pasillos).ThenInclude(s => s.Secciones).ThenInclude(c => c.Columnas).ThenInclude(r => r.Filas).FirstOrDefaultAsync(x => x.Id == id);

            if (almacen == null)
            {
                return NotFound();
            }

            return View(almacen);
        }

        // GET: Almacenes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Almacenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Direccion,CodigoPostal,Poblacion,Pasillos")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(almacen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(almacen);
        }

        // GET: Almacenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Almacen almacen = await _context.Almacen.Include(a => a.Pasillos).ThenInclude(s => s.Secciones).ThenInclude(c => c.Columnas).ThenInclude(r => r.Filas).FirstOrDefaultAsync(x => x.Id == id);

            if (almacen == null)
            {
                return NotFound();
            }
            return View(almacen);
        }

        // POST: Almacenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,CodigoPostal,Poblacion,Pasillos")] Almacen almacen)
        {
            await _almacenServices.VaciarHijosAlmacen(almacen);
            if (id != almacen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(almacen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlmacenExists(almacen.Id))
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
            return View(almacen);
        }

        // GET: Almacenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Almacen almacen = await _context.Almacen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (almacen == null)
            {
                return NotFound();
            }

            return View(almacen);
        }

        // POST: Almacenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Almacen almacen = await _context.Almacen.FindAsync(id);
            _context.Almacen.Remove(almacen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlmacenExists(int id)
        {
            return _context.Almacen.Any(e => e.Id == id);
        }
    }
}
