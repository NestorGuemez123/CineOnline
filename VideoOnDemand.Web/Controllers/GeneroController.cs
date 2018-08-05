using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class GeneroController : BaseController
    {
        // GET: Genero
        public ActionResult Index()
        {
            VideoOnDemandContext context = new VideoOnDemandContext();
            GeneroRepository repository = new GeneroRepository(context);
            var lst = repository.Query(g => g.Activo == true);
            var models = MapHelper.Map<IEnumerable<GeneroViewModel>>(lst);

            return View(models);
        }

        // GET: Genero/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        [HttpPost]
        public ActionResult Create(GeneroViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GeneroRepository repository = new GeneroRepository(context);
                    repository.Query(g => g.Activo == true);
                    #region Validaciones
                    //validar nombre unico
                    var geneQry = new Genero { Nombre = model.Nombre };
                    bool existeGenero = repository.QueryByExample(geneQry).Count > 0;
                    if (existeGenero)
                    {
                        ModelState.AddModelError("Nombre", "El nombre del genero ya existe.");
                        return View(model);
                    }
                    #endregion
                    Genero genero = MapHelper.Map<Genero>(model);

                    repository.Insert(genero);
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

        // GET: Genero/Edit/5
        public ActionResult Edit(int? id)
        {
            GeneroRepository repository = new GeneroRepository(context);
            var genero = repository.Query(t => t.GeneroId == id).First();
            var model = MapHelper.Map<GeneroViewModel>(genero);


            return View(model);
        }

        // POST: Genero/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GeneroViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GeneroRepository repository = new GeneroRepository(context);

                    #region Validaciones
                    //validar nombre unico   

                    bool existeGenero = repository.Query(x => x.Nombre == model.Nombre && x.GeneroId != model.GeneroId).Count > 0;
                    if (existeGenero)
                    {
                        ModelState.AddModelError("Name", "El nombre del genero ya existe.");
                        return View(model);
                    }
                    #endregion

                    Genero genero = MapHelper.Map<Genero>(model);

                    repository.Update(genero);
                    context.SaveChanges();
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Genero/Delete/5
        public ActionResult Delete(int id)
        {
            var repository = new GeneroRepository(context);
            var genero = repository.Query(t => t.GeneroId == id).First();
            var model = MapHelper.Map<GeneroViewModel>(genero);


            return View(model);
        }

        // POST: Genero/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, GeneroViewModel model)
        {
            try
            {

                GeneroRepository repository = new GeneroRepository(context);
                var genero = repository.Query(e => e.GeneroId == id).First();
                genero.Activo = false;
                repository.Update(genero);
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
