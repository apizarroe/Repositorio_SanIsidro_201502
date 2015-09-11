using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Core.Entities;

namespace Infraestructura.Data.SQL
{
    public class Proveedor_DAL
    {
        public List<Proveedor> ObtieneProveedores()
        {
            List<Proveedor> lstProveedores = new List<Proveedor>();
            try
            {
                MUNI_INTEGRADOEntities1 objContext = new MUNI_INTEGRADOEntities1();
                var lstProvTmp = (from prov in objContext.MA_PROVEEDOR
                                      from emp in objContext.MA_PERSONAJURIDICA
                                          .Where(emp => emp.idPersona == prov.IdPersona)
                                      select new { prov, emp }).ToList();

                foreach (var objProvTmp in lstProvTmp)
                {
                    Proveedor objProveedor = new Proveedor();
                    objProveedor.IdProveedor = objProvTmp.prov.IdProveedor;
                    objProveedor.RazonSocial = objProvTmp.emp.RazonSocial;
                    lstProveedores.Add(objProveedor);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstProveedores;
        }
    }
}
