﻿using Microsoft.AspNet.Identity;
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
        public ActionResult Index(int page = 1, string busqueda = null, string genero = null, int pageSize = 3)
        {
            MediaViewModel media = new MediaViewModel();
            MediaRepository mediaRepo = new MediaRepository(context);
            FavoritoRepository repository = new FavoritoRepository(context);
            var includes = new Expression<Func<Media, object>>[] { s => s.Generos };
            var lst = repository.Query(x=> x.media.EstadosMedia == EEstatusMedia.VISIBLE).OrderBy(x=> x.media.Nombre);
            int totalDePaginas;
            int totalDeFilas;
            ICollection<Media> movies;
            movies = mediaRepo.QueryPageByNombreAndGeneroIncluding(busqueda, genero, includes, out totalDePaginas, out totalDeFilas, "Nombre", page - 1, pageSize);

            ViewBag.Busqueda = busqueda;
            ViewBag.Genero = genero;
            var paginador = new PaginatorViewModel<MediaViewModel>();
            paginador.Page = page;
            paginador.PageSize = pageSize;
            paginador.Results = MapHelper.Map<ICollection<MediaViewModel>>(movies);
            paginador.TotalPages = totalDePaginas;
            paginador.TotalRows = totalDeFilas;
            return View(paginador);
        }

        // GET: Favorito/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Favorito/Create
        public ActionResult Create(int? id)
        {
            FavoritoRepository repository = new FavoritoRepository(context);
            UsuarioRepository repositoryUsuario = new UsuarioRepository(context);
            try
            {
                var us = Session["UserId"] as string;

                var persona = repository.Query(t => t.mediaId == id).First();
                var model = MapHelper.Map<FavoritoViewModel>(persona);
                return View(model);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        // POST: Favorito/Create
        [HttpPost]
        public ActionResult Create(bool esFalse, int? id,FavoritoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FavoritoRepository repository = new FavoritoRepository(context);
                    MediaRepository repository2 = new MediaRepository(context);
                    UsuarioRepository repositori3 = new UsuarioRepository(context);
                    var user = User.Identity.GetUserId();
                    var dia = DateTime.Now;
                    var usuarios = repositori3.Query(u => u.IdentityId.Equals(user)).First();
                    var idMedia = repository2.Query(x => x.MediaId == id).First();
                    #region Validacion de id de media
                    var Validacion = new Favorito { mediaId = id };
                    bool Existeid = repository.Query(x => x.mediaId == id).Count()>0 ;
                    if (Existeid)
                    {
                        return RedirectToAction("Details/" + id, "MovieCatalogo");
                    }
                    #endregion
                    model.esMovie = esFalse;
                    model.media = idMedia;
                    model.usuario = usuarios;
                    model.FechaAgregado = dia;
                    //model.usuario = UsuariosId;
                    Favorito persona = MapHelper.Map<Favorito>(model);
                    repository.Insert(persona);
                    context.SaveChanges();

                }
                if (esFalse == true)
                {
                    return RedirectToAction("Details/" + id, "Serie");
                }
                return RedirectToAction("Details/"+id, "MovieCatalogo");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
        public ActionResult Delete(int? id)
        {
            FavoritoRepository repository = new FavoritoRepository(context);
            var persona = repository.Query(t => t.id == id).First();
            var model = MapHelper.Map<FavoritoViewModel>(persona);
            return View(model);
        }

        // POST: Favorito/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FavoritoViewModel model)
        {
            try
            {
                FavoritoRepository repository = new FavoritoRepository(context);
                var persona = repository.Query(e => e.id == id).First();
                var IdMedia = persona.mediaId;
                repository.Delete(persona);
                context.SaveChanges();

                return RedirectToAction("Details/" + IdMedia, "MovieCatalogo");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult DeleteSerie(int? id, FavoritoViewModel model)
        {
            try
            {
                FavoritoRepository repository = new FavoritoRepository(context);
                var persona = repository.Query(e => e.id == id).First();
                var IdMedia = persona.mediaId;
                repository.Delete(persona);
                context.SaveChanges();

                return RedirectToAction("Details/" + IdMedia, "Serie");
            }
            catch
            {
                return View();
            }
        }
    }
}
