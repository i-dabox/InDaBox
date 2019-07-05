using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class Almacen
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(30)]
        public string Direccion { get; set; }
        [Required]
        [MaxLength(5)]
        public string CodigoPostal { get; set; }
        [Required]
        [MaxLength(20)]
        public string Poblacion { get; set; }

        public List<Pasillo> Pasillos { get; set; }
    }
}
