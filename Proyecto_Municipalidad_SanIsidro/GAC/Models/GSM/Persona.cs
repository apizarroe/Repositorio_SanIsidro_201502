using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSM.Models.GSM
{
    public class Persona
    {
        public String DNI { get; set; }
        public String Nombre { get; set; }
        public String Codigo { get; set; }



        #region "Acceso a datos"

        public static List<Persona> GetEmpleado(Parametro oParametro)
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {
                var lst = cn.USP_GSM_BuscarPersona(oParametro.name, oParametro.value, oParametro.value2);

                List<Persona> lista = (from x in lst
                                                     select new Persona
                                                     {
                                                         Codigo = x.idPersona.Value.ToString(),
                                                         DNI = x.NroDocIdentidad,
                                                         Nombre = x.Nombres + " " + x.ApellidoPaterno + " " + x.ApellidoMaterno
                                                     }).ToList();

                return lista;
            }
        }

        #endregion

    }
}