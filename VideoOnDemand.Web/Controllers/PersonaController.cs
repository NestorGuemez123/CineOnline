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
    public class PersonaController : BaseController
    {
        // GET: Persona
        public ActionResult Index()
        {
            PersonaRepository repository = new PersonaRepository(context);
            var lst = repository.GetAll();
            var models = MapHelper.Map<IEnumerable<PersonaViewModel>>(lst);

            return View(models);
        }

        // GET: Persona/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Persona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persona/Create
        [HttpPost]
        public ActionResult Create(PersonaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PersonaRepository repository = new PersonaRepository(context);
                    #region validacion
                    var topicQry = new Persona { Name = model.Name };
                    //Consulto los temas con el nombre y valido su existe un elemento
                    bool existeTopic = repository.QueryByExample(topicQry).Count > 0;
                    if (existeTopic)
                    {
                        ModelState.AddModelError("Name", "El nombre del tema ya existe");
                        return View(model);
                    }
                    #endregion
                    Persona persona = MapHelper.Map<Persona>(model);
                    repository.Insert(persona);
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
        // GET: Persona/Edit/5
        public ActionResult Edit(int id)
        {
            PersonaRepository repository = new PersonaRepository(context);
            var persona = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<PersonaViewModel>(persona);
            return View(model);
        }

        // POST: Persona/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PersonaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PersonaRepository repository = new PersonaRepository(context);
                    #region validacion
                    //Consulto los temas con el nombre y valido su existe un elemento
                    bool existePersona = repository.Query(x => x.Name == model.Name && x.Id != model.Id).Count > 0;
                    if (existePersona)
                    {
                        ModelState.AddModelError("Name", "El nombre del tema ya existe");
                        return View(model);
                    }
                    #endregion
                    Persona persona = MapHelper.Map<Persona>(model);
                    repository.Update(persona);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(int id)
        {
            PersonaRepository repository = new PersonaRepository(context);
            var persona = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<PersonaViewModel>(persona);
            return View(model);
        }

        // POST: Persona/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PersonaViewModel collection)
        {
            try
            {
                PersonaRepository repository = new PersonaRepository(context);
                var persona = repository.Query(e => e.Id == id).First();
                persona.Status = false;
                repository.Update(persona);
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
