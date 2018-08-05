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
            var serieModel = MapHelper.Map<SerieViewModel>(serie);

            var episodios = episodioRepository.Query(e => e.SerieId == id).Where(e => e.EstadosMedia == EEstatusMedia.VISIBLE || e.EstadosMedia == EEstatusMedia.INVISIBLE);
            var episodiosModel = MapHelper.Map<ICollection<EpisodioViewModel>>(episodios);

            serieModel.Episodios = episodiosModel;

            return View(serieModel);
        }

        // GET: ManageSerie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageSerie/Create
        [HttpPost]
        public ActionResult Create(SerieViewModel model)
        {
            try
            {
                SerieRepository serieRepository = new SerieRepository(context);

                if (ModelState.IsValid)
                {
                    var serie = MapHelper.Map<Serie>(model);
                    serie.EstadosMedia = EEstatusMedia.VISIBLE;
                    serie.FechaRegistro = DateTime.Now;
                    serieRepository.Insert(serie);
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
            var serie = repository.Query(s => s.MediaId == id).First();
            var model = MapHelper.Map<SerieViewModel>(serie);

            return View(model);
        }

        // POST: ManageSerie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SerieViewModel model)
        {
            try
            {
                SerieRepository repository = new SerieRepository(context);
                var serie = MapHelper.Map<Serie>(model);
                repository.Update(serie);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageSerie/Delete/5
        public ActionResult Delete(int id)
        {
            var repository = new SerieRepository(context);
            var serie = repository.Query(s => s.MediaId == id).First();
            var model = MapHelper.Map<SerieViewModel>(serie);

            return View(model);
        }

        // POST: ManageSerie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SerieViewModel model)
        {
            try
            {
                SerieRepository repository = new SerieRepository(context);
                var serie = repository.Query(s => s.MediaId == id).First();
                serie.EstadosMedia = EEstatusMedia.ELIMINADO;
                repository.Update(serie);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            catch
            {
                return View(model);
            }
        }
    }
}