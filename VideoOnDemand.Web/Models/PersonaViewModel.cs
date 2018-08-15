using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class PersonaViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(25)]
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [MaxLength(500)]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNacimiento { get; set; }

        [DisplayName("Estado para los usuarios")]
        public bool? Status { get; set; }
    }
}