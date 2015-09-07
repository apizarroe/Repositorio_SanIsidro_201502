using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.GSM;

using bus = Negocio.GSM.InspeccionBUS;
using ent = Entidades.GSM;

namespace GAC.Controllers.GSM
{
    public class GSMInspeccionController : Controller
    {
        //
        // GET: /Inspeccion/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inspeccion(String id)
        {
            Inspeccion oInspeccion = new Inspeccion();
            oInspeccion.nuevo = true;//Es nuevo por defecto
            int Id = 0;
            int.TryParse(id, out Id);

            if (id != null && Id > 0)
            {
                oInspeccion.IdRegistro = Id;
                var Inspeccion = bus.GetObject().getInspeccion(Id);
                if (Inspeccion != null)
                {
                    oInspeccion.IdRegistro = Inspeccion.CodigoInspeccion;
                    oInspeccion.direccion = Inspeccion.LugarInspeccion;
                    oInspeccion.fechaProgramada = Inspeccion.FechaInspeccion.ToString("dd/MM/yyyy");
                    oInspeccion.horaFin = Inspeccion.HoraIni.ToString("hh\\:mm");
                    oInspeccion.horaInicio = Inspeccion.HoraFin.ToString("hh\\:mm");
                    oInspeccion.IdPersona = Inspeccion.idPersona;
                    oInspeccion.nuevo = false;
                    oInspeccion.oServicio = new Servicio();
                    oInspeccion.oServicio.IdServicio = Inspeccion.CodigoServicio;
                    oInspeccion.oServicio.Nombre = Inspeccion.NombreServicio;
                    oInspeccion.oServicio.IdTipoServicio = Inspeccion.CodigoCategoriaServicio;
                    oInspeccion.oServicio.TipoServicio = Inspeccion.nombreCategoria;
                    oInspeccion.personaAsignada = Inspeccion.Nombres;

                }
                //Buscar Iniciativa Vecinal : 
            }
            return View(oInspeccion);

        }

        public JsonResult ListaInspeccion(int Pagina, int Paginacion, String fnIni, String fnFin, String tipo)
        {
            List<ent.USP_GSM_GetInspeccion> lst = new List<ent.USP_GSM_GetInspeccion>();
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
                ent.SM_PARAMETRO oparametro = new ent.SM_PARAMETRO();
                oparametro.Pagina = Pagina;
                oparametro.Paginacion = Paginacion;
                oparametro.fecha1 = FeInicio;
                oparametro.fecha2 = FeFin;
                oparametro.Tipo = intTipo;

                lst = (from x in bus.GetObject().GetListInspeccion(oparametro) select x).ToList();


                var strList = new
                {
                    data = (from x in lst
                            select new
                            {
                                IdRegistro = x.CodigoInspeccion,
                                servicio = x.NombreServicio,
                                tipoServicio = x.nombreCategoria,
                                fechaProgramada = x.FechaInspeccion.ToString("dd/MM/yyyy"),
                                personaAsignada = x.Nombres
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

                    var Ins = bus.GetObject().Cancelar(Id);
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

                ent.SM_INSPECCION obj = new ent.SM_INSPECCION();

                obj.CodigoPersonaEjecutor = oInspeccion.IdPersona;
                obj.CodigoServicio = int.Parse(oInspeccion.personaAsignada);
                obj.coUsuario = 7; // oInspeccion.coUsuario; --> falta pagina de login
                obj.coVia = 2; // oInspeccion.coVi a;
                obj.Estado = 1;// oInspeccion.Estado;
                obj.FechaCreacion = DateTime.Now;
                obj.FechaInspeccion = fe;
                obj.HoraFin = hf;
                obj.HoraIni = hi;
                obj.LugarInspeccion = oInspeccion.direccion;

                var Ins = bus.GetObject().SaveInspeccion(obj);

                var oReq = new Inspeccion();
                oReq.IdRegistro = Ins.CodigoInspeccion;


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
    }
}
