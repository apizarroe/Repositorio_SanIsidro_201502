using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;
using PryMuniIntegrado.WEB.Models;

namespace PryMuniIntegrado.WEB.Controllers
{
    public class PecosaController : Controller
    {
        ModeloPecosa Modelo = new ModeloPecosa();
        public ActionResult Index()
        {
            return View(Modelo);
        }

        [HttpPost]
        public ActionResult ListaPecosaPorCodigo(string codigo)
        {
            if (ModelState.IsValid)
            {
                this.Modelo.ListaPecosaFiltrada = PecosaBL.ListarPecosaPorCodigo(codigo);
                if (this.Modelo.ListaPecosaFiltrada != null)
                {
                    return View("Index", Modelo);
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult DetallePecosa(string codigo)
        {
            return View(new DetallePecosa(codigo));
        }

        [HttpPost]
        public ActionResult GrabaPecosa(DetallePecosa detalle)
        {   
            if (!PecosaBL.GrabarPecosa(detalle.Pecosa))
            {
                ViewBag.Error = "No se pudo grabar Pecosa !!!";
                return View("DetallePecosa", detalle);
            }
            return View("Index", Modelo);
        }
    }
}
