using Newtonsoft.Json;
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
        [HttpGet]
        public ActionResult ConsultarPorTemporada(int id)
        {
            EpisodioRepository episodioRepositoy = new EpisodioRepository(context);
            var episodios = episodioRepositoy.Query(e => e.Temporada == id && e.EstadosMedia == EEstatusMedia.VISIBLE);
            var Episodios = MapHelper.Map<ICollection<EpisodioCarrucelViewModel>>(episodios);

            return Json(new
            {
                Success = true,
                Episodios = JsonConvert.SerializeObject(Episodios)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
