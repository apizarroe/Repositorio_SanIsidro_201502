using GSM.Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ent = GSM.Models.Entity;
using Global = Util.GSM.Global;
namespace GSM.Models.GSM
{
    public class Inspeccion
    {
        public Servicio oServicio { get; set; }
        public int IdRegistro { get; set; }
        public String fechaProgramada { get; set; }
        public String horaInicio { get; set; }
        public String horaFin { get; set; }
        public String direccion { get; set; }
        public String personaAsignada { get; set; }
        public int IdPersona { get; set; }
        public bool nuevo { get; set; }
        public int IdUsuario { get; set; }
        public int IdCodVia { get; set; }
        public int Filas { get; set; }

        #region "Acceso a Datos"

        public static List<Inspeccion> GetInspeccion(Parametro oParametro)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_GetInspeccion(oParametro.Id);

                List<Inspeccion> lista = (from x in lst
                                          select new Inspeccion
                                                         {
                                                             IdRegistro = x.CodigoInspeccion,
                                                             oServicio = new Servicio
                                                             {
                                                                 IdServicio = x.CodigoServicio,
                                                                 Nombre = x.NombreServicio,
                                                                 IdTipoServicio = x.CodigoCategoriaServicio,
                                                                 TipoServicio = x.nombreCategoria
                                                             },
                                                             fechaProgramada = x.FechaInspeccion.Value.ToString("dd/MM/yyyy"),
                                                             horaFin = x.HoraFin.Value.ToString("hh\\:mm"),
                                                             horaInicio = x.HoraIni.Value.ToString("hh\\:mm"),
                                                             direccion = x.LugarInspeccion,
                                                             personaAsignada = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno,
                                                             IdPersona = x.idPersona.Value
                                                         }).ToList();
                return lista;
            }
        }

        public static Inspeccion getInspeccion(int id)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_GetInspeccion(id);
                var item = (from x in lst
                            select new Inspeccion
                            {
                                IdRegistro = x.CodigoInspeccion,
                                oServicio = new Servicio
                                {
                                    IdServicio = x.CodigoServicio,
                                    Nombre = x.NombreServicio,
                                    IdTipoServicio = x.CodigoCategoriaServicio,
                                    TipoServicio = x.nombreCategoria
                                },
                                fechaProgramada = x.FechaInspeccion.Value.ToString("dd/MM/yyyy"),
                                horaFin = x.HoraFin.Value.ToString("hh\\:mm"),
                                horaInicio = x.HoraIni.Value.ToString("hh\\:mm"),
                                direccion = x.LugarInspeccion,
                                personaAsignada = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno,
                                IdPersona = x.idPersona.Value
                            }).FirstOrDefault();
                return item;
            }
        }

        public static Inspeccion SaveInspeccion(Inspeccion nuevo)
        {

            Ent.SM_INSPECCION obj = new Ent.SM_INSPECCION();
            #region "Carga Variables"
            // obj.CodigoInspeccion = nuevo.CodigoInspeccion; //Se autogenera
            obj.CodigoPersonaEjecutor = nuevo.IdPersona;
            obj.CodigoServicio = nuevo.oServicio.IdServicio;
            obj.coUsuario = nuevo.IdUsuario;
            obj.coVia = nuevo.IdCodVia;
            obj.ESTADO = (int)Global.EstadoInspeccion.Programado;
            obj.FechaCreacion = DateTime.Now;
            obj.FechaInspeccion = DateTime.Parse(nuevo.fechaProgramada);
            obj.HORAFIN = TimeSpan.Parse(nuevo.horaFin);
            obj.HORAINI = TimeSpan.Parse(nuevo.horaInicio);
            obj.LugarInspeccion = nuevo.direccion;
            obj.ResponsableServicio = "0";
            #endregion

            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                cn.SM_INSPECCION.Add(obj);
                cn.SaveChanges();
                //nuevo.CodigoInspeccion = obj.CodigoInspeccion;
                return  Inspeccion.getInspeccion(obj.CodigoInspeccion);
            }


        }

        
        public static Inspeccion Update(Inspeccion nuevo)
        {
            Ent.SM_INSPECCION obj = new Ent.SM_INSPECCION();
            obj.CodigoInspeccion = nuevo.IdRegistro;
            obj.CodigoPersonaEjecutor = nuevo.IdPersona;
            obj.CodigoServicio = nuevo.oServicio.IdServicio;
            obj.coUsuario = nuevo.IdUsuario;
            obj.coVia = nuevo.IdCodVia;
            obj.ESTADO = (int)Global.EstadoInspeccion.Programado;
            obj.FechaActualizacion = DateTime.Now;
            obj.FechaInspeccion = DateTime.Parse(nuevo.fechaProgramada);
            obj.HORAFIN = TimeSpan.Parse(nuevo.horaFin);
            obj.HORAINI = TimeSpan.Parse(nuevo.horaInicio);
            obj.LugarInspeccion = nuevo.direccion;

            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                cn.SM_INSPECCION.Attach(obj);
                var objx = cn.Entry(obj);
                objx.Property(e => e.CodigoPersonaEjecutor).IsModified = true;
                objx.Property(e => e.CodigoServicio).IsModified = true;
                objx.Property(e => e.coUsuario).IsModified = true;
                objx.Property(e => e.coVia).IsModified = true;
                objx.Property(e => e.ESTADO).IsModified = true;
                objx.Property(e => e.FechaCreacion).IsModified = true;
                objx.Property(e => e.FechaInspeccion).IsModified = true;
                objx.Property(e => e.HORAFIN).IsModified = true;
                objx.Property(e => e.HORAINI).IsModified = true;
                objx.Property(e => e.LugarInspeccion).IsModified = true;
                objx.Property(e => e.ResponsableServicio).IsModified = true;
                cn.SaveChanges();
                return getInspeccion(obj.CodigoInspeccion); ;

            }
        }

        public static List<Inspeccion> GetListInspeccion(Parametro oParametro)
        {
            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_ListaInspeccion(oParametro.Tipo, oParametro.fecha1, oParametro.fecha2, oParametro.Pagina, oParametro.Paginacion);

                List<Inspeccion> lista = (from x in lst
                                          select new Inspeccion
                                                         {
                                                             IdRegistro = x.CodigoInspeccion,
                                                             oServicio = new Servicio
                                                             {
                                                                 IdServicio = x.CodigoServicio,
                                                                 Nombre = x.NombreServicio,
                                                                 TipoServicio = x.nombreCategoria
                                                             },
                                                             fechaProgramada = x.FechaInspeccion.Value.ToString("dd/MM/yyyy"),
                                                             personaAsignada = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno,
                                                             Filas = x.Filas.Value
                                                         }).ToList();
                return lista;
            }
        }

        public static int Cancelar(int Id)
        {
            Ent.SM_INSPECCION obj = new Ent.SM_INSPECCION();

            using (var cn = new Ent.MUNI_INTEGRADOEntities())
            {
                obj = (from x in cn.SM_INSPECCION where x.CodigoInspeccion == Id && x.ESTADO == (int)Global.EstadoInspeccion.Programado select x).FirstOrDefault();

                if (obj == null)
                {
                    return -3;
                }
                try
                {
                    obj.FechaActualizacion = DateTime.Now;
                    obj.ESTADO = (int)Global.EstadoInspeccion.Cancelado;

                    cn.SM_INSPECCION.Attach(obj);
                    var objx = cn.Entry(obj);
                    objx.Property(e => e.CodigoPersonaEjecutor).IsModified = true;
                    objx.Property(e => e.CodigoServicio).IsModified = true;
                    objx.Property(e => e.coUsuario).IsModified = true;
                    objx.Property(e => e.coVia).IsModified = true;
                    objx.Property(e => e.ESTADO).IsModified = true;
                    objx.Property(e => e.FechaCreacion).IsModified = true;
                    objx.Property(e => e.FechaInspeccion).IsModified = true;
                    objx.Property(e => e.HORAFIN).IsModified = true;
                    objx.Property(e => e.HORAINI).IsModified = true;
                    objx.Property(e => e.LugarInspeccion).IsModified = true;
                    objx.Property(e => e.ResponsableServicio).IsModified = true;
                    //objx.Property(e => e.).IsModified = true;

                    cn.SaveChanges();
                    return 1;
                }
                catch (Exception ex) { }
                return -1;
            }
        }

        #endregion


    }
}