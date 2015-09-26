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
    public class SolicitudCatastroController : Controller
    {
        
        //
        // GET: /SolicitudCatastro/
        
        public ActionResult Index()
        {
            return View(ADSolicitud.getAll());
        }
        //public ActionResult GetSolicitud(int id=0)
        //{

        //    return this.Json((from obj
        //                          in LNSolicitud.getOne(id)
        //                      select new
        //                      {
        //                          int_IdSolicitud = obj.int_IdSolicitud,
        //                          var_NroSolicitud = obj.var_NroSolicitud,
        //                          var_TipoSolicitud = obj.utb_GCT_TipoSolicitud.var_TipoSolicitud,
        //                          var_Descripcion = obj.var_Descripcion,
        //                          dtm_FechaEmision = obj.dtm_FechaEmision
        //                      }), JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetSolicitudes()
        {

            return this.Json((from obj 
                                  in ADSolicitud.getAll() 
                              select new { int_IdSolicitud = obj.int_IdSolicitud, 
                                            var_NroSolicitud = obj.var_NroSolicitud, 
                                            var_TipoSolicitud = obj.CT_TIPOSOLICITUD.var_TipoSolicitud,
                                           var_Descripcion= obj.var_Descripcion,
                                           //var_NroPropuesta = obj.CT_PROPUESTAINSPECCION.First().var_NroPropuesta, 
                                            dtm_FechaEmision = obj.dtm_FechaEmision }), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /SolicitudCatastro/Details/5

        public ActionResult Details(int id = 0)
        {
            CT_SOLICITUD tbsolicitudcatastro = ADSolicitud.getOne(id);
            if (tbsolicitudcatastro == null)
            {
                return HttpNotFound();
            }
            return View(tbsolicitudcatastro);
        }

        //
        // GET: /SolicitudCatastro/Create

        public ActionResult Create()
        {
            CT_SOLICITUD tbsolicitudcatastro = new CT_SOLICITUD();
            tbsolicitudcatastro.dtm_FechaEmision = DateTime.Now;



            ViewBag.int_IdTipoSolicitud = new SelectList(ADTipoSolicitud.getAll(), "int_IdTipoSolicitud", "var_TipoSolicitud", 0);

            return View(tbsolicitudcatastro);
        }

        //
        // POST: /SolicitudCatastro/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CT_SOLICITUD tbsolicitudcatastro)
        {
            if (ModelState.IsValid)
            {
                tbsolicitudcatastro.dtm_FechaRegistro = DateTime.Now;
                tbsolicitudcatastro.dtm_FechaEmision = DateTime.Now;
                
                ADSolicitud.Add(tbsolicitudcatastro);
                return RedirectToAction("Index");
            }

            return View(tbsolicitudcatastro);
        }

        //
        // GET: /SolicitudCatastro/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CT_SOLICITUD tbsolicitudcatastro = ADSolicitud.getOne(id);
            if (tbsolicitudcatastro == null)
            {
                return HttpNotFound();
            }
            ViewBag.int_IdTipoSolicitud = new SelectList(ADTipoSolicitud.getAll(), "int_IdTipoSolicitud", "var_TipoSolicitud", 0);
            return View(tbsolicitudcatastro);
        }

        //
        // POST: /SolicitudCatastro/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CT_SOLICITUD tbsolicitudcatastro)
        {
            if (ModelState.IsValid)
            {
                CT_SOLICITUD tbsolicitudcatastrofind = ADSolicitud.getOne(tbsolicitudcatastro.int_IdSolicitud);

                tbsolicitudcatastro.dtm_FechaEmision = tbsolicitudcatastrofind.dtm_FechaEmision;
                tbsolicitudcatastro.dtm_FechaRegistro = tbsolicitudcatastrofind.dtm_FechaRegistro;

                ADSolicitud.Edit(tbsolicitudcatastro);
                return RedirectToAction("Index");
            }
            return View(tbsolicitudcatastro);
        }

        //
        // GET: /SolicitudCatastro/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CT_SOLICITUD tbsolicitudcatastro = ADSolicitud.getOne(id);
            if (tbsolicitudcatastro == null)
            {
                return HttpNotFound();
            }
            return View(tbsolicitudcatastro);
        }

        //
        // POST: /SolicitudCatastro/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_SOLICITUD tbsolicitudcatastro = ADSolicitud.getOne(id);
            ADSolicitud.Del(tbsolicitudcatastro);
            return RedirectToAction("Index");
        }

    }
}