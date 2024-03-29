﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InDaBox.Models
{
    public class Fila
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }

        public int ColumnaId { get; set; }
        public Columna Columna { get; set; }
    }
}
