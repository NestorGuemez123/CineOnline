using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class OpinionViewModel
    {
        public int? OpinionId { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario usuario { get; set; }
        public int? MediaId { get; set; }
        public Media media { get; set; }
        public int? Puntuacion { get; set; }

        [MaxLength(200, ErrorMessage = "La reseña no puede ser mayor a 200 caracteres")]
        public string Descripcion { get; set; }

    }
}