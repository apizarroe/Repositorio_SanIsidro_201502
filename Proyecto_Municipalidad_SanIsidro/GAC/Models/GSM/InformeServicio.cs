using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ent = GSM.Models.Entity;
using Global = Util.GSM.Global;

namespace GSM.Models.GSM
{
    public class InformeServicio
    {

        public int IdInforme { get; set; }
        public String Fecha { get; set; }
        public Servicio oServicio { get; set; }
        public List<InformeInspeccion> lstInforme { get; set; }
        public String Contenido { get; set; }
        public String Observacion { get; set; }

        public int Estado { get; set; }
        public bool nuevo { get; set; }
        public int Filas { get; set; }



        #region Acceso a datos

        public static List<InformeServicio> List(Parametro oParametro)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_ListaInformeServicio(oParametro.Id, oParametro.Id2, oParametro.Tipo, oParametro.fecha1, oParametro.fecha2, oParametro.Pagina, oParametro.Paginacion);

                List<InformeServicio> lista = (from x in lst
                                               select new InformeServicio
                                               {
                                                   IdInforme = x.CodigoInformeServicio,
                                                   oServicio = new Servicio
                                                   {
                                                       IdServicio = x.CodigoServicio,
                                                       Nombre = x.NombreServicio,
                                                       TipoServicio = x.nombreCategoria
                                                   },
                                                   Fecha = x.FechaEmision.Value.ToString("dd/MM/yyyy hh:mm"),
                                                   Filas = x.Filas.Value
                                               }).ToList();
                return lista;
            }
        }

        public static InformeServicio Get(int Id)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_GET_INFORME_SERVICIO(Id);

                List<InformeServicio> lista = (from x in lst
                                               select new InformeServicio
                                               {
                                                   IdInforme = x.CodigoInformeServicio,
                                                   oServicio = new Servicio
                                                   {
                                                       IdServicio = x.CodigoServicio,
                                                       Nombre = x.NombreServicio,
                                                       TipoServicio = x.nombreCategoria
                                                   },
                                                   Fecha = x.FechaEmision.Value.ToString("dd/MM/yyyy hh:mm"),
                                                   Contenido = x.Contenido,
                                                   Observacion = x.Observacion
                                               }
                                                 ).ToList();
                return lista.FirstOrDefault();
            }
        }

        public static List<InformeInspeccion> GetDatails(int Id, int IdServicio, bool flNew)
        {
            var lst = InformeInspeccion.ListByIdServicio(IdServicio);
            if (flNew)
            {
                return (from x in lst
                        where x.Estado == (int)Global.EstadoInformeInspeccion.Activo
                            && x.IdInformeServRef == 0
                        select x).ToList();
            }
            else
            {
                return (from x in lst
                        where x.Estado == (int)Global.EstadoInformeInspeccion.Informado
                            && x.IdInformeServRef == Id
                        select x).ToList();
            }
        }
        
        public static InformeServicio Insert(InformeServicio objInfo)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                Ent.SM_INFORME_SERVICIO obj = new Ent.SM_INFORME_SERVICIO();
                //obj.CodigoInformeServicio = objInfo.IdInforme;
                obj.CodigoServicio = objInfo.oServicio.IdServicio;
                obj.Contenido = objInfo.Contenido;
                obj.ESTADO = (int)Global.EstadoInformeServicio.Pendiente;
                obj.FechaEmision = DateTime.Parse(objInfo.Fecha);
                obj.Observacion = objInfo.Observacion;

                obj.CodigoEmpleadoAprobador = Usuario.GetObject().CodUsuario;
                obj.CodigoEmpleadoElaborador = Usuario.GetObject().CodUsuario;
                obj.FechaCreacion = DateTime.Now;
                 

                cn.SM_INFORME_SERVICIO.Add(obj);
                cn.SaveChanges();
                return InformeServicio.Get(obj.CodigoInformeServicio);
            }
        }

        public static InformeServicio Update(InformeServicio objInfo)
        {
            Ent.SM_INFORME_SERVICIO obj = new Ent.SM_INFORME_SERVICIO();
            obj.CodigoInformeServicio = objInfo.IdInforme;
            obj.CodigoServicio = objInfo.oServicio.IdServicio;
            obj.Contenido = objInfo.Contenido;
            obj.ESTADO = (int)Global.EstadoInformeServicio.Pendiente;
            obj.FechaEmision = DateTime.Parse(objInfo.Fecha);
            obj.Observacion = objInfo.Observacion;

            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                cn.SM_INFORME_SERVICIO.Attach(obj);
                var objx = cn.Entry(obj);
                objx.Property(e => e.CodigoInformeServicio).IsModified = true;
                objx.Property(e => e.CodigoServicio).IsModified = true;
                objx.Property(e => e.Contenido).IsModified = true;
                objx.Property(e => e.ESTADO).IsModified = true;
                objx.Property(e => e.FechaEmision).IsModified = true;
                objx.Property(e => e.Observacion).IsModified = true;
                cn.SaveChanges();
                return Get(obj.CodigoInformeServicio);
            }
        }

        public static InformeServicio Cancel(int Id)
        {
            Ent.SM_INFORME_SERVICIO obj = new Ent.SM_INFORME_SERVICIO();
            obj.CodigoInformeServicio = Id;
            obj.ESTADO = (int)Global.EstadoInformeServicio.Anulado;
            
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                cn.SM_INFORME_SERVICIO.Attach(obj);
                var objx = cn.Entry(obj);
                objx.Property(e => e.CodigoInformeServicio).IsModified = true;
                objx.Property(e => e.ESTADO).IsModified = true;
                cn.SaveChanges();
                return Get(obj.CodigoInformeServicio); 
            }
        }
        
        #endregion


    }
}