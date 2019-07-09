using InDaBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Services
{
   public interface ILocalizacion
    {
        Task<List<Localizacion>> BusquedaLocalizacion(string busqueda);
    }
}
