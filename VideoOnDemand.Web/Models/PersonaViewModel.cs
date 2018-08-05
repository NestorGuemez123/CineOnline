using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class PersonaViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Descripcion { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public bool? Status { get; set; }
    }
}