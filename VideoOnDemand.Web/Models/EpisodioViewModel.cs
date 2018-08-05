using System;
using System.Collections.Generic;
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
        public EEstatusMedia? EstadosMedia { get; set; }
        public int? Temporada { get; set; }
    }

    public class NuevoEpisodioViewModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }
        public int? Temporada { get; set; }
        public int? SerieId { get; set; }


    }

    public class ModificadoEpisodioViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaRegistro { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }
        public int? Temporada { get; set; }
        public int? SerieId { get; set; }
        public EEstatusMedia? EstadosMedia { get; set; }

    }

}