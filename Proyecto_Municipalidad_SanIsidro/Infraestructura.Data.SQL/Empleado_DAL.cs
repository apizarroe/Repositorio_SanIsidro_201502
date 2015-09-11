using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Core.Entities;

namespace Infraestructura.Data.SQL
{
    public class Empleado_DAL
    {
        public List<Empleado> ObtieneXArea(int pIntIdArea)
        {
            List<Empleado> lstEmpleados = new List<Empleado>();
            try
            {
                MUNI_INTEGRADOEntities1 objContext = new MUNI_INTEGRADOEntities1();

                var lstEmpleadosTmp = (from emp in objContext.MA_EMPLEADO
                                       join per in objContext.MA_PERSONA on emp.idPersona equals per.idPersona
                                       join pernat in objContext.MA_PERSONANATURAL on emp.idPersona equals pernat.idPersona
                                       where (emp.idArea.HasValue && emp.idArea.Value == pIntIdArea)
                                       select new { emp, per,pernat }).OrderBy(x => x.pernat.ApellidoPaterno);

                foreach (var objEmpleadoTmp in lstEmpleadosTmp)
                {
                    Empleado objEmpleado = new Empleado();
                    objEmpleado.IdEmpleado = objEmpleadoTmp.emp.idEmpleado;
                    objEmpleado.Nombre = objEmpleadoTmp.pernat.Nombres;
                    objEmpleado.Apellido = objEmpleadoTmp.pernat.ApellidoPaterno;
                    lstEmpleados.Add(objEmpleado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return lstEmpleados;
        }
    }
}
