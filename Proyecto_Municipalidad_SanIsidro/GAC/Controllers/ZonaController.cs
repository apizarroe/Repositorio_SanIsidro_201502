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
    public class ZonaController : Controller
    {
        
        //Change from catastro group 
        //
        // GET: /Zona/

        public ActionResult Index()
        {
            return View(ADZona.getAll());
        }
        public ActionResult GetZonas(int int_IdSolicitud = 0)
        {


            if (int_IdSolicitud == -1)
            {
                return this.Json((from obj
                              in ADZona.getAll()
                                  select new
                                  {
                                      int_IdZona = obj.int_IdZona,
                                      var_Nombre = obj.var_Nombre
                                  }), JsonRequestBehavior.AllowGet);
            }
            else
            {

                return this.Json((from obj
                                      in ADZona.getAllPorSolicitud(int_IdSolicitud)
                                  select new
                                  {
                                      int_IdZona = obj.int_IdZona,
                                      var_Nombre = obj.var_Nombre
                                  }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetZona(int int_IdZona = 0)
        {

            var entity = ADZona.getOne(int_IdZona);

            return this.Json(new
            {
                int_IdZona = entity.int_IdZona,
                var_CodigoZona = entity.var_CodigoZona
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EliminarZona(int Id)
        {
            if (ModelState.IsValid)
            {
                String success = "0";
                CT_ZONA oZona = ADZona.getOne(Id);
                if (ADManzana.getAll(Id).Count() > 0) {
                    success = "1";
                }
                else if (ADZona.Del(oZona) > 0)
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

        //
        // GET: /Zona/Details/5

        public ActionResult Details(int id = 0)
        {
            CT_ZONA utb_gct_zona = ADZona.getOne(id);
            if (utb_gct_zona == null)
            {
                return HttpNotFound();
            }
            return View(utb_gct_zona);
        }

        //
        // GET: /Zona/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Zona/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CT_ZONA utb_gct_zona)
        {
            if (ModelState.IsValid)
            {
                ADZona.Add(utb_gct_zona);
                return RedirectToAction("Index");
            }

            return View(utb_gct_zona);
        }


        public ActionResult InsertarZona(CT_ZONA oZona)
        {
            if (ModelState.IsValid)
            {
                oZona.dtm_FechaCreacion = DateTime.Now;
                oZona.dtm_FechaRegistro = DateTime.Now;
                oZona.dtm_FechaActualizacion  = DateTime.Now;
                
                ADZona.Add(oZona);
                return Json(new
                {
                    success = true,
                    oZona.int_IdZona,
                });
            }
            else
            {
                return Json(new { success = false});
            }
        }

        //
        // GET: /Zona/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CT_ZONA utb_gct_zona = ADZona.getOne(id);
            if (utb_gct_zona == null)
            {
                return HttpNotFound();
            }
            return View(utb_gct_zona);
        }

        //
        // POST: /Zona/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CT_ZONA utb_gct_zona)
        {
            if (ModelState.IsValid)
            {
                ADZona.Edit(utb_gct_zona);
                return RedirectToAction("Index");
            }
            return View(utb_gct_zona);
        }

        //
        // GET: /Zona/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CT_ZONA utb_gct_zona = ADZona.getOne(id);
            if (utb_gct_zona == null)
            {
                return HttpNotFound();
            }
            return View(utb_gct_zona);
        }

        //
        // POST: /Zona/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_ZONA utb_gct_zona = ADZona.getOne(id);
            ADZona.Del(utb_gct_zona);
            return RedirectToAction("Index");
        }

        
    }
}