using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class Columna
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }

        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }

        public List<Fila> Filas { get; set; }
    }
}
