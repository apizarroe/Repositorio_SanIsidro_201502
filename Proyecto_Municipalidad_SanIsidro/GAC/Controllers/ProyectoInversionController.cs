using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GAC.Models.ProyectoInversion;
using Dominio.Core.Entities;
using Infraestructura.Data.SQL;

namespace GAC.Controllers
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

            ViewBag.MostrarSearch = "0";

            return View("Detail", objProyectoInversion);
        }

        public ActionResult Save_Update(UpdateProyectoInversionModel pObjModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();

                    int intResultado = objProyectoInversion_DAL.Actualiza(pObjModel.IdProyecto, pObjModel.CodSNIP, pObjModel.Descripcion,
                        pObjModel.Nombre, pObjModel.IdVia, pObjModel.Ubicacion, pObjModel.Beneficiarios, pObjModel.ValorReferencial, pObjModel.IdEstado);

                    if (intResultado == 1)
                    {
                        //TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        ViewBag.MsgSuccess = "Se realizó la operación satisfactoriamente";
                        //return RedirectToAction("Update");
                    }
                    else if (intResultado == -998)
                    {
                        ModelState.AddModelError("", "No se pueden actualizar los datos del proyecto debido a que ha cambiado de estado.");
                    }
                    else if (intResultado == -997)
                    {
                        ModelState.AddModelError("", "No se pueden actualizar los datos del proyecto debido a que el código SNIP ingresado ya existe para otro proyecto.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo actualizar el proyecto");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }
            ViewBag.MostrarSearch = "0";

            return View("Update", pObjModel);
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
