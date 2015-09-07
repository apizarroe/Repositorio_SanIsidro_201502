using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent = Entidades.GSM;
using Dao = AccesoDatos.GSM;

namespace Negocio.GSM
{
    public class ServicioBUS
    {

        private Dao.ServicioDAO objDaoServicio;
        private static ServicioBUS objBusServicio;
        private static bool init = false;

        public static ServicioBUS GetObject()
        {

            if (!init)
            {
                objBusServicio = new ServicioBUS();
                objBusServicio.objDaoServicio = new Dao.ServicioDAO();
                init = true;
            } 
            return objBusServicio;
        }

        public List<Ent.USP_GSM_BuscarServicio> GetServicio(Ent.SM_PARAMETRO oParametro)
        {
            return objBusServicio.objDaoServicio.GetServicio(oParametro);
        }

        public List<Ent.SM_CATEGORIA_SERVICIO> GetTipoCategoria()
        {
            return objBusServicio.objDaoServicio.GetTipoCategoria();
        }
    }
}
