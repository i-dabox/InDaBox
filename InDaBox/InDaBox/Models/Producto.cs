using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }
        [MaxLength(1000)]
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public DateTime? Caducidad { get; set; }
        public int Cantidad { get; set; }
        public bool Borrado { get; set; }

        
    }
}
