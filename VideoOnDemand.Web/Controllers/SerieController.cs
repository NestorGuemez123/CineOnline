using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class SerieController : BaseController
    {
        // GET: Serie
        public ActionResult Index(int page = 1, string busqueda = null, string genero = null, int pageSize = 3)
        {
            SerieRepository serieRepository = new SerieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);

            var includes = new Expression<Func<Serie, object>>[] { s => s.Generos };
            
            int totalDePaginas;
            int totalDeFilas;

            ICollection<Serie> series;


            series = serieRepository.QueryPageByNombreAndGeneroIncluding(busqueda, genero, includes, out totalDePaginas, out totalDeFilas, "Nombre", page - 1, pageSize);
            ViewBag.Busqueda = busqueda;
            ViewBag.Genero = genero;
            ViewBag.ListaGeneros = generoRepository.Query(g=>g.Activo==true).Select(g => g.Nombre).Where( g=> g != genero).ToList();

            var paginador = new PaginatorViewModel<ThumbnailSerieViewModel>();
            paginador.Page = page;
            paginador.PageSize = pageSize;
            paginador.Results = MapHelper.Map<ICollection<ThumbnailSerieViewModel>>(series);
            paginador.TotalPages = totalDePaginas;
            paginador.TotalRows = totalDeFilas;

            return View(paginador);
        }

        // GET: Serie/Details/5
        public ActionResult Details(int id)
        {
            #region Repositorios necesarios
            SerieRepository serieRepository = new SerieRepository(context);
            #endregion

            #region Consulta la serie en la bd
            var relaciones = new Expression<Func<Serie, object>>[] { s => s.Generos, s => s.Actores, s => s.Opiniones.Select(o => o.Usuario) };
            Serie serie = serieRepository.QueryIncluding(s => s.MediaId == id, relaciones, "FechaRegistro").SingleOrDefault();
            
            #endregion

            #region Mapeo de la serie con su view model adecuado
            var model = MapHelper.Map<CompletoSerieViewModel>(serie);
            #endregion

            return View(model);
        }
    }
}
