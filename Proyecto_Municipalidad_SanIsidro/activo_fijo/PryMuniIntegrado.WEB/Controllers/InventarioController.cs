using PryMuniIntegrado.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using PryMuniIntegrado.BL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.WEB.Controllers
{
    public class InventarioController : Controller
    {
        #region Propiedades
        public ModeloInventario Modelo { get; set; }
        #endregion
        public InventarioController()
        {
            this.Modelo = new ModeloInventario();
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(Modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ModeloInventario modelo)
        {
            if (ModelState.IsValid && (!string.IsNullOrEmpty(modelo.Cantidad) 
                || !string.IsNullOrEmpty(modelo.Periodo) || !string.IsNullOrEmpty(modelo.Estado)))
            {
                var cant = string.IsNullOrEmpty(modelo.Cantidad) ? 0 : int.Parse(modelo.Cantidad);
                var period = string.IsNullOrEmpty(modelo.Periodo) ? 0 : int.Parse(modelo.Periodo);
                var estado = string.IsNullOrEmpty(modelo.Estado) ? "Todos" : modelo.Estado;

                modelo.ListaInventarioFiltrada = InventarioBL.ListarInventarioPorEstadoAño(cant, period, estado);
                if (modelo.ListaInventarioFiltrada != null)
                {
                    return View(modelo);
                }
            }
            return View("Index", Modelo);
        }
        
        [HttpGet]
        public ActionResult DetalleInventario(string codigo)
        {
            if(ModelState.IsValid)
            {
                this.Modelo.CodigoInventario = codigo;
                this.Modelo.ObtenerInventario(codigo);
                return View(Modelo);
            }
            return View(Modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetalleInventario(ModeloInventario modelo)
        {            
            if(ModelState.IsValid)
            {
                this.Modelo.CodigoInventario = modelo.CodigoInventario;
                this.Modelo.ObtenerInventario(modelo.CodigoInventario, modelo.CodigoActivo);
                return View(Modelo);
            }            
            return View(Modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GrabaInventario(ModeloInventario modelo)
        {
            if (ModelState.IsValid)
            {
                this.Modelo.CodigoInventario = modelo.Inventario.Codigo;
                this.Modelo.GrabaInventario(modelo.Inventario.Codigo, modelo.Inventario.Estado, modelo.Inventario.InicioReal);
                return View("Index", new ModeloInventario());
            }
            return View(Modelo);
        }

        public ActionResult ActualizaActivo(string codigoActivo, string codigoInventario, string iEvaluar)
        {
            bool evalua = !bool.Parse(!string.IsNullOrEmpty(iEvaluar) ? iEvaluar : "True");
            this.Modelo.ActualizarActivoInventario(codigoActivo, codigoInventario, evalua);
            return View("DetalleInventario", this.Modelo);
        }

        public ActionResult EliminaActivo(string codigoActivo, string codigoInventario)
        {
            this.Modelo.EliminaActivoInventario(codigoActivo, codigoInventario);
            return View("DetalleInventario", this.Modelo);
        }

        [HttpGet]
        public ActionResult AbrirEmpleadoComite(string codigo)
        {
            var model = EmpleadoBL.ListarEmpleadoDeComite(codigo);
            return PartialView("_ListaEmpleado", model);
        }

        [HttpPost]
        public ActionResult CerrarEmpleadoComite()
        {
            return RedirectToAction("DetalleInventario", "Inventario", Modelo);
        }        
    }
}