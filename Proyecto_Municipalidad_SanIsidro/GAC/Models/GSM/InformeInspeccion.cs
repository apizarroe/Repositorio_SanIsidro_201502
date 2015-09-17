using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ent = GSM.Models.Entity;
using Global = Util.GSM.Global;
namespace GSM.Models.GSM
{
    public class InformeInspeccion
    {
        public int IdInforme { get; set; }
        public int IdInspeccion { get; set; }
        public Servicio oServicio { get; set; }
        public String Fecha { get; set; }
        public String descripcion { get; set; }
        public String direccion { get; set; }
        public String titulo { get; set; }
        public int Estado { get; set; }
        public bool nuevo { get; set; }
        public int Filas { get; set; }
        public int IdInformeServRef { get; set; }

        #region Acceso a datos

        public static List<InformeInspeccion> List(int Id, int pagina, int paginacion)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_ListaInformeInspeccion(Id, pagina, paginacion);

                List<InformeInspeccion> lista = (from x in lst
                                                 select new InformeInspeccion
                                                 {
                                                     IdInforme = x.CodigoInformeInspeccion,
                                                     IdInspeccion = x.CodigoInspeccion.Value,
                                                     oServicio = new Servicio
                                                     {
                                                         IdServicio = x.CodigoServicio,
                                                         Nombre = x.NombreServicio
                                                     },
                                                     Fecha = x.FechaInformeInspeccion.Value.ToString("dd/MM/yyyy hh:mm"),
                                                     Filas = x.Filas.Value
                                                 }).ToList();
                return lista;
            }
        }

        public static InformeInspeccion Get(int Id)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_GET_INFORME_INSPECCION(Id);

                List<InformeInspeccion> lista = (from x in lst
                                                 where x.ESTADO == (int)Global.EstadoInformeInspeccion.Activo
                                                 select new InformeInspeccion
                                                 {
                                                     IdInforme = x.CodigoInspeccion.Value,
                                                     IdInspeccion = x.CodigoInspeccion.Value,
                                                     oServicio = new Servicio
                                                     {
                                                         IdServicio = x.CodigoServicio,
                                                         Nombre = x.NombreServicio,
                                                         TipoServicio = x.nombreCategoria
                                                     },
                                                     Fecha = x.FechaInformeInspeccion.Value.ToString("dd/MM/yyyy hh:mm"),
                                                     descripcion = x.CuerpoInspeccion,
                                                     titulo = x.DescripcionInforme
                                                 }
                                                 ).ToList();
                return lista.FirstOrDefault();
            }
        }

        public static InformeInspeccion Insert(InformeInspeccion objInfo)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                Ent.SM_INFORME_INSPECCION obj = new Ent.SM_INFORME_INSPECCION();
                obj.CodigoExpedienteServicio = objInfo.oServicio.IdServicio;
                obj.CodigoInspeccion = objInfo.IdInspeccion;
                obj.CuerpoInspeccion = objInfo.descripcion;
                obj.DescripcionInforme = objInfo.titulo;
                obj.ESTADO = (int)Util.GSM.Global.EstadoInformeInspeccion.Activo;
                obj.FechaInformeInspeccion = DateTime.Parse(objInfo.Fecha);

                cn.SM_INFORME_INSPECCION.Add(obj);
                cn.SaveChanges();
                //nuevo.CodigoInspeccion = obj.CodigoInspeccion;
                return InformeInspeccion.Get(obj.CodigoInformeInspeccion);
            }
        }

        public static InformeInspeccion Update(InformeInspeccion objInfo)
        {

            Ent.SM_INFORME_INSPECCION obj = new Ent.SM_INFORME_INSPECCION();

            obj.CodigoExpedienteServicio = objInfo.oServicio.IdServicio;
            obj.CodigoInformeInspeccion = objInfo.IdInforme;
            obj.CodigoInspeccion = objInfo.IdInspeccion;
            obj.CuerpoInspeccion = objInfo.descripcion;
            obj.DescripcionInforme = objInfo.titulo;
            obj.ESTADO = (int)Global.EstadoInformeInspeccion.Activo;
            obj.FechaInformeInspeccion = DateTime.Parse(objInfo.Fecha);

            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                cn.SM_INFORME_INSPECCION.Attach(obj);
                var objx = cn.Entry(obj);
                objx.Property(e => e.CodigoExpedienteServicio).IsModified = true;
                objx.Property(e => e.CodigoInformeInspeccion).IsModified = true;
                objx.Property(e => e.CodigoInspeccion).IsModified = true;
                objx.Property(e => e.CuerpoInspeccion).IsModified = true;
                objx.Property(e => e.DescripcionInforme).IsModified = true;
                objx.Property(e => e.ESTADO).IsModified = true;
                objx.Property(e => e.FechaInformeInspeccion).IsModified = true;
                cn.SaveChanges();
                return Get(obj.CodigoInformeInspeccion); ;

            }
        }

        public static InformeInspeccion Cancel(int Id)
        {
            Ent.SM_INFORME_INSPECCION obj = new Ent.SM_INFORME_INSPECCION();
            obj.CodigoInformeInspeccion = Id;
            obj.ESTADO = (int)Global.EstadoInformeInspeccion.Inactivo;

            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                cn.SM_INFORME_INSPECCION.Attach(obj);
                var objx = cn.Entry(obj);
                //objx.Property(e => e.CodigoExpedienteServicio).IsModified = true;
                objx.Property(e => e.CodigoInformeInspeccion).IsModified = true;
                //objx.Property(e => e.CodigoInspeccion).IsModified = true;
                //objx.Property(e => e.CuerpoInspeccion).IsModified = true;
                //objx.Property(e => e.DescripcionInforme).IsModified = true;
                objx.Property(e => e.ESTADO).IsModified = true;
                cn.SaveChanges();
                return Get(obj.CodigoInformeInspeccion); ;

            }
        }

        public static List<InformeInspeccion> ListByIdServicio(int IdServicio)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_LIST_INFO_SERVICIO_BYSERVICIO(IdServicio);

                List<InformeInspeccion> lista = (from x in lst
                                                 select new InformeInspeccion
                                                 {
                                                     IdInforme = x.CodigoInformeInspeccion,
                                                     IdInformeServRef = x.IdInformeServicio,
                                                     oServicio = new Servicio
                                                     {
                                                         IdServicio = x.CodigoServicio,
                                                         Nombre = x.NombreServicio,
                                                         TipoServicio = x.nombreCategoria
                                                     },
                                                     Estado = x.ESTADO.Value,
                                                     Fecha=x.FechaInformeInspeccion.Value.ToString("dd/MM/yyyy")

                                                 }).ToList();
                return lista;
            }
        }

        public static int UpdateByInformeServicio(int IdInforme, List<InformeInspeccion> lst)
        {
            int fila = 0; 
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                foreach (var item in lst)
                { 
                    Ent.SM_INFORME_INSPECCION obj = new Ent.SM_INFORME_INSPECCION();
                    obj.CodigoInformeInspeccion = item.IdInforme;
                    obj.IdInformeServicio = IdInforme;
                    obj.ESTADO = (int)Global.EstadoInformeInspeccion.Informado;

                    cn.SM_INFORME_INSPECCION.Attach(obj);
                    var objx = cn.Entry(obj); 
                    objx.Property(e => e.CodigoInformeInspeccion).IsModified = true;
                    objx.Property(e => e.IdInformeServicio).IsModified = true; 
                    objx.Property(e => e.ESTADO).IsModified = true;
                    fila++;
                }
                cn.SaveChanges();
            } 
            return fila;
        }


        #endregion

    }
}