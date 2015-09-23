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
    public class InformeController : Controller
    {
        ModeloInforme Modelo = new ModeloInforme(); 
        public ActionResult Index()
        {
            return View(Modelo);
        }

        [HttpPost]
        public ActionResult ListaInformePorCodigo(string codigo)
        {
            if (ModelState.IsValid)
            {
                this.Modelo.ListaInformeFiltrada = InformeBL.ListarInformePorCodigo(codigo);
                if (this.Modelo.ListaInformeFiltrada != null)
                {
                    return View("Index", Modelo);
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult DetalleInforme(string codigo)
        {
            return View(new DetalleInforme(codigo));
        }
    }
}
