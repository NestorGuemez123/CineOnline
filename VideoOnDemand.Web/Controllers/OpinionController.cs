using Microsoft.AspNet.Identity;
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
        // GET: Opinion/Create
        public ActionResult Create(int? id)
        {
            OpinionRepository repository = new OpinionRepository(context);
            UsuarioRepository repositoryUsuario = new UsuarioRepository(context);
            try{
                var us = Session["UserId"] as string;

                var media = repository.Query(t => t.MediaId == id).First();
                var model = MapHelper.Map<OpinionViewModel>(media);

                return View(model);
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        // POST: Opinion/Create
        [HttpPost]
        public ActionResult Create(int? id, OpinionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OpinionRepository repoOpinion = new OpinionRepository(context);
                    MediaRepository repoMedia = new MediaRepository(context);
                    UsuarioRepository repoUser = new UsuarioRepository(context);
                    var user = User.Identity.GetUserId();
                    var dia = DateTime.Now;
                    var usuarios = repoUser.Query(u => u.IdentityId.Equals(user)).First();
                    var IdMedia = repoMedia.Query(m => m.MediaId == id).First();
                    model.media = IdMedia;
                    model.usuario = usuarios;
                    model.FechaRegistro = dia;

                    Opinion opinion = MapHelper.Map<Opinion>(model);
                    repoOpinion.Insert(opinion);
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
    }
}