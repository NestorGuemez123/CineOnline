using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class MediaViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaLanzamiento { get; set; }

        public ICollection<Genero> Generos { get; set; }
        public ICollection<Persona> Actores { get; set; }
        public ICollection<Opinion> Opiniones { get; set; }
        public EEstatusMedia? EstadosMedia { get; set; }
    }
}