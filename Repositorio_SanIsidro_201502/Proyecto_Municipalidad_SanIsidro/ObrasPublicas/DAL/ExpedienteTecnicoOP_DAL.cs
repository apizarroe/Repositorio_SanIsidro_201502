using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObrasPublicas.Entities;
using ObrasPublicas.DAL;
using Infraestructura.Data.SQL;
using System.Data.Objects;

namespace ObrasPublicas.DAL
{
     public class ExpedienteTecnicoOP_DAL
    {
        public int Inserta(int pIntIdProyecto, String pStrDescripcion, String pStrEspecificaciones, 
            Decimal pDecValorReferencial, String pStrTipoEjecutor, String pStrNomEjecutor, String pStrApeEjecutor, String pStrRazonSocialEjecutor, 
            String pStrNomContacto, String pStrApeContaco, String pStrEmailContacto,
            String pStrTelfContacto, String pStrDireccionContacto, String pStrNomSupervisor,
            String pStrApeSupervisor, String pStrTelfSupervisor, String pStrEmailSupervisor,
            List<DocumentoExpedienteTecnicoOP> pLstDocumentos)
        {
            int intResultado = -999;

            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                ObjectParameter objResult = new ObjectParameter("pIntIdExp_out", typeof(int));
                //int intRes = 
                 objContext.sp_gop_ins_exp(pIntIdProyecto, pStrDescripcion, pStrEspecificaciones, pDecValorReferencial,
                    pStrTipoEjecutor, pStrNomEjecutor, pStrApeEjecutor, pStrRazonSocialEjecutor, pStrNomContacto,
                    pStrApeContaco, pStrEmailContacto, pStrTelfContacto, pStrDireccionContacto, pStrNomSupervisor, pStrApeSupervisor,
                    pStrTelfSupervisor, pStrEmailSupervisor, objResult);

                 int intIdExpediente = Convert.ToInt32(objResult.Value.ToString());

                 foreach (var objDoc in pLstDocumentos) {
                     OP_DOCUMENTO_EXPEDIENTE_TECNICO objOP_DOCUMENTO_EXPEDIENTE_TECNICO = new OP_DOCUMENTO_EXPEDIENTE_TECNICO();
                     objOP_DOCUMENTO_EXPEDIENTE_TECNICO.coDocumento = objDoc.IdDocumento;
                     objOP_DOCUMENTO_EXPEDIENTE_TECNICO.coExpediente = intIdExpediente;
                     objOP_DOCUMENTO_EXPEDIENTE_TECNICO.feEmision = DateTime.Now;
                     objOP_DOCUMENTO_EXPEDIENTE_TECNICO.nomArchivo = objDoc.NomArchivo;
                     objOP_DOCUMENTO_EXPEDIENTE_TECNICO.noTipoDocExpTec = objDoc.TipoDocumento;
                     objOP_DOCUMENTO_EXPEDIENTE_TECNICO.nuNroDOcumento = objDoc.NroDocumento;
                     objOP_DOCUMENTO_EXPEDIENTE_TECNICO.txDescripcion = objDoc.Descripcion;
                     objOP_DOCUMENTO_EXPEDIENTE_TECNICO.txRutaArchivo = objDoc.RutaArchivo;

                     objContext.AddToOP_DOCUMENTO_EXPEDIENTE_TECNICO(objOP_DOCUMENTO_EXPEDIENTE_TECNICO);
                 }
                 objContext.SaveChanges();

                 if (intIdExpediente > 0)
                 {
                     intResultado = 1;
                 }
                 else {
                     intResultado = intIdExpediente;
                 }
            }
            catch (Exception ex) {

            }
            return intResultado;
        }

