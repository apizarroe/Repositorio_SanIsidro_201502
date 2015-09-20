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
            try {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
                CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();

                if (objCronogramaEjecucionObra_DAL.ExisteCronograma(e))
                {
                    ModelState.AddModelError("Err_General2", "Ya existe un cronograma para el proyecto seleccionado.");
                    ViewBag.NoGrabar = "1";
                }
                CreateCronogramaEjecucionObraModel objCreateCronogramaEjecucionObraModel = new CreateCronogramaEjecucionObraModel();
                objCreateCronogramaEjecucionObraModel.IdProyecto = objProyectoInversion.IdProyecto;
                objCreateCronogramaEjecucionObraModel.NomProyecto = objProyectoInversion.Nombre;
                objCreateCronogramaEjecucionObraModel.IdExpediente = e;
                objCreateCronogramaEjecucionObraModel.ValorRefProyecto = objProyectoInversion.ValorReferencial;
                objCreateCronogramaEjecucionObraModel.ValorRefExpediente = objExpedienteTecnicoOP_DAL.ObtieneValorReferencialXIdExpediente(e);
                objCreateCronogramaEjecucionObraModel.UbicacionProyecto = objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;

                return View(objCreateCronogramaEjecucionObraModel);
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
        }

        public ActionResult Search(int id)
        {
            Models.ProyectoInversion.SearchProyectoInversionModel objSearchProyectoInversionModel = new Models.ProyectoInversion.SearchProyectoInversionModel();
            if (id == 1)
            {
                objSearchProyectoInversionModel.Tipo = "CV";
            }
            else {
                objSearchProyectoInversionModel.Tipo  = "CU";
            }
            return View(objSearchProyectoInversionModel);
        }

        public ActionResult Listado(int p, int e, int c)
        {
            try
            {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);

                CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
                CronogramaEjecucionOP objCronogramaEjecucionOP = objCronogramaEjecucionObra_DAL.ObtieneXId(e, c);

                if (objProyectoInversion == null)
                {
                    ViewBag.MensajeError = "El código de proyecto no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else if (objCronogramaEjecucionOP == null)
                {
                    ViewBag.MensajeError = "El código de expediente o cronograma no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else
                {
                    List<ActividadCronogramaOP> lstActividades = objCronogramaEjecucionObra_DAL.ObtieneActvidades(e, c, 0);

                    ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();

                    ListadoCronogramaEjecucionObraModel objListadoCronogramaEjecucionObraModel = new ListadoCronogramaEjecucionObraModel();
                    objListadoCronogramaEjecucionObraModel.IdProyecto = objProyectoInversion.IdProyecto;
                    objListadoCronogramaEjecucionObraModel.NomProyecto = objProyectoInversion.Nombre;
                    objListadoCronogramaEjecucionObraModel.IdExpediente = e;
                    objListadoCronogramaEjecucionObraModel.IdCronograma = c;
                    objListadoCronogramaEjecucionObraModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
                    objListadoCronogramaEjecucionObraModel.ValorRefExpediente = objExpedienteTecnicoOP_DAL.ObtieneValorReferencialXIdExpediente(e);
                    objListadoCronogramaEjecucionObraModel.UbicacionProyecto = objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;
                    objListadoCronogramaEjecucionObraModel.PlazoEjecucion = objCronogramaEjecucionOP.PlazoEjecucion;
                    objListadoCronogramaEjecucionObraModel.CostoProyecto = objProyectoInversion.CostoProyecto;

                    ViewBag.OKEliminar = TempData["OKEliminar"];
                    ViewBag.MsgEliminar = TempData["MsgEliminar"];

                    ViewBag.ListadoActividades = lstActividades;
                    return View(objListadoCronogramaEjecucionObraModel);
                }
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
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
                        valid = false;
                        ModelState.AddModelError("General", "No puede crear el cronograma debido a que el proyecto no está en estado VIABLE.");
                    }
                    else if (intResultado == -996)
                    {
                        valid = false;
                        ModelState.AddModelError("General", "Ya existe un cronograma para el proyecto seleccionado.");
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
            try
            {
                UpdateCronogramaEjecucionObraModel objUpdateCronogramaEjecucionObraModel = new UpdateCronogramaEjecucionObraModel();
                objUpdateCronogramaEjecucionObraModel.IdProyecto = p;
                objUpdateCronogramaEjecucionObraModel.IdExpediente = p;
                objUpdateCronogramaEjecucionObraModel.IdCronograma = c;

                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
                objUpdateCronogramaEjecucionObraModel.NomProyecto = objProyectoInversion.Nombre;
                objUpdateCronogramaEjecucionObraModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
                objUpdateCronogramaEjecucionObraModel.UbicacionProyecto = objProyectoInversion.TipoVia + " " + objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;

                CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
                CronogramaEjecucionOP objCronogramaEjecucionOP = objCronogramaEjecucionObra_DAL.ObtieneXId(e, c);

                objUpdateCronogramaEjecucionObraModel.PlazoEjecucion = objCronogramaEjecucionOP.PlazoEjecucion;

                return View("Update", objUpdateCronogramaEjecucionObraModel);
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
        }

        public ActionResult EditActividad(int p, int e, int c, int a)
        {
            try
            {
                UpdateActividadCronogramaEjecucionModel objUpdateActividadCronogramaEjecucionModel = new UpdateActividadCronogramaEjecucionModel();
                objUpdateActividadCronogramaEjecucionModel.IdProyecto = p;
                objUpdateActividadCronogramaEjecucionModel.IdExpediente = e;
                objUpdateActividadCronogramaEjecucionModel.IdCronograma = c;

                ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();

                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
                CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();

                CronogramaEjecucionOP objCronogramaEjecucionOP = objCronogramaEjecucionObra_DAL.ObtieneXId(e, c);
                ActividadCronogramaOP objActividadCronogramaOP = objCronogramaEjecucionObra_DAL.ObtieneActvidadXId(c, a);

                if (objProyectoInversion == null)
                {
                    ViewBag.MensajeError = "El código de proyecto no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else if (objCronogramaEjecucionOP == null)
                {
                    ViewBag.MensajeError = "El código de expediente o de cronograma no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else if (objActividadCronogramaOP == null)
                {
                    ViewBag.MensajeError = "El código de actividad no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else
                {
                    objUpdateActividadCronogramaEjecucionModel.NomProyecto = objProyectoInversion.Nombre;
                    objUpdateActividadCronogramaEjecucionModel.UbicacionProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
                    objUpdateActividadCronogramaEjecucionModel.ValorRefProyecto = objProyectoInversion.TipoVia + " " + objProyectoInversion.TipoVia + " " + objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;
                    objUpdateActividadCronogramaEjecucionModel.CostoProyecto = objProyectoInversion.CostoProyecto;
                    objUpdateActividadCronogramaEjecucionModel.ValorRefExpediente = objExpedienteTecnicoOP_DAL.ObtieneValorReferencialXIdExpediente(e);

                    objUpdateActividadCronogramaEjecucionModel.PlazoEjecucion = objCronogramaEjecucionOP.PlazoEjecucion;

                    objUpdateActividadCronogramaEjecucionModel.IdActividad = objActividadCronogramaOP.IdActividad;
                    objUpdateActividadCronogramaEjecucionModel.CantidadRRHHAct = objActividadCronogramaOP.CantidadRRHH;
                    objUpdateActividadCronogramaEjecucionModel.CostoAct = objActividadCronogramaOP.Costo;

                    if (objActividadCronogramaOP.FechaFinEjec.HasValue)
                    {
                        objUpdateActividadCronogramaEjecucionModel.FechaFinEjecAct = objActividadCronogramaOP.FechaFinEjec.Value.ToString("dd/MM/yyyy");
                    }
                    objUpdateActividadCronogramaEjecucionModel.FechaFinProgAct = objActividadCronogramaOP.FechaFinProg.ToString("dd/MM/yyyy");
                    if (objActividadCronogramaOP.FechaIniEjec.HasValue)
                    {
                        objUpdateActividadCronogramaEjecucionModel.FechaIniEjecAct = objActividadCronogramaOP.FechaIniEjec.Value.ToString("dd/MM/yyyy");
                    }
                    objUpdateActividadCronogramaEjecucionModel.FechaIniProgAct = objActividadCronogramaOP.FechaIniProg.ToString("dd/MM/yyyy");

                    if (objActividadCronogramaOP.IdArea.HasValue)
                    {
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
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
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
                else if (intResultado == -998)
                {
                    valid = false;
                    ModelState.AddModelError("General", "No puede modificar el cronograma debido a que el proyecto está en estado VIABLE.");
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
            try
            {
                CreateActividadCronogramaEjecucionModel objCreateActividadCronogramaEjecucionModel = new CreateActividadCronogramaEjecucionModel();

                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);

                CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
                CronogramaEjecucionOP objCronogramaEjecucionOP = objCronogramaEjecucionObra_DAL.ObtieneXId(e, c);

                if (objProyectoInversion == null)
                {
                    ViewBag.MensajeError = "El código de proyecto no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else if (c!=0 && objCronogramaEjecucionOP == null)
                {
                    ViewBag.MensajeError = "El código de expediente o de cronograma no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else
                {
                    ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();

                    objCreateActividadCronogramaEjecucionModel.IdCronograma = c;
                    objCreateActividadCronogramaEjecucionModel.IdExpediente = e;
                    objCreateActividadCronogramaEjecucionModel.IdProyecto = p;
                    objCreateActividadCronogramaEjecucionModel.NomProyecto = objProyectoInversion.Nombre;
                    objCreateActividadCronogramaEjecucionModel.ValorRefProyecto = objProyectoInversion.ValorReferencial;
                    objCreateActividadCronogramaEjecucionModel.UbicacionProyecto = objProyectoInversion.TipoVia + " " + objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;
                    objCreateActividadCronogramaEjecucionModel.ValorRefExpediente = objExpedienteTecnicoOP_DAL.ObtieneValorReferencialXIdExpediente(e);
                    //objCreateActividadCronogramaEjecucionModel.FechaIniActividad = objCronogramaEjecucionObra_DAL.Obtiene_MinFechaXCronograma(c,e);
                    objCreateActividadCronogramaEjecucionModel.CostoProyecto = objProyectoInversion.CostoProyecto;
                    return View(objCreateActividadCronogramaEjecucionModel);
                }
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
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
                    //objActividadCronogramaOP.FechaFinEjec = Convert.ToDateTime(pObjModel.FechaFinEjecAct);
                    objActividadCronogramaOP.FechaFinProg = Convert.ToDateTime(pObjModel.FechaFinProgAct);
                    //objActividadCronogramaOP.FechaIniEjec = Convert.ToDateTime(pObjModel.FechaIniEjecAct);
                    objActividadCronogramaOP.FechaIniProg = Convert.ToDateTime(pObjModel.FechaIniProgAct);
                    objActividadCronogramaOP.IdTipoResponsable = pObjModel.ResponsableActTipo;
                    
                    if (pObjModel.ResponsableActTipo == "P")
                    {
                        objActividadCronogramaOP.IdEmpleado = Convert.ToInt32(pObjModel.IdResponsablePersonaNatural);
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
                        if (intResultado == -995)
                        {
                            valid = false;
                            ModelState.AddModelError("General", "El nuevo costo de la actividad hará superar el valor referencial establecido para el proyecto.");
                        }
                        else if (intResultado == -997)
                        {
                            valid = false;
                            ModelState.AddModelError("General", "No puede modificar el cronograma debido a que el proyecto no está en estado VIABLE / ADJUDICADO.");
                        }
                        else {
                            valid = false;
                            ModelState.AddModelError("General", "No se pudo insertar la actividad en el cronograma");                        
                        }
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
            try
            {
                CreateActividadCronogramaEjecucionModel objCreateActividadCronogramaEjecucionModel = new CreateActividadCronogramaEjecucionModel();

                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);

                CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
                CronogramaEjecucionOP objCronogramaEjecucionOP = objCronogramaEjecucionObra_DAL.ObtieneXId(e, c);

                if (objProyectoInversion == null)
                {
                    ViewBag.MensajeError = "El código de proyecto no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else if (objCronogramaEjecucionOP == null)
                {
                    ViewBag.MensajeError = "El código de expediente o de cronograma no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else
                {
                    UpdateActividadCronogramaEjecucionModel objUpdateActividadCronogramaEjecucionModel = new UpdateActividadCronogramaEjecucionModel();

                    objUpdateActividadCronogramaEjecucionModel.IdCronograma = c;
                    objUpdateActividadCronogramaEjecucionModel.IdExpediente = e;
                    objUpdateActividadCronogramaEjecucionModel.IdProyecto = p;
                    objUpdateActividadCronogramaEjecucionModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
                    objUpdateActividadCronogramaEjecucionModel.UbicacionProyecto = objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;

                    return View(objUpdateActividadCronogramaEjecucionModel);
                }
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
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
                    else
                    {
                        objActividadCronogramaOP.IdEmpleado = Convert.ToInt32(pObjModel.IdResponsablePersonaJuridica);
                    }

                    intResultado = objCronogramaEjecucionObra_DAL.ActualizaActividad(pObjModel.IdCronograma, pObjModel.IdProyecto,
                         pObjModel.IdExpediente, pObjModel.IdActividad, objActividadCronogramaOP);

                    if (intResultado == 1)
                    {
                        //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        //return RedirectToAction("Index");
                    }
                    else if (intResultado == -997)
                    {
                        valid = false;
                        ModelState.AddModelError("General", "No puede modificar el cronograma debido a que el proyecto no está en estado VIABLE.");
                    }
                    else if (intResultado == -995)
                    {
                        valid = false;
                        ModelState.AddModelError("General", "El nuevo costo de la actividad hará superar el valor referencial establecido para el proyecto.");
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

        public ActionResult DeleteActividad(int p, int e, int c, int a)
        {

            try
            {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);

                CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();

                CronogramaEjecucionOP objCronogramaEjecucionOP = objCronogramaEjecucionObra_DAL.ObtieneXId(e, c);

                ListadoCronogramaEjecucionObraModel objListadoCronogramaEjecucionObraModel = new ListadoCronogramaEjecucionObraModel();
                objListadoCronogramaEjecucionObraModel.IdProyecto = objProyectoInversion.IdProyecto;
                objListadoCronogramaEjecucionObraModel.NomProyecto = objProyectoInversion.Nombre;
                objListadoCronogramaEjecucionObraModel.IdExpediente = e;
                objListadoCronogramaEjecucionObraModel.IdCronograma = c;
                objListadoCronogramaEjecucionObraModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
                objListadoCronogramaEjecucionObraModel.UbicacionProyecto = objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;
                objListadoCronogramaEjecucionObraModel.PlazoEjecucion = objCronogramaEjecucionOP.PlazoEjecucion;

                int intResultado = objCronogramaEjecucionObra_DAL.EliminarActividad(c, e, a);

                if (intResultado == 1)
                {
                    TempData["OKEliminar"] = "1";
                    TempData["MsgEliminar"] = "Se eliminó la actividad satisfactoriamente.";
                }
                else
                {
                    ViewBag.OKEliminar = "0";
                    if (intResultado == -998)
                    {
                        TempData["MsgEliminar"] = "No puede eliminar la actividad debido a que el proyecto no está en estado VIABLE.";
                    }
                    else
                    {
                        TempData["MsgEliminar"] = "No se pudo eliminar la actividad.";
                    }
                }

                //return Listado(p, e, c);
                return RedirectToAction("Listado", new { p = p, e = e, c = c });
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
        }
        
    }
}
