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
    public class MovieController : BaseController
    {
        // GET: Movie
        public ActionResult Index()
        {
            MovieRepository repository = new MovieRepository(context);
            var lst = repository.Query(X=>X.EstadosMedia>0);
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(lst);
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
            PersonaRepository personaRepository = new PersonaRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);

            var model = new MovieViewModel();
            var actores = personaRepository.Query(a => a.Status == true);
            var generos = generoRepository.Query(g => g.Activo == true);

            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);
            return View(model);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieViewModel model)
        {           
            try
            {
                MovieRepository MovieRepository = new MovieRepository(context);
                if (ModelState.IsValid)
                {
                    //Llamado al repositorio
                   
                    #region Validaciones
                    var movie = MapHelper.Map<Movie>(model);
                    movie.EstadosMedia = EEstatusMedia.VISIBLE;
                    movie.FechaRegistro = DateTime.Now;
                    MovieRepository.InsertComplete(movie, model.GenerosSeleccionados, model.ActoresSeleccionados);
                    #endregion                          
                 context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);                 }
               
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
            var personaRepository = new PersonaRepository(context);
            var generoRepository = new GeneroRepository(context);

            // Expresión lambda para incluir las relaciones (No hay carga perezosa)
            var includes = new Expression<Func<Movie, object>>[] { s => s.Actores, s => s.Generos };

            var movie = repository.QueryIncluding(x => x.MediaId == id, includes).SingleOrDefault();
            var model = MapHelper.Map<MovieViewModel>(movie);

            var actores = personaRepository.Query(a => a.Status == true);
            var generos = generoRepository.Query(g => g.Activo == true);
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
            model.ActoresSeleccionados = movie.Actores.Select(a => a.Id.Value).ToArray();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);
            model.GenerosSeleccionados = movie.Generos.Select(g => g.GeneroId.Value).ToArray();

            return View(model);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MovieViewModel model)
        {
            PersonaRepository personaRepository = new PersonaRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            try
            {
               MovieRepository repository = new MovieRepository(context);

                if (ModelState.IsValid)
                {

                    var movie = MapHelper.Map<Movie>(model);
                    repository.UpdateComplete(movie, model.ActoresSeleccionados, model.GenerosSeleccionados);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                var actores = personaRepository.Query(a => a.Status == true);
                var generos = generoRepository.Query(g => g.Activo == true);

                model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
                model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);

                return View(model);
            }
            catch 
            {
                var actores = personaRepository.Query(a => a.Status == true);
                model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
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
