using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Core.Entities;

namespace Infraestructura.Data.SQL
{
    public class Via_DAL
    {
        public List<Via> ObtieneVias(String pStrIdTipo)
        {
            List<Via> lstVias = new List<Via>();
            try
            {
                MuniIntegrado objContext = new MuniIntegrado();
                var lstViasTmp = objContext.MA_VIA.Where(v => v.noTipoVia == pStrIdTipo || pStrIdTipo == null).ToList();

                foreach (MA_VIA objViaTmp in lstViasTmp)
                {
                    Via objVia = new Via();
                    objVia.IdVia = objViaTmp.coVia;
                    objVia.Nombre = objViaTmp.noNomVia;
                    objVia.Tipo = objViaTmp.noTipoVia;
                    lstVias.Add(objVia);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstVias;
        }
    }
}
