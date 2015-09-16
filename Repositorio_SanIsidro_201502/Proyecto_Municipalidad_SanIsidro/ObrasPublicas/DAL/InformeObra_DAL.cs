using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObrasPublicas.Entities;

namespace ObrasPublicas.DAL
{
    public class InformeObra_DAL
    {
        public int Inserta(int pIntIdProyecto, String pStrTitulo, String pStrConclusion, String pStrRecomendacion, String pStrTipoInforme)
        {
            int intResultado = -999;
            int intRows;
            try
            {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                Entities.ProyectoInversion objProyectoInversion =  objProyectoInversion_DAL.ObtieneXId(pIntIdProyecto);

                if (objProyectoInversion != null) {

                    if (objProyectoInversion.IdEstado == Entities.ProyectoInversion.STR_ID_ESTADO_ADJUDICADO)
                    {
                        EntregaMaterial_DAL objEntregaMaterial_DAL = new EntregaMaterial_DAL();
                        
                        ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                        
                        OP_INFORME_OBRA objOP_INFORME_OBRA = new OP_INFORME_OBRA();
                        objOP_INFORME_OBRA.coProyecto = pIntIdProyecto;
                        objOP_INFORME_OBRA.flEstado = 1;
                        objOP_INFORME_OBRA.feEmision = DateTime.Now;
                        objOP_INFORME_OBRA.noTipoInforme = pStrTipoInforme;
                        objOP_INFORME_OBRA.txConclusion = pStrConclusion;
                        objOP_INFORME_OBRA.txRecomendacion = pStrRecomendacion;
                        objOP_INFORME_OBRA.txTituloInforme = pStrTitulo;

                        List<OP_ENTREGA_MATERIAL> lstEntregas = objContext.OP_ENTREGA_MATERIAL.Where(ent => ent.coProyecto == pIntIdProyecto).ToList();

                        foreach (var objEntrega in lstEntregas) {
                            objEntrega.OP_INFORME_OBRA.Add(objOP_INFORME_OBRA);
                        }

                        var lstActividades = (from act in objContext.OP_ACTIVIDAD_EJECUCION
                                              join cro in objContext.OP_CRONOGRAMA_EJECUCION on act.coCronograma equals cro.coCronograma
                                              join exp in objContext.OP_EXPEDIENTE_TECNICO on cro.coExpediente equals exp.coExpediente
                                              join proy in objContext.OP_PROYECTO_INVERSION_PUBLICA on exp.coProyecto equals proy.coProyecto
                                              where proy.coProyecto == pIntIdProyecto
                                              select new { act }).ToList();

                        foreach (var objActividad in lstActividades)
                        {
                            objActividad.act.OP_INFORME_OBRA.Add(objOP_INFORME_OBRA);
                        }
                        
                        objContext.AddToOP_INFORME_OBRA(objOP_INFORME_OBRA);
                        
                        intRows = objContext.SaveChanges();

                        if (intRows > 0)
                        {
                            intResultado = 1;
                        }
                    }
                    else
                    {
                        intResultado = -998;
                    }
                }
            }
            catch (Exception ex)
            {
                intResultado = -999;
            }
            return intResultado;
        }

        public int Actualiza(int pIntIdInforme, int pIntIdProyecto, String pStrConclusion, String pStrRecomendacion)
        {
            int intResultado = -999;
            int intRows;
            try
            {

                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                Entities.ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(pIntIdProyecto);

                if (objProyectoInversion != null)
                {
                    if (objProyectoInversion.IdEstado == Entities.ProyectoInversion.STR_ID_ESTADO_ADJUDICADO)
                    {
                        ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                        OP_INFORME_OBRA objOP_INFORME_OBRA = objContext.OP_INFORME_OBRA.First(x => x.coInforme == pIntIdInforme);

                        if (objOP_INFORME_OBRA != null)
                        {
                            objOP_INFORME_OBRA.txConclusion = pStrConclusion;
                            objOP_INFORME_OBRA.txRecomendacion = pStrRecomendacion;

                            intRows = objContext.SaveChanges();

                            if (intRows > 0)
                            {
                                intResultado = 1;
                            }
                        }
                    }
                    else
                    {
                        intResultado = -998;
                    }
                }
            }
            catch (Exception ex)
            {
                intResultado = -999;
            }
            return intResultado;
        }

