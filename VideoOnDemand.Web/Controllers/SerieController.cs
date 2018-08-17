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
            ViewBag.ListaGeneros = generoRepository.Query(g => g.Activo == true).OrderBy(g => g.Nombre).Select(g => g.Nombre).Where( g=> g != genero).ToList();

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
            FavoritoRepository favoritoRepository = new FavoritoRepository(context);
            EpisodioRepository episodioRepository = new EpisodioRepository(context);
            #endregion

            #region Consulta la serie en la bd
            var relaciones = new Expression<Func<Serie, object>>[] { s => s.Generos, s => s.Actores};
            Serie serie = serieRepository.QueryIncluding(s => s.MediaId == id, relaciones, "FechaRegistro").SingleOrDefault();


            #endregion

            #region Mapeo de la serie con su view model adecuado
            var model = MapHelper.Map<CompletoSerieViewModel>(serie);

            bool enFav = favoritoRepository.Query(x => x.mediaId == id).Count() > 0;
            if (enFav == true)
            {
                var TodoFav = favoritoRepository.Query(x => x.mediaId == id).First();
                var idFav = TodoFav.id;
                model.IdFavorito = idFav;
            }
            model.MiFavorito = enFav;
            model.Temporadas = episodioRepository.Query(e => e.SerieId == model.MediaId && e.EstadosMedia == EEstatusMedia.VISIBLE).OrderBy(e => e.Temporada).Select(e => e.Temporada.Value).Distinct().ToArray();
            model.esMovie = true;
            #endregion

            return View(model);
        }
    }
}
