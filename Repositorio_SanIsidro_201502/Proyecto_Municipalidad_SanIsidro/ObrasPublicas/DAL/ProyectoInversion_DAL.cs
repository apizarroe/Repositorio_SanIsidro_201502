using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObrasPublicas.Entities;
using ObrasPublicas.DAL;
using Infraestructura.Data.SQL;

namespace ObrasPublicas.DAL
{
    public class ProyectoInversion_DAL
    {
        public int Inserta(String pStrCodSNIP, String pStrDescripcion, String pStrNombre, int pIntIdVia, String pStrUbicacion, int pIntBeneficiarios, Decimal pDblValor)
        {
            int intResultado = -999;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                
                OP_PROYECTO_INVERSION_PUBLICA objProyectoInversion = new OP_PROYECTO_INVERSION_PUBLICA();
                
                objProyectoInversion.feRegistro = DateTime.Now;
                objProyectoInversion.noNombre = pStrNombre;
                objProyectoInversion.txUbicacion = pStrUbicacion;
                objProyectoInversion.coVia = pIntIdVia;
                objProyectoInversion.txDescripcion = pStrDescripcion;
                objProyectoInversion.nuBeneficiarios = pIntBeneficiarios;
                objProyectoInversion.nuValorReferencialPerfil = pDblValor;
                objProyectoInversion.noEstado = ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA;

                objContext.AddToOP_PROYECTO_INVERSION_PUBLICA(objProyectoInversion);
                int intRows = objContext.SaveChanges();

                if (intRows > 0)
                {
                    intResultado = 1;
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("IX_op_proyecto_inversion_publica"))
                {
                    intResultado = -998;
                }
            }
            return intResultado;
        }

