using GSM.Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSM.Controllers.GSM
{
    public class GSMServicioController : Controller
    {
        public ActionResult InfoIndex()
        {
            return View();
        }

        public ActionResult InfoServicio(String id)
        {
            InformeServicio oInfoInspeccion = new InformeServicio();
            oInfoInspeccion.oServicio = new Servicio();
            oInfoInspeccion.lstInforme = new List<InformeInspeccion>();
            oInfoInspeccion.nuevo = true;//Es nuevo por defecto
            int Id = 0;
            int.TryParse(id, out Id);

            if (id != null && Id > 0)
            {
                var cInspeccion = InformeServicio.Get(Id);
                if (cInspeccion == null)
                {
                    oInfoInspeccion = new InformeServicio();
                    oInfoInspeccion.oServicio = new Servicio();
                    oInfoInspeccion.lstInforme = new List<InformeInspeccion>();
                }
                else
                {
                    oInfoInspeccion = cInspeccion;
                    oInfoInspeccion.lstInforme = InformeServicio.GetDatails(oInfoInspeccion.IdInforme, oInfoInspeccion.oServicio.IdServicio, false);
                }
            }
            return View(oInfoInspeccion);
        }

        public JsonResult GetListaDetalleById(int IdServicio)
        {
            List<InformeInspeccion> lista = new List<InformeInspeccion>();
            if (IdServicio > 0)
            {
                lista = InformeServicio.GetDatails(0, IdServicio, true);
            }
            var vjson = Json(lista, JsonRequestBehavior.AllowGet);
            vjson.MaxJsonLength = int.MaxValue;
            return vjson;
        }

        public JsonResult ListaInfoServicio(String id, String idServicio, String tipo, String fnIni, String fnFin, int Pagina, int Paginacion)
        {
            List<InformeServicio> lst = new List<InformeServicio>();
            try
            {

                DateTime FeInicio = new DateTime(1900, 1, 1);
                DateTime FeFin = new DateTime(1900, 1, 1);
                int intTipo = 0;
                int IdInforme = 0;
                int IdServicio = 0;
                bool fl = false;

                if (!DateTime.TryParse(fnIni, out FeInicio))
                {
                    FeInicio = new DateTime(1900, 1, 1);
                }
                if (!DateTime.TryParse(fnFin, out FeFin))
                {
                    FeFin = DateTime.Now.AddDays(7);
                }
                int.TryParse(tipo, out  intTipo);
                int.TryParse(id, out  IdInforme);
                int.TryParse(idServicio, out  IdServicio);

                //(oParametro.Tipo,oParametro.fecha1,oParametro.fecha2,oParametro.Pagina,oParametro.Paginacion);
                /*******************************************************/
                Parametro oparametro = new Parametro();
                oparametro.Pagina = Pagina;
                oparametro.Paginacion = Paginacion;
                oparametro.fecha1 = FeInicio;
                oparametro.fecha2 = FeFin;
                oparametro.Tipo = intTipo;
                oparametro.Id = IdInforme;
                oparametro.Id2 = IdServicio;
                /*******************************************************/
                lst = InformeServicio.List(oparametro);

                var strList = new
                {
                    data = (from x in lst
                            select new
                            {
                                IdInforme = x.IdInforme,
                                Fecha = x.Fecha,
                                Servicio = x.oServicio.Nombre,
                                TipoServicio = x.oServicio.TipoServicio
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
        public JsonResult CancelInfoServicio(InformeServicio oInforme)
        {
            int Id = oInforme.IdInforme;
            try
            {
                if (Id > 0)
                {

                    var Ins = InformeServicio.Cancel(Id);
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
        public JsonResult SaveInfoServicio(InformeServicio oInforme)
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
                        var oGuardado = InformeServicio.Insert(oInforme);
                        if (oGuardado.IdInforme > 0)
                        {
                            int fila = InformeInspeccion.UpdateByInformeServicio(oGuardado.IdInforme, oInforme.lstInforme);
                            msj = "1Informe de Servicio registrada con éxito. Id: " + oGuardado.IdInforme;
                        }
                        else
                        {
                            msj = "0Problemas para registrar informe de Servicio";
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
                msj = "0Problemas para registrar informe de Servicio";
            }


            var vjson2 = Json(new { ID = msj }, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;
            return vjson2;
        }

        [HttpPost]
        public JsonResult UpdateInfoServicio(InformeServicio oInforme)
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
                        int id = InformeServicio.Update(oInforme).IdInforme;
                        if (id > 0)
                        {
                            msj = "1Informe de servicio actualizada con éxito. Id: " + id;
                        }
                        else
                        {
                            msj = "0Problemas para registrar informe de servicio";
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
                msj = "0Problemas para registrar informe de servicio";
            }


            var vjson2 = Json(new { ID = msj }, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;
            return vjson2;
        }

        /*********************************************************/
        public JsonResult ListaBusqueda(int Pagina, int Paginacion, String Cod, String Nom, String Tipo)
        {
            List<Servicio> lst = new List<Servicio>();
            try
            {
                int codint = 0;
                int tipoint = 0;
                int.TryParse(Cod, out codint); int.TryParse(Tipo, out tipoint);
                /*******************************************************/
                Parametro oparametro = new Parametro();
                oparametro.Pagina = Pagina;
                oparametro.Paginacion = Paginacion;
                oparametro.Id = codint;
                oparametro.name = Nom;
                oparametro.Tipo = tipoint;
                lst = Servicio.GetServicio(oparametro);
                if (lst != null || lst.Count > 0)
                {
                    var strList = new
                    {
                        data = lst,
                        total = lst[0].Filas
                    };
                    var vjson = Json(strList, JsonRequestBehavior.AllowGet);
                    vjson.MaxJsonLength = int.MaxValue;
                    return vjson;
                }
            }
            catch (Exception ex)
            {
                var strList = new
                {
                    data = lst,
                    total = lst[0].Filas
                };
                var vjson = Json(strList, JsonRequestBehavior.AllowGet);
                vjson.MaxJsonLength = int.MaxValue;
                return vjson;
            }

            var strList2 = new
            {
                data = new List<Servicio>(),
                total = 0
            };
            var vjson2 = Json(strList2, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;

            return vjson2;
        }

    }

}
