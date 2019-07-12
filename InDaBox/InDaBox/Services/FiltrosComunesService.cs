using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InDaBox.Data;
using InDaBox.Models;
using Microsoft.EntityFrameworkCore;

namespace InDaBox.Services
{
    public class FiltrosComunesService : IFiltrosComunes
    {
        private readonly ApplicationDbContext _context;

        public FiltrosComunesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AnadirNuevoFiltro(string Busqueda)
        {
            var estaBusqueda = _context.FiltrosComunes.Where(fil => fil.Filtro.Contains(Busqueda));
            if (estaBusqueda != null)
            {
                FiltrosComunes filtroComun = new FiltrosComunes()
                {
                    Filtro = Busqueda,
                    Contador = 1,
                    FechaDeInsercion = DateTime.Now
                };
                _context.Update(filtroComun);
                _context.SaveChanges();

            }
           
        }

        public async Task<List<FiltrosComunes>> RecogerFiltrosComunes()
        {
            //Todo mejor solo con los 10 primeros con contador mas alto
            return await _context.FiltrosComunes.ToListAsync();
        }

        public Task ReducirContadores()
        {
            throw new NotImplementedException();
        }
    }
}
