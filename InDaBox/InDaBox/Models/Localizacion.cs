using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class Localizacion
    {
        public int Id { get; set; }

        //public int AlmacenId { get; set; }
        //public Almacen Almacen { get; set; }
        //public  int PasilloId { get; set; }
        //public Pasillo Pasillo { get; set; }
        //public int SeccionId { get; set; }
        //public Seccion Seccion { get; set; }
        //public int ColumnaId { get; set; }
        //public Columna Columna { get; set; }
        public int FilaId { get; set; }
        public Fila Fila { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}

