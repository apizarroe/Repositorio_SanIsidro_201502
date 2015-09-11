﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GSM.Models.GSM;
using GSM.Models;
namespace GSM.Controllers.GSM
{
    public class GSMInspeccionController : Controller
    {
        //
        // GET: /Inspeccion/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inspecciones(String id)
        {
            Inspeccion oInspeccion = new Inspeccion();
            oInspeccion.oServicio = new Servicio();
            oInspeccion.nuevo = true;//Es nuevo por defecto
            int Id = 0;
            int.TryParse(id, out Id);

            if (id != null && Id > 0)
            {
                oInspeccion.IdRegistro = Id;
                var cInspeccion = Inspeccion.getInspeccion(Id);
                if (cInspeccion != null)
                {
                    oInspeccion = new Inspeccion();

                }
            }
            return View(oInspeccion);

        }

        public JsonResult ListaInspeccion(int Pagina, int Paginacion, String fnIni, String fnFin, String tipo)
        {
            List<Inspeccion> lst = new List<Inspeccion>();
            try
            {
                DateTime FeInicio = new DateTime(1900, 1, 1);
                DateTime FeFin = new DateTime(1900, 1, 1);
                int intTipo = 0;
                bool fl = false;

                if (!DateTime.TryParse(fnIni, out FeInicio))
                {
                    FeInicio = new DateTime(1900, 1, 1);
                }
                if (!DateTime.TryParse(fnFin, out FeFin))
                {
                    FeFin = DateTime.Now.AddDays(7);
                }
                if (!int.TryParse(tipo, out  intTipo))
                {
                    intTipo = 0;
                }

                //(oParametro.Tipo,oParametro.fecha1,oParametro.fecha2,oParametro.Pagina,oParametro.Paginacion);
                /*******************************************************/
                Parametro oparametro = new Parametro();
                oparametro.Pagina = Pagina;
                oparametro.Paginacion = Paginacion;
                oparametro.fecha1 = FeInicio;
                oparametro.fecha2 = FeFin;
                oparametro.Tipo = intTipo;

                lst = Inspeccion.GetListInspeccion(oparametro);


                var strList = new
                {
                    data = (from x in lst
                            select new
                            {
                                IdRegistro = x.IdRegistro,
                                servicio = x.oServicio.Nombre,
                                tipoServicio = x.oServicio.TipoServicio,
                                fechaProgramada = x.fechaProgramada,
                                personaAsignada = x.personaAsignada
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
        public JsonResult CancelInspeccion(Inspeccion oInspeccion)
        {
            int Id = oInspeccion.IdRegistro;

            try
            {
                if (Id > 0)
                {

                    var Ins = Inspeccion.Cancelar(Id);
                    if (Ins != null)
                    {
                        var vjson = Json(new { ID = Ins }, JsonRequestBehavior.AllowGet);
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
        public JsonResult SaveInspeccion(Inspeccion oInspeccion)
        {

            try
            {
                DateTime fe = DateTime.Now;
                string[] vhi = oInspeccion.horaInicio.Split(':');
                string[] vhf = oInspeccion.horaFin.Split(':');

                TimeSpan hi = new TimeSpan(int.Parse(vhi[0]), int.Parse(vhi[1]), 0);
                TimeSpan hf = new TimeSpan(int.Parse(vhf[0]), int.Parse(vhf[1]), 0);
                DateTime.TryParse(oInspeccion.fechaProgramada, out fe);

                Inspeccion obj = new Inspeccion();
                obj.oServicio = new Servicio();
                obj.IdPersona = oInspeccion.IdPersona;
                obj.oServicio.IdServicio = int.Parse(oInspeccion.personaAsignada);
                obj.IdUsuario = Usuario.GetObject().CodUsuario; // oInspeccion.coUsuario; --> falta pagina de login
                obj.IdCodVia = Usuario.GetObject().CodVia; // oInspeccion.coVi a;
                obj.fechaProgramada = fe.ToString("dd/MM/yyyy");
                obj.horaFin = hf.ToString("hh\\:mm");
                obj.horaInicio = hi.ToString("hh\\:mm");
                obj.direccion = oInspeccion.direccion;

                var Ins = Inspeccion.SaveInspeccion(obj);

                var oReq = new Inspeccion();
                oReq.IdRegistro = Ins.IdRegistro;


                var vjson = Json(new { ID = oReq.IdRegistro }, JsonRequestBehavior.AllowGet);
                vjson.MaxJsonLength = int.MaxValue;

                return vjson;
            }
            catch (Exception ex)
            {

            }

            var vjson2 = Json(new { ID = "Problemas a registrar inspeccion" }, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;

            return vjson2;
        }
         
        [HttpPost]
        public JsonResult UpdateInspeccion(Inspeccion oInspeccion)
        {

            try
            {
                DateTime fe = DateTime.Now;
                string[] vhi = oInspeccion.horaInicio.Split(':');
                string[] vhf = oInspeccion.horaFin.Split(':');

                TimeSpan hi = new TimeSpan(int.Parse(vhi[0]), int.Parse(vhi[1]), 0);
                TimeSpan hf = new TimeSpan(int.Parse(vhf[0]), int.Parse(vhf[1]), 0);
                DateTime.TryParse(oInspeccion.fechaProgramada, out fe);

                Inspeccion obj = new Inspeccion();
                obj.oServicio = new Servicio();

                obj.IdRegistro = oInspeccion.IdRegistro;
                obj.IdPersona = oInspeccion.IdPersona;
                obj.oServicio.IdServicio = int.Parse(oInspeccion.personaAsignada);
                obj.IdUsuario = Usuario.GetObject().CodUsuario; // oInspeccion.coUsuario; --> falta pagina de login
                obj.IdCodVia = Usuario.GetObject().CodVia; // oInspeccion.coVi a;
                obj.fechaProgramada = fe.ToString("dd/MM/yyyy");
                obj.horaFin = hf.ToString("hh\\:mm");
                obj.horaInicio = hi.ToString("hh\\:mm");
                obj.direccion = oInspeccion.direccion;

                var Ins = Inspeccion.Update(obj);
                 
                var vjson = Json(new { ID = Ins.IdRegistro }, JsonRequestBehavior.AllowGet);
                vjson.MaxJsonLength = int.MaxValue;

                return vjson;
            }
            catch (Exception ex)
            {

            }

            var vjson2 = Json(new { ID = "Problemas para actualizar inspeccion" }, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;

            return vjson2;
        }

    }
}
