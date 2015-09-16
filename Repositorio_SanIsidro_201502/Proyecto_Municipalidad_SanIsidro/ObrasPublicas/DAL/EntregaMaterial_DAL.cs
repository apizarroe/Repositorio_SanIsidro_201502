using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Core.Entities;
using ObrasPublicas.Entities;
using ObrasPublicas.DAL;
using Infraestructura.Data.SQL;

namespace ObrasPublicas.DAL
{
    public class EntregaMaterial_DAL
    {
        public int Inserta(DateTime pDatFecEntregaProg,DateTime pDatFecEntregaEfec,String pStrObservaciones,
            String pStrTipoEntrega,int pIntIdProveedor,int pIntIdMaterial, int pIntCantidad, int pIntIdProyecto) {
            int intResultado = -999;
            ObrasPublicasEntities objContext = new ObrasPublicasEntities();

            try{
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion =  objProyectoInversion_DAL.ObtieneXId(pIntIdProyecto);

                if (objProyectoInversion.IdEstado != ProyectoInversion.STR_ID_ESTADO_ADJUDICADO &&
                    objProyectoInversion.IdEstado != ProyectoInversion.STR_ID_ESTADO_VIABLE)
                {
                    intResultado = -998;
                }
                else
                {
                    OP_ENTREGA_MATERIAL objEntregaMaterial = new OP_ENTREGA_MATERIAL();
                    objEntregaMaterial.coMaterial = pIntIdMaterial;
                    objEntregaMaterial.coProveedor = pIntIdProveedor;
                    objEntregaMaterial.coProyecto = pIntIdProyecto;
                    objEntregaMaterial.feEntregaEfectiva = pDatFecEntregaEfec;
                    objEntregaMaterial.feEntregaProgramada = pDatFecEntregaProg;
                    objEntregaMaterial.noTipoEntrega = pStrTipoEntrega;
                    objEntregaMaterial.nuCantidad = pIntCantidad;
                    objEntregaMaterial.txObservaciones = pStrObservaciones;

                    objContext.AddToOP_ENTREGA_MATERIAL(objEntregaMaterial);
                    int intRows = objContext.SaveChanges();

                    if (intRows > 0)
                    {
                        intResultado = 1;
                    }
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

        public int Actualiza(int pIntIdEntrega, int pIntIdProyecto, DateTime pDatFecEntregaProg, DateTime pDatFecEntregaEfec, String pStrObservaciones,
            String pStrTipoEntrega, int pIntIdProveedor, int pIntIdMaterial, int pIntCantidad)
        {
            int intResultado = -999;
            ObrasPublicasEntities objContext = new ObrasPublicasEntities();

            try
            {
                ProyectoInversion_DAL objProyectoInversion_DAL = new ProyectoInversion_DAL();
                ProyectoInversion objProyectoInversion =  objProyectoInversion_DAL.ObtieneXId(pIntIdProyecto);

                if (objProyectoInversion.IdEstado != ProyectoInversion.STR_ID_ESTADO_ADJUDICADO &&
                    objProyectoInversion.IdEstado != ProyectoInversion.STR_ID_ESTADO_VIABLE)
                {
                    intResultado = -998;
                }
                else
                {
                    OP_ENTREGA_MATERIAL objEntregaMaterial = objContext.OP_ENTREGA_MATERIAL.Where(ent => ent.coEntrega == pIntIdEntrega).First();

                    if (objEntregaMaterial != null)
                    {
                        objEntregaMaterial.coMaterial = pIntIdMaterial;
                        objEntregaMaterial.coProveedor = pIntIdProveedor;
                        objEntregaMaterial.feEntregaEfectiva = pDatFecEntregaEfec;
                        objEntregaMaterial.feEntregaProgramada = pDatFecEntregaProg;
                        objEntregaMaterial.noTipoEntrega = pStrTipoEntrega;
                        objEntregaMaterial.nuCantidad = pIntCantidad;
                        objEntregaMaterial.txObservaciones = pStrObservaciones;

                        int intRows = objContext.SaveChanges();

                        if (intRows > 0)
                        {
                            intResultado = 1;
                        }
                    }
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

        public EntregaMaterialOP ObtieneEntregaXId(int pIntIdProyecto, int pIntIdEntrega)
        {
            EntregaMaterialOP objEntregaMaterialOP = null;
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                var objResult = objContext.sp_gop_get_ent_mat_x_id(pIntIdProyecto,pIntIdEntrega).ToList();

                List<sp_gop_get_ent_mat_x_id_Result> lstEntregaTmp = objResult;

                //var lstEntregasTmp = (from ent in objContext.OP_ENTREGA_MATERIAL
                //                      join prov in objContextIntegrado.MA_PROVEEDOR on ent.coProveedor equals prov.IdProveedor
                //                      join emp in objContextIntegrado.MA_PERSONAJURIDICA on prov.IdPersona equals emp.idPersona
                //                     join mat in objContext.OP_MATERIAL on ent.coMaterial equals mat.coMaterial
                //                     where( ent.coEntrega == pIntIdEntrega)
                //                     select new { ent, prov, emp, mat }).ToList();  

                //where(ent.coEntrega == pIntIdProyecto)

                if (lstEntregaTmp != null && lstEntregaTmp.Count() == 1)
                {
                    foreach (var objEntregaTmp in lstEntregaTmp)
                    {
                        objEntregaMaterialOP = new EntregaMaterialOP();
                        objEntregaMaterialOP.IdEntrega = objEntregaTmp.coEntrega;

                        ProyectoInversion objProyectoInversion = new ProyectoInversion();
                        if (objEntregaTmp.coProyecto.HasValue) {
                            objProyectoInversion.IdProyecto = objEntregaTmp.coProyecto.Value;
                        }
                        objProyectoInversion.Nombre = objEntregaTmp.noNombre;
                        objEntregaMaterialOP.Proyecto = objProyectoInversion;

                        Proveedor objProveedor = new Proveedor();
                        objProveedor.IdProveedor = objEntregaTmp.coProveedor;
                        objProveedor.RazonSocial = objEntregaTmp.RazonSocial;
                        objEntregaMaterialOP.Proveedor = objProveedor;

                        if (objEntregaTmp.nuCantidad.HasValue)
                        {
                            objEntregaMaterialOP.Cantidad = objEntregaTmp.nuCantidad.Value;
                        }
                        if (objEntregaTmp.feEntregaEfectiva.HasValue)
                        {
                            objEntregaMaterialOP.FecEntregaEfec = objEntregaTmp.feEntregaEfectiva.Value;
                        }
                        if (objEntregaTmp.feEntregaProgramada.HasValue)
                        {
                            objEntregaMaterialOP.FecEntregaProg = objEntregaTmp.feEntregaProgramada.Value;
                        }

                        MaterialOP objMaterial = new MaterialOP();
                        objMaterial.IdMaterial = objEntregaTmp.coMaterial;
                        objMaterial.Nombre = objEntregaTmp.noMaterial;
                        objEntregaMaterialOP.Material = objMaterial;

                        objEntregaMaterialOP.Observaciones = objEntregaTmp.txObservaciones;
                        objEntregaMaterialOP.TipoEntrega = objEntregaTmp.noTipoEntrega;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return objEntregaMaterialOP;
        }

        public List<EntregaMaterialOP> ObtieneEntregasXIdProyecto(int pIntIdProyecto, int pIntFlagInforme)
        {
            List<EntregaMaterialOP> lstEntregas = new List<EntregaMaterialOP>();
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                //MUNI_INTEGRADOEntities1 objContextIntegrado = new MUNI_INTEGRADOEntities1();

                var objResult = objContext.sp_gop_get_ent_mat_x_proy(pIntIdProyecto, pIntFlagInforme).ToList();

                List<sp_gop_get_ent_mat_x_proy_Result> lstEntregaTmp = objResult;
                
                foreach (var objEntregaTmp in lstEntregaTmp)
                {
                    EntregaMaterialOP objEntregaMaterialOP = new EntregaMaterialOP();

                    ProyectoInversion objProyectoInversion = new ProyectoInversion();

                    if (objEntregaTmp.coProyecto.HasValue) {
                        objProyectoInversion.IdProyecto = objEntregaTmp.coProyecto.Value;
                    }
                    objEntregaMaterialOP.IdEntrega = objEntregaTmp.coEntrega;
                    objEntregaMaterialOP.Proyecto = objProyectoInversion;

                    Proveedor objProveedor = new Proveedor();
                    objProveedor.IdProveedor = objEntregaTmp.coProveedor;
                    objProveedor.RazonSocial = objEntregaTmp.RazonSocial;
                    objEntregaMaterialOP.Proveedor = objProveedor;

                    if (objEntregaTmp.nuCantidad.HasValue) {
                        objEntregaMaterialOP.Cantidad = objEntregaTmp.nuCantidad.Value;
                    }
                    if (objEntregaTmp.feEntregaEfectiva.HasValue){
                        objEntregaMaterialOP.FecEntregaEfec = objEntregaTmp.feEntregaEfectiva.Value;
                    }
                    if (objEntregaTmp.feEntregaProgramada.HasValue) {
                        objEntregaMaterialOP.FecEntregaProg = objEntregaTmp.feEntregaProgramada.Value;
                    }

                    MaterialOP objMaterial = new MaterialOP();
                    objMaterial.IdMaterial = objEntregaTmp.coMaterial;
                    objMaterial.Nombre = objEntregaTmp.noMaterial;
                    objEntregaMaterialOP.Material = objMaterial;

                    objEntregaMaterialOP.Observaciones = objEntregaTmp.txObservaciones;
                    objEntregaMaterialOP.TipoEntrega = objEntregaTmp.noTipoEntrega;
                    lstEntregas.Add(objEntregaMaterialOP);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstEntregas;
        }

        public List<EntregaMaterialOP> ObtieneEntregasXIdInforme(int pIntIdInforme)
        {
            List<EntregaMaterialOP> lstEntregas = new List<EntregaMaterialOP>();
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();

                var objResult = objContext.sp_gop_get_ent_mat_x_informe(pIntIdInforme).ToList();

                List<sp_gop_get_ent_mat_x_informe_Result> lstEntregaTmp = objResult;

                foreach (var objEntregaTmp in lstEntregaTmp)
                {
                    EntregaMaterialOP objEntregaMaterialOP = new EntregaMaterialOP();

                    ProyectoInversion objProyectoInversion = new ProyectoInversion();

                    if (objEntregaTmp.coProyecto.HasValue)
                    {
                        objProyectoInversion.IdProyecto = objEntregaTmp.coProyecto.Value;
                    }
                    objEntregaMaterialOP.IdEntrega = objEntregaTmp.coEntrega;
                    objEntregaMaterialOP.Proyecto = objProyectoInversion;

                    Proveedor objProveedor = new Proveedor();
                    objProveedor.IdProveedor = objEntregaTmp.coProveedor;
                    objProveedor.RazonSocial = objEntregaTmp.RazonSocial;
                    objEntregaMaterialOP.Proveedor = objProveedor;

                    if (objEntregaTmp.nuCantidad.HasValue)
                    {
                        objEntregaMaterialOP.Cantidad = objEntregaTmp.nuCantidad.Value;
                    }
                    if (objEntregaTmp.feEntregaEfectiva.HasValue)
                    {
                        objEntregaMaterialOP.FecEntregaEfec = objEntregaTmp.feEntregaEfectiva.Value;
                    }
                    if (objEntregaTmp.feEntregaProgramada.HasValue)
                    {
                        objEntregaMaterialOP.FecEntregaProg = objEntregaTmp.feEntregaProgramada.Value;
                    }

                    MaterialOP objMaterial = new MaterialOP();
                    objMaterial.IdMaterial = objEntregaTmp.coMaterial;
                    objMaterial.Nombre = objEntregaTmp.noMaterial;
                    objEntregaMaterialOP.Material = objMaterial;

                    objEntregaMaterialOP.Observaciones = objEntregaTmp.txObservaciones;
                    objEntregaMaterialOP.TipoEntrega = objEntregaTmp.noTipoEntrega;
                    lstEntregas.Add(objEntregaMaterialOP);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstEntregas;
        }

        public List<ItemCombo> ObtieneTiposEntrega()
        {
            List<ItemCombo> lstTipos = new List<ItemCombo>();

            lstTipos.Add(new ItemCombo { Id = EntregaMaterialOP.STR_ID_TIPO_CONFORME, Nombre = "Conforme" });
            lstTipos.Add(new ItemCombo { Id = EntregaMaterialOP.STR_ID_TIPO_FUERA_DE_FECHA, Nombre = "Fuera de fecha" });

            return lstTipos;
        }
    }
}
