using GSM.Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSM.Controllers.GSM
{
    public class GSMInformeInspeccionController : Controller
    {
        //
        // GET: /GSMInformeInspeccion/

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult InfoInspeccion(String id)
        {
            InformeInspeccion oInfoInspeccion = new InformeInspeccion();
            oInfoInspeccion.oServicio = new Servicio();
            oInfoInspeccion.nuevo = true;//Es nuevo por defecto
            int Id = 0;
            int.TryParse(id, out Id);

            if (id != null && Id > 0)
            {
                var cInspeccion = InformeInspeccion.Get(Id);
                if (cInspeccion == null)
                {
                    oInfoInspeccion = new InformeInspeccion();
                    oInfoInspeccion.oServicio = new Servicio();

                }
                else
                {
                    oInfoInspeccion = cInspeccion;
                }
            }
            return View(oInfoInspeccion);
        }

        public JsonResult ListaInfoInspeccion(int Pagina, int Paginacion, String id)
        {
            List<InformeInspeccion> lst = new List<InformeInspeccion>();
            try
            {
                int intTipo = 0;

                if (!int.TryParse(id, out  intTipo))
                {
                    intTipo = 0;
                }
                /*******************************************************/
                lst = InformeInspeccion.List(intTipo, Pagina, Paginacion);


                var strList = new
                {
                    data = (from x in lst
                            select new
                            {
                                IdInforme = x.IdInforme,
                                IdRegistro = x.IdInspeccion,
                                IdServicio = x.oServicio.IdServicio,
                                Servicio = x.oServicio.Nombre,
                                Fecha = x.Fecha
                            }).ToList(),
                    total = lst[0].Filas
                };
                var vjson = Json(strList, JsonRequestBehavior.AllowGet);
                vjson.MaxJsonLength = int.MaxValue;
                return vjson;
            }
            catch (Exception ex)
            {

            }

            var strList2 = new
            {
                data = lst,
                total = 0
            };
            var vjson2 = Json(strList2, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;

            return vjson2;
        }

        [HttpPost]
        public JsonResult CancelInfoInspeccion(InformeInspeccion oInforme)
        {
            int Id = oInforme.IdInforme;
            try
            {
                if (Id > 0)
                {

                    var Ins = InformeInspeccion.Cancel(Id);
                    if (Ins == null)
                    {
                        var vjson = Json(new { ID = "1" }, JsonRequestBehavior.AllowGet);
                        vjson.MaxJsonLength = int.MaxValue;
                        return vjson;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            var vjson2 = Json(new { ID = "-3" }, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;
            return vjson2;
        }

        [HttpPost]
        public JsonResult SaveInfoInspeccion(InformeInspeccion oInforme)
        {
            String msj = "";
            try
            {
                DateTime fe = DateTime.Now;

                if (DateTime.TryParse(oInforme.Fecha, out fe))
                {
                    if (fe < DateTime.Now.Date)
                    {
                        msj = "0La fecha no es valida";
                    }
                    else
                    {
                        var oGuardado=InformeInspeccion.Insert(oInforme);
                        if (oGuardado.IdInforme > 0)
                        {
                            Inspeccion.Inspeccionado(oGuardado.IdInspeccion);
                            msj = "1Informe de inspección registrada con éxito. Id: " + oGuardado.IdInforme;
                        }
                        else {
                            msj = "0Problemas para registrar informe de inspección";
                        }
                    }
                }
                else
                {
                    msj = "0La fecha no es valida";
                }

            }
            catch (Exception ex)
            {
                msj = "0Problemas para registrar informe de inspección";
            }


            var vjson2 = Json(new { ID = msj }, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;
            return vjson2;
        }

        [HttpPost]
        public JsonResult UpdateInfoInspeccion(InformeInspeccion oInforme)
        {
            String msj = "";
            try
            {
                DateTime fe = DateTime.Now;

                if (DateTime.TryParse(oInforme.Fecha, out fe))
                {
                    if (fe < DateTime.Now.Date)
                    {
                        msj = "0La fecha no es valida";
                    }
                    else
                    {
                        int id = InformeInspeccion.Update(oInforme).IdInforme;
                        if (id > 0)
                        {
                            msj = "1Informe de inspección actualizada con éxito. Id: " + id;
                        }
                        else {
                            msj = "0Problemas para registrar informe de inspección";
                        }
                    }
                }
                else
                {
                    msj = "0La fecha no es valida";
                }

            }
            catch (Exception ex)
            {
                msj = "0Problemas para registrar informe de inspección";
            }


            var vjson2 = Json(new { ID = msj }, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;
            return vjson2;
        }


    }
}
