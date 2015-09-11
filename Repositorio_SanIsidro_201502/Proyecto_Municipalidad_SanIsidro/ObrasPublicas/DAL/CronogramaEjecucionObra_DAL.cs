using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObrasPublicas.Entities;
using ObrasPublicas.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Data.Objects;

namespace ObrasPublicas.DAL
{
    public class CronogramaEjecucionObra_DAL
    {
        public int Inserta(int pIntIdExpediente, int pIntPlazoEjecucion)
        {
            int intResultado = -999;
            int intRows;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                OP_CRONOGRAMA_EJECUCION objCronogramaEjecucion = new OP_CRONOGRAMA_EJECUCION();
                objCronogramaEjecucion.coExpediente = pIntIdExpediente;
                objCronogramaEjecucion.feEmision = DateTime.Now;
                objCronogramaEjecucion.nuPlazoEjecucion = pIntPlazoEjecucion;

                objContext.AddToOP_CRONOGRAMA_EJECUCION(objCronogramaEjecucion);
                intRows = objContext.SaveChanges();

                if (intRows > 0)
                {
                    intResultado = 1;
                }
            }
            catch (Exception ex)
            {
                intResultado = -999;
            }
            return intResultado;
        }

        public int InsertaActividad(int pIntIdExpediente, int pIntIdCronograma, ActividadCronogramaOP pObjActividadCronogramaOP)
        {
            int intResultado = -999;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                ObjectParameter objResult = new ObjectParameter("pIntResult_out", typeof(int));
                
                objContext.sp_gop_ins_act_cron_ejec_obra(pIntIdCronograma, pObjActividadCronogramaOP.Nombre, pObjActividadCronogramaOP.FechaIniEjec,
                    pObjActividadCronogramaOP.FechaFinProg, pObjActividadCronogramaOP.FechaIniEjec, pObjActividadCronogramaOP.FechaFinEjec,
                    pObjActividadCronogramaOP.Costo, pObjActividadCronogramaOP.CantidadRRHH, pObjActividadCronogramaOP.IdTipoResponsable,
                    pObjActividadCronogramaOP.IdEmpleado,objResult);

                intResultado=Convert.ToInt32(objResult.Value.ToString());
            }
            catch (Exception ex)
            {
            }
            return intResultado;
        }

        public int Actualiza(int pIntIdCronograma, int pIntIdExpediente, int pIntPlazoEjecucion)
        {
            int intResultado = -999;
            int intRows=0;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                OP_CRONOGRAMA_EJECUCION objCronogramaEjecucion = objContext.OP_CRONOGRAMA_EJECUCION.First(x => x.coCronograma == pIntIdCronograma);

                if (objCronogramaEjecucion != null) {
                    objCronogramaEjecucion.nuPlazoEjecucion = pIntPlazoEjecucion;

                    intRows = objContext.SaveChanges();

                    if (intRows > 0)
                    {
                        intResultado = 1;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return intResultado;
        }

        public int ActualizaActividad(int pIntIdCronograma, int pIntIdExpediente, int pIntIdActividad, ActividadCronogramaOP pObjActividadCronogramaOP)
        {
            int intResultado = -999;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                ObjectParameter objResult = new ObjectParameter("pIntResult_out", typeof(int));

                objContext.sp_gop_upd_act_cron_ejec_obra(pIntIdCronograma, pIntIdActividad, pObjActividadCronogramaOP.Nombre, pObjActividadCronogramaOP.FechaIniEjec,
                    pObjActividadCronogramaOP.FechaFinProg, pObjActividadCronogramaOP.FechaIniEjec, pObjActividadCronogramaOP.FechaFinEjec,
                    pObjActividadCronogramaOP.Costo, pObjActividadCronogramaOP.CantidadRRHH, pObjActividadCronogramaOP.IdTipoResponsable,
                    pObjActividadCronogramaOP.IdEmpleado, objResult);

                intResultado = Convert.ToInt32(objResult.Value.ToString());
            }
            catch (Exception ex)
            {
            }
            return intResultado;
        }

        public CronogramaEjecucionOP ObtieneXId(int pIntIdExpediente, int pIntIdCronograma)
        {
            CronogramaEjecucionOP objCronogramaEjecucionObra = null;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                var lstCronograma = (from cro in objContext.OP_CRONOGRAMA_EJECUCION
                                      join exp in objContext.OP_EXPEDIENTE_TECNICO on cro.coExpediente equals exp.coExpediente
                                      join proy in objContext.OP_PROYECTO_INVERSION_PUBLICA on exp.coProyecto equals proy.coProyecto
                                      where exp.coExpediente == pIntIdExpediente && cro.coCronograma == pIntIdCronograma
                                      select new { exp, proy, cro }).ToList();

                if (lstCronograma != null)
                {
                    objCronogramaEjecucionObra = new CronogramaEjecucionOP();

                    ProyectoInversion objProyectoInversion = new ProyectoInversion();
                    objProyectoInversion.IdProyecto = lstCronograma[0].proy.coProyecto;
                    objProyectoInversion.Nombre = lstCronograma[0].proy.noNombre;
                    objCronogramaEjecucionObra.ProyectoInversion = objProyectoInversion;

                    ExpedienteTecnicoOP objExpedienteTecnicoOP = new ExpedienteTecnicoOP();
                    objExpedienteTecnicoOP.IdExpediente = lstCronograma[0].exp.coExpediente;

                    objCronogramaEjecucionObra.ExpedienteTecnico = objExpedienteTecnicoOP;

                    if (lstCronograma[0].cro.feEmision.HasValue)
                    {
                        objCronogramaEjecucionObra.FechaEmision = lstCronograma[0].cro.feEmision.Value;
                    }
                    objCronogramaEjecucionObra.IdCronograma = lstCronograma[0].cro.coCronograma;
                    if (lstCronograma[0].cro.nuPlazoEjecucion.HasValue)
                    {
                        objCronogramaEjecucionObra.PlazoEjecucion = lstCronograma[0].cro.nuPlazoEjecucion.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return objCronogramaEjecucionObra;
        }

        public List<ActividadCronogramaOP> ObtieneActvidades(int pIntIdExpediente, int pIntIdCronograma)
        {
            List<ActividadCronogramaOP> lstActividades = new List<ActividadCronogramaOP>();
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_act_x_cron_ejec_obra(pIntIdExpediente, pIntIdCronograma).ToList();

                List<sp_gop_get_act_x_cron_ejec_obra_Result> lstActividadesTmp = objResult;

                foreach (sp_gop_get_act_x_cron_ejec_obra_Result objAct in lstActividadesTmp)
                {
                    ActividadCronogramaOP objActividadCronograma = new ActividadCronogramaOP();
                    if (objAct.NUCANTIDADRRHH.HasValue)
                    {
                        objActividadCronograma.CantidadRRHH = objAct.NUCANTIDADRRHH.Value;
                    }
                    if (objAct.NUCOSTODIRECTO.HasValue)
                    {
                        objActividadCronograma.Costo = objAct.NUCOSTODIRECTO.Value;
                    }
                    if (objAct.FEFINEJECUCION.HasValue)
                    {
                        objActividadCronograma.FechaFinEjec = objAct.FEFINEJECUCION.Value;
                    }
                    if (objAct.FEFINPROGRAMADA.HasValue)
                    {
                        objActividadCronograma.FechaFinProg = objAct.FEFINPROGRAMADA.Value;
                    }
                    if (objAct.FEINICIOEJECUCION.HasValue)
                    {
                        objActividadCronograma.FechaIniEjec = objAct.FEINICIOEJECUCION.Value;
                    }
                    if (objAct.FEINICIOPROGRAMADA.HasValue)
                    {
                        objActividadCronograma.FechaIniProg = objAct.FEINICIOPROGRAMADA.Value;
                    }
                    if (objAct.idPersonaNatural.HasValue)
                    {
                        objActividadCronograma.IdEmpleado = objAct.idPersonaNatural.Value;
                        objActividadCronograma.ResponsableNom = objAct.NOMBRES;
                        objActividadCronograma.ResponsableApe = objAct.APELLIDOPATERNO;
                        objActividadCronograma.IdTipoResponsable = "P";
                    }
                    if (objAct.idPersonaJuridica.HasValue)
                    {
                        objActividadCronograma.IdEmpleado = objAct.idPersonaJuridica.Value;
                        objActividadCronograma.ResponsableRazSoc  = objAct.RAZONSOCIAL;
                        objActividadCronograma.IdTipoResponsable = "E";
                    }
                    objActividadCronograma.Nombre = objAct.noActividad;
                    objActividadCronograma.IdActividad = objAct.COACTIVIDAD;

                    lstActividades.Add(objActividadCronograma);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstActividades;
        }

        public ActividadCronogramaOP ObtieneActvidadXId(int pIntIdCronograma, int pIntIdActividad)
        {
            ActividadCronogramaOP objActividadCronograma = null;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_act_cron_ejec_obra_x_id(pIntIdCronograma, pIntIdActividad).ToList();

                List<sp_gop_get_act_cron_ejec_obra_x_id_Result> lstActividad = objResult;

                foreach (sp_gop_get_act_cron_ejec_obra_x_id_Result objAct in lstActividad)
                {
                    objActividadCronograma = new ActividadCronogramaOP();
                    if (objAct.NUCANTIDADRRHH.HasValue)
                    {
                        objActividadCronograma.CantidadRRHH = objAct.NUCANTIDADRRHH.Value;
                    }
                    if (objAct.NUCOSTODIRECTO.HasValue)
                    {
                        objActividadCronograma.Costo = objAct.NUCOSTODIRECTO.Value;
                    }
                    if (objAct.FEFINEJECUCION.HasValue)
                    {
                        objActividadCronograma.FechaFinEjec = objAct.FEFINEJECUCION.Value;
                    }
                    if (objAct.FEFINPROGRAMADA.HasValue)
                    {
                        objActividadCronograma.FechaFinProg = objAct.FEFINPROGRAMADA.Value;
                    }
                    if (objAct.FEINICIOEJECUCION.HasValue)
                    {
                        objActividadCronograma.FechaIniEjec = objAct.FEINICIOEJECUCION.Value;
                    }
                    if (objAct.FEINICIOPROGRAMADA.HasValue)
                    {
                        objActividadCronograma.FechaIniProg = objAct.FEINICIOPROGRAMADA.Value;
                    }
                    if (objAct.idPersonaNatural.HasValue)
                    {
                        objActividadCronograma.IdEmpleado = objAct.idEmpleado;
                        objActividadCronograma.ResponsableNom = objAct.NOMBRES;
                        objActividadCronograma.ResponsableApe = objAct.APELLIDOPATERNO;
                        objActividadCronograma.IdTipoResponsable = "P";
                        if (objAct.idArea.HasValue)
                        {
                            objActividadCronograma.IdArea = objAct.idArea.Value;
                        }
                    }
                    if (objAct.idPersonaJuridica.HasValue)
                    {
                        objActividadCronograma.IdEmpleado = objAct.idEmpleado;
                        objActividadCronograma.ResponsableRazSoc = objAct.RAZONSOCIAL;
                        objActividadCronograma.IdTipoResponsable = "E";
                    }
                    objActividadCronograma.Nombre = objAct.noActividad;

                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return objActividadCronograma;
        }

        public List<ItemCombo> ObtieneAreas() {
            List<ItemCombo> lstAreas = new List<ItemCombo>();
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_areas().ToList();

                List<sp_gop_get_areas_Result> lstAreasTmp = objResult;

                foreach (sp_gop_get_areas_Result objArea in lstAreasTmp)
                {
                    ItemCombo objItemCombo = new ItemCombo();

                    objItemCombo.Id = objArea.idArea.ToString();
                    objItemCombo.Nombre = objArea.descArea;

                    lstAreas.Add(objItemCombo);
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstAreas;
        }

        public List<ItemCombo> ObtieneEmpleadosPersonaNatural(int pIntIdArea)
        {
            List<ItemCombo> lstEmpleados = new List<ItemCombo>();
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_emp_pern_x_area(pIntIdArea).ToList();

                List<sp_gop_get_emp_pern_x_area_Result> lstEmpleadosTmp = objResult;

                foreach (sp_gop_get_emp_pern_x_area_Result objEmpleado in lstEmpleadosTmp)
                {
                    ItemCombo objItemCombo = new ItemCombo();

                    objItemCombo.Id = objEmpleado.idEmpleado.ToString();
                    objItemCombo.Nombre = objEmpleado.ApellidoPaterno + " " + objEmpleado.Nombres;

                    lstEmpleados.Add(objItemCombo);
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstEmpleados;
        }

        public List<ItemCombo> ObtieneEmpleadosPersonaJuridica()
        {
            List<ItemCombo> lstEmpleados = new List<ItemCombo>();
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_emp_perj().ToList();

                List<sp_gop_get_emp_perj_Result> lstEmpleadosTmp = objResult;

                foreach (sp_gop_get_emp_perj_Result objEmpleado in lstEmpleadosTmp)
                {
                    ItemCombo objItemCombo = new ItemCombo();

                    objItemCombo.Id = objEmpleado.idEmpleado.ToString();
                    objItemCombo.Nombre = objEmpleado.RazonSocial;
                    lstEmpleados.Add(objItemCombo);

                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstEmpleados;
        }
    }
}
