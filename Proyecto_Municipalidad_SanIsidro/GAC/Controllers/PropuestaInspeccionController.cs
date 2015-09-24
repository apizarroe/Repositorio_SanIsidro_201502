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
    public class PropuestaInspeccionController : Controller
    {
        private db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();

        //
        // GET: /PropuestaInspeccion/

        public ActionResult Index()
        {

            return View(ADPropuestaInspeccion.getAll());
        }

        public ActionResult Calificar()
        {
            return View(ADPropuestaInspeccion.getAll());
        }

        //
        // GET: /PropuestaInspeccion/Details/5

        public ActionResult Details(int id = 0)
        {
            CT_PROPUESTAINSPECCION ct_propuestainspeccion = ADPropuestaInspeccion.getOne(id);
            if (ct_propuestainspeccion == null)
            {
                return HttpNotFound();
            }
            return View(ct_propuestainspeccion);
        }

        //
        // GET: /PropuestaInspeccion/Create

        public ActionResult Create(int id=0)
        {
            int id_solicitud3 = 0;
            CT_PROPUESTAINSPECCION oCT_PROPUESTAINSPECCION = null;
            if (id > 0)
            {
                oCT_PROPUESTAINSPECCION=ADPropuestaInspeccion.getOne(id);
                id_solicitud3 = oCT_PROPUESTAINSPECCION.int_IdSolicitud.Value;

                ViewBag.TextBotton = "Editar Propuesta";
            }
            else
            {
                oCT_PROPUESTAINSPECCION = new CT_PROPUESTAINSPECCION();
                oCT_PROPUESTAINSPECCION.dtm_FechaDocumento = DateTime.Now;
                oCT_PROPUESTAINSPECCION.dtm_FechaRegistro = DateTime.Now;
                oCT_PROPUESTAINSPECCION.var_NroPropuesta = "";
                ViewBag.TextBotton = "Emitir Propuesta";
            }





            ViewBag.int_IdSolicitud = new SelectList(ADSolicitud.getAll(), "int_IdSolicitud", "var_NroSolicitud", id_solicitud3);
            return View(oCT_PROPUESTAINSPECCION);
        }


        public ActionResult Asignar()
        {


            var stands =
                          ADEmpleado.getAll()
                            .Select(s => new
                            {
                                idEmpleado = s.idEmpleado,
                                Description = string.Format("{0},{1}", s.MA_PERSONA.MA_PERSONANATURAL.First().ApellidoPaterno, s.MA_PERSONA.MA_PERSONANATURAL.First().Nombres)
                            });


            ViewBag.idEmpleado = new SelectList(stands, "idEmpleado", "Description");
            return View();
        }
        /// <summary>
        /// Asigan  un tecnico a una propuesta especifica
        /// </summary>
        /// <param name="oCT_PROPUESTAINSPECCION_EMPLEADO"> Objeto que contiene todos los datos de la relaci</param>
        /// <returns>True o false deacuerdo al exito</returns>
        public ActionResult InsertarAsignar(CT_PROPUESTAINSPECCION_EMPLEADO oCT_PROPUESTAINSPECCION_EMPLEADO)
        {
            if (ModelState.IsValid)
            {
                CT_SOLICITUD o = db.CT_SOLICITUD.Find(oCT_PROPUESTAINSPECCION_EMPLEADO.int_IdPropuestaInspeccion);
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
        /// <summary>
        /// Obtiene los tecnicos disponibles para asignar
        /// </summary>
        /// <param name="strfechainicio">Fecha de Inicio de inspección</param>
        /// <param name="strfechafin">Fecha fin de inspección</param>
        /// <returns></returns>
        public ActionResult GetTecnico(String strfechainicio, String strfechafin)
        {

            if (strfechainicio == "Invalid date")
            {
                return Json(new { success = false });
            }
            DateTime pdtm_FechaInicio = DateTime.Parse(strfechainicio);
            DateTime dtm_FechaFin = DateTime.Parse(strfechafin);

            return this.Json((from E in db.MA_EMPLEADO
                              from PN in db.MA_PERSONANATURAL
                              where
                              PN.idPersona == E.idPersona && 
                                E.idArea == 11 &&
                                !
                                  (from CT_PROPUESTAINSPECCION_EMPLEADO in db.CT_PROPUESTAINSPECCION_EMPLEADO
                                   where
                                     (CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaInicio <= pdtm_FechaInicio &&
                                     CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaFin >= pdtm_FechaInicio) ||
                                     (CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaInicio <= dtm_FechaFin &&
                                     CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaFin >= dtm_FechaFin) ||
                                     (pdtm_FechaInicio >= CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaInicio && pdtm_FechaInicio <= CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaFin &&
                                     dtm_FechaFin >= CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaInicio && dtm_FechaFin <= CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaFin) ||
                                     (CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaInicio <= pdtm_FechaInicio &&
                                     CT_PROPUESTAINSPECCION_EMPLEADO.dtm_FechaFin >= dtm_FechaFin)
                                   select new
                                   {
                                       CT_PROPUESTAINSPECCION_EMPLEADO.idEmpleado
                                   }).Contains(new { idEmpleado = E.idEmpleado })
                              select new
                              {
                                  idEmpleado = E.idEmpleado,
                                  Description = PN.ApellidoPaterno + PN.ApellidoMaterno  + PN.Nombres
                              }).Distinct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsignar(int int_IdPropuestaInspeccion = 0)
        {
            CT_SOLICITUD o = db.CT_SOLICITUD.Find(int_IdPropuestaInspeccion);
            

            return this.Json((from obj
                                  in db.CT_PROPUESTAINSPECCION_EMPLEADO.ToList().Where(x => x.int_IdPropuestaInspeccion == o.CT_PROPUESTAINSPECCION.First().int_IdPropuestaInspeccion)
                              select new
                              {
                                  chr_CodigoPredio = obj.MA_EMPLEADO.CodigoEmpleado,
                                  var_Nombre = string.Format("{0},{1}", obj.MA_EMPLEADO.MA_PERSONA.MA_PERSONANATURAL.First().ApellidoPaterno , obj.MA_EMPLEADO.MA_PERSONA.MA_PERSONANATURAL.First().Nombres) 
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
                if (ct_propuestainspeccion.int_IdPropuestaInspeccion > 0)
                {
                    ct_propuestainspeccion.var_NroPropuesta = "";
                    ct_propuestainspeccion.int_Estado = 1;
                    db.Entry(ct_propuestainspeccion).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else {
                    ct_propuestainspeccion.var_NroPropuesta = "";
                    ct_propuestainspeccion.int_Estado = 1;
                    db.CT_PROPUESTAINSPECCION.Add(ct_propuestainspeccion);
                    db.SaveChanges();
                }
                
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
                oCT_PROPUESTAINSPECCION.dtm_FechaDocumento = DateTime.Now;


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