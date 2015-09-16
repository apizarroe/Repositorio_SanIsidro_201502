using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObrasPublicas.Entities;
using Infraestructura.Data.SQL;
using ObrasPublicas.DAL;
using ObrasPublicas.Models.InformeObra;

namespace ObrasPublicas.Controllers
{
    public class InformeObraController : BaseController
    {
        //
        // GET: /InformeObra/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create(int p, int e)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);

            CreateInformeObraModel objCreateInformeObraModel = new CreateInformeObraModel();
            objCreateInformeObraModel.IdProyecto = objProyectoInversion.IdProyecto;
            objCreateInformeObraModel.NomProyecto = objProyectoInversion.Nombre;
            objCreateInformeObraModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
            objCreateInformeObraModel.UbicacionProyecto = objProyectoInversion.TipoVia + " " + objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;
            
            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
            ExpedienteTecnicoOP objExpedienteTecnicoOP =  objExpedienteTecnicoOP_DAL.ObtieneXId(e);

            if(objExpedienteTecnicoOP!=null){
                if(String.IsNullOrWhiteSpace(objExpedienteTecnicoOP.EjecutorRazonSocial)){
                    objCreateInformeObraModel.NomEjecutor = objExpedienteTecnicoOP.EjecutorNom + " " + objExpedienteTecnicoOP.EjecutorApe;
                }
                else{
                    objCreateInformeObraModel.NomEjecutor = objExpedienteTecnicoOP.EjecutorRazonSocial;
                }
                objCreateInformeObraModel.NomSupervisor = objExpedienteTecnicoOP.SupervisorNom + " " + objExpedienteTecnicoOP.SupervisorApe;
            }

            EntregaMaterial_DAL objEntregaMaterial_DAL = new EntregaMaterial_DAL();
            ViewBag.lstEntregas = objEntregaMaterial_DAL.ObtieneEntregasXIdProyecto(p,1);

            CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
            ViewBag.lstActividades = objCronogramaEjecucionObra_DAL.ObtieneActvidades(e, -1,1);

            return View(objCreateInformeObraModel);
        }

