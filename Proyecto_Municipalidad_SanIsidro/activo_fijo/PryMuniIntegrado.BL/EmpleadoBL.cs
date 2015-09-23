using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using PryMuniIntegrado.DAL;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.BL
{
    public class EmpleadoBL
    {
        public static ObservableCollection<Empleado> ListarEmpleadoDeComite(string codigo)
        {
            return EmpleadoDAL.ListarEmpleadoDeComite(codigo);
        }
    }
}
