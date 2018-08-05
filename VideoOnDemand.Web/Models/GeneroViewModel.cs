using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class GeneroViewModel
    {
        public int? GeneroId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }

        public ICollection<Media> Medias { get; set; }
    }
}