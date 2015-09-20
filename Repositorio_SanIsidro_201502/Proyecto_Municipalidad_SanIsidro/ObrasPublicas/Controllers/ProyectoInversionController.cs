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
            try
            {
                ViewBag.MsgSuccess = TempData["MsgSuccess"];
                ViewBag.Action = TempData["Action"];

                Infraestructura.Data.SQL.Via_DAL objVia_DAL = new Infraestructura.Data.SQL.Via_DAL();
                var lstVias = objVia_DAL.ObtieneVias(null).Select(v => v.Tipo).Distinct();

                ViewBag.ListaVias = lstVias;

                return View();
            }
            catch (Exception ex) {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
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
            try
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

                Infraestructura.Data.SQL.Via_DAL objVia_DAL = new Infraestructura.Data.SQL.Via_DAL();
                var lstVias = objVia_DAL.ObtieneVias(null).Select(v => v.Tipo).Distinct();

                ViewBag.ListaVias = lstVias;

                return View("Index", pObjModel);
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
        public ActionResult Search(UpdateProyectoInversionModel pObjModel)
        {
            try
            {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltro(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                ViewBag.lstProyectos = lstProyectos;
                ViewBag.Action = "UPD";

                return View("Update", pObjModel);
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
        public ActionResult Search2(Models.ProyectoInversion.SearchProyectoInversionModel pObjModel)
        {
            try
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
                else if (pObjModel.Tipo == "CV")
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
                else if (pObjModel.Tipo == "CIO")
                {
                    //SOLO ADJUDICADOS
                    List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltroConExpedientes(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                    ViewBag.lstProyectos = lstProyectos;
                    return View("~/Views/informeobra/Index.aspx", pObjModel);
                }
                else if (pObjModel.Tipo == "UIO")
                {
                    List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltroConInformesObra(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                    ViewBag.lstProyectos = lstProyectos;
                    return View("~/Views/informeobra/search.aspx", pObjModel);
                }
                else
                {
                    List<ProyectoInversion> lstProyectos = objProyectoInversion_DAL.BuscarXFiltro(pObjModel.SearchCodSNIP,
                    pObjModel.SearchNombre, pObjModel.SearchUbicacion, pObjModel.SearchIdEstado);
                    ViewBag.lstProyectos = lstProyectos;
                    return View("Update", pObjModel);
                }
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
        }

        public ActionResult Edit(int id)
        {
            try {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(id);

                if (objProyectoInversion == null)
                {
                    ViewBag.MensajeError = "El código de proyecto no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else
                {
                    if (objProyectoInversion.IdEstado != ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)
                    {
                        if (TempData["CambioViable"]!=null && TempData["CambioViable"].ToString() != "1")
                        {
                            ViewBag.MsgError = "No puede modificar el proyecto debido a que se encuentra en estado " + objProyectoInversion.NomEstado.ToUpper();
                        }
                        return Detail(objProyectoInversion.IdProyecto);
                    }
                    else
                    {
                        UpdateProyectoInversionModel objModel = new UpdateProyectoInversionModel();
                        objModel.IdProyecto = objProyectoInversion.IdProyecto;
                        objModel.CodSNIP = objProyectoInversion.CodSNIP;
                        objModel.Descripcion = objProyectoInversion.Descripcion;
                        objModel.Nombre = objProyectoInversion.Nombre;
                        objModel.IdVia = objProyectoInversion.IdVia;
                        objModel.NomVia = objProyectoInversion.NomVia;
                        objModel.Ubicacion = objProyectoInversion.Ubicacion;
                        objModel.ValorReferencial = objProyectoInversion.ValorReferencial;
                        objModel.Beneficiarios = objProyectoInversion.Beneficiarios;
                        objModel.TipoVia = objProyectoInversion.TipoVia;
                        objModel.IdEstado = objProyectoInversion.IdEstado;

                        ViewBag.MostrarSearch = "0";

                        ViewBag.MsgSuccess = TempData["MsgSuccess"];

                        return View("Update", objModel);
                    }
                }
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
        }

        public ActionResult Detail(int id)
        {
            try {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(id);

                if (objProyectoInversion == null)
                {
                    ViewBag.MensajeError = "El código de proyecto no existe";
                    return View("~/Views/Shared/ErrorInterno.aspx");
                }
                else
                {
                    ViewBag.FromUpdate = TempData["FromUpdate"];
                    ViewBag.MostrarSearch = "0";

                    return View("Detail", objProyectoInversion);
                }
            }
            catch (Exception ex)
            {
                String strMensaje = Helpers.ErrorHelper.ObtieneMensajeXException(ex);
                ViewBag.MensajeError = strMensaje;
                return View("~/Views/Shared/ErrorInterno.aspx");
            }
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
                        TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        //ViewBag.MsgSuccess = "Se realizó la operación satisfactoriamente";
                        TempData["CambioViable"] = "1";
                        return RedirectToAction("Edit", new { id = pObjModel.IdProyecto });

                        //return Edit(
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

        public ActionResult Lista_NombreVias(String pStrTipoVia)
        {
            try
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
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FiltrarVias(String pStrTipoVia, String pStrDesc)
        {
            try
            {
                Via_DAL objVia_DAL = new Via_DAL();

                var lstNomVias = objVia_DAL.ObtieneVias(pStrTipoVia).Where(x => (x.Tipo + " " + x.Nombre).ToLower().Contains(pStrDesc.ToLower())).OrderBy(x => x.Nombre);

                return Json(lstNomVias, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
