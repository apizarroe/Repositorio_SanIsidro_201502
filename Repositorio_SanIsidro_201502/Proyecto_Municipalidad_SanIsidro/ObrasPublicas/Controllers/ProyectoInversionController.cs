using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObrasPublicas.Models.ProyectoInversion;
using ObrasPublicas.Entities;
using ObrasPublicas.DAL;
using Infraestructura.Data.SQL;

namespace ObrasPublicas.Controllers
{
    public class ProyectoInversionController : Controller
    {

        //
        // GET: /ProyectoInversion/

        public ActionResult Index()
        {
            ViewBag.MsgSuccess = TempData["MsgSuccess"];
            ViewBag.Action = TempData["Action"];
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProyectoInversionModel pObjModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                    int intResultado = objProyectoInversion_DAL.Inserta(null, pObjModel.Descripcion,
                        pObjModel.Nombre, pObjModel.IdVia, pObjModel.Ubicacion, pObjModel.Beneficiarios, pObjModel.ValorReferencial);

                    if (intResultado == 1)
                    {
                        TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        return RedirectToAction("Index");
                        //ViewBag.MsgSuccess = "Se realizó la operación satisfactoriamente";
                    }
                    /*else if (intResultado == -998)
                    {
                        ModelState.AddModelError("", "El código SNIP " + pObjModel.CodSNIP+" ya ha sido registrado.");
                    }*/
                    else
                    {
                        ModelState.AddModelError("", "No se pudo insertar el proyecto");
                    }
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError("", ErrorCodeToString(999));
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            return View("Index", pObjModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Search(UpdateProyectoInversionModel pObjModel)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltro(pObjModel.SearchCodSNIP,
                pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
            ViewBag.lstProyectos = lstProyectos;
            ViewBag.Action = "UPD";

            return View("Update", pObjModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Search2(Models.ProyectoInversion.SearchProyectoInversionModel pObjModel)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();

            if (pObjModel.Tipo == "P")
            {
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltro(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                return View("Update", pObjModel);
            }
            else if (pObjModel.Tipo == "EC")
            {
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltroSinExpedientes(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                return View("~/Views/ExpedienteTecnicoOP/Index.aspx", pObjModel);
            }
            else if (pObjModel.Tipo == "EU")
            {
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltroConExpedientes(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                return View("~/Views/ExpedienteTecnicoOP/Search.aspx", pObjModel);
            }
            else if (pObjModel.Tipo == "CC")
            {
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltroSinCronograma(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                return View("~/Views/CronogramaEjecucionObra/Index.aspx", pObjModel);
            }
            else if (pObjModel.Tipo == "CU")
            {
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltroConCronograma(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                return View("~/Views/CronogramaEjecucionObra/Search.aspx", pObjModel);
            }
            else if (pObjModel.Tipo == "EMC")
            {
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltroConExpedientes(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                return View("~/Views/EntregaMaterialOP/Index.aspx", pObjModel);
            }
            else if (pObjModel.Tipo == "EMU")
            {
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltroConEntregaMaterial(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                return View("~/Views/EntregaMaterialOP/Search.aspx", pObjModel);
            }
            else {
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltro(pObjModel.SearchCodSNIP,
                pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                return View("Update", pObjModel);
            }
        }

        public ActionResult Edit(int id)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(id);

            if (objProyectoInversion.IdEstado != ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)
            {
                ViewBag.MsgError = "No puede modificar el proyecto debido a que se encuentra en estado " + objProyectoInversion.NomEstado.ToUpper();
                return Detail(objProyectoInversion.IdProyecto);
            }
            else {
                UpdateProyectoInversionModel objModel = new UpdateProyectoInversionModel();
                objModel.IdProyecto = objProyectoInversion.IdProyecto;
                objModel.CodSNIP = objProyectoInversion.CodSNIP;
                objModel.Descripcion = objProyectoInversion.Descripcion;
                objModel.Nombre = objProyectoInversion.Nombre;
                objModel.IdVia = objProyectoInversion.IdVia;
                objModel.Ubicacion = objProyectoInversion.Ubicacion;
                objModel.ValorReferencial = objProyectoInversion.ValorReferencial;
                objModel.Beneficiarios = objProyectoInversion.Beneficiarios;
                objModel.TipoVia = objProyectoInversion.TipoVia;
                objModel.IdEstado = objProyectoInversion.IdEstado;
                
                ViewBag.MostrarSearch = "0";

                return View("Update", objModel);
            }
        }

        public ActionResult Detail(int id)
        {
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(id);

            ViewBag.FromUpdate = TempData["FromUpdate"];
            ViewBag.MostrarSearch = "0";

            return View("Detail", objProyectoInversion);
        }

        public ActionResult Save_Update(UpdateProyectoInversionModel pObjModel)
        {
            bool bolGrabaOK = false; 
            if (ModelState.IsValid)
            {
                try
                {
                    ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();

                    int intResultado = objProyectoInversion_DAL.Actualiza(pObjModel.IdProyecto, pObjModel.CodSNIP, pObjModel.Descripcion,
                        pObjModel.Nombre, pObjModel.IdVia, pObjModel.Ubicacion, pObjModel.Beneficiarios, pObjModel.ValorReferencial, pObjModel.IdEstado);

                    if (intResultado == 1)
                    {
                        bolGrabaOK = true;
                        //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        ViewBag.MsgSuccess = "Se realizó la operación satisfactoriamente";
                        //return RedirectToAction("Update");
                    }
                    else if (intResultado == -998)
                    {
                        ViewBag.Error = "1";
                        ModelState.AddModelError("", "No se pueden actualizar los datos del proyecto debido a que ha cambiado de estado.");
                    }
                    else if (intResultado == -997)
                    {
                        ViewBag.Error = "1";
                        ModelState.AddModelError("", "No se pueden actualizar los datos del proyecto debido a que el código SNIP ingresado ya existe para otro proyecto.");
                    }
                    else
                    {
                        ViewBag.Error = "1";
                        ModelState.AddModelError("", "No se pudo actualizar el proyecto");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }
            ViewBag.MostrarSearch = "0";

            if (bolGrabaOK)
            {
                if (pObjModel.IdEstado == ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)
                {
                    return View("Update", pObjModel);
                }
                else {
                    TempData["FromUpdate"] = "1";
                    return Detail(pObjModel.IdProyecto);
                }
            }
            else {
                return View("Update", pObjModel);
            }
        }

        private static string ErrorCodeToString(Int16 pIntCodError)
        {
            switch (pIntCodError)
            {
                case 999:
                    return "El nombre de usuario ya existe. Escriba un nombre de usuario diferente.";
                default:
                    return "Error desconocido. Compruebe los datos especificados e inténtelo de nuevo. Si el problema continúa, póngase en contacto con el administrador del sistema.";
            }
        }


        public ActionResult Lista_NombreVias(String pStrTipoVia)
        {
            Via_DAL objVia_DAL = new Via_DAL();

            var lstNomVias = objVia_DAL.ObtieneVias(pStrTipoVia).Select(x =>
                                                                           new SelectListItem
                                                                           {
                                                                               Value = x.IdVia.ToString(),
                                                                               Text = x.Nombre
                                                                           }).OrderBy(x => x.Text);

            return Json(lstNomVias, JsonRequestBehavior.AllowGet);
        }
    }
}
