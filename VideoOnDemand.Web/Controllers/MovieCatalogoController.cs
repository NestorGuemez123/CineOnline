using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index(string Search)
        {
            MovieRepository repository = new MovieRepository(context);
            Movie movie = new Movie();
            movie.Nombre = Search;

            ICollection<Movie> list = null;

            if (!String.IsNullOrEmpty(Search))
            {
                list = repository.QueryByExample(movie);

            }
            else
            {

                list = repository.GetAll().ToList();
            }
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(list); //Se agrega esto
            var MovieQry = models.Where(m => m.EstadosMedia.Equals(EEstatusMedia.VISIBLE));

            return View(MovieQry);
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