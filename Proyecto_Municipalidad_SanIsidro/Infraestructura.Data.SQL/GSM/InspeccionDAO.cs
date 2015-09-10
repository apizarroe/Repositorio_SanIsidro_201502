using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent = Dominio.Core.Entities.GSM;
using Util.GSM;

namespace Infraestructura.Data.SQL.GSM
{
    public class InspeccionDAO
    {
        public List<Ent.USP_GSM_GetInspeccion> GetInspeccion(Ent.SM_PARAMETRO oParametro)
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_GetInspeccion(oParametro.Id);

                List<Ent.USP_GSM_GetInspeccion> lista = (from x in lst
                                                         select new Ent.USP_GSM_GetInspeccion
                                                     {
                                                         CodigoInspeccion = x.CodigoInspeccion,
                                                         CodigoServicio = x.CodigoServicio,
                                                         NombreServicio = x.NombreServicio,
                                                         nombreCategoria = x.nombreCategoria,
                                                         FechaInspeccion = x.FechaInspeccion.Value,
                                                         HoraFin = x.HoraFin.Value,
                                                         HoraIni = x.HoraIni.Value,
                                                         LugarInspeccion = x.LugarInspeccion,
                                                         Nombres = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno,
                                                         idPersona = x.idPersona.Value,
                                                         CodigoCategoriaServicio = x.CodigoCategoriaServicio
                                                     }).ToList();
                return lista;
            }
        }

        public Ent.USP_GSM_GetInspeccion SaveInspeccion(Ent.SM_INSPECCION nuevo)
        {

            Entity.SM_INSPECCION obj = new Entity.SM_INSPECCION();
            #region "Carga Variables"
            // obj.CodigoInspeccion = nuevo.CodigoInspeccion; //Se autogenera
            obj.CodigoPersonaEjecutor = nuevo.CodigoPersonaEjecutor;
            obj.CodigoServicio = nuevo.CodigoServicio;
            obj.coUsuario = nuevo.coUsuario;
            obj.coVia = nuevo.coVia;
            obj.Estado = nuevo.Estado;
            obj.FechaCreacion = nuevo.FechaCreacion;
            obj.FechaInspeccion = nuevo.FechaInspeccion;
            obj.HoraFin = nuevo.HoraFin;
            obj.HoraIni = nuevo.HoraIni;
            obj.LugarInspeccion = nuevo.LugarInspeccion;
            obj.ResponsableServicio = "0";
            #endregion

            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {
                cn.SM_INSPECCION.Add(obj);
                cn.SaveChanges();
                //nuevo.CodigoInspeccion = obj.CodigoInspeccion;
                return getInspeccion(obj.CodigoInspeccion);
            }


        }

        public Ent.USP_GSM_GetInspeccion getInspeccion(int id)
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_GetInspeccion(id);

                var item = (from x in lst
                            select new Ent.USP_GSM_GetInspeccion
                            {
                                CodigoInspeccion = x.CodigoInspeccion,
                                CodigoServicio = x.CodigoServicio,
                                NombreServicio = x.NombreServicio,
                                nombreCategoria = x.nombreCategoria,
                                FechaInspeccion = x.FechaInspeccion.Value,
                                HoraFin = x.HoraFin.Value,
                                HoraIni = x.HoraIni.Value,
                                LugarInspeccion = x.LugarInspeccion,
                                Nombres = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno,
                                idPersona = x.idPersona.Value,
                                CodigoCategoriaServicio = x.CodigoCategoriaServicio
                            }).FirstOrDefault();

                return item;
            }
        }

        public Ent.USP_GSM_GetInspeccion Update(Ent.SM_INSPECCION nuevo)
        {
            Entity.SM_INSPECCION obj = new Entity.SM_INSPECCION();
            obj.CodigoInspeccion = nuevo.CodigoInspeccion;
            obj.CodigoPersonaEjecutor = nuevo.CodigoPersonaEjecutor;
            obj.CodigoServicio = nuevo.CodigoServicio;
            obj.coUsuario = nuevo.coUsuario;
            obj.coVia = nuevo.coVia;
            obj.Estado = nuevo.Estado;
            obj.FechaCreacion = nuevo.FechaCreacion;
            obj.FechaInspeccion = nuevo.FechaInspeccion;
            obj.HoraFin = nuevo.HoraFin;
            obj.HoraIni = nuevo.HoraIni;
            obj.LugarInspeccion = nuevo.LugarInspeccion;

            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {

                //var obj = (from x in cn.PVecinal_IniciativaVecinal where x.Id == oIV.Id select x).First();
                //obj = oIV;
                //cn.SaveChanges();
                //return oIV;

                cn.SM_INSPECCION.Attach(obj);
                var objx = cn.Entry(obj);
                objx.Property(e => e.CodigoPersonaEjecutor).IsModified = true;
                objx.Property(e => e.CodigoServicio).IsModified = true;
                objx.Property(e => e.coUsuario).IsModified = true;
                objx.Property(e => e.coVia).IsModified = true;
                objx.Property(e => e.Estado).IsModified = true;
                objx.Property(e => e.FechaCreacion).IsModified = true;
                objx.Property(e => e.FechaInspeccion).IsModified = true;
                objx.Property(e => e.HoraFin).IsModified = true;
                objx.Property(e => e.HoraIni).IsModified = true;
                objx.Property(e => e.LugarInspeccion).IsModified = true;
                objx.Property(e => e.ResponsableServicio).IsModified = true;
                //objx.Property(e => e.).IsModified = true;

                cn.SaveChanges();
                return getInspeccion(obj.CodigoInspeccion); ;

            }
        }

        public List<Ent.USP_GSM_GetInspeccion> GetListInspeccion(Ent.SM_PARAMETRO oParametro)
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_ListaInspeccion(oParametro.Tipo, oParametro.fecha1, oParametro.fecha2, oParametro.Pagina, oParametro.Paginacion);

                List<Ent.USP_GSM_GetInspeccion> lista = (from x in lst
                                                         select new Ent.USP_GSM_GetInspeccion
                                                         {
                                                             CodigoInspeccion = x.CodigoInspeccion,
                                                             CodigoServicio = x.CodigoServicio,
                                                             NombreServicio = x.NombreServicio,
                                                             nombreCategoria = x.nombreCategoria,
                                                             FechaInspeccion = x.FechaInspeccion.Value,
                                                             Nombres = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno,
                                                             Filas = x.Filas.Value
                                                         }).ToList();
                return lista;
            }
        }


        public int Cancelar(int Id)
        {
            Entity.SM_INSPECCION obj = new Entity.SM_INSPECCION(); 

            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {
                obj = (from x in cn.SM_INSPECCION where x.CodigoInspeccion == Id && x.Estado==(int)Global.EstadoInspeccion.Programado select x).FirstOrDefault();

                if (obj == null) {
                    return -3;
                }
                try
                {
                    obj.FechaActualizacion = DateTime.Now;
                    obj.Estado = (int)Global.EstadoInspeccion.Cancelado;

                    cn.SM_INSPECCION.Attach(obj);
                    var objx = cn.Entry(obj);
                    objx.Property(e => e.CodigoPersonaEjecutor).IsModified = true;
                    objx.Property(e => e.CodigoServicio).IsModified = true;
                    objx.Property(e => e.coUsuario).IsModified = true;
                    objx.Property(e => e.coVia).IsModified = true;
                    objx.Property(e => e.Estado).IsModified = true;
                    objx.Property(e => e.FechaCreacion).IsModified = true;
                    objx.Property(e => e.FechaInspeccion).IsModified = true;
                    objx.Property(e => e.HoraFin).IsModified = true;
                    objx.Property(e => e.HoraIni).IsModified = true;
                    objx.Property(e => e.LugarInspeccion).IsModified = true;
                    objx.Property(e => e.ResponsableServicio).IsModified = true;
                    //objx.Property(e => e.).IsModified = true;

                    cn.SaveChanges();
                    return 1;
                }catch(Exception ex){}
                return -1;
            }
        }

    }
}
