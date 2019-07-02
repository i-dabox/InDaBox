using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class Seccion
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string NombreSeccion { get; set; }
        [Required]
        public int NumeroDeColumnas { get; set; }
        [Required]
        public int NumeroDeFilas { get; set; }

        public int PasilloId { get; set; }
        public Pasillo Pasillo { get; set; }

        public List<Columna> Columnas { get; set; }
    }
}
