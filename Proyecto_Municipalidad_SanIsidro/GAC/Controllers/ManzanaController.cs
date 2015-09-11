using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Core.Entities.ModeloGestionCatastral;
using Infraestructura.Data.SQL;
namespace GAC.Controllers
{
    public class ManzanaController : Controller
    {
        //
        // GET: /Manzana/
        public ActionResult GetManzanas(int int_IdZona = 0)
        {

            return this.Json((from obj
                                  in ADManzana.getAll(int_IdZona)
                              select new
                              {
                                  int_IdManzana = obj.int_IdManzana,
                                  var_Nombre = obj.var_Nombre
                              }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetManzana(int int_IdManzana = 0)
        {

            var entity = ADManzana.getOne(int_IdManzana);

            return this.Json(new
            {
                int_IdManzana = entity.int_IdManzana,
                var_CodigoManzana = entity.var_CodigoManzana,
                var_Nombre = entity.var_Nombre
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InsertarManzana(CT_MANZANA oManzana)
        {
            if (ModelState.IsValid)
            {
                oManzana.dtm_FechaCreacion = DateTime.Now;
                oManzana.dtm_FechaRegistro = DateTime.Now;
                oManzana.dtm_FechaActualizacion = DateTime.Now;

                ADManzana.Add(oManzana);
                return Json(new
                {
                    success = true,
                    oManzana.int_IdManzana,
                });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public ActionResult EliminarManzana(int Id)
        {
            if (ModelState.IsValid)
            {
                String success = "0";
                CT_MANZANA oManzana = ADManzana.getOne(Id);
                if (ADLote.getAll(Id).Count() > 0)
                {
                    success = "1";
                }
                else if (ADManzana.Del(oManzana) > 0)
                {
                    success = "2";
                }
                return Json(new
                {
                    success = success,
                });
            }
            else
            {
                return Json(new { success = "-1" });
            }
        }

    }
}
