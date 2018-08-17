using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class EpisodioViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Estado para los usuarios")]
        public EEstatusMedia? EstadosMedia { get; set; }
        public int? Temporada { get; set; }
    }

    public class NuevoEpisodioViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La dduración es requerida")]
        [DisplayName("Duración (min)")]
        public int? DuracionMin { get; set; }
        [Required(ErrorMessage = "La fecha de lanzamiento es requerido")]
        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }
        [Required(ErrorMessage = "La temporada es requerida")]
        public int? Temporada { get; set; }
        public int? SerieId { get; set; }
    }

    public class ModificadoEpisodioViewModel
    {
        public int? MediaId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La dduración es requerida")]
        [DisplayName("Duración (min)")]
        public int? DuracionMin { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "La fecha de registro es requerido")]
        [DisplayName("Fecha de registro")]
        public DateTime? FechaRegistro { get; set; }
        [Required(ErrorMessage = "La fecha de lanzamiento es requerido")]
        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        [Required(ErrorMessage = "La temporada es requerida")]
        public int? Temporada { get; set; }
        public int? SerieId { get; set; }

        [DisplayName("Estado para el usuario")]
        public EEstatusMedia? EstadosMedia { get; set; }

    }

    /*--------------- Para el usuario --------------*/
    public class EpisodioCarrucelViewModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
    }
}