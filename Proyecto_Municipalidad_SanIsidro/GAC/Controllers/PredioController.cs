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
    public class PredioController : Controller
    {
        //
        // GET: /Predio/

        public ActionResult InsertarPredio(CT_PREDIO oPredio)
        {
            if (ModelState.IsValid)
            {
                oPredio.dtm_FechaCreacion = DateTime.Now;
                oPredio.dtm_FechaRegistro = DateTime.Now;
                oPredio.dtm_FechaActualizacion = DateTime.Now;

                ADPredio.Add(oPredio);
                return Json(new
                {
                    success = true,
                    oPredio.int_IdPredio,
                });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public ActionResult GetPredio(int int_IdLote=0)
        {

            return this.Json((from obj
                                  in ADPredio.getAll(int_IdLote)
                              select new
                              {
                                  chr_CodigoPredio = obj.chr_CodigoPredio,
                                  var_Nombre = obj.var_Nombre,
                                  int_IdPredio = obj.int_IdPredio
                              }), JsonRequestBehavior.AllowGet);
        }


        public ActionResult EliminarPredio(int Id)
        {
            if (ModelState.IsValid)
            {
                String success = "0";
                CT_PREDIO oPredio = ADPredio.getOne(Id);

                if (ADPredio.Del(oPredio) > 0)
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
