using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
    }
}
