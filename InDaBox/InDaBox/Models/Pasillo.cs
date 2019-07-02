using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class Pasillo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }
        [Required]
        public int NumeroDeSecciones { get; set; }

        public int AlmacenId { get; set; }
        public Almacen Almacen { get; set; }

        public List<Seccion> Secciones { get; set; }
    }
}
