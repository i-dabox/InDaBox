using InDaBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Services
{
    public interface IFiltrosComunes
    {
        void AnadirNuevoFiltro(string Busqueda);
        Task<List<FiltrosComunes>> RecogerFiltrosComunes();
        Task ReducirContadores();
    }
}
