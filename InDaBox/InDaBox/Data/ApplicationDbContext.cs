using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InDaBox.Models;

namespace InDaBox.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<InDaBox.Models.Almacen> Almacen { get; set; }
        public DbSet<InDaBox.Models.Pasillo> Pasillo { get; set; }
        public DbSet<InDaBox.Models.Seccion> Seccion { get; set; }
        public DbSet<InDaBox.Models.Columna> Columna { get; set; }
        public DbSet<InDaBox.Models.Fila> Fila { get; set; }
        public DbSet<InDaBox.Models.Producto> Producto { get; set; }
        public DbSet<InDaBox.Models.Localizacion> Localizacion { get; set; }
        public DbSet<InDaBox.Models.FiltrosComunes> FiltrosComunes { get; set; }
    }
}
