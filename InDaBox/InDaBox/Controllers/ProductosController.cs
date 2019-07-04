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
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string busqueda)
        {
            if (busqueda != null)
            {
                List<Producto> productos = _context.Producto.Where(producto => producto.Borrado != true).ToList();

                if(productos.Where(x=>x.Nombre.ToLower().Contains(busqueda.ToLower())).ToList().Count > 0)
                {
                    productos = productos.Where(x => x.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
                }

                if(DateTime.TryParse(busqueda, out DateTime result))
                {
                    productos = productos.Where(x => x.Caducidad == Convert.ToDateTime(busqueda)).ToList();
                }
                if (Int32.TryParse(busqueda, out int resultado))
                {
                    productos = productos.Where(x => x.Cantidad == Convert.ToInt32(busqueda)).ToList();
                }
                return View(productos);
            }
            else
            {
                return View(await _context.Producto.Where(producto => producto.Borrado != true).ToListAsync());
            }
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Imagen,Caducidad,Cantidad,Borrado")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }


        //Borrado Logico
        private void Borrado(Producto producto)
        {
            // poner el booleano de este producto en true producto

            producto.Borrado = true;
            // actualizar bbdd
            _context.Update(producto);
            _context.SaveChanges();

        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Imagen,Caducidad,Cantidad,Borrado")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        { //TODO preguntar si es necesario que el borrado logico suponga un valor de cantidad = 0 o no es necesario.
            if (id == null)
            {
                return NotFound();
            }

            Producto producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
            //return View(Borrado(producto));

        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            //_context.Producto.Remove(producto);
            //await _context.SaveChangesAsync();
            Borrado(producto);

            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}
