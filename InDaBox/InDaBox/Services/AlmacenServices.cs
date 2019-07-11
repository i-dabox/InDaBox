using InDaBox.Data;
using InDaBox.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Services
{
    public class AlmacenServices : IAlmacen
    {
        private readonly ApplicationDbContext _context;

        public AlmacenServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task VaciarHijosAlmacen(Almacen almacen)
        {
            List<Pasillo> pasillos = await _context.Pasillo.Where(alm => alm.Almacen.Id == almacen.Id).ToListAsync();
            foreach (Pasillo pasillo in pasillos)
            {
                _context.Remove(pasillo);

            }
                _context.SaveChanges();

        }
    }
}
