using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Core.Entities.ModeloGestionCatastral;

namespace GAC.Controllers
{
    public class PropuestaInspeccionController : Controller
    {
        private db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();

        //
        // GET: /PropuestaInspeccion/

        public ActionResult Index()
        {
            var ct_propuestainspeccion = db.CT_PROPUESTAINSPECCION.Include(c => c.CT_SOLICITUD);
            return View(ct_propuestainspeccion.ToList());
        }

        public ActionResult Calificar()
        {
            var ct_propuestainspeccion = db.CT_PROPUESTAINSPECCION.Include(c => c.CT_SOLICITUD);
            return View(ct_propuestainspeccion.ToList());
        }

        //
        // GET: /PropuestaInspeccion/Details/5

        public ActionResult Details(int id = 0)
        {
            CT_PROPUESTAINSPECCION ct_propuestainspeccion = db.CT_PROPUESTAINSPECCION.Find(id);
            if (ct_propuestainspeccion == null)
            {
                return HttpNotFound();
            }
            return View(ct_propuestainspeccion);
        }

        //
        // GET: /PropuestaInspeccion/Create

        public ActionResult Create()
        {
            CT_PROPUESTAINSPECCION oCT_PROPUESTAINSPECCION = new CT_PROPUESTAINSPECCION();
            oCT_PROPUESTAINSPECCION.dtm_FechaDocumento = DateTime.Now;
            oCT_PROPUESTAINSPECCION.dtm_FechaRegistro  = DateTime.Now;
            oCT_PROPUESTAINSPECCION.var_NroPropuesta= "";


            ViewBag.int_IdSolicitud = new SelectList(db.CT_SOLICITUD, "int_IdSolicitud", "var_NroSolicitud");
            return View(oCT_PROPUESTAINSPECCION);
        }
        public ActionResult Asignar()
        {



            ViewBag.idEmpleado = new SelectList(db.MA_EMPLEADO, "idEmpleado", "CodigoEmpleado");
            return View();
        }
        public ActionResult InsertarAsignar(CT_PROPUESTAINSPECCION_EMPLEADO oCT_PROPUESTAINSPECCION_EMPLEADO)
        {
            if (ModelState.IsValid)
            {
                CT_SOLICITUD o = db.CT_SOLICITUD.Find(oCT_PROPUESTAINSPECCION_EMPLEADO.int_IdPropuestaInspeccion);


                oCT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaInicio = DateTime.Now;
                oCT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaFin = DateTime.Now;
                oCT_PROPUESTAINSPECCION_EMPLEADO.int_IdPropuestaInspeccion = o.CT_PROPUESTAINSPECCION.First().int_IdPropuestaInspeccion;


                db.CT_PROPUESTAINSPECCION_EMPLEADO.Add(oCT_PROPUESTAINSPECCION_EMPLEADO);
                db.SaveChanges();
                return Json(new
                {
                    success = true,
                });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public ActionResult GetAsignar(int int_IdPropuestaInspeccion = 0)
        {
            CT_SOLICITUD o = db.CT_SOLICITUD.Find(int_IdPropuestaInspeccion);
            

            return this.Json((from obj
                                  in db.CT_PROPUESTAINSPECCION_EMPLEADO.ToList().Where(x => x.int_IdPropuestaInspeccion == o.CT_PROPUESTAINSPECCION.First().int_IdPropuestaInspeccion)
                              select new
                              {
                                  chr_CodigoPredio = obj.MA_EMPLEADO.CodigoEmpleado,
                                  var_Nombre = "Zona 2"
                              }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CalificarCreate(int id = 0)
        {

            CT_PROPUESTAINSPECCION oCT_PROPUESTAINSPECCION = db.CT_PROPUESTAINSPECCION.Find(id);
            

            ViewBag.int_IdSolicitud = new SelectList(db.CT_SOLICITUD, "int_IdSolicitud", "var_NroSolicitud");
            return View(oCT_PROPUESTAINSPECCION);
        }
        //
        // POST: /PropuestaInspeccion/Create

        [HttpPost]
        public ActionResult Create(CT_PROPUESTAINSPECCION ct_propuestainspeccion)
        {
            if (ModelState.IsValid)
            {
                ct_propuestainspeccion.var_NroPropuesta = "";
                ct_propuestainspeccion.int_Estado = 1;
                db.CT_PROPUESTAINSPECCION.Add(ct_propuestainspeccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.int_IdSolicitud = new SelectList(db.CT_SOLICITUD, "int_IdSolicitud", "var_NroSolicitud", ct_propuestainspeccion.int_IdSolicitud);
            return View(ct_propuestainspeccion);
        }

        [HttpPost]
        public ActionResult CalificarCreate(CT_PROPUESTAINSPECCION ct_propuestainspeccion)
        {
            if (ModelState.IsValid)
            {
                CT_PROPUESTAINSPECCION oCT_PROPUESTAINSPECCION = db.CT_PROPUESTAINSPECCION.Find(ct_propuestainspeccion.int_IdPropuestaInspeccion);

                oCT_PROPUESTAINSPECCION.var_Observacion = ct_propuestainspeccion.var_Observacion;
                oCT_PROPUESTAINSPECCION.int_Estado = ct_propuestainspeccion.int_Estado;

                db.Entry(oCT_PROPUESTAINSPECCION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("calificar");
            }

            ViewBag.int_IdSolicitud = new SelectList(db.CT_SOLICITUD, "int_IdSolicitud", "var_NroSolicitud", ct_propuestainspeccion.int_IdSolicitud);
            return View(ct_propuestainspeccion);
        }

        //
        // GET: /PropuestaInspeccion/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CT_PROPUESTAINSPECCION ct_propuestainspeccion = db.CT_PROPUESTAINSPECCION.Find(id);
            if (ct_propuestainspeccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.int_IdSolicitud = new SelectList(db.CT_SOLICITUD, "int_IdSolicitud", "var_NroSolicitud", ct_propuestainspeccion.int_IdSolicitud);
            return View(ct_propuestainspeccion);
        }

        //
        // POST: /PropuestaInspeccion/Edit/5

        [HttpPost]
        public ActionResult Edit(CT_PROPUESTAINSPECCION ct_propuestainspeccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ct_propuestainspeccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.int_IdSolicitud = new SelectList(db.CT_SOLICITUD, "int_IdSolicitud", "var_NroSolicitud", ct_propuestainspeccion.int_IdSolicitud);
            return View(ct_propuestainspeccion);
        }

        //
        // GET: /PropuestaInspeccion/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CT_PROPUESTAINSPECCION ct_propuestainspeccion = db.CT_PROPUESTAINSPECCION.Find(id);
            if (ct_propuestainspeccion == null)
            {
                return HttpNotFound();
            }
            return View(ct_propuestainspeccion);
        }

        //
        // POST: /PropuestaInspeccion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_PROPUESTAINSPECCION ct_propuestainspeccion = db.CT_PROPUESTAINSPECCION.Find(id);
            db.CT_PROPUESTAINSPECCION.Remove(ct_propuestainspeccion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}