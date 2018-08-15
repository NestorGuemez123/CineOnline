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

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [DisplayName("Decripción")]
        [MaxLength(500, ErrorMessage ="Longitud máxima de Caracteres es 500")]
        public string Descripcion { get; set; }
    
        [DisplayName("Duración (min)")]
        [Required(ErrorMessage="La duración es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor ingrese un valor válido mayor de 1")]
        public int DuracionMin { get; set; }

        [DisplayName("Fecha de registro")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }

        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La Fecha es requerida")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        [DisplayName("Estado para los usuarios")]
        public EEstatusMedia? EstadosMedia { get; set; }

        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] GenerosSeleccionados { get; set; }

        public bool MiFavorito { get; set; }
        public int? IdFavorito { get; set; }
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        public int[] ActoresSeleccionados { get; set; }

        public ICollection<Genero> Generos { get; set; }
        public ICollection<Persona> Actores { get; set; }
        public ICollection<Opinion> Opiniones { get; set; }
    }

    public class ThumbnailMovieViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }
        public int DuracionMin { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaLanzamiento { get; set; }
        //La agregue porque la necesitaba mi vista v:
        public EEstatusMedia? EstadosMedia { get; set; }

        // Mantiene la lista de generos
        public ICollection<GeneroViewModel> Generos { get; set; }
    }
}