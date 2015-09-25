using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using Dominio.Core.Entities.ModeloGestionCatastral;
namespace Infraestructura.Data.SQL
{
    public class ADEmpleado
    {
        public static IEnumerable<MA_EMPLEADO> getAll()
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return  db.MA_EMPLEADO
                            .Where(s => s.MA_AREA.idArea == 11)
                            .ToList();
        }
    }
}
