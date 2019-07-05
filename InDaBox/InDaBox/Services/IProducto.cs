using InDaBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Services
{
    public interface IProducto
    {
         void BorradoProducto(Producto producto);
         Task<List<Producto>> BusquedaProducto(string busqueda);
        Task<List<Producto>> ProductoCaducado();
    }
}
