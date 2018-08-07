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
    public class FavoritoController :BaseController
    {
        // GET: Favorito
        public ActionResult Index()
        {
            FavoritoRepository repository = new FavoritoRepository(context);
            var lst = repository.GetAll();
            var models = MapHelper.Map<List<FavoritoViewModel>>(lst);
            return View(models);
        }

        // GET: Favorito/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Favorito/Create
        public ActionResult Create(int id)
        {
            FavoritoRepository repository = new FavoritoRepository(context);
            var persona = repository.Query(t => t.mediaId == id).First();
            var models = MapHelper.Map<FavoritoViewModel>(persona);
            return View(models);
        }

        // POST: Favorito/Create
        [HttpPost]
        public ActionResult Create(FavoritoViewModel model)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Favorito/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Favorito/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Favorito/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Favorito/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
