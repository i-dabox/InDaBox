using InDaBox.Data;
using InDaBox.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Services
{
    public class LocalizacionServices : ILocalizacion
    {
        private readonly ApplicationDbContext _context;

        public LocalizacionServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Localizacion>> BusquedaLocalizacion(string busqueda)
        {
            if (busqueda != null)
            {

                List<Localizacion> localizaciones = await _context.Localizacion.Include(fila => fila.Fila).Include(producto => producto.Producto).Where(borr => borr.Producto.Borrado != true).ToListAsync();

                if (localizaciones.Where(loc => loc.Producto.Nombre.ToLower().Contains(busqueda.ToLower())).ToList().Count > 0)
                {
                    localizaciones = localizaciones.Where(loc => loc.Producto.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
                }
                if (DateTime.TryParse(busqueda, out DateTime result))
                {
                    localizaciones = localizaciones.Where(loc => loc.Producto.Caducidad == Convert.ToDateTime(busqueda)).ToList();
                }
                if (Int32.TryParse(busqueda, out int resultado))
                {
                    localizaciones = localizaciones.Where(loc => loc.Producto.Cantidad == Convert.ToInt32(busqueda)).ToList();
                }
                return localizaciones;
            }
            else
            {
                List<Localizacion> localizaciones = await _context.Localizacion.Include(fila => fila.Fila).Include(producto => producto.Producto).Where(borr => borr.Producto.Borrado != true).ToListAsync();
                return localizaciones;
                //await _context.Localizacion.Where(loc => loc.Producto.Borrado != true).ToListAsync();
            }
        }
    }
}












