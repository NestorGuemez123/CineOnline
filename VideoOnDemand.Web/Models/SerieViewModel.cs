using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class SerieViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        [DisplayName("Estado para los usuarios")]
        public EEstatusMedia? EstadosMedia { get; set; }
    }

    public class DetalladoSerieViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }

        public ICollection<EpisodioViewModel> Episodios { get; set; }
    }

    public class NuevoSerieViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [DisplayName("Duracion (min)")]
        public int? DuracionMin { get; set; }

        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        // Mantiene la lista de generos completa
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        // Mantiene los generos que seleccione el administrador
        public int[] GenerosSeleccionados { get; set; }

        // Mantiene la lista de actores completa
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        // Mantiene los generos que seleccione el administrador
        public int[] ActoresSeleccionados { get; set; }

    }

    public class ModificadoSerieViewModel
    {
        public int? MediaId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [DisplayName("Duracion (min)")]
        public int? DuracionMin { get; set; }
       
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }

        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        [DisplayName("Estado para los usuarios")]
        public EEstatusMedia? EstadosMedia { get; set; }

        // Mantiene la lista de generos completa
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }

        // Mantiene los generos que seleccione el administrador
        public int[] GenerosSeleccionados { get; set; }

        // Mantiene la lista de actores completa
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }

        // Mantiene los generos que seleccione el administrador
        public int[] ActoresSeleccionados { get; set; }
    }

    public class EliminadoSerieViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [DisplayName("Duracion (min)")]
        public int? DuracionMin { get; set; }

        [DisplayName("Fecha de registro")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }

        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        [DisplayName("Estado para los usuarios")]
        public EEstatusMedia? EstadosMedia { get; set; }

        // Mantiene la lista de generos
        public ICollection<GeneroViewModel> Generos { get; set; }

        // Mantiene la lista de actores
        public ICollection<PersonaViewModel> Actores { get; set; }
    }
    
    public class ThumbnailSerieViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public DateTime? FechaLanzamiento { get; set; }

        // Mantiene la lista de generos
        public ICollection<GeneroViewModel> Generos { get; set; }
    }

}