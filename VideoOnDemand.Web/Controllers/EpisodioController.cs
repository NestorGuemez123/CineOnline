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
    public class EpisodioController : BaseController
    {

        // GET: Episodio
        public ActionResult Index()
        {
            EpisodioRepository repository = new EpisodioRepository(context);
            var lst = repository.Query(e => e.EstadosMedia == EEstatusMedia.VISIBLE || e.EstadosMedia == EEstatusMedia.INVISIBLE);

            var models = MapHelper.Map<IEnumerable<EpisodioViewModel>>(lst);

            return View(models);
        }

        // GET: Episodio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Episodio/Create
        public ActionResult Create(int serieId)
        {
            var model = new NuevoEpisodioViewModel();
            model.SerieId = serieId;
            return View(model);
        }

        // POST: Episodio/Create
        [HttpPost]
        public ActionResult Create(NuevoEpisodioViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                { 
                    EpisodioRepository episodioRepository = new EpisodioRepository(context);
                    var episodio = MapHelper.Map<Episodio>(model);
                    episodio.EstadosMedia = EEstatusMedia.VISIBLE;
                    episodio.FechaRegistro = DateTime.Now;

                    episodioRepository.Insert(episodio);

                    context.SaveChanges();

                    return RedirectToActionPermanent("Details", "Serie", new { id = episodio.SerieId });

                }
                else
                {
                    return View(model);
                }

            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        // GET: Episodio/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new EpisodioRepository(context);
            var episodio = repository.Find(id);
            var model = MapHelper.Map<ModificadoEpisodioViewModel>(episodio);

            return View(model);
        }

        // POST: Episodio/Edit/5
        [HttpPost]
        public ActionResult Edit(ModificadoEpisodioViewModel model)
        {
            try
            {
                EpisodioRepository repository = new EpisodioRepository(context);
                var episodio = MapHelper.Map<Episodio>(model);
                repository.Update(episodio);
                context.SaveChanges();

                return RedirectToAction("Details", "Serie", new { id = episodio.SerieId});
            }
            catch
            {
                return View();
            }
        }

        // GET: Episodio/Delete/5
        public ActionResult Delete(int id)
        {
            var repository = new EpisodioRepository(context);
            var episodio = repository.Find(id);
            var model = MapHelper.Map<ModificadoEpisodioViewModel>(episodio);

            return View(model);
        }

        // POST: Episodio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int serieId)
        {
            EpisodioRepository repository = new EpisodioRepository(context);
            try
                {
                var episodio = repository.Find(id);
                episodio.EstadosMedia = EEstatusMedia.ELIMINADO;
                repository.Update(episodio);
                context.SaveChanges();

                return RedirectToAction("Details", "Serie", new { id = serieId});
                }
              
            catch
                {
                    var episodio = repository.Find(id);
                    var model = MapHelper.Map<ModificadoEpisodioViewModel>(episodio);
                    return View(model);
                }
        }
    }
}
