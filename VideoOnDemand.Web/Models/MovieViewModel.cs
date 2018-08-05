using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class MovieViewModel 
    {
        public int? MediaId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int? DuracionMin { get; set; }
        public DateTime? FechaRegistro { get; set; }
        [Required]
        [DisplayName("Fecha de Lanzamiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaLanzamiento { get; set; }
        public EEstatusMedia? EstadosMedia { get; set; }

        public ICollection<Genero> Generos { get; set; }
        public ICollection<Persona> Actores { get; set; }
        public ICollection<Opinion> Opiniones { get; set; }
    }
}