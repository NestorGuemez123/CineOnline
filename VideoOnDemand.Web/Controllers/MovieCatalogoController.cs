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
        // GET: MovieCatalogo
        public ActionResult Index(int page = 1, string busqueda = null, string genero=null, int pageSize = 3)
        {
            MovieRepository movieRepository = new MovieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var includes = new Expression<Func<Movie, object>>[] { s => s.Generos };
            int totalDePaginas;
            int totalDeFilas;
            ICollection<Movie> movies;

            movies = movieRepository.QueryPageByNombreAndGeneroIncluding(busqueda, genero, includes, out totalDePaginas, out totalDeFilas, "Nombre", page - 1, pageSize);

            ViewBag.Busqueda = busqueda;
            ViewBag.Genero = genero;
            ViewBag.ListaGeneros = generoRepository.GetAll().Select(g => g.Nombre).Where(g => g != genero).ToList();

            var paginador = new PaginatorViewModel<ThumbnailMovieViewModel>();
            paginador.Page = page;
            paginador.PageSize = pageSize;
            paginador.Results = MapHelper.Map<ICollection<ThumbnailMovieViewModel>>(movies);
            paginador.TotalPages = totalDePaginas;
            paginador.TotalRows = totalDeFilas;
            
            return View(paginador);
        }

        // GET: Genero/Details/5
        public ActionResult Details(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            GeneroRepository GeneroRepository = new GeneroRepository(context);

            var movie = repository.Query(t => t.MediaId == id).First();
            var model = MapHelper.Map<MovieViewModel>(movie);

            var generos = GeneroRepository.Query(g => g.Activo == true);

            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);
            model.GenerosSeleccionados = movie.Generos.Select(g => g.GeneroId.Value).ToArray();
            
            return View(model);            
        }
        public ActionResult Reproductor()
        {
            return View();
        }
    }
}