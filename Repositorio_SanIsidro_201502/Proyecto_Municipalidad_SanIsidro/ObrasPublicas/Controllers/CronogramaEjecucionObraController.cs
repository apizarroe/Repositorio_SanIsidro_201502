using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObrasPublicas.Entities;
using Infraestructura.Data.SQL;
using ObrasPublicas.DAL;
using ObrasPublicas.Models.CronogramaEjecucionObra;

namespace ObrasPublicas.Controllers
{
    public class CronogramaEjecucionObraController : BaseController
    {
        //
        // GET: /CronogramaEjecucionObra/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update(int p, int e, int c)
        {
            UpdateCronogramaEjecucionObraModel objUpdateCronogramaEjecucionObraModel = new UpdateCronogramaEjecucionObraModel();

            objUpdateCronogramaEjecucionObraModel.IdCronograma = c;
            objUpdateCronogramaEjecucionObraModel.IdExpediente = e;
            objUpdateCronogramaEjecucionObraModel.IdProyecto = p;
            return View(objUpdateCronogramaEjecucionObraModel);
        }

        [HttpGet]
        public ActionResult Create(int p, int e)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
            CreateCronogramaEjecucionObraModel objCreateCronogramaEjecucionObraModel = new CreateCronogramaEjecucionObraModel();
            objCreateCronogramaEjecucionObraModel.IdProyecto = objProyectoInversion.IdProyecto;
            objCreateCronogramaEjecucionObraModel.NomProyecto = objProyectoInversion.Nombre;
            objCreateCronogramaEjecucionObraModel.IdExpediente = e;

            return View(objCreateCronogramaEjecucionObraModel);
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Listado(int p, int e, int c)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
            
            CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
            List<ActividadCronogramaOP> lstActividades = objCronogramaEjecucionObra_DAL.ObtieneActvidades(e,c);

            ListadoCronogramaEjecucionObraModel objListadoCronogramaEjecucionObraModel = new ListadoCronogramaEjecucionObraModel();
            objListadoCronogramaEjecucionObraModel.IdProyecto = objProyectoInversion.IdProyecto;
            objListadoCronogramaEjecucionObraModel.NomProyecto = objProyectoInversion.Nombre;
            objListadoCronogramaEjecucionObraModel.IdExpediente = e;
            objListadoCronogramaEjecucionObraModel.IdCronograma = c;

            ViewBag.ListadoActividades = lstActividades;
            return View(objListadoCronogramaEjecucionObraModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCronogramaEjecucionObraModel pObjModel)
        {
            var valid = TryUpdateModel(pObjModel);

            if (valid)
            {
                try
                {
                    CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
                    int intResultado = objCronogramaEjecucionObra_DAL.Inserta(pObjModel.IdExpediente,pObjModel.PlazoEjecucion);
                    
                    if (intResultado == 1)
                    {
                        //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        //return RedirectToAction("Index");
                    }
                    else if (intResultado == -997)
                    {
                        ModelState.AddModelError("General", "No puede crear el cronograma debido a que el proyecto está en estado ADJUDICADO.");
                    }
                    else
                    {
                        valid = false;
                        ModelState.AddModelError("General", "No se pudo insertar el cronograma");
                    }
                }
                catch (Exception ex)
                {
                    valid = false;
                    ModelState.AddModelError("General", ex.ToString());
                }
            }

            return Json(new
            {
                Valid = valid,
                Errors = GetErrorsFromModelState()
            });
        }

        //public ViewResult BlankEditorRow()
        //{
        //    return View("_ActividadEjecucionObraRow", new Models.CronogramaEjecucionObra.ActividadEjecucionObraModel());
        //}

        public ActionResult Edit(int p, int e, int c)
        {
            UpdateCronogramaEjecucionObraModel objUpdateCronogramaEjecucionObraModel = new UpdateCronogramaEjecucionObraModel();
            objUpdateCronogramaEjecucionObraModel.IdProyecto = p;
            objUpdateCronogramaEjecucionObraModel.IdExpediente = p;
            objUpdateCronogramaEjecucionObraModel.IdCronograma = c;

            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
            objUpdateCronogramaEjecucionObraModel.NomProyecto = objProyectoInversion.Nombre;

            CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
            CronogramaEjecucionOP objCronogramaEjecucionOP = objCronogramaEjecucionObra_DAL.ObtieneXId(e, c);
            
            objUpdateCronogramaEjecucionObraModel.PlazoEjecucion = objCronogramaEjecucionOP.PlazoEjecucion;

            return View("Update",objUpdateCronogramaEjecucionObraModel);
        }

        public ActionResult EditActividad(int p, int e, int c, int a)
        {
            UpdateActividadCronogramaEjecucionModel objUpdateActividadCronogramaEjecucionModel = new UpdateActividadCronogramaEjecucionModel();
            objUpdateActividadCronogramaEjecucionModel.IdProyecto = p;
            objUpdateActividadCronogramaEjecucionModel.IdExpediente = p;
            objUpdateActividadCronogramaEjecucionModel.IdCronograma = c;

            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
            objUpdateActividadCronogramaEjecucionModel.NomProyecto = objProyectoInversion.Nombre;

            CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
            CronogramaEjecucionOP objCronogramaEjecucionOP = objCronogramaEjecucionObra_DAL.ObtieneXId(e, c);

            ActividadCronogramaOP objActividadCronogramaOP = objCronogramaEjecucionObra_DAL.ObtieneActvidadXId(c, a);

            objUpdateActividadCronogramaEjecucionModel.PlazoEjecucion = objCronogramaEjecucionOP.PlazoEjecucion;

            objUpdateActividadCronogramaEjecucionModel.IdActividad = objActividadCronogramaOP.IdActividad;
            objUpdateActividadCronogramaEjecucionModel.CantidadRRHHAct = objActividadCronogramaOP.CantidadRRHH;
            objUpdateActividadCronogramaEjecucionModel.CostoAct = objActividadCronogramaOP.Costo;
            objUpdateActividadCronogramaEjecucionModel.FechaFinEjecAct = objActividadCronogramaOP.FechaFinEjec.ToString("dd/MM/yyyy");
            objUpdateActividadCronogramaEjecucionModel.FechaFinProgAct = objActividadCronogramaOP.FechaFinProg.ToString("dd/MM/yyyy");
            objUpdateActividadCronogramaEjecucionModel.FechaIniEjecAct = objActividadCronogramaOP.FechaIniEjec.ToString("dd/MM/yyyy");
            objUpdateActividadCronogramaEjecucionModel.FechaIniProgAct = objActividadCronogramaOP.FechaIniProg.ToString("dd/MM/yyyy");

            if (objActividadCronogramaOP.IdArea.HasValue) {
                objUpdateActividadCronogramaEjecucionModel.IdAreaResponsable = objActividadCronogramaOP.IdArea.Value.ToString();
            }

            objUpdateActividadCronogramaEjecucionModel.ResponsableActTipo = objActividadCronogramaOP.IdTipoResponsable;

            //if (objActividadCronogramaOP.IdTipoResponsable == "E")
            //{
                objUpdateActividadCronogramaEjecucionModel.IdResponsablePersonaJuridica = objActividadCronogramaOP.IdEmpleado.ToString();

                var lstEmpleadosEmpresa = objCronogramaEjecucionObra_DAL.ObtieneEmpleadosPersonaJuridica();
                ViewBag.lstEmpleadosEmpresa = lstEmpleadosEmpresa;
            //}
            //else {
                objUpdateActividadCronogramaEjecucionModel.IdResponsablePersonaNatural = objActividadCronogramaOP.IdEmpleado.ToString();
                //if (objActividadCronogramaOP.IdArea.HasValue)
                //{
                if (objActividadCronogramaOP.IdArea.HasValue)
                {
                    var lstEmpleadosPersona = objCronogramaEjecucionObra_DAL.ObtieneEmpleadosPersonaNatural(objActividadCronogramaOP.IdArea.Value);
                    ViewBag.lstEmpleadosPersona = lstEmpleadosPersona;
                }
                //}
            //}
            objUpdateActividadCronogramaEjecucionModel.NomAct = objActividadCronogramaOP.Nombre;

            return View("UpdateActividad", objUpdateActividadCronogramaEjecucionModel);
        }

