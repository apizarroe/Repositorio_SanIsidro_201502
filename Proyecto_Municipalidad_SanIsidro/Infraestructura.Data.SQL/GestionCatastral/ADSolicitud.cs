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
    public class ADSolicitud
    {
        

        public static IEnumerable<CT_SOLICITUD> getAll()
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            return db.CT_SOLICITUD.ToList();

        }
        public static  CT_SOLICITUD getOne(int id = 0)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            return db.CT_SOLICITUD.Find(id);
        }
        public static int Add(CT_SOLICITUD otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            db.CT_SOLICITUD.Add(otbSolicitudCatastro);
            return db.SaveChanges();
        }
        public static  int Edit(CT_SOLICITUD otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            db.Entry(otbSolicitudCatastro).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public static int Del(CT_SOLICITUD otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            db.CT_SOLICITUD.Remove(otbSolicitudCatastro);
            return db.SaveChanges();
        }
    }
}
