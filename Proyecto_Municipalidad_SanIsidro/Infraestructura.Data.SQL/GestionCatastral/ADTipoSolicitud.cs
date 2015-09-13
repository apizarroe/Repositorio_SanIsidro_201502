using Dominio.Core.Entities.ModeloGestionCatastral;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Data.SQL
{
    public class ADTipoSolicitud
    {
        

        public static  IEnumerable<CT_TIPOSOLICITUD> getAll()
        {
            
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_TIPOSOLICITUD.ToList();

        }
    }
}