        [HttpPost]
        public ActionResult Save_Update(UpdateCronogramaEjecucionObraModel pObjModel)
        {
            var valid = TryUpdateModel(pObjModel);

            if (valid)
            {
                CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
                int intResultado = objCronogramaEjecucionObra_DAL.Actualiza(pObjModel.IdCronograma, pObjModel.IdExpediente, 
                    pObjModel.PlazoEjecucion);

                if (intResultado == 1)
                {
                    //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                    //return RedirectToAction("Index");
                }
                else if (intResultado == -997)
                {
                    ModelState.AddModelError("General", "No puede modificar el cronograma debido a que el proyecto está en estado ADJUDICADO.");
                }
                else
                {
                    valid = false;
                    ModelState.AddModelError("General", "No se pudo modificar el cronograma");
                }
            }
            return Json(new
            {
                Valid = valid,
                Errors = GetErrorsFromModelState()
            });
        }

        [HttpGet]
        public ActionResult CreateActividad(int p, int e, int c)
        {
            CreateActividadCronogramaEjecucionModel objCreateActividadCronogramaEjecucionModel = new CreateActividadCronogramaEjecucionModel();

            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);

            objCreateActividadCronogramaEjecucionModel.IdCronograma = c;
            objCreateActividadCronogramaEjecucionModel.IdExpediente = e;
            objCreateActividadCronogramaEjecucionModel.IdProyecto = p;
            objCreateActividadCronogramaEjecucionModel.NomProyecto = objProyectoInversion.Nombre;
            
