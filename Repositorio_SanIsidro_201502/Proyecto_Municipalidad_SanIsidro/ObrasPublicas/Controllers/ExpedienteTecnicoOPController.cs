using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObrasPublicas.Models.ExpedienteTecnicoOP;
using ObrasPublicas.Entities;
using Infraestructura.Data.SQL;
using ObrasPublicas.DAL;
using Dominio.Core.Entities;

namespace ObrasPublicas.Controllers
{
    public class ExpedienteTecnicoOPController : BaseController
    {
        const String STR_DOCUMENTOS_EXPEDIENTE_OP = "SES_DOCUMENTOS_EXPEDIENTE_OP";
        double dblMaxSizeFileUpload = 10 * 1024 * 1024;

        public ActionResult Index()
        {
            ViewBag.MsgSuccess = TempData["MsgSuccess"];
            ViewBag.Action = TempData["Action"];
            return View();
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            Session[STR_DOCUMENTOS_EXPEDIENTE_OP] = null;
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(id);
            CreateExpedienteTecnicoOPModel objCreateExpedienteTecnicoOPModel = new CreateExpedienteTecnicoOPModel();
            objCreateExpedienteTecnicoOPModel.IdProyecto = objProyectoInversion.IdProyecto;
            objCreateExpedienteTecnicoOPModel.NomProyecto = objProyectoInversion.Nombre;
            objCreateExpedienteTecnicoOPModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
            objCreateExpedienteTecnicoOPModel.ValorReferencial = objProyectoInversion.ValorReferencial;
            objCreateExpedienteTecnicoOPModel.UbicacionProyecto = objProyectoInversion.TipoVia + " " + objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;

            return View(objCreateExpedienteTecnicoOPModel);
        }