        public int Actualiza(int pIntIdExpediente, int pIntIdProyecto, String pStrDescripcion, String pStrEspecificaciones,
            Decimal pDecValorReferencial, String pStrTipoEjecutor, String pStrNomEjecutor, String pStrApeEjecutor, String pStrRazonSocialEjecutor,
            String pStrNomContacto, String pStrApeContaco, String pStrEmailContacto,
            String pStrTelfContacto, String pStrDireccionContacto, String pStrNomSupervisor,
            String pStrApeSupervisor, String pStrTelfSupervisor, String pStrEmailSupervisor,
            int pIntIdEjecutor, int pIntIdContacto, int pIntIdSupervisor, List<DocumentoExpedienteTecnicoOP> pLstDocumentos)
        {
            int intResultado = -999;

            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                ObjectParameter objResult = new ObjectParameter("pIntIdExp_out", typeof(int));
                
                objContext.sp_gop_upd_exp(pIntIdExpediente, pIntIdProyecto, pStrDescripcion, pStrEspecificaciones, pDecValorReferencial,
                   pStrTipoEjecutor, pStrNomEjecutor, pStrApeEjecutor, pStrRazonSocialEjecutor, pStrNomContacto,
                   pStrApeContaco, pStrEmailContacto, pStrTelfContacto, pStrDireccionContacto, pStrNomSupervisor, pStrApeSupervisor,
                   pStrTelfSupervisor, pStrEmailSupervisor, pIntIdEjecutor, pIntIdContacto,pIntIdSupervisor, objResult);


                var lstDocumentos = objContext.OP_DOCUMENTO_EXPEDIENTE_TECNICO.Where(doc => doc.coExpediente == pIntIdExpediente).ToList();
                
                foreach (OP_DOCUMENTO_EXPEDIENTE_TECNICO objDoc in lstDocumentos)
                {
                    objContext.OP_DOCUMENTO_EXPEDIENTE_TECNICO.DeleteObject(objDoc);
                }

                if (pLstDocumentos != null)
                {
                    foreach (DocumentoExpedienteTecnicoOP objDocumento in pLstDocumentos)
                    {
                        OP_DOCUMENTO_EXPEDIENTE_TECNICO objDocumentoExpTec = new OP_DOCUMENTO_EXPEDIENTE_TECNICO();
                        objDocumentoExpTec.coExpediente = pIntIdExpediente;
                        objDocumentoExpTec.feEmision = objDocumento.FechaEmision;
                        objDocumentoExpTec.noTipoDocExpTec = objDocumento.TipoDocumento;
                        objDocumentoExpTec.nuNroDOcumento = objDocumento.NroDocumento;
                        objDocumentoExpTec.txDescripcion = objDocumento.Descripcion;
                        objDocumentoExpTec.txRutaArchivo = objDocumento.RutaArchivo;
                        objDocumentoExpTec.nomArchivo = objDocumento.NomArchivo;
                        objContext.AddToOP_DOCUMENTO_EXPEDIENTE_TECNICO(objDocumentoExpTec);
                    }
                }
                objContext.SaveChanges();
                
                int intIdExpediente = Convert.ToInt32(objResult.Value.ToString());

                if (intIdExpediente > 0)
                {
                    intResultado = 1;
                }
            }
            catch (Exception ex)
            {

            }
            return intResultado;
        }

        public ExpedienteTecnicoOP ObtieneXId(int pIntIdExpediente)
        {
            ExpedienteTecnicoOP objExpedienteTecnicoOP = null;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                var objResult = objContext.sp_gop_get_exp_x_id(pIntIdExpediente).ToList();

                List<sp_gop_get_exp_x_id_Result> lstExpediente = objResult;
                //var lstExpedientes = (from exp in objContext.OP_EXPEDIENTE_TECNICO
                //                      from ejecEmp in objContextIntegrado.MA_PERSONAJURIDICA
                //                          .Where(ejecEmp2 => ejecEmp2.idPersonaJuridica == exp.coEjecutor).DefaultIfEmpty()
                //                      from ejecPer in objContextIntegrado.MA_PERSONANATURAL
                //                              .Where(ejecPer => ejecPer.idPersonaNatural == exp.coEjecutor).DefaultIfEmpty()
                //                      join super in objContextIntegrado.MA_PERSONANATURAL on exp.coSupervisor equals super.idPersonaNatural
                //                      join contac in objContextIntegrado.MA_PERSONANATURAL on exp.coContacto equals contac.idPersonaNatural
                //                      join super2 in objContextIntegrado.MA_PERSONA on exp.coSupervisor equals super2.idPersona
                //                      join contac2 in objContextIntegrado.MA_PERSONA on exp.coContacto equals contac2.idPersona
                //                      join proy in objContext.OP_PROYECTO_INVERSION_PUBLICA on exp.coProyecto equals proy.coProyecto
                //                      where exp.coExpediente == pIntIdExpediente
                //                      select new { exp, ejecEmp, ejecPer, super, contac, contac2, super2, proy }).ToList();

                if (lstExpediente.Count == 1)
                {
                    var lstDocumentos = objContext.OP_DOCUMENTO_EXPEDIENTE_TECNICO.Where(doc => doc.coExpediente == pIntIdExpediente).ToList();

                    objExpedienteTecnicoOP = new ExpedienteTecnicoOP();
                    objExpedienteTecnicoOP.IdExpediente = pIntIdExpediente;

                    if (lstExpediente[0].coContacto.HasValue) {
                        objExpedienteTecnicoOP.ContactoId = lstExpediente[0].coContacto.Value;
                    }
                    objExpedienteTecnicoOP.ContactoApe = lstExpediente[0].CONT_APE_PAT;
                    objExpedienteTecnicoOP.ContactoDireccion = lstExpediente[0].CONT_DIRECCION;
                    objExpedienteTecnicoOP.ContactoEmail = lstExpediente[0].CONT_CORREO;
                    objExpedienteTecnicoOP.ContactoNom = lstExpediente[0].CONT_NOM;
                    objExpedienteTecnicoOP.ContactoTelefono = lstExpediente[0].CONT_TELF;
                    objExpedienteTecnicoOP.Descripcion = lstExpediente[0].txDescripcion;
                    if (lstExpediente[0].coEjecutor.HasValue) {
                        objExpedienteTecnicoOP.EjecutorId = lstExpediente[0].coEjecutor.Value;                    
                    }
                    if (lstExpediente[0].EJEC_ID_PERN.HasValue)
                    {
                        objExpedienteTecnicoOP.EjecutorApe = lstExpediente[0].EJEC_APE_PAT;
                        objExpedienteTecnicoOP.EjecutorNom = lstExpediente[0].EJEC_NOM;
                        objExpedienteTecnicoOP.TipoEjecutor = "P";
                    }
                    if (lstExpediente[0].EJEC_ID_PERJ.HasValue)
                    {
                        objExpedienteTecnicoOP.EjecutorRazonSocial = lstExpediente[0].EJEC_RAZSOC;
                        objExpedienteTecnicoOP.TipoEjecutor = "E";
                    }
                    objExpedienteTecnicoOP.Especificaciones = lstExpediente[0].txEspecificacionesTecnicas;
                    if (lstExpediente[0].coSupervisor.HasValue) {
                        objExpedienteTecnicoOP.SupervisorId = lstExpediente[0].coSupervisor.Value;
                    }
                    objExpedienteTecnicoOP.SupervisorApe = lstExpediente[0].SUP_APE_PAT;
                    objExpedienteTecnicoOP.SupervisorEmail = lstExpediente[0].SUP_CORREO;
                    objExpedienteTecnicoOP.SupervisorNom = lstExpediente[0].SUP_NOM;
                    objExpedienteTecnicoOP.SupervisorTelefono = lstExpediente[0].SUP_TELF;

                    if (lstExpediente[0].coProyecto.HasValue)
                    {
                        ProyectoInversion objProyectoInversion = new ProyectoInversion();
                        objProyectoInversion.IdProyecto = lstExpediente[0].coProyecto.Value;
                        objProyectoInversion.Nombre = lstExpediente[0].NOM_PROYECTO;
                        objExpedienteTecnicoOP.Proyecto = objProyectoInversion;
                    }
                    if (lstExpediente[0].nuValorReferencial.HasValue)
                    {
                        objExpedienteTecnicoOP.ValorReferencial = lstExpediente[0].nuValorReferencial.Value;
                    }

                    List<DocumentoExpedienteTecnicoOP> lstDocumentosNew = new List<DocumentoExpedienteTecnicoOP>();
                    foreach (var objDocumento in lstDocumentos)
                    {
                        DocumentoExpedienteTecnicoOP objDocumentoExpedienteTecnicoOP = new DocumentoExpedienteTecnicoOP();
                        objDocumentoExpedienteTecnicoOP.Descripcion = objDocumento.txRutaArchivo;
                        if (objDocumento.feEmision.HasValue)
                        {
                            objDocumentoExpedienteTecnicoOP.FechaEmision = objDocumento.feEmision.Value;
                        }
                        objDocumentoExpedienteTecnicoOP.IdDocumento = objDocumento.coDocumento;
                        objDocumentoExpedienteTecnicoOP.IdExpediente = objDocumento.coExpediente;
                        objDocumentoExpedienteTecnicoOP.NomArchivo = objDocumento.nomArchivo;
                        objDocumentoExpedienteTecnicoOP.RutaArchivo = objDocumento.txRutaArchivo;
                        objDocumentoExpedienteTecnicoOP.NroDocumento = objDocumento.nuNroDOcumento;
                        objDocumentoExpedienteTecnicoOP.TipoDocumento = objDocumento.noTipoDocExpTec;
                        objDocumentoExpedienteTecnicoOP.Descripcion = objDocumento.txDescripcion;
                        objDocumentoExpedienteTecnicoOP.NomTipoDocumento = ObtieneTiposDocumento(null).Where(doc => doc.Id == objDocumento.noTipoDocExpTec).First().Nombre;

                        lstDocumentosNew.Add(objDocumentoExpedienteTecnicoOP);
                    }
                    objExpedienteTecnicoOP.Documentos = lstDocumentosNew;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return objExpedienteTecnicoOP;
        }


        public List<ItemCombo> ObtieneTiposDocumento(String pStrTipo)
        {
            List<ItemCombo> lstTipo = new List<ItemCombo>();

            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_ESPEC_TEC, Nombre = "Especificaciones Técnicas" });
            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_ESTUDIOS_BAS, Nombre = "Estudios básicos y específicos" });
            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_METRADOS, Nombre = "Metrados y Presupuesto de obra" });
            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_ANALISIS_COS, Nombre = "Análisis de Costos unitarios" });
            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_PRES_ANALIT, Nombre = "Presupuesto Analítico" });
            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_PROG_OBRA, Nombre = "Programación de obra" });
            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_LISTADO_INSUMOS, Nombre = "Listado de insumos" });
            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_PLANOS_EJEC, Nombre = "Planos de ejecución de obra" });
            lstTipo.Add(new ItemCombo { Id = ExpedienteTecnicoOP.STR_ID_TIPO_DOC_EST_SUELO, Nombre = "Estudios de suelo" });
           
            return lstTipo;
        }
    }
}
