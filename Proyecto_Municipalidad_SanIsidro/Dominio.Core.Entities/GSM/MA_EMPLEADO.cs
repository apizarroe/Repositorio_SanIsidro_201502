

namespace Dominio.Core.Entities.GSM
{
    using System;
    using System.Collections.Generic;

    public   class MA_EMPLEADO
    {
        public MA_EMPLEADO()
        {
        }

        public int idEmpleado { get; set; }
        public int idPersona { get; set; }
        public int idCargo { get; set; }
        public int idArea { get; set; }
        public string CodigoEmpleado { get; set; }
        public int UsrRegistra { get; set; }
        public System.DateTime FecRegistra { get; set; }
        public int UsrModifica { get; set; }
        public System.DateTime FecModifica { get; set; }


    }
}
