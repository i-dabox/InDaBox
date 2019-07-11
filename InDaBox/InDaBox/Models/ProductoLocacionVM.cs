using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class ProductoLocacionVM
    {
        public List< Producto> Productos { get; set; }
        public List<Localizacion>Localizaciones { get; set; }
    }
}
