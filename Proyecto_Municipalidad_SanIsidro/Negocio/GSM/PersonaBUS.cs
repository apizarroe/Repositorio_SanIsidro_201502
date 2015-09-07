using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ent = Entidades.GSM;
using Dao = AccesoDatos.GSM;

namespace Negocio.GSM
{
    public class PersonaBUS
    {

        private Dao.PersonaDAO objDaoPersona;
        private static PersonaBUS objBusPersona;
        private static bool init = false;

        public static PersonaBUS GetObject()
        {

            if (!init)
            {
                objBusPersona = new PersonaBUS();
                objBusPersona.objDaoPersona = new Dao.PersonaDAO();
                init = true;
            }

            return objBusPersona;
        }


        public List<Ent.MA_PERSONANATURAL> GetEmpleado(Ent.SM_PARAMETRO oParametro)
        {
            return objBusPersona.objDaoPersona.GetEmpleado(oParametro);
        }
    }
}
