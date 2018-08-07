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
        public string Descripcion { get; set; }

        [DisplayName("Duración (min)")]
        public int? DuracionMin { get; set; }

        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }
        public int? Temporada { get; set; }
        public int? SerieId { get; set; }
    }

    public class ModificadoEpisodioViewModel
    {
        public int? MediaId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [DisplayName("Duración (min)")]
        public int? DuracionMin { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }

        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        public int? Temporada { get; set; }
        public int? SerieId { get; set; }

        [DisplayName("Estado para el usuario")]
        public EEstatusMedia? EstadosMedia { get; set; }

    }

}