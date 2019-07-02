using InDaBox.Controllers;
using InDaBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class AlmacenVm
    {
        public Almacen Almacenes { get; set; }
        public Pasillo Pasillos { get; set; }
        public Seccion Secciones { get; set; }
        public Columna Columnas { get; set; }
        public Fila Filas { get; set; }
    }
}