            return View(objCreateActividadCronogramaEjecucionModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateActividad(CreateActividadCronogramaEjecucionModel pObjModel)
        {
            var valid = TryUpdateModel(pObjModel);

            if (valid)
            {
                try
                {
                    int intResultado = 1;
                    CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();

                    ActividadCronogramaOP objActividadCronogramaOP = new ActividadCronogramaOP();
                    objActividadCronogramaOP.CantidadRRHH = pObjModel.CantidadRRHHAct;
                    objActividadCronogramaOP.Costo = pObjModel.CostoAct;
                    objActividadCronogramaOP.Nombre = pObjModel.NomAct;
                    objActividadCronogramaOP.FechaFinEjec = Convert.ToDateTime(pObjModel.FechaFinEjecAct);
                    objActividadCronogramaOP.FechaFinProg = Convert.ToDateTime(pObjModel.FechaFinProgAct);
                    objActividadCronogramaOP.FechaIniEjec = Convert.ToDateTime(pObjModel.FechaIniEjecAct);
                    objActividadCronogramaOP.FechaIniProg = Convert.ToDateTime(pObjModel.FechaIniProgAct);
                    objActividadCronogramaOP.IdTipoResponsable = pObjModel.ResponsableActTipo;
                    
                    if (pObjModel.ResponsableActTipo == "P")
                    {
                        objActividadCronogramaOP.IdEmpleado = Convert.ToInt32(pObjModel.IdResponsablePersonaNatural);
                    }
                    else if (intResultado == -997)
                    {
                        ModelState.AddModelError("General", "No puede modificar el cronograma debido a que el proyecto está en estado ADJUDICADO.");
                    }
                    else
                    {
                        objActividadCronogramaOP.IdEmpleado = Convert.ToInt32(pObjModel.IdResponsablePersonaJuridica);
                    }

                   intResultado = objCronogramaEjecucionObra_DAL.InsertaActividad(pObjModel.IdExpediente, pObjModel.IdProyecto,
                        pObjModel.IdCronograma, objActividadCronogramaOP);

                    if (intResultado == 1)
                    {
                        //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        //return RedirectToAction("Index");
                    }
                    else
                    {
                        valid = false;
                        ModelState.AddModelError("General", "No se pudo insertar la actividad en el cronograma");
                    }
                }
                catch (Exception ex)
                {
                    valid = false;
                    ModelState.AddModelError("General", ex.ToString());
                }
            }

            return Json(new
            {
                Valid = valid,
                Errors = GetErrorsFromModelState()
            });
        }

        [HttpGet]
        public ActionResult UpdateActividad(int p, int e, int c, int a)
        {
            UpdateActividadCronogramaEjecucionModel objUpdateActividadCronogramaEjecucionModel = new UpdateActividadCronogramaEjecucionModel();

            objUpdateActividadCronogramaEjecucionModel.IdCronograma = c;
            objUpdateActividadCronogramaEjecucionModel.IdExpediente = e;
            objUpdateActividadCronogramaEjecucionModel.IdProyecto = p;
            return View(objUpdateActividadCronogramaEjecucionModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Save_UpdateActividad(UpdateActividadCronogramaEjecucionModel pObjModel)
        {
            var valid = TryUpdateModel(pObjModel);

            if (valid)
            {
                try
                {
                    int intResultado = 1;
                    CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();

                    ActividadCronogramaOP objActividadCronogramaOP = new ActividadCronogramaOP();
                    objActividadCronogramaOP.IdActividad = pObjModel.IdActividad;
                    objActividadCronogramaOP.Nombre = pObjModel.NomAct;
                    objActividadCronogramaOP.CantidadRRHH = pObjModel.CantidadRRHHAct;
                    objActividadCronogramaOP.Costo = pObjModel.CostoAct;
                    objActividadCronogramaOP.FechaFinEjec = Convert.ToDateTime(pObjModel.FechaFinEjecAct);
                    objActividadCronogramaOP.FechaFinProg = Convert.ToDateTime(pObjModel.FechaFinProgAct);
                    objActividadCronogramaOP.FechaIniEjec = Convert.ToDateTime(pObjModel.FechaIniEjecAct);
                    objActividadCronogramaOP.FechaIniProg = Convert.ToDateTime(pObjModel.FechaIniProgAct);
                    objActividadCronogramaOP.IdTipoResponsable = pObjModel.ResponsableActTipo;

                    if (pObjModel.ResponsableActTipo == "P")
                    {
                        objActividadCronogramaOP.IdEmpleado = Convert.ToInt32(pObjModel.IdResponsablePersonaNatural);
                    }
                    else if (intResultado == -997)
                    {
                        ModelState.AddModelError("General", "No puede modificar el cronograma debido a que el proyecto está en estado ADJUDICADO.");
                    }
                    else
                    {
                        objActividadCronogramaOP.IdEmpleado = Convert.ToInt32(pObjModel.IdResponsablePersonaJuridica);
                    }

                    intResultado = objCronogramaEjecucionObra_DAL.ActualizaActividad(pObjModel.IdExpediente,
                         pObjModel.IdCronograma, pObjModel.IdActividad, objActividadCronogramaOP);

                    if (intResultado == 1)
                    {
                        //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        //return RedirectToAction("Index");
                    }
                    else
                    {
                        valid = false;
                        ModelState.AddModelError("General", "No se pudo modificar la actividad");
                    }
                }
                catch (Exception ex)
                {
                    valid = false;
                    ModelState.AddModelError("General", ex.ToString());
                }
            }

            return Json(new
            {
                Valid = valid,
                Errors = GetErrorsFromModelState()
            });
        }

        public ActionResult Lista_EmpleadosPersonaNatural(int pIntIdArea)
        {
            CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();

            var lstEmpleados = objCronogramaEjecucionObra_DAL.ObtieneEmpleadosPersonaNatural(pIntIdArea).Select(x =>
                                                                           new SelectListItem
                                                                           {
                                                                               Value = x.Id.ToString(),
                                                                               Text = x.Nombre
                                                                           }).OrderBy(x => x.Text);

            return Json(lstEmpleados, JsonRequestBehavior.AllowGet);
        }
        
    }
}
