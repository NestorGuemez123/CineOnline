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
    public class MovieController : BaseController
    {
        // GET: Movie
        public ActionResult Index()
        {
            var models = new List<MovieViewModel>().AsEnumerable();
            try
            {
                MovieRepository repository = new MovieRepository(context);

                //Consulte Los individuos del repositorio
                //var lst = repository.Query(x=>x.EstadosMedia==EEstatusMedia.VISIBLE);
                var lst = repository.Query(X=>X.EstadosMedia>0);
                //Mapeamos la lista de individuos con una lista de individuosViewModel
                models = MapHelper.Map<IEnumerable<MovieViewModel>>(lst);
            }
            catch (Exception ex)
            {

            }
            return View(models);//No olvidar
            
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            //var model = new GeneroViewModel();
            //GeneroRepository generoRepository = new GeneroRepository(context);
            //var lst = generoRepository.GetAll();
            //model.AvailableGeneros = MapHelper.Map<ICollection<GeneroViewModel>>(lst);
            //return View(model);
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieViewModel model)
        {
                
            try
            {
                if (ModelState.IsValid)
                {
                    //Llamado al repositorio
                    MovieRepository repository = new MovieRepository(context);
                    #region Validaciones
                    //Validar Nombre Unico
                    var MovieQry = new Movie { Nombre = model.Nombre};

                    #endregion
                    //Mapear el modelo de vista a una entidad topic
                    Movie movie = MapHelper.Map<Movie>(model);
                    movie.EstadosMedia = EEstatusMedia.VISIBLE;
                    
                    repository.Insert(movie);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {

            var repository = new MovieRepository(context);
                     
            var movie = repository.Query(x => x.MediaId == id).First();
            var model = MapHelper.Map<MovieViewModel>(movie);
            //Consultando topics ordenados por name

            return View(model);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MovieViewModel model)
        {
            //var topicRepository = new TopicRepository(context);
            try
            {
                var repository = new MovieRepository(context);

                if (ModelState.IsValid)
                {

                    var movie = MapHelper.Map<Movie>(model);
                    repository.Update(movie);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

              
                return View(model);
            }
            catch (Exception ex)
            {
                
                return View(model);

            }
        }
        
        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            var movie = repository.Query(t => t.MediaId == id).First();
            var model = MapHelper.Map<MovieViewModel>(movie);//El objeto de entrada por uno de salida y se lo paso al modelo
            return View(model);//No lo olvides! regresalo a la vista
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, MovieViewModel model)
        {
            try
            {
                MovieRepository repository = new MovieRepository(context);
                var movie = repository.Query(t => t.MediaId == id).First();//Agrege esta linea
                //Mapear el modelo de vista a una entidad topic
                //Movie movies = MapHelper.Map<Movie>(model);
                movie.EstadosMedia = EEstatusMedia.ELIMINADO;
                repository.Update(movie);
                context.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
