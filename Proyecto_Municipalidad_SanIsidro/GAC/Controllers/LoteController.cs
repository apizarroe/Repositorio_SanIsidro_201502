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
    public class LoteController : Controller
    {
        //
        // GET: /Lote/
        public ActionResult GetLote(int int_IdManzana = 0)
        {

            return this.Json((from obj
                                  in ADLote.getAll(int_IdManzana)
                              select new
                              {
                                  int_IdLote = obj.int_IdLote,
                                  var_Nombre = obj.var_Nombre
                              }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUnLote(int int_IdLote = 0)
        {

            var entity = ADLote.getOne(int_IdLote);

            return this.Json(new
            {
                int_IdLote = entity.int_IdLote,
                var_CodigoLote = entity.var_CodigoLote,
                var_Nombre = entity.var_Nombre,
                var_Direccion = entity.var_Direccion
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertarLote(CT_LOTE oLote)
        {
            if (ModelState.IsValid)
            {
                oLote.dtm_FechaCreacion = DateTime.Now;
                oLote.dtm_FechaRegistro = DateTime.Now;
                oLote.dtm_FechaActualizacion = DateTime.Now;

                ADLote.Add(oLote);
                return Json(new
                {
                    success = true,
                    oLote.int_IdLote,
                });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public ActionResult EliminarLote(int Id)
        {
            if (ModelState.IsValid)
            {
                String success = "0";
                CT_LOTE oLote = ADLote.getOne(Id);
                if (ADPredio.getAll(Id).Count() > 0)
                {
                    success = "1";
                }
                else if (ADLote.Del(oLote) > 0)
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
