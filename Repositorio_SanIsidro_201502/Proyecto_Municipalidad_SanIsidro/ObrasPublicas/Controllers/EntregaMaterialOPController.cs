using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObrasPublicas.Models.EntregaMaterialOP;
using ObrasPublicas.Entities;
using Infraestructura.Data.SQL;
using ObrasPublicas.DAL;
using Dominio.Core.Entities;

namespace ObrasPublicas.Controllers
{
    public class EntregaMaterialOPController : Controller
    {
        //
        // GET: /EntregaMaterial/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int p)
        {
            //id=id de proyecto
            ViewBag.MsgSuccess = TempData["MsgSuccess"];
            ViewBag.Action = TempData["Action"];
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
            
            CreateEntregaMaterialOP objCreateEntregaMaterialOP = new CreateEntregaMaterialOP();
            objCreateEntregaMaterialOP.IdProyecto = p;
            objCreateEntregaMaterialOP.NomProyecto = objProyectoInversion.Nombre;

            return View(objCreateEntregaMaterialOP);
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Listado(int p)
        {
            EntregaMaterial_DAL objEntregaMaterial_DAL = new EntregaMaterial_DAL();
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(p);
            List<EntregaMaterialOP> lstEntregas = objEntregaMaterial_DAL.ObtieneEntregasXIdProyecto(p);

            ListadoEntregaMaterialModel objListadoEntregaMaterialModel = new ListadoEntregaMaterialModel();
            objListadoEntregaMaterialModel.IdProyecto = p;
            objListadoEntregaMaterialModel.NomProyecto = objProyectoInversion.Nombre;

            ViewBag.ListadoEntregas = lstEntregas;
            return View(objListadoEntregaMaterialModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEntregaMaterialOP pObjModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EntregaMaterial_DAL objEntregaMaterial_DAL = new EntregaMaterial_DAL();
                    int intResultado = objEntregaMaterial_DAL.Inserta(Convert.ToDateTime(pObjModel.FecEntregaProg),
                        Convert.ToDateTime(pObjModel.FecEntregaEfec), pObjModel.Observaciones, pObjModel.TipoEntrega,
                        pObjModel.IdProveedor, pObjModel.IdMaterial, pObjModel.Cantidad, pObjModel.IdProyecto);

                    if (intResultado == 1)
                    {
                        TempData["MsgSuccess"] = "Se realizó la operación satisfactoriamente";
                        return RedirectToAction("Create", new { p = pObjModel.IdProyecto });
                        //ViewBag.MsgSuccess = "Se realizó la operación satisfactoriamente";
                    }
                    else if (intResultado == -998)
                    {
                        ModelState.AddModelError("", "No se pueden crear más entregas debido a que el proyecto está en estado ADJUDICADO.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo insertar la entrega de material");
                    }
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError("", ErrorCodeToString(999));
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            return View(pObjModel);
        }

        public ActionResult Edit(int p, int ent)
        {
            EntregaMaterial_DAL objEntregaMaterial_DAL = new EntregaMaterial_DAL();
            EntregaMaterialOP objEntregaMaterialOP = objEntregaMaterial_DAL.ObtieneEntregaXId(p, ent);

            UpdateEntregaMaterialOP objUpdateEntregaMaterialOP = new UpdateEntregaMaterialOP();
            objUpdateEntregaMaterialOP.IdEntrega = objEntregaMaterialOP.IdEntrega;
            objUpdateEntregaMaterialOP.Cantidad = objEntregaMaterialOP.Cantidad;
            objUpdateEntregaMaterialOP.FecEntregaEfec = objEntregaMaterialOP.FecEntregaEfec.ToString("dd/MM/yyyy");
            objUpdateEntregaMaterialOP.FecEntregaProg = objEntregaMaterialOP.FecEntregaProg.ToString("dd/MM/yyyy");
            objUpdateEntregaMaterialOP.IdEntrega = objEntregaMaterialOP.IdEntrega;

            if (objEntregaMaterialOP.Material != null) {
                objUpdateEntregaMaterialOP.IdMaterial = objEntregaMaterialOP.Material.IdMaterial;
            }
            if (objEntregaMaterialOP.Proveedor != null) {
                objUpdateEntregaMaterialOP.IdProveedor = objEntregaMaterialOP.Proveedor.IdProveedor;
            }

            if (objEntregaMaterialOP.Proyecto != null)
            {
                objUpdateEntregaMaterialOP.IdProyecto = objEntregaMaterialOP.Proyecto.IdProyecto;
                objUpdateEntregaMaterialOP.NomProyecto = objEntregaMaterialOP.Proyecto.Nombre;
            }
            objUpdateEntregaMaterialOP.Observaciones = objEntregaMaterialOP.Observaciones;
            objUpdateEntregaMaterialOP.TipoEntrega = objEntregaMaterialOP.TipoEntrega;

            return View("Update",objUpdateEntregaMaterialOP);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Save_Update(UpdateEntregaMaterialOP pObjModel)
        {
            if (ModelState.IsValid)
            {
                bool bolGrabaOK = false;
                try
                {
                    EntregaMaterial_DAL objEntregaMaterial_DAL = new EntregaMaterial_DAL();
                    ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();

                    ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(pObjModel.IdProyecto);
                    pObjModel.IdProyecto = pObjModel.IdProyecto;
                    pObjModel.NomProyecto = objProyectoInversion.Nombre;

                    int intResultado = objEntregaMaterial_DAL.Actualiza(pObjModel.IdEntrega, pObjModel.IdProyecto, Convert.ToDateTime(pObjModel.FecEntregaProg),
                        Convert.ToDateTime(pObjModel.FecEntregaEfec), pObjModel.Observaciones, pObjModel.TipoEntrega,
                        pObjModel.IdProveedor, pObjModel.IdMaterial, pObjModel.Cantidad);

                    if (intResultado == 1)
                    {
                        bolGrabaOK = true;
                        ViewBag.MsgSuccess = "Se realizó la operación satisfactoriamente";
                    }
                    else
                    {
                        ViewBag.Error = "1";
                        ModelState.AddModelError("", "No se pudo actualizar la entrega de material");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }

            return View("Update", pObjModel);
        }
    }
}