        [HttpGet]
        public ActionResult Anular(int p, int e, int i)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);

            InformeObra_DAL objInformeObra_DAL = new InformeObra_DAL();

            List<InformeObra> lstInformes = objInformeObra_DAL.ObtieneXIdProyecto(p);

            int intResultado = objInformeObra_DAL.Anular(p, i, InformeObra.INT_ID_ESTADO_ANULADO);

            if (intResultado == 1)
            {
                ViewBag.OKAnular = "1";
                ViewBag.MsgAnular = "Se anuló el informe satisfactoriamente.";
            }
            else
            {
                ViewBag.OKAnular = "0";
                if (intResultado == -998)
                {
                    ViewBag.MsgAnular = "Se se pudo anular el informe debido a que el proyecto ya no se encuentra en estado ADJUDICADO.";
                }
                else {
                    ViewBag.MsgAnular = "No se pudo anular el informe.";
                }
            }

            ListadoInformeObraModel objListadoInformeObraModel = new ListadoInformeObraModel();
            objListadoInformeObraModel.IdProyecto = objProyectoInversion.IdProyecto;
            objListadoInformeObraModel.NomProyecto = objProyectoInversion.Nombre;
            objListadoInformeObraModel.IdExpediente = e;
            objListadoInformeObraModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
            objListadoInformeObraModel.UbicacionProyecto = objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;

            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
            ExpedienteTecnicoOP objExpedienteTecnicoOP = objExpedienteTecnicoOP_DAL.ObtieneXId(e);

            if (objExpedienteTecnicoOP != null)
            {
                if (String.IsNullOrWhiteSpace(objExpedienteTecnicoOP.EjecutorRazonSocial))
                {
                    objListadoInformeObraModel.NomEjecutor = objExpedienteTecnicoOP.EjecutorNom + " " + objExpedienteTecnicoOP.EjecutorApe;
                }
                else
                {
                    objListadoInformeObraModel.NomEjecutor = objExpedienteTecnicoOP.EjecutorRazonSocial;
                }
                objListadoInformeObraModel.NomSupervisor = objExpedienteTecnicoOP.SupervisorNom + " " + objExpedienteTecnicoOP.SupervisorApe;
            }

            ViewBag.ListadoInformes = lstInformes;

            return View("Listado",objListadoInformeObraModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInformeObraModel pObjModel)
        {
            var valid = TryUpdateModel(pObjModel);

            if (valid)
            {
                try
                {
                    InformeObra_DAL objInformeObra_DAL = new InformeObra_DAL();
                    int intResultado = objInformeObra_DAL.Inserta(pObjModel.IdProyecto, pObjModel.Titulo, pObjModel.Conclusion, pObjModel.Recomendacion, pObjModel.TipoInforme);

                    if (intResultado == 1)
                    {
                        //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        //return RedirectToAction("Index");
                    }
                    else if (intResultado == -998)
                    {
                        valid = false;
                        ModelState.AddModelError("General", "No puede crear el informe debido a que el proyecto no se encuentra en estado ADJUDICADO.");
                    }
                    else
                    {
                        valid = false;
                        ModelState.AddModelError("General", "No se pudo insertar el informe");
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

        public ActionResult Edit(int p, int e, int i)
        {
            UpdateInformeObraModel objModel = new UpdateInformeObraModel();
            objModel.IdProyecto = p;
            objModel.IdInforme = i;
            
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();

            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
            objModel.NomProyecto = objProyectoInversion.Nombre;
            objModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
            objModel.UbicacionProyecto = objProyectoInversion.TipoVia + " " + objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;

            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
            ExpedienteTecnicoOP objExpedienteTecnicoOP = objExpedienteTecnicoOP_DAL.ObtieneXId(e);

            if (objExpedienteTecnicoOP != null)
            {
                if (String.IsNullOrWhiteSpace(objExpedienteTecnicoOP.EjecutorRazonSocial))
                {
                    objModel.NomEjecutor = objExpedienteTecnicoOP.EjecutorNom + " " + objExpedienteTecnicoOP.EjecutorApe;
                }
                else
                {
                    objModel.NomEjecutor = objExpedienteTecnicoOP.EjecutorRazonSocial;
                }
                objModel.NomSupervisor = objExpedienteTecnicoOP.SupervisorNom + " " + objExpedienteTecnicoOP.SupervisorApe;
            }

            InformeObra_DAL objInformeObra_DAL = new InformeObra_DAL();

            InformeObra objInformeObra = objInformeObra_DAL.ObtieneXId(i);

            objModel.Conclusion = objInformeObra.Conclusion;
            objModel.IdInforme = objInformeObra.IdInforme;
            objModel.IdProyecto = objInformeObra.ProyectoInversion.IdProyecto;
            objModel.NomProyecto = objInformeObra.ProyectoInversion.Nombre;
            objModel.Recomendacion = objInformeObra.Recomendacion;
            objModel.TipoInforme = objInformeObra.TipoInforme;
            objModel.Titulo = objInformeObra.Titulo;
            objModel.NomEstado = objInformeObra.NomEstado;


            EntregaMaterial_DAL objEntregaMaterial_DAL = new EntregaMaterial_DAL();
            ViewBag.lstEntregas = objEntregaMaterial_DAL.ObtieneEntregasXIdInforme(i);

            CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new CronogramaEjecucionObra_DAL();
            ViewBag.lstActividades = objCronogramaEjecucionObra_DAL.ObtieneActvidadesXIdInforme(i);
            
            return View("Update", objModel);
        }

        [HttpPost]
        public ActionResult Save_Update(UpdateInformeObraModel pObjModel)
        {
            var valid = TryUpdateModel(pObjModel);

            if (valid)
            {
                InformeObra_DAL objInformeObra_DAL = new InformeObra_DAL();
                int intResultado = objInformeObra_DAL.Actualiza(pObjModel.IdInforme, pObjModel.IdProyecto,
                    pObjModel.Conclusion, pObjModel.Recomendacion);

                if (intResultado == 1)
                {
                    //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                    //return RedirectToAction("Index");
                }
                else if (intResultado == -998)
                {
                    valid = false;
                    ModelState.AddModelError("General", "No puede modificar el informe debido a que el proyecto no está en estado ADJUDICADO.");
                }
                else
                {
                    valid = false;
                    ModelState.AddModelError("General", "No se pudo modificar el informe");
                }
            }
            return Json(new
            {
                Valid = valid,
                Errors = GetErrorsFromModelState()
            });
        }

        public ActionResult Listado(int p, int e)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);

            InformeObra_DAL objInformeObra_DAL = new InformeObra_DAL();
            List<InformeObra> lstInformes = objInformeObra_DAL.ObtieneXIdProyecto(p);

            ListadoInformeObraModel objListadoInformeObraModel = new ListadoInformeObraModel();
            objListadoInformeObraModel.IdProyecto = objProyectoInversion.IdProyecto;
            objListadoInformeObraModel.IdExpediente = e;
            objListadoInformeObraModel.NomProyecto = objProyectoInversion.Nombre;
            objListadoInformeObraModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
            objListadoInformeObraModel.UbicacionProyecto = objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;

            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
            ExpedienteTecnicoOP objExpedienteTecnicoOP = objExpedienteTecnicoOP_DAL.ObtieneXId(e);

            if (objExpedienteTecnicoOP != null)
            {
                if (String.IsNullOrWhiteSpace(objExpedienteTecnicoOP.EjecutorRazonSocial))
                {
                    objListadoInformeObraModel.NomEjecutor = objExpedienteTecnicoOP.EjecutorNom + " " + objExpedienteTecnicoOP.EjecutorApe;
                }
                else
                {
                    objListadoInformeObraModel.NomEjecutor = objExpedienteTecnicoOP.EjecutorRazonSocial;
                }
                objListadoInformeObraModel.NomSupervisor = objExpedienteTecnicoOP.SupervisorNom + " " + objExpedienteTecnicoOP.SupervisorApe;
            }

            ViewBag.ListadoInformes = lstInformes;

            return View(objListadoInformeObraModel);
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}
