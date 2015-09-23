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
    public class EvaluacionController : Controller
    {
        ModeloEvaluacion Modelo = new ModeloEvaluacion();
        public ActionResult Index()
        {
            return View(Modelo);
        }

        [HttpPost]
        public ActionResult ListaPorCodigoEvaluacion(string codigoEvaluacion)
        {
            if (ModelState.IsValid)
            {
                this.Modelo.ListaEvaluacionFiltrada = EvaluacionBL.ListarEvaluacionPorCodigo(codigoEvaluacion);
                if (this.Modelo.ListaEvaluacionFiltrada != null)
                {
                    return View("Index", Modelo);
                }
            }
            return View("Index");

        }

        [HttpPost]
        public ActionResult ListaPorCodigoInventario(string codigoInventario)
        {
            if (ModelState.IsValid)
            {
                this.Modelo.ListaEvaluacionFiltrada = EvaluacionBL.ListarEvaluacionPorCodigo(codigoInventario);
                if (this.Modelo.ListaEvaluacionFiltrada != null)
                {
                    return View("Index", Modelo);
                }
            }
            return View("Index");

        }

        [HttpGet]
        public ActionResult DetalleEvaluacion(string codigo)
        {
            return View(new DetalleEvaluacion(codigo));
        }
    }
}