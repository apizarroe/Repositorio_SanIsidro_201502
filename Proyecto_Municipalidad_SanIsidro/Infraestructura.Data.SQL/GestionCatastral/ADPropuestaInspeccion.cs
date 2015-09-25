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
    public class ADPropuestaInspeccion
    {
        
        public static IEnumerable<CT_PROPUESTAINSPECCION> getAll()
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            var ct_propuestainspeccion = db.CT_PROPUESTAINSPECCION.Include(c => c.CT_SOLICITUD);
            return ct_propuestainspeccion.ToList();

        }
        public static CT_PROPUESTAINSPECCION getOne(int id)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return  db.CT_PROPUESTAINSPECCION.Find(id);
        }
    }
}
