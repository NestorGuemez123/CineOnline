using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class OpinionController : BaseController
    {
        // GET: Opinion
        public ActionResult Index()
        {   
            OpinionRepository repository = new OpinionRepository(context);
            var lst = repository.GetAll();
            var models = MapHelper.Map<IEnumerable<OpinionViewModel>>(lst);

            return View(models);
        }

        public ActionResult Create(int? id)
        {
            OpinionRepository repository = new OpinionRepository(context);
            UsuarioRepository repositoryUsuario = new UsuarioRepository(context);
            try{
                var us = Session["UserId"] as string;

                var media = repository.Query(t => t.MediaId == id).FirstOrDefault();
                var model = MapHelper.Map<OpinionViewModel>(media);

                var usuarios = repositoryUsuario.Query(u => u.IdentityId.Equals(us)).FirstOrDefault();

                return View(model);
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        // POST: Genero/Create
        [HttpPost]
        public ActionResult Create(int? id, OpinionViewModel model)
        {
            try
            {
                OpinionRepository repository = new OpinionRepository(context);
                var persona = repository.Query(t => t.MediaId == id).First();
                var models = MapHelper.Map<OpinionViewModel>(persona);
                return View(models);                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}