using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ent = Dominio.Core.Entities.GSM;
using Dao = Infraestructura.Data.SQL.GSM;

namespace Models.GSM
{
    public class InspeccionBUS
    {
        private Dao.InspeccionDAO objDaoInspeccion;
        private static InspeccionBUS objBusInspeccion;
        private static bool init = false;

        public static InspeccionBUS GetObject()
        {

            if (!init)
            {
                objBusInspeccion = new InspeccionBUS();
                objBusInspeccion.objDaoInspeccion = new Dao.InspeccionDAO();
                init = true;
            }

            return objBusInspeccion;
        }

        public List<Ent.USP_GSM_GetInspeccion> GetInspeccion(Ent.SM_PARAMETRO oParametro)
        {
            return objBusInspeccion.objDaoInspeccion.GetInspeccion(oParametro);
        }


        public List<Ent.USP_GSM_GetInspeccion> GetListInspeccion(Ent.SM_PARAMETRO oParametro)
        {
            return objBusInspeccion.objDaoInspeccion.GetListInspeccion(oParametro);
        }

        public Ent.USP_GSM_GetInspeccion SaveInspeccion(Ent.SM_INSPECCION nuevo)
        {
            return objBusInspeccion.objDaoInspeccion.SaveInspeccion(nuevo);

        }

        public Ent.USP_GSM_GetInspeccion Update(Ent.SM_INSPECCION nuevo)
        {
            return objBusInspeccion.objDaoInspeccion.Update(nuevo);
        }

        public Ent.USP_GSM_GetInspeccion getInspeccion(int id)
        {
            return objBusInspeccion.objDaoInspeccion.getInspeccion(id);
        }

        public int Cancelar(int Id)
        {
            return objBusInspeccion.objDaoInspeccion.Cancelar(Id);
        }
    }
}
