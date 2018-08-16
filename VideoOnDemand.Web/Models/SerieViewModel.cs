using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    /*-------------View models usados para el administrador--------------*/

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
        [MaxLength(100,ErrorMessage = "El nombre debe ser menor o igual a 100 caracteres")]
        public string Nombre { get; set; }

        [MaxLength(500,ErrorMessage ="La descripción debe ser menor o igual a 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de lanzamiento es requerida")]
        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        // Mantiene la lista de generos completa
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        // Mantiene los generos que seleccione el administrador

        [Required]
        public int[] GenerosSeleccionados { get; set; }
         
        // Mantiene la lista de actores completa
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        // Mantiene los generos que seleccione el administrador

        [Required]
        public int[] ActoresSeleccionados { get; set; }

    }

    public class ModificadoSerieViewModel
    {
        public int? MediaId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre debe ser menor o igual a 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [MaxLength(500, ErrorMessage = "La descripción debe ser menor o igual a 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duración es requerida")]
        [DisplayName("Duracion (min)")]
        public int? DuracionMin { get; set; }
       
        public DateTime? FechaRegistro { get; set; }

        [Required(ErrorMessage = "La fecha de lanzamiento es requerida")]
        [DisplayName("Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaLanzamiento { get; set; }

        [Required(ErrorMessage = "El estado para los usuarios es requerido")]
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
    
    /*-------------View models usados para el usuario--------------*/

    //El view model que se usa para el usuario en la vista Index del SerieController
    //Muestra los datos basicos de una serie
    public class ThumbnailSerieViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }
        public int DuracionMin { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaLanzamiento { get; set; }
        
        // Mantiene la lista de generos
        public ICollection<GeneroViewModel> Generos { get; set; }
    }


    /// <summary>
    /// Representa una serie detallada con datos generales, actores, generos y opiniones.
    /// Se usa para el usuario y corresponde a la vista Details del SerieController.
    /// </summary>
    public class CompletoSerieViewModel
    {
        public int? MediaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public DateTime? FechaLanzamiento { get; set; }

        // Mantiene la lista de generos
        public ICollection<GeneroViewModel> Generos { get; set; }

        // Mantiene la lista de actores
        public ICollection<PersonaViewModel> Actores { get; set; }

        // Mantiene datos sobre si la pelicula pertenece a favoritos
        public bool MiFavorito { get; set; }
        public int? IdFavorito { get; set; }
        public bool esMovie { get; set; }

    }
}