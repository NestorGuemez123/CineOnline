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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        public EEstatusMedia? EstadosMedia { get; set; }

        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] GenerosSeleccionados { get; set; }


        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        public int[] ActoresSeleccionados { get; set; }

        public ICollection<Genero> Generos { get; set; }
        public ICollection<Persona> Actores { get; set; }
        public ICollection<Opinion> Opiniones { get; set; }
    }
}