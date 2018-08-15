﻿using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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
    public class OpinionController : BaseController
    {
        [HttpPost]
        public ActionResult Registrar(OpinionViewModel model)
        {

            UsuarioRepository usuarioRepository = new UsuarioRepository(context);
            var usuarioIdentirty = User.Identity.GetUserId();
            var usuarioId = usuarioRepository.Query(u => u.IdentityId == usuarioIdentirty).Select(u => u.Id).SingleOrDefault();
            model.UsuarioId = usuarioId;
            model.FechaRegistro = DateTime.Now;

            if (ModelState.IsValid)
            {
                OpinionRepository opinionRepository = new OpinionRepository(context);
                var opinion = MapHelper.Map<Opinion>(model);
                opinionRepository.Insert(opinion);
                context.SaveChanges();
                return Json(new
                {
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new
                {
                    Success = false
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Consultar(int id)
        {
            OpinionRepository opinionRepository = new OpinionRepository(context);
            var relaciones = new Expression<Func<Opinion, object>>[] {o => o.Usuario };
            var Opiniones = opinionRepository.QueryIncluding(o => o.MediaId == id, relaciones, "FechaRegistro");

            return Json(new
            {
                Success = true,
                Opiniones = JsonConvert.SerializeObject(Opiniones)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}