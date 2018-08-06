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
    public class ManageSerieController : BaseController
    {
        // GET: ManageSerie
        public ActionResult Index()
        {
            SerieRepository repository = new SerieRepository(context);
            var lst = repository.Query(s => s.EstadosMedia == EEstatusMedia.VISIBLE | s.EstadosMedia == EEstatusMedia.INVISIBLE);
            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);

            return View(models);
        }


        // GET: ManageSerie/Create
        public ActionResult Create()
        {
            PersonaRepository personaRepository = new PersonaRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);

            var model = new NuevoSerieViewModel();
            var actores = personaRepository.Query(a => a.Status == true);
            var generos = generoRepository.Query(g => g.Activo == true);

            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);

            return View(model);
        }

        // POST: ManageSerie/Create
        [HttpPost]
        public ActionResult Create(NuevoSerieViewModel model)
        {
            try
            {
                SerieRepository serieRepository = new SerieRepository(context);

                if (ModelState.IsValid)
                {
                    var serie = MapHelper.Map<Serie>(model);
                    serie.EstadosMedia = EEstatusMedia.VISIBLE;
                    serie.FechaRegistro = DateTime.Now;
                    serieRepository.InsertComplete(serie, model.ActoresSeleccionados, model.GenerosSeleccionados);

                    // Guardar y registrar cambios
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        // GET: ManageSerie/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new SerieRepository(context);
            var personaRepository = new PersonaRepository(context);
            var generoRepository = new GeneroRepository(context);

            // Expresión lambda para incluir las relaciones (No hay carga perezosa)
            var includes = new Expression<Func<Serie, object>>[] { s => s.Actores, s => s.Generos };

            var serie = repository.QueryIncluding(s => s.MediaId == id, includes).SingleOrDefault();
            var model = MapHelper.Map<ModificadoSerieViewModel>(serie);

            var actores = personaRepository.Query(a => a.Status == true);
            var generos = generoRepository.Query(g => g.Activo == true);
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
            model.ActoresSeleccionados = serie.Actores.Select(a => a.Id.Value).ToArray();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(generos);
            model.GenerosSeleccionados = serie.Generos.Select(g => g.GeneroId.Value).ToArray();

            return View(model);
        }

        // POST: ManageSerie/Edit/5
        [HttpPost]
        public ActionResult Edit(ModificadoSerieViewModel model)
        {
            PersonaRepository personaRepository = new PersonaRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);

            try
            {
                SerieRepository repository = new SerieRepository(context);

                if (ModelState.IsValid)
                {
                    var serie = MapHelper.Map<Serie>(model);
                    repository.UpdateComplete(serie, model.ActoresSeleccionados, model.GenerosSeleccionados);
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

        // GET: ManageSerie/Delete/5
        public ActionResult Delete(int id)
        {
            var repository = new SerieRepository(context);

            var includes = new Expression<Func<Serie, object>>[] { s => s.Actores, s => s.Generos };
            var serie = repository.QueryIncluding(s => s.MediaId == id, includes).SingleOrDefault();
            var model = MapHelper.Map<EliminadoSerieViewModel>(serie);

            return View(model);
        }

        // POST: ManageSerie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EliminadoSerieViewModel model)
        {
            SerieRepository repository = new SerieRepository(context);

            try
            {
                var serie = repository.Find(id);
                serie.EstadosMedia = EEstatusMedia.ELIMINADO;
                repository.Update(serie);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            catch
            {
                var modeloNuevo = repository.Find(id);
                var includes = new Expression<Func<Serie, object>>[] { s => s.Actores, s => s.Generos };
                var serie = repository.QueryIncluding(s => s.MediaId == id, includes).SingleOrDefault();
                model = MapHelper.Map<EliminadoSerieViewModel>(serie);

                return View(model);
            }
        }
    }
}
