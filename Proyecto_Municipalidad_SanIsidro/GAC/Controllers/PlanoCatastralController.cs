using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Core.Entities.ModeloGestionCatastral;
using Infraestructura.Data.SQL;
namespace GAC.Controllers
{
    public class PlanoCatastralController : Controller
    {
        //
        // GET: /PlanoCatastral/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ImprimirPlano(int id=0,int opt=0)
        {

            ViewBag.opt = opt;
            if (opt == 1)
            {
                ViewBag.titulo = "Zona";
                ViewBag.nombre = ADZona.getOne(id).var_Nombre;
                
            }
            if (opt == 2)
            {
                ViewBag.titulo = "Manzana";
                ViewBag.nombre = ADManzana.getOne(id).var_Nombre;
            }
            if (opt == 3)
            {
                ViewBag.titulo = "Lote";
                ViewBag.nombre = ADLote.getOne(id).var_Nombre;
            }

            ViewBag.int_IdSolicitud = id;
            return View();
        }

    }
}
