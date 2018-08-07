using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class OpinionViewModel
    {
        public int? OpinionId { get; set; }
        public int? Puntuacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public int? UsuarioId { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public int? MediaId { get; set; }
        public MediaViewModel Media { get; set; }
    }
}