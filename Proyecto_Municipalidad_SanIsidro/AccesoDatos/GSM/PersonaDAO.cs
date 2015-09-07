using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent = Entidades.GSM;
namespace AccesoDatos.GSM
{
    public class PersonaDAO
    {
        public List<Ent.MA_PERSONANATURAL> GetEmpleado(Ent.SM_PARAMETRO oParametro)
        { 
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            { 
                var lst = cn.USP_GSM_BuscarPersona(oParametro.name, oParametro.value, oParametro.value2);

                List<Ent.MA_PERSONANATURAL> lista = (from x in lst
                                                     select new Ent.MA_PERSONANATURAL
                                                     {
                                                         idPersona = x.idPersona.Value,
                                                         NroDocIdentidad = x.NroDocIdentidad,
                                                         Nombres = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno
                                                     }).ToList();

                return lista;
            } 
        }
    }
}
