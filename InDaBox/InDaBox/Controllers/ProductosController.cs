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
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProducto _productoServices;
        private readonly ILocalizacion _localizacionServices;
        private readonly IFiltrosComunes _filtrosComunes;


        public ProductosController(ApplicationDbContext context, IProducto productoServices, ILocalizacion localizacionServices, IFiltrosComunes filtrosComunes)
        {
            _context = context;
            _productoServices = productoServices;
            _localizacionServices = localizacionServices;
            _filtrosComunes = filtrosComunes;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string busqueda)
        {
            ProductoLocacionVM productoLocacion = new ProductoLocacionVM()
            {
                Productos = await _productoServices.BusquedaProducto(busqueda),
                Localizaciones = await _context.Localizacion.Include(l => l.Fila).Include(l => l.Producto).ToListAsync()

            };
            ViewData["FilaId"] = new SelectList(_context.Fila, "Id", "Nombre");
            return View(productoLocacion);

            //List<Localizacion> localizaciones = await _localizacionServices.BusquedaLocalizacion(busqueda);
            //return View(localizaciones);

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
            _productoServices.BorradoProducto(producto);

        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["localizaciones"] = await _context.Fila.ToListAsync();
            ViewData["FilaId"] = _context.Producto.Include(x => x.Localizaciones).ThenInclude(x => x.Fila).Where(x => x.Id == id).ToListAsync();
            var producto = await _context.Producto.Include(x => x.Localizaciones).ThenInclude(x => x.Fila).Where(x => x.Id == id).FirstOrDefaultAsync();
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
        public async Task<IActionResult> Edit(int id, int localizacionId, [Bind("Id,Nombre,Descripcion,Imagen,Caducidad,Cantidad,Borrado")] Producto productoEditado)
        {
            if (id != productoEditado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Producto producto = await _context.Producto.Include(prod => prod.Localizaciones).FirstOrDefaultAsync(prod => prod.Id == id);
                producto.Nombre = productoEditado.Nombre;
                producto.Descripcion = productoEditado.Descripcion;
                producto.Imagen = productoEditado.Imagen;
                producto.Caducidad = productoEditado.Caducidad;
                producto.Cantidad = productoEditado.Cantidad;
                producto.Borrado = productoEditado.Borrado;
                Localizacion localizacion = await _context.Localizacion.FirstOrDefaultAsync(loc => loc.ProductoId == producto.Id);
                if (localizacion != null)
                {
                    localizacion.FilaId = localizacionId;
                    producto.Localizaciones.Add(localizacion);
                }

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
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> BorrarProductosRotos(int id, int productosRotos)
        {
            Producto producto = await _context.Producto.FirstOrDefaultAsync(Id => Id.Id == id);
            producto.Cantidad -= productosRotos;

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index");
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