        public InformeObra ObtieneXId(int pIntIdInforme)
        {
            InformeObra objInformeObra = null;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                var lstInformeObra = (from io in objContext.OP_INFORME_OBRA
                                     join proy in objContext.OP_PROYECTO_INVERSION_PUBLICA on io.coProyecto equals proy.coProyecto
                                     where io.coInforme == pIntIdInforme
                                     select new { io, proy }).ToList();

                if (lstInformeObra != null)
                {
                    objInformeObra = new InformeObra();

                    ProyectoInversion objProyectoInversion = new ProyectoInversion();
                    objProyectoInversion.IdProyecto = lstInformeObra[0].proy.coProyecto;
                    objProyectoInversion.Nombre = lstInformeObra[0].proy.noNombre;
                    objInformeObra.ProyectoInversion = objProyectoInversion;

                    objInformeObra.IdInforme = pIntIdInforme;

                    if (lstInformeObra[0].io.feEmision.HasValue)
                    {
                        objInformeObra.FechaEmision = lstInformeObra[0].io.feEmision.Value;
                    }
                    if (lstInformeObra[0].io.flEstado.HasValue) {
                        objInformeObra.IdEstado = lstInformeObra[0].io.flEstado.Value;
                        objInformeObra.NomEstado = ObtieneEstados().Where(est => est.Id == lstInformeObra[0].io.flEstado.Value.ToString()).First().Nombre;
                    }
                    objInformeObra.TipoInforme = lstInformeObra[0].io.noTipoInforme;
                    objInformeObra.Recomendacion = lstInformeObra[0].io.txRecomendacion;
                    objInformeObra.Titulo = lstInformeObra[0].io.txTituloInforme;
                    objInformeObra.Conclusion = lstInformeObra[0].io.txConclusion;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return objInformeObra;
        }

        public List<InformeObra> ObtieneXIdProyecto(int pIntIdProyecto)
        {
            List<InformeObra> lstInformes = new List<InformeObra>();
            try
            {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                Entities.ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(pIntIdProyecto);
                
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                var lstInformeObra = (from io in objContext.OP_INFORME_OBRA
                                      join proy in objContext.OP_PROYECTO_INVERSION_PUBLICA on io.coProyecto equals proy.coProyecto
                                      where io.coProyecto == pIntIdProyecto
                                      select new { io }).OrderByDescending(a => a.io.feEmision).ToList();

                if (lstInformeObra != null)
                {
                    foreach(var objInformeObraTmp in lstInformeObra){
                        InformeObra objInformeObra = new InformeObra();

                        objProyectoInversion.IdProyecto = objProyectoInversion.IdProyecto;
                        objProyectoInversion.Nombre = objProyectoInversion.Nombre;
                        objInformeObra.ProyectoInversion = objProyectoInversion;

                        objInformeObra.IdInforme = objInformeObraTmp.io.coInforme;

                        if (lstInformeObra[0].io.feEmision.HasValue)
                        {
                            objInformeObra.FechaEmision = objInformeObraTmp.io.feEmision.Value;
                        }
                        if (lstInformeObra[0].io.flEstado.HasValue)
                        {
                            objInformeObra.IdEstado = objInformeObraTmp.io.flEstado.Value;
                            objInformeObra.NomEstado = ObtieneEstados().Where(est => est.Id == objInformeObraTmp.io.flEstado.Value.ToString()).First().Nombre;
                        }
                        objInformeObra.TipoInforme = objInformeObraTmp.io.noTipoInforme;
                        objInformeObra.Titulo = objInformeObraTmp.io.txTituloInforme;
                        lstInformes.Add(objInformeObra);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstInformes;
        }
        public List<ItemCombo> ObtieneEstados()
        {
            List<ItemCombo> lstEstados = new List<ItemCombo>();
            lstEstados.Add(new ItemCombo { Id = InformeObra.INT_ID_ESTADO_GENERADO.ToString(), Nombre = "Generado" });
            lstEstados.Add(new ItemCombo { Id = InformeObra.INT_ID_ESTADO_ANULADO.ToString(), Nombre = "Anulado" });

            return lstEstados;
        }

        public int Anular(int pIntIdProyecto, int pIntIdInforme, int pIntIdEstado) {
            int intResultado = -999;
            int intRows;
            try
            {

                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                Entities.ProyectoInversion objProyectoInversion = objProyectoInversion_DAL.ObtieneXId(pIntIdProyecto);

                if (objProyectoInversion != null)
                {
                    if (objProyectoInversion.IdEstado == Entities.ProyectoInversion.STR_ID_ESTADO_ADJUDICADO)
                    {
                        ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                        OP_INFORME_OBRA objOP_INFORME_OBRA = objContext.OP_INFORME_OBRA.First(x => x.coInforme == pIntIdInforme);

                        if (objOP_INFORME_OBRA != null)
                        {
                            objOP_INFORME_OBRA.flEstado = pIntIdEstado;

                            intRows = objContext.SaveChanges();

                            if (intRows > 0)
                            {
                                intResultado = 1;
                            }
                        }
                    }
                    else
                    {
                        intResultado = -998;
                    }
                }
            }
            catch (Exception ex)
            {
                intResultado = -999;
            }
            return intResultado;
        }
    }
}