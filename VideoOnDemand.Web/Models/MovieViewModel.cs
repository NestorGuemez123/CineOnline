using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class MovieViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaLanzamiento { get; set; }
    }
}