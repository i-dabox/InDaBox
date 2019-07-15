using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class FiltrosComunes
    {
        public int Id { get; set; }
        public string Filtro { get; set; }
        public long Contador { get; set; }
        public DateTime FechaDeInsercion { get; set; }
    }
}