        [HttpGet]
        public ActionResult Update()
        {
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateExpedienteTecnicoOPModel pObjModel, HttpPostedFileBase documentoUpload)
        {
            if (pObjModel.TipoBotonClick == "ADJUNTAR")
            {
                if (ModelState.IsValid)
                {
                    if (documentoUpload == null)
                    {
                        ModelState.AddModelError("Err_documentoUpload", "Seleccione un archivo.");
                    }
                    else if (documentoUpload.ContentLength > dblMaxSizeFileUpload)
                    {
                        ModelState.AddModelError("Err_documentoUpload", "El archivo supera el tamaño máximo permitido (10 MB).");
                    }
                    else
                    {
                        DocumentoExpTecOPModel objDocumentoExpTecOP = new DocumentoExpTecOPModel();
                        List<DocumentoExpedienteTecnicoOP> lstDocumentos = (List<DocumentoExpedienteTecnicoOP>)Session[STR_DOCUMENTOS_EXPEDIENTE_OP];

                        byte[] fileBytes = null;
                        String strNomArchivo = "";

                        if (lstDocumentos == null)
                        {
                            lstDocumentos = new List<DocumentoExpedienteTecnicoOP>();
                        }

                        DocumentoExpedienteTecnicoOP objDocumento = new DocumentoExpedienteTecnicoOP();

                        if (documentoUpload != null)
                        {
                            if ((documentoUpload != null) && (documentoUpload.ContentLength > 0) && !string.IsNullOrEmpty(documentoUpload.FileName))
                            {
                                strNomArchivo = documentoUpload.FileName;
                                string fileContentType = documentoUpload.ContentType;
                                fileBytes = new byte[documentoUpload.ContentLength];
                                documentoUpload.InputStream.Read(fileBytes, 0, Convert.ToInt32(documentoUpload.ContentLength));
                            }
                        }

                        ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
                        var lstTipos = objExpedienteTecnicoOP_DAL.ObtieneTiposDocumento(null);

                        objDocumento.Descripcion = pObjModel.DescripcionDocAdj;
                        objDocumento.FechaEmision = Convert.ToDateTime(pObjModel.FechaEmisionDocAdj);
                        objDocumento.NroDocumento = pObjModel.NroDocumentoAdj;
                        objDocumento.TipoDocumento = pObjModel.TipoDocmentoDocAdj;
                        objDocumento.NomTipoDocumento = lstTipos.Where(doc => doc.Id == pObjModel.TipoDocmentoDocAdj).First().Nombre;
                        objDocumento.Archivo = fileBytes;
                        objDocumento.NomArchivo = strNomArchivo;
                        objDocumento.RutaArchivo = Request.Url.GetLeftPart(UriPartial.Authority) + "/files/obras_publicas/docs_tecnicos/" + strNomArchivo;
                        lstDocumentos.Add(objDocumento);
                        Session[STR_DOCUMENTOS_EXPEDIENTE_OP] = lstDocumentos;
                    }
                }
            }
            else if (pObjModel.TipoBotonClick == "REMOVER")
            {
                List<DocumentoExpedienteTecnicoOP> lstDocumentos = (List<DocumentoExpedienteTecnicoOP>)Session[STR_DOCUMENTOS_EXPEDIENTE_OP];

                if (lstDocumentos != null)
                {
                    lstDocumentos.RemoveAt(Convert.ToInt32(pObjModel.IdDocumentoEliminar));
                }
                Session[STR_DOCUMENTOS_EXPEDIENTE_OP] = lstDocumentos;
            }
            else {

                if (ModelState.IsValid)
                {
                    try
                    {
                        List<DocumentoExpedienteTecnicoOP> lstDocumentos = (List<DocumentoExpedienteTecnicoOP>)Session[STR_DOCUMENTOS_EXPEDIENTE_OP];

                        if (lstDocumentos == null || lstDocumentos.Count == 0)
                        {
                            ModelState.AddModelError("", "Debe adjuntar al menos un archivo.");
                        }
                        else {

                            String strRutaFiles = Server.MapPath(@"\files");
                            String strRutaObrasPublicas = Server.MapPath(@"\files") + @"\obras_publicas";
                            String strRutaDocsTecnicos = Server.MapPath(@"\files") + @"\obras_publicas\docs_tecnicos";
                            if (!System.IO.Directory.Exists(strRutaFiles))
                            {
                                System.IO.Directory.CreateDirectory(strRutaFiles);
                            }
                            if (!System.IO.Directory.Exists(strRutaObrasPublicas))
                            {
                                System.IO.Directory.CreateDirectory(strRutaObrasPublicas);
                            }
                            if (!System.IO.Directory.Exists(strRutaDocsTecnicos))
                            {
                                System.IO.Directory.CreateDirectory(strRutaDocsTecnicos);
                            }
                            foreach (var objDoc in lstDocumentos)
                            {
                                System.IO.File.WriteAllBytes(strRutaDocsTecnicos + @"\" + objDoc.NomArchivo, objDoc.Archivo);
                            }

                            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
                            int intResultado = objExpedienteTecnicoOP_DAL.Inserta(pObjModel.IdProyecto, pObjModel.Descripcion, pObjModel.Especificaciones,
                                pObjModel.ValorReferencial, pObjModel.TipoEjecutor, pObjModel.EjecutorNom,
                                pObjModel.EjecutorApe, pObjModel.EjecutorRazonSocial, pObjModel.ContactoNom, pObjModel.ContactoApe,
                                pObjModel.ContactoEmail, pObjModel.ContactoTelefono, pObjModel.ContactoDireccion, pObjModel.SupervisorNom,
                                pObjModel.SupervisorApe, pObjModel.SupervisorTelefono, pObjModel.SupervisorEmail, lstDocumentos);

                            if (intResultado == 1)
                            {
                                //Session[STR_DOCUMENTOS_EXPEDIENTE_OP] = null;
                                ViewBag.MsgSuccess = "Se realizó la operación satisfactoriamente";
                                //return RedirectToAction("Create",);
                            }
                            else if (intResultado == -998)
                            {
                                ModelState.AddModelError("", "No puede registrar el expediente porque el proyecto ya tiene asociado uno.");
                            }
                            else if (intResultado == -997)
                            {
                                ModelState.AddModelError("", "No puede registrar el expediente debido a que el proyecto está en estado ADJUDICADO.");
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo insertar el expediente");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.ToString());
                    }
                }
            }
            
            //return Json("", JsonRequestBehavior.AllowGet);
            return View(pObjModel);
        }

        public ActionResult Edit(int id)
        {
            //id = IdExpediente
            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ExpedienteTecnicoOP objExpedienteTecnicoOP = objExpedienteTecnicoOP_DAL.ObtieneXId(id);

            //if (objProyectoInversion.IdEstado != ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)
            //{
            //    ViewBag.MsgError = "No puede modificar el proyecto debido a que se encuentra en estado " + objProyectoInversion.NomEstado.ToUpper();
            //    return Detail(objProyectoInversion.IdProyecto);
            //}
            //else
            //{
                UpdateExpedienteTecnicoOPModel objModel = new UpdateExpedienteTecnicoOPModel();
                objModel.IdProyecto = objExpedienteTecnicoOP.Proyecto.IdProyecto;
                objModel.NomProyecto = objExpedienteTecnicoOP.Proyecto.Nombre;
                ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(id);
                objModel.ValorRefProyecto = objProyectoInversion.ValorReferencial.ToString("#,##0.00");
                objModel.UbicacionProyecto = objProyectoInversion.TipoVia + " " + objProyectoInversion.NomVia + " " + objProyectoInversion.Ubicacion;

                objModel.Descripcion = objExpedienteTecnicoOP.Descripcion;
                objModel.Especificaciones =objExpedienteTecnicoOP.Especificaciones;
                objModel.ValorReferencial = objExpedienteTecnicoOP.ValorReferencial;
                objModel.IdExpediente = objExpedienteTecnicoOP.IdExpediente;
                //objModel.PartidaPresupuestaria = objExpedienteTecnicoOP.PartidaPresupuestaria;
                objModel.TipoEjecutor = objExpedienteTecnicoOP.TipoEjecutor;
                objModel.IdEjecutor = objExpedienteTecnicoOP.EjecutorId;
                objModel.EjecutorNom = objExpedienteTecnicoOP.EjecutorNom;
                objModel.EjecutorApe = objExpedienteTecnicoOP.EjecutorApe;
                objModel.EjecutorRazonSocial = objExpedienteTecnicoOP.EjecutorRazonSocial;
                objModel.IdContacto = objExpedienteTecnicoOP.ContactoId;
                objModel.ContactoNom = objExpedienteTecnicoOP.ContactoNom;
                objModel.ContactoApe = objExpedienteTecnicoOP.ContactoApe;
                objModel.ContactoTelefono = objExpedienteTecnicoOP.ContactoTelefono;
                objModel.ContactoEmail = objExpedienteTecnicoOP.ContactoEmail;
                objModel.ContactoDireccion = objExpedienteTecnicoOP.ContactoDireccion;
                objModel.IdSupervisor = objExpedienteTecnicoOP.SupervisorId;
                objModel.SupervisorNom = objExpedienteTecnicoOP.SupervisorNom;
                objModel.SupervisorApe = objExpedienteTecnicoOP.SupervisorApe;
                objModel.SupervisorTelefono = objExpedienteTecnicoOP.SupervisorTelefono;
                objModel.SupervisorEmail = objExpedienteTecnicoOP.SupervisorEmail;

                Session[STR_DOCUMENTOS_EXPEDIENTE_OP] = objExpedienteTecnicoOP.Documentos;

                ViewBag.MostrarSearch = "0";

                return View("Update", objModel);
            //}
        }

        public ActionResult Detail(int id)
        {
            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
            ExpedienteTecnicoOP objExpedienteTecnicoOP = objExpedienteTecnicoOP_DAL.ObtieneXId(id);

            ViewBag.FromUpdate = TempData["FromUpdate"];
            ViewBag.MostrarSearch = "0";

            return View("Detail", objExpedienteTecnicoOP);
        }

        [HttpPost]
        public ActionResult Save_Update(UpdateExpedienteTecnicoOPModel pObjModel, HttpPostedFileBase documentoUpload)
        {
            if (pObjModel.TipoBotonClick == "ADJUNTAR")
            {
                if (ModelState.IsValid)
                {
                    //DocumentoExpTecOPModel objDocumentoExpTecOP = new DocumentoExpTecOPModel();
                    List<DocumentoExpedienteTecnicoOP> lstDocumentos = (List<DocumentoExpedienteTecnicoOP>)Session[STR_DOCUMENTOS_EXPEDIENTE_OP];

                    if (lstDocumentos == null)
                    {
                        lstDocumentos = new List<DocumentoExpedienteTecnicoOP>();
                    }
                    else if (documentoUpload.ContentLength > dblMaxSizeFileUpload)
                    {
                        ModelState.AddModelError("Err_documentoUpload", "El archivo supera el tamaño máximo permitido (10 MB).");
                    }
                    else {
                        DocumentoExpedienteTecnicoOP objDocumento = new DocumentoExpedienteTecnicoOP();

                        if (documentoUpload != null)
                        {
                            byte[] fileBytes = null;
                            String strNomArchivo = "";

                            if ((documentoUpload != null) && (documentoUpload.ContentLength > 0) && !string.IsNullOrEmpty(documentoUpload.FileName))
                            {
                                strNomArchivo = documentoUpload.FileName;
                                string fileContentType = documentoUpload.ContentType;
                                fileBytes = new byte[documentoUpload.ContentLength];
                                documentoUpload.InputStream.Read(fileBytes, 0, Convert.ToInt32(documentoUpload.ContentLength));
                            }

                            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();
                            var lstTipos = objExpedienteTecnicoOP_DAL.ObtieneTiposDocumento(null);

                            objDocumento.Descripcion = pObjModel.DescripcionDocAdj;
                            objDocumento.FechaEmision = Convert.ToDateTime(pObjModel.FechaEmisionDocAdj);
                            objDocumento.NroDocumento = pObjModel.NroDocumentoAdj;
                            objDocumento.TipoDocumento = pObjModel.TipoDocmentoDocAdj;
                            objDocumento.NomTipoDocumento = lstTipos.Where(doc => doc.Id == pObjModel.TipoDocmentoDocAdj).First().Nombre;
                            objDocumento.Archivo = fileBytes;
                            objDocumento.NomArchivo = strNomArchivo;
                            objDocumento.RutaArchivo = Request.Url.GetLeftPart(UriPartial.Authority) + "/files/obras_publicas/docs_tecnicos/" + strNomArchivo;
                            lstDocumentos.Add(objDocumento);
                            Session[STR_DOCUMENTOS_EXPEDIENTE_OP] = lstDocumentos;
                        }
                    }
                }
            }
            else if (pObjModel.TipoBotonClick == "REMOVER")
            {
                List<DocumentoExpedienteTecnicoOP> lstDocumentos = (List<DocumentoExpedienteTecnicoOP>)Session[STR_DOCUMENTOS_EXPEDIENTE_OP];

                if (lstDocumentos != null)
                {
                    lstDocumentos.RemoveAt(Convert.ToInt32(pObjModel.IdDocumentoEliminar));
                }
                Session[STR_DOCUMENTOS_EXPEDIENTE_OP] = lstDocumentos;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        List<DocumentoExpedienteTecnicoOP> lstDocumentos = (List<DocumentoExpedienteTecnicoOP>)Session[STR_DOCUMENTOS_EXPEDIENTE_OP];

                        if (lstDocumentos == null || lstDocumentos.Count == 0)
                        {
                            ModelState.AddModelError("", "Debe adjuntar al menos un archivo.");
                        }
                        else {

                            String strRutaFiles = Server.MapPath(@"\files");
                            String strRutaObrasPublicas = Server.MapPath(@"\files") + @"\obras_publicas";
                            String strRutaDocsTecnicos = Server.MapPath(@"\files") + @"\obras_publicas\docs_tecnicos";
                            if (!System.IO.Directory.Exists(strRutaFiles))
                            {
                                System.IO.Directory.CreateDirectory(strRutaFiles);
                            }
                            if (!System.IO.Directory.Exists(strRutaObrasPublicas))
                            {
                                System.IO.Directory.CreateDirectory(strRutaObrasPublicas);
                            }
                            if (!System.IO.Directory.Exists(strRutaDocsTecnicos))
                            {
                                System.IO.Directory.CreateDirectory(strRutaDocsTecnicos);
                            }
                            foreach (var objDoc in lstDocumentos)
                            {
                                if (objDoc.Archivo != null)
                                {
                                    System.IO.File.WriteAllBytes(strRutaDocsTecnicos + @"\" + objDoc.NomArchivo, objDoc.Archivo);
                                }
                            }

                            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();

                            int intResultado = objExpedienteTecnicoOP_DAL.Actualiza(pObjModel.IdExpediente, pObjModel.IdProyecto, pObjModel.Descripcion,
                                pObjModel.Especificaciones, pObjModel.ValorReferencial, pObjModel.TipoEjecutor, pObjModel.EjecutorNom,
                                pObjModel.EjecutorApe, pObjModel.EjecutorRazonSocial, pObjModel.ContactoNom, pObjModel.ContactoApe,
                                pObjModel.ContactoEmail, pObjModel.ContactoTelefono, pObjModel.ContactoDireccion, pObjModel.SupervisorNom,
                                pObjModel.SupervisorApe, pObjModel.SupervisorTelefono, pObjModel.SupervisorEmail, pObjModel.IdEjecutor,
                                pObjModel.IdContacto, pObjModel.IdSupervisor, lstDocumentos);

                            if (intResultado == 1)
                            {
                                //Session[STR_DOCUMENTOS_EXPEDIENTE_OP] = null;
                                ViewBag.MsgSuccess = "Se realizó la operación satisfactoriamente";
                                //return RedirectToAction("Create",);
                            }
                            else if (intResultado == -997)
                            {
                                ModelState.AddModelError("", "No puede registrar el expediente debido a que el proyecto está en estado ADJUDICADO.");
                            }
                            else
                            {
                                ModelState.AddModelError("", "No se pudo modificar el expediente");
                            }                        
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.ToString());
                    }
                }
            }
            return View("Update",pObjModel);
        }

        public ActionResult Lista_TiposDoc()
        {
            ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ExpedienteTecnicoOP_DAL();

            var lstTiposDoc = objExpedienteTecnicoOP_DAL.ObtieneTiposDocumento(null).Select(x =>
                                                                           new SelectListItem
                                                                           {
                                                                               Value = x.Id.ToString(),
                                                                               Text = x.Nombre
                                                                           }).OrderBy(x => x.Text);

            return Json(lstTiposDoc, JsonRequestBehavior.AllowGet);
        }
    }
}
