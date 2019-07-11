using InDaBox.Data;
using InDaBox.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Services
{
    public class ProductoServices : IProducto

    {
        private readonly ApplicationDbContext _context;

        public ProductoServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public void BorradoProducto(Producto producto)
        {
            producto.Borrado = true;

            // actualizar bbdd
            _context.Update(producto);
            _context.SaveChanges();
        }



        public Task<List<Producto>> ProductoCaducado()//(string busqueda)
        {
            //if (busqueda != null)
            //{
            List<Producto> productos = _context.Producto.Where(producto => producto.Borrado != true && (producto.Caducidad < (DateTime.Now) || (producto.Caducidad < (DateTime.Now.AddDays(7))))).ToList();

            return Task.FromResult(productos);

            //if (Caducidad != null)
            //{

            //    List<Producto> caducados = _context.Producto.Where(producto => producto.Caducidad != Null).ToList();
            //    // }
            //}
        }


        async Task<List<Producto>> IProducto.BusquedaProducto(string busqueda)
        {
            if (busqueda != null)
            {
                List<Producto> productos = await _context.Producto.Where(producto => producto.Borrado != true).Include(x=>x.Localizaciones).ThenInclude(a => a.Fila).ToListAsync();

                if (productos.Where(x => x.Nombre.ToLower().Contains(busqueda.ToLower())).ToList().Count > 0)
                {
                    productos = productos.Where(x => x.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
                }

                if (DateTime.TryParse(busqueda, out DateTime result))
                {
                    productos = productos.Where(x => x.Caducidad == Convert.ToDateTime(busqueda)).ToList();
                }
                if (Int32.TryParse(busqueda, out int resultado))
                {
                    productos = productos.Where(x => x.Cantidad == Convert.ToInt32(busqueda)).ToList();
                }

                return productos;
            }
            else
            {

                return await _context.Producto.Where(producto => producto.Borrado != true).Include(loc=>loc.Localizaciones).ThenInclude(a=>a.Fila).ToListAsync();
            }
        }

      
        //async Task<List<ProductoLocacionVM>> IProducto.ProductoLocacion()
        //{
        //    List<Producto> productos = await _context.Producto.Where(x => x.Borrado != true).ToListAsync();
        //    List<ProductoLocacionVM> productoLocacionVMsList = new List<ProductoLocacionVM>();
        //    foreach (Producto producto in productos)
        //    {
        //        List<Localizacion> localizaciones = await _context.Localizacion.Include(x => x.Producto).Where(x => x.Producto.Nombre == producto.Nombre).ToListAsync();
        //        ProductoLocacionVM productoLocacionVM = new ProductoLocacionVM()
        //        {
        //            Localizaciones = localizaciones,
        //            Productos = producto
        //        };
        //        productoLocacionVMsList.Add(productoLocacionVM);
        //    }
        //    return productoLocacionVMsList;
        //}

        //async  Task<List<ProductoLocacionVM>> IProducto.ProductoLocacion()
        //{
        //    //List<Producto> productos = new List<Producto>();
        //    //productos = await _context.Producto.Where(x => x.Borrado != true).ToListAsync();

        //    //List<Localizacion> localizaciones = new List<Localizacion>();
        //    //localizaciones = await _context.Localizacion.Include(x =>x.Producto).ToListAsync();
        //    List<ProductoLocacionVM> ProductoLocaciones = new List<ProductoLocacionVM>();
        //    ProductoLocaciones = await _context.Localizacion productoLocacions;


        public async Task<List<Producto>> ProductoLocacion()
        {
           return await _context.Producto.Include(x=>x.Localizaciones).ToListAsync();           

        }
    }
}











