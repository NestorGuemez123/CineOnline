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
    public class MovieCatalogoController : BaseController
    {
        //public SelectList GeneroList(object selectedItem = null)
        //{
        //    var repository = new GeneroRepository(context);
        //    var genero = repository.Query(null, "Nombre").ToList();
        //    genero.Insert(0, new Genero { GeneroId = null, Nombre = "seleccione" });
        //    return new SelectList(genero, "GeneroId", "Nombre", selectedItem);
        //}

        // GET: MovieCatalogo
        public ActionResult Index(int page = 1, string busqueda = null, int pageSize = 3)
        {
            MovieRepository movieRepository = new MovieRepository(context);
            var includes = new Expression<Func<Movie, object>>[] { s => s.Generos };
            int totalDePaginas;
            int totalDeFilas;
            ICollection<Movie> movies;

            if (String.IsNullOrEmpty(busqueda))
            {
                var MovieBuscada = new Movie { Nombre = busqueda, EstadosMedia = EEstatusMedia.VISIBLE };
                movies = movieRepository.QueryPageByExampleIncluding(MovieBuscada, includes, out totalDePaginas, out totalDeFilas, "Nombre", page - 1, pageSize);
            }
            else
            {
                var MovieBuscada = new Movie { Nombre = busqueda, EstadosMedia = EEstatusMedia.VISIBLE };
                movies = movieRepository.QueryPageIncluding(null, includes, out totalDePaginas, out totalDeFilas, "Nombre", page - 1, pageSize);
            }
            var paginador = new PaginatorViewModel<ThumbnailSerieViewModel>();
            paginador.Page = page;
            paginador.PageSize = pageSize;
            paginador.Results = MapHelper.Map<ICollection<ThumbnailSerieViewModel>>(movies);
            paginador.TotalPages = totalDePaginas;
            paginador.TotalRows = totalDeFilas;
            
            return View(paginador);
        }

        // GET: Genero/Details/5
        public ActionResult Details(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            var movie = repository.Query(t => t.MediaId == id).First();
            var model = MapHelper.Map<MovieViewModel>(movie);


            return View(model);            
        }
    }
}