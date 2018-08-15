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
            ViewBag.ListaGeneros = generoRepository.Query(x=>x.Activo==true).Select(g => g.Nombre).Where(g => g != genero).ToList();

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
            PersonaRepository PersonaRepository = new PersonaRepository(context);
            var includes = new Expression<Func<Movie, object>>[] { s => s.Actores, s => s.Generos };

            var movie = repository.QueryIncluding(x => x.MediaId == id, includes).SingleOrDefault();
            var model = MapHelper.Map<MovieViewModel>(movie);

            var generos = GeneroRepository.Query(g => g.Activo == true);
            var actores = PersonaRepository.Query(a => a.Status == true);

            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);
            model.GenerosSeleccionados = movie.Generos.Select(g => g.GeneroId.Value).ToArray();
            model.ActoresDisponibles = MapHelper.Map <ICollection<PersonaViewModel>>(actores);
            model.ActoresSeleccionados = movie.Actores.Select(a => a.Id.Value).ToArray();


            return View(model);            
        }

        public ActionResult Reproductor()
        {
            return View();
        }
    }
}