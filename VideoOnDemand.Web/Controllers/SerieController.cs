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
        // GET: Serie
        public ActionResult Index(int page = 1, string busqueda = null, string genero = null, int pageSize = 3)
        {
            SerieRepository serieRepository = new SerieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);

            var includes = new Expression<Func<Serie, object>>[] { s => s.Generos };
            
            int totalDePaginas;
            int totalDeFilas;

            ICollection<Serie> series;


            series = serieRepository.QueryPageByNombreAndGeneroIncluding(busqueda, genero, includes, out totalDePaginas, out totalDeFilas, "Nombre", page - 1, pageSize);       
            
            ViewBag.Busqueda = busqueda;
            ViewBag.Genero = genero;
            ViewBag.ListaGeneros = generoRepository.GetAll().Select(g => g.Nombre).Where( g=> g != genero).ToList();

            var paginador = new PaginatorViewModel<ThumbnailSerieViewModel>();
            paginador.Page = page;
            paginador.PageSize = pageSize;
            paginador.Results = MapHelper.Map<ICollection<ThumbnailSerieViewModel>>(series);
            paginador.TotalPages = totalDePaginas;
            paginador.TotalRows = totalDeFilas;

            return View(paginador);
        }

        // GET: Serie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Serie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Serie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Serie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Serie/Edit/5
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

        // GET: Serie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Serie/Delete/5
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
