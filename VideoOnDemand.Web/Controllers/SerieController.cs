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
        // GET: ManageSerie
        public ActionResult Index()
        {
            SerieRepository repository = new SerieRepository(context);
            var lst = repository.Query(s => s.EstadosMedia == EEstatusMedia.VISIBLE | s.EstadosMedia == EEstatusMedia.INVISIBLE);
            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);

            return View(models);
        }

        // GET: ManageSerie/Details/5
        public ActionResult Details(int id)
        {
            var serieRepository = new SerieRepository(context);
            var episodioRepository = new EpisodioRepository(context);

            var serie = serieRepository.Find(id);
            var serieModel = MapHelper.Map<DetalladoSerieViewModel>(serie);

            var episodios = episodioRepository.Query(e => e.SerieId == id).Where(e => e.EstadosMedia == EEstatusMedia.VISIBLE || e.EstadosMedia == EEstatusMedia.INVISIBLE);
            var episodiosModel = MapHelper.Map<ICollection<EpisodioViewModel>>(episodios);

            serieModel.Episodios = episodiosModel;

            return View(serieModel);
        }

        // GET: ManageSerie/Create
        public ActionResult Create()
        {
            PersonaRepository personaRepository = new PersonaRepository(context);
            var model = new NuevoSerieViewModel();
            var actores = personaRepository.Query( a => a.Status == true);
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);

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
                    serieRepository.InsertComplete(serie, model.ActoresSeleccionados);

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

            // Expresión lambda para incluir las relaciones (No hay carga perezosa)
            var includes = new Expression<Func<Serie, object>>[] { s => s.Actores };
            var serie = repository.QueryIncluding(s => s.MediaId == id, includes).SingleOrDefault();
            var model = MapHelper.Map<ModificadoSerieViewModel>(serie);

            var actores = personaRepository.Query( a => a.Status == true);
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
            model.ActoresSeleccionados = serie.Actores.Select(a => a.Id.Value).ToArray();

            return View(model);
        }

        // POST: ManageSerie/Edit/5
        [HttpPost]
        public ActionResult Edit(ModificadoSerieViewModel model)
        {
            PersonaRepository personaRepository = new PersonaRepository(context);
            try
            {
                SerieRepository repository = new SerieRepository(context);

                if (ModelState.IsValid)
                {
                    var serie = MapHelper.Map<Serie>(model);
                    repository.UpdateComplete(serie, model.ActoresSeleccionados);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                var actores = personaRepository.Query(a => a.Status == true);
                model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actores);
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

            var includes = new Expression<Func<Serie, object>>[] { s => s.Actores };
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
                var includes = new Expression<Func<Serie, object>>[] { s => s.Actores };
                var serie = repository.QueryIncluding(s => s.MediaId == id, includes).SingleOrDefault();
                model = MapHelper.Map<EliminadoSerieViewModel>(serie);

                return View(model);
            }
        }
    }
}