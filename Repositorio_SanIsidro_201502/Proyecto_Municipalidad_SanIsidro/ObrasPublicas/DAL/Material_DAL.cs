using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObrasPublicas.Entities;
using ObrasPublicas.DAL;

namespace ObrasPublicas.DAL
{
    public class Material_DAL
    {
        public List<MaterialOP> ObtieneMateriales()
        {
            List<MaterialOP> lstMateriales = new List<MaterialOP>();
            try
            {
                ObrasPublicasEntities objContext = new ObrasPublicasEntities();
                var lstMaterialesTmp = objContext.OP_MATERIAL;

                foreach (var objMaterialTmp in lstMaterialesTmp)
                {
                    MaterialOP objMaterialOP = new MaterialOP();
                    objMaterialOP.IdMaterial = objMaterialTmp.coMaterial;
                    objMaterialOP.Nombre = objMaterialTmp.noMaterial;
                    lstMateriales.Add(objMaterialOP);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstMateriales;
        }
    }
}
