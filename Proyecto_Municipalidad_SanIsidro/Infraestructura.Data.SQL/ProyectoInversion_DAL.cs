using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Core.Entities;

namespace Infraestructura.Data.SQL
{
    public class ProyectoInversion_DAL
    {
        public int Inserta(String pStrCodSNIP, String pStrDescripcion, String pStrNombre, int pIntIdVia, String pStrUbicacion, int pIntBeneficiarios, Decimal pDblValor)
        {
            int intResultado = -999;
            try
            {
                MuniIntegrado objContext = new MuniIntegrado();

                OP_PROYECTO_INVERSION_PUBLICA objProyectoInversion = new OP_PROYECTO_INVERSION_PUBLICA();
                //objProyectoInversion.coSNIP = pStrCodSNIP;
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
                MuniIntegrado objContext = new MuniIntegrado();

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
                //db_muniEntities objContext = new db_muniEntities();
                MuniIntegrado objContext = new MuniIntegrado();

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

                var lstProyectosTmp = (from pi in objContext.OP_PROYECTO_INVERSION_PUBLICA
                                       join via in objContext.MA_VIA on pi.coVia equals via.coVia
                                       where ((via.noTipoVia + " " + via.noNomVia + " " + pi.txUbicacion).ToLower().Contains(pStrUbicacion.ToLower()) || pStrUbicacion == "")
                                       && (pi.noNombre.ToLower().Contains(pStrNombre.ToLower()) || pStrNombre == "")
                                       && (pi.noEstado == pStrIdEstado || pStrIdEstado == "0")
                                       select new { pi, via });

                foreach (var objProyTmp in lstProyectosTmp)
                {
                    ProyectoInversion objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = objProyTmp.pi.coSNIP;
                    objProyecto.Nombre = objProyTmp.pi.noNombre;
                    objProyecto.IdProyecto = objProyTmp.pi.coProyecto;
                    objProyecto.Ubicacion = objProyTmp.pi.txUbicacion;
                    objProyecto.NomVia = objProyTmp.via.noNomVia;
                    objProyecto.TipoVia = objProyTmp.via.noTipoVia;
                    objProyecto.IdEstado = objProyTmp.pi.noEstado;
                    objProyecto.NomEstado = ObtieneEstados().Where(e => e.IdEstado == objProyTmp.pi.noEstado).First().Nombre;

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
                MuniIntegrado objContext = new MuniIntegrado();

                var lstProyectosTmp = (from pi in objContext.OP_PROYECTO_INVERSION_PUBLICA
                                       join via in objContext.MA_VIA on pi.coVia equals via.coVia
                                       where pi.coProyecto == pIntIdProyecto
                                       select new { pi, via }).ToList();

                //var lstProyectosTmp = objContext.op_proyecto_inversion_publica.Where(p => p.coProyecto == pIntIdProyecto).ToList();

                if (lstProyectosTmp.Count == 1)
                {
                    objProyecto = new ProyectoInversion();
                    objProyecto.CodSNIP = lstProyectosTmp[0].pi.coSNIP;
                    objProyecto.IdProyecto = lstProyectosTmp[0].pi.coProyecto;
                    objProyecto.Nombre = lstProyectosTmp[0].pi.noNombre;
                    objProyecto.Ubicacion = lstProyectosTmp[0].pi.txUbicacion;
                    objProyecto.IdVia = lstProyectosTmp[0].pi.coVia;
                    objProyecto.Ubicacion = lstProyectosTmp[0].pi.txUbicacion;
                    objProyecto.Descripcion = lstProyectosTmp[0].pi.txDescripcion;
                    objProyecto.IdEstado = lstProyectosTmp[0].pi.noEstado;
                    objProyecto.NomEstado = ObtieneEstados().Where(e => e.IdEstado == lstProyectosTmp[0].pi.noEstado).First().Nombre;
                    if (lstProyectosTmp[0].pi.nuBeneficiarios.HasValue)
                    {
                        objProyecto.Beneficiarios = lstProyectosTmp[0].pi.nuBeneficiarios.Value;
                    }
                    objProyecto.ValorReferencial = lstProyectosTmp[0].pi.nuValorReferencialPerfil;
                    objProyecto.TipoVia = lstProyectosTmp[0].via.noTipoVia;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return objProyecto;
        }

        public List<EstadoProyectoInversion> ObtieneEstados()
        {
            List<EstadoProyectoInversion> lstEstados = new List<EstadoProyectoInversion>();

            lstEstados.Add(new EstadoProyectoInversion { IdEstado = ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA, Nombre = "En consulta" });
            lstEstados.Add(new EstadoProyectoInversion { IdEstado = ProyectoInversion.STR_ID_ESTADO_VIABLE, Nombre = "Viable" });
            lstEstados.Add(new EstadoProyectoInversion { IdEstado = ProyectoInversion.STR_ID_ESTADO_INVIABLE, Nombre = "Inviable" });
            lstEstados.Add(new EstadoProyectoInversion { IdEstado = "A", Nombre = "Adjudicado" });
            lstEstados.Add(new EstadoProyectoInversion { IdEstado = "C", Nombre = "Cerrado" });

            return lstEstados;
        }

    }
}