        public int Actualiza(int pIntIdProyecto, String pStrCodSNIP, String pStrDescripcion,
            String pStrNombre, int pIntIdVia, String pStrUbicacion, int pIntBeneficiarios, Decimal pDblValor, String pStrIdEstado)
        {
            int intResultado = -999;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                var lstProyectosTmp = (from pi in objContext.OP_PROYECTO_INVERSION_PUBLICA
                                       where pi.coSNIP == pStrCodSNIP && pi.coProyecto != pIntIdProyecto
                                       select new { pi}).ToList();

                if (lstProyectosTmp.Count == 0)
                {
                    OP_PROYECTO_INVERSION_PUBLICA objProyecto = objContext.OP_PROYECTO_INVERSION_PUBLICA.First(pi => pi.coProyecto == pIntIdProyecto);

                    if (objProyecto == null)
                    {
                        intResultado = -996;
                    }
                    else
                    {
                        if (objProyecto.noEstado != ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)
                        {
                            intResultado = -998;
                        }
                        else
                        {
                            if (pStrIdEstado != ProyectoInversion.STR_ID_ESTADO_INVIABLE)
                            {
                                objProyecto.coSNIP = pStrCodSNIP;
                            }
                            objProyecto.txDescripcion = pStrDescripcion;
                            objProyecto.noNombre = pStrNombre;
                            objProyecto.coVia = pIntIdVia;
                            objProyecto.txUbicacion = pStrUbicacion;
                            objProyecto.nuBeneficiarios = pIntBeneficiarios;
                            objProyecto.nuValorReferencialPerfil = pDblValor;
                            objProyecto.noEstado = pStrIdEstado;
                            intResultado = objContext.SaveChanges();
                        }
                    }
                }
                else
                {
                    intResultado = -997;
                }
            }
            catch (Exception ex)
            {
            }
            return intResultado;
        }

        public List<ProyectoInversion> BuscarXFiltro(String pStrCodSNIP, String pStrNombre, String pStrUbicacion, String pStrIdEstado)
        {
            List<ProyectoInversion> lstProyectos = new List<ProyectoInversion>();
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                if (String.IsNullOrWhiteSpace(pStrNombre))
                {
                    pStrNombre = "";
                }
                if (String.IsNullOrWhiteSpace(pStrUbicacion))
                {
                    pStrUbicacion = "";
                }
                if (String.IsNullOrWhiteSpace(pStrIdEstado))
                {
                    pStrIdEstado = "0";
                }

                var objResult = objContext.sp_gop_get_proy_x_filtro(pStrNombre, pStrCodSNIP, pStrUbicacion, pStrIdEstado).ToList();

                List<sp_gop_get_proy_x_filtro_Result> lstProyectosTmp = objResult;

                foreach (var objProyTmp in lstProyectosTmp)
                {
                    ProyectoInversion objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = objProyTmp.coSNIP;
                    objProyecto.Nombre = objProyTmp.noNombre;
                    objProyecto.IdProyecto = objProyTmp.coProyecto;
                    objProyecto.Ubicacion = objProyTmp.txUbicacion;
                    objProyecto.NomVia = objProyTmp.noNomVia;
                    objProyecto.TipoVia = objProyTmp.noTipoVia;
                    objProyecto.IdEstado = objProyTmp.noEstado;
                    objProyecto.NomEstado = ObtieneEstados(null).Where(e => e.Id == objProyTmp.noEstado).First().Nombre;

                    lstProyectos.Add(objProyecto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstProyectos;
        }

        public ProyectoInversion ObtieneXId(int pIntIdProyecto)
        {
            ProyectoInversion objProyecto = null;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                //var lstProyectosTmp = (from pi in objContext.OP_PROYECTO_INVERSION_PUBLICA
                //                       join via in objContextIntegrado.MA_VIA on pi.coVia equals via.coVia
                //                       where pi.coProyecto == pIntIdProyecto
                //                       select new { pi, via }).ToList();

                var objResult = objContext.sp_gop_get_proy_x_id(pIntIdProyecto).ToList();

                List<sp_gop_get_proy_x_id_Result> lstProyectosTmp = objResult;

                //var lstProyectosTmp = objContext.op_proyecto_inversion_publica.Where(p => p.coProyecto == pIntIdProyecto).ToList();

                if (lstProyectosTmp.Count == 1)
                {
                    objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = lstProyectosTmp[0].coSNIP;
                    objProyecto.IdProyecto = lstProyectosTmp[0].coProyecto;
                    objProyecto.Nombre = lstProyectosTmp[0].noNombre;
                    objProyecto.Ubicacion = lstProyectosTmp[0].txUbicacion;
                    objProyecto.IdVia = lstProyectosTmp[0].coVia;
                    objProyecto.Ubicacion = lstProyectosTmp[0].txUbicacion;
                    objProyecto.NomVia = lstProyectosTmp[0].noNomVia;
                    objProyecto.Descripcion = lstProyectosTmp[0].txDescripcion;
                    objProyecto.IdEstado = lstProyectosTmp[0].noEstado;
                    objProyecto.NomEstado = ObtieneEstados(null).Where(e => e.Id == lstProyectosTmp[0].noEstado).First().Nombre;
                    if (lstProyectosTmp[0].nuBeneficiarios.HasValue)
                    {
                        objProyecto.Beneficiarios = lstProyectosTmp[0].nuBeneficiarios.Value;
                    }
                    objProyecto.ValorReferencial = lstProyectosTmp[0].nuValorReferencialPerfil;
                    objProyecto.TipoVia = lstProyectosTmp[0].noTipoVia;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return objProyecto;
        }

        public List<ItemCombo> ObtieneEstados(String pStrTipo)
        {
            List<ItemCombo> lstEstados = new List<ItemCombo>();

            if (pStrTipo == null)
            {
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA, Nombre = "En consulta" });
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_VIABLE, Nombre = "Viable" });
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_INVIABLE, Nombre = "Inviable" });
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_ADJUDICADO, Nombre = "Adjudicado" });
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_CERRADO, Nombre = "Cerrado" });
            }
            else if (pStrTipo == "EXP")
            {
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_VIABLE, Nombre = "Viable" });
            }
            else if (pStrTipo == "CRO")
            {
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_VIABLE, Nombre = "Viable" });
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_ADJUDICADO, Nombre = "Adjudicado" });
            }
            else if (pStrTipo == "INFO")
            {
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_ADJUDICADO, Nombre = "Adjudicado" });
            }
            else if (pStrTipo == "ENTMAT")
            {
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_VIABLE, Nombre = "Viable" });
                lstEstados.Add(new ItemCombo { Id = ProyectoInversion.STR_ID_ESTADO_ADJUDICADO, Nombre = "Adjudicado" });
            }
            else { 
            }

            return lstEstados;
        }

        public List<ProyectoInversion> BuscarXFiltroConExpedientes(String pStrCodSNIP, String pStrNombre, String pStrUbicacion, String pStrIdEstado)
        {
            List<ProyectoInversion> lstProyectos = new List<ProyectoInversion>();
            try
            {
                if (String.IsNullOrWhiteSpace(pStrNombre))
                {
                    pStrNombre = "";
                }
                if (String.IsNullOrWhiteSpace(pStrUbicacion))
                {
                    pStrUbicacion = "";
                }
                if (String.IsNullOrWhiteSpace(pStrIdEstado))
                {
                    pStrIdEstado = "0";
                }

                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_proy_con_exp(pStrNombre,pStrCodSNIP,pStrUbicacion,pStrIdEstado).ToList();

                List<sp_gop_get_proy_con_exp_Result> lstProyectosTmp = objResult;

                foreach (var objProyTmp in lstProyectosTmp)
                {
                    ProyectoInversion objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = objProyTmp.coSNIP;
                    objProyecto.Nombre = objProyTmp.noNombre;
                    objProyecto.IdProyecto = objProyTmp.coProyecto;
                    objProyecto.IdExpediente = objProyTmp.coExpediente;
                    objProyecto.Ubicacion = objProyTmp.txUbicacion;
                    objProyecto.NomVia = objProyTmp.noNomVia;
                    objProyecto.TipoVia = objProyTmp.noTipoVia;
                    objProyecto.IdEstado = objProyTmp.noEstado;
                    objProyecto.NomEstado = ObtieneEstados(null).Where(e => e.Id == objProyTmp.noEstado).First().Nombre;

                    lstProyectos.Add(objProyecto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstProyectos;
        }

        public List<ProyectoInversion> BuscarXFiltroSinExpedientes(String pStrCodSNIP, String pStrNombre, String pStrUbicacion, String pStrIdEstado)
        {
            List<ProyectoInversion> lstProyectos = new List<ProyectoInversion>();
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            try
            {
                if (String.IsNullOrWhiteSpace(pStrNombre))
                {
                    pStrNombre = "";
                }
                if (String.IsNullOrWhiteSpace(pStrUbicacion))
                {
                    pStrUbicacion = "";
                }
                if (String.IsNullOrWhiteSpace(pStrIdEstado))
                {
                    pStrIdEstado = "0";
                }

                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_proy_sin_exp(pStrNombre, pStrCodSNIP, pStrUbicacion, pStrIdEstado).ToList();

                List<sp_gop_get_proy_sin_exp_Result> lstProyectosTmp = objResult;

                foreach (var objProyTmp in lstProyectosTmp)
                {
                    ProyectoInversion objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = objProyTmp.coSNIP;
                    objProyecto.Nombre = objProyTmp.noNombre;
                    objProyecto.IdProyecto = objProyTmp.coProyecto;
                    objProyecto.Ubicacion = objProyTmp.txUbicacion;
                    objProyecto.NomVia = objProyTmp.noNomVia;
                    objProyecto.TipoVia = objProyTmp.noTipoVia;
                    objProyecto.IdEstado = objProyTmp.noEstado;
                    objProyecto.NomEstado = ObtieneEstados(null).Where(e => e.Id == objProyTmp.noEstado).First().Nombre;

                    lstProyectos.Add(objProyecto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstProyectos;
        }

        public List<ProyectoInversion> BuscarXFiltroConCronograma(String pStrCodSNIP, String pStrNombre, String pStrUbicacion, String pStrIdEstado)
        {
            List<ProyectoInversion> lstProyectos = new List<ProyectoInversion>();
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            try
            {
                if (String.IsNullOrWhiteSpace(pStrNombre))
                {
                    pStrNombre = "";
                }
                if (String.IsNullOrWhiteSpace(pStrUbicacion))
                {
                    pStrUbicacion = "";
                }
                if (String.IsNullOrWhiteSpace(pStrIdEstado))
                {
                    pStrIdEstado = "0";
                }

                //var lstProyectosTmp = (from pi in objContext.OP_PROYECTO_INVERSION_PUBLICA
                //                       join via in objContextIntegrado.MA_VIA on pi.coVia equals via.coVia
                //                       join exp in objContext.OP_EXPEDIENTE_TECNICO on pi.coProyecto equals exp.coProyecto
                //                       join cro in objContext.OP_CRONOGRAMA_EJECUCION on exp.coExpediente equals cro.coExpediente
                //                       where ((via.noTipoVia + " " + via.noNomVia + " " + pi.txUbicacion).ToLower().Contains(pStrUbicacion.ToLower()) || pStrUbicacion == "")
                //                       && (pi.noNombre.ToLower().Contains(pStrNombre.ToLower()) || pStrNombre == "")
                //                       && (pi.noEstado == pStrIdEstado || pStrIdEstado == "0")
                //                       select new { pi, via, exp, cro }).OrderBy(x=>x.exp.coExpediente).ThenBy(x=> x.cro.feEmision);

                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_proy_con_cro(pStrNombre, pStrCodSNIP, pStrUbicacion, pStrIdEstado).ToList();

                List<sp_gop_get_proy_con_cro_Result> lstProyectosTmp = objResult;

                foreach (var objProyTmp in lstProyectosTmp)
                {
                    ProyectoInversion objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = objProyTmp.coSNIP;
                    objProyecto.Nombre = objProyTmp.noNombre;
                    objProyecto.IdProyecto = objProyTmp.coProyecto;
                    objProyecto.Ubicacion = objProyTmp.txUbicacion;
                    objProyecto.NomVia = objProyTmp.noNomVia;
                    objProyecto.TipoVia = objProyTmp.noTipoVia;
                    objProyecto.IdEstado = objProyTmp.noEstado;
                    objProyecto.NomEstado = ObtieneEstados(null).Where(e => e.Id == objProyTmp.noEstado).First().Nombre;

                    objProyecto.IdExpediente = objProyTmp.coExpediente;
                    objProyecto.IdCronograma = objProyTmp.coCronograma;
                    if (objProyTmp.feEmision.HasValue)
                    {
                        objProyecto.FechaEmisionCrono = objProyTmp.feEmision.Value;
                    }
                    if (objProyTmp.nuPlazoEjecucion.HasValue)
                    {
                        objProyecto.PlazoEjecucionCrono = objProyTmp.nuPlazoEjecucion.Value;
                    }
                    lstProyectos.Add(objProyecto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstProyectos;
        }

        public List<ProyectoInversion> BuscarXFiltroSinCronograma(String pStrCodSNIP, String pStrNombre, String pStrUbicacion, String pStrIdEstado)
        {
            List<ProyectoInversion> lstProyectos = new List<ProyectoInversion>();
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            try
            {
                if (String.IsNullOrWhiteSpace(pStrNombre))
                {
                    pStrNombre = "";
                }
                if (String.IsNullOrWhiteSpace(pStrUbicacion))
                {
                    pStrUbicacion = "";
                }
                if (String.IsNullOrWhiteSpace(pStrIdEstado))
                {
                    pStrIdEstado = "0";
                }

                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_proy_sin_cro(pStrNombre, pStrCodSNIP, pStrUbicacion, pStrIdEstado).ToList();

                List<sp_gop_get_proy_sin_cro_Result> lstProyectosTmp = objResult;

                foreach (var objProyTmp in lstProyectosTmp)
                {
                    ProyectoInversion objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = objProyTmp.coSNIP;
                    objProyecto.Nombre = objProyTmp.noNombre;
                    objProyecto.IdProyecto = objProyTmp.coProyecto;
                    objProyecto.Ubicacion = objProyTmp.txUbicacion;
                    objProyecto.NomVia = objProyTmp.noNomVia;
                    objProyecto.IdExpediente = objProyTmp.coExpediente;
                    objProyecto.TipoVia = objProyTmp.noTipoVia;
                    objProyecto.IdEstado = objProyTmp.noEstado;
                    objProyecto.NomEstado = ObtieneEstados(null).Where(e => e.Id == objProyTmp.noEstado).First().Nombre;

                    lstProyectos.Add(objProyecto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstProyectos;
        }

        public List<ProyectoInversion> BuscarXFiltroConEntregaMaterial(String pStrCodSNIP, String pStrNombre, String pStrUbicacion, String pStrIdEstado)
        {
            List<ProyectoInversion> lstProyectos = new List<ProyectoInversion>();
            ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
            try
            {
                if (String.IsNullOrWhiteSpace(pStrNombre))
                {
                    pStrNombre = "";
                }
                if (String.IsNullOrWhiteSpace(pStrUbicacion))
                {
                    pStrUbicacion = "";
                }
                if (String.IsNullOrWhiteSpace(pStrIdEstado))
                {
                    pStrIdEstado = "0";
                }

                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_proy_con_entmat(pStrNombre, pStrCodSNIP, pStrUbicacion, pStrIdEstado).ToList();

                List<sp_gop_get_proy_con_entmat_Result> lstProyectosTmp = objResult;

                foreach (var objProyTmp in lstProyectosTmp)
                {
                    ProyectoInversion objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = objProyTmp.coSNIP;
                    objProyecto.Nombre = objProyTmp.noNombre;
                    objProyecto.IdProyecto = objProyTmp.coProyecto;
                    objProyecto.Ubicacion = objProyTmp.txUbicacion;
                    objProyecto.NomVia = objProyTmp.noNomVia;
                    objProyecto.TipoVia = objProyTmp.noTipoVia;
                    objProyecto.IdEstado = objProyTmp.noEstado;
                    objProyecto.NomEstado = ObtieneEstados(null).Where(e => e.Id == objProyTmp.noEstado).First().Nombre;

                    lstProyectos.Add(objProyecto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstProyectos;
        }

        public List<ProyectoInversion> BuscarXFiltroConInformesObra(String pStrCodSNIP, String pStrNombre, String pStrUbicacion, String pStrIdEstado)
        {
            List<ProyectoInversion> lstProyectos = new List<ProyectoInversion>();
            try
            {
                if (String.IsNullOrWhiteSpace(pStrNombre))
                {
                    pStrNombre = "";
                }
                if (String.IsNullOrWhiteSpace(pStrUbicacion))
                {
                    pStrUbicacion = "";
                }
                if (String.IsNullOrWhiteSpace(pStrIdEstado))
                {
                    pStrIdEstado = "0";
                }

                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var objResult = objContext.sp_gop_get_proy_con_infobra(pStrNombre, pStrCodSNIP, pStrUbicacion, pStrIdEstado).ToList();

                List<sp_gop_get_proy_con_infobra_Result> lstProyectosTmp = objResult;

                foreach (var objProyTmp in lstProyectosTmp)
                {
                    ProyectoInversion objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = objProyTmp.coSNIP;
                    objProyecto.Nombre = objProyTmp.noNombre;
                    objProyecto.IdProyecto = objProyTmp.coProyecto;
                    objProyecto.Ubicacion = objProyTmp.txUbicacion;
                    objProyecto.NomVia = objProyTmp.noNomVia;
                    objProyecto.TipoVia = objProyTmp.noTipoVia;
                    objProyecto.IdEstado = objProyTmp.noEstado;
                    objProyecto.NomEstado = ObtieneEstados(null).Where(e => e.Id == objProyTmp.noEstado).First().Nombre;
                    objProyecto.IdExpediente = objProyTmp.coExpediente;

                    lstProyectos.Add(objProyecto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstProyectos;
        }

    }
}
