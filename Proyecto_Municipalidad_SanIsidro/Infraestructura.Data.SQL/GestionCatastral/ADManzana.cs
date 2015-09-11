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
    public class ADManzana
    {
        

        public static  IEnumerable<CT_MANZANA> getAll()
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            return db.CT_MANZANA.ToList();

        }
        public static IEnumerable<CT_MANZANA> getAll(int int_IdZona)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            return db.CT_MANZANA.ToList().Where(x => x.int_IdZona == int_IdZona);

        }
        public static CT_MANZANA getOne(int id = 0)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            return db.CT_MANZANA.Find(id);
        }
        public static int Add(CT_MANZANA oCT_MANZANA)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            db.CT_MANZANA.Add(oCT_MANZANA);
            return db.SaveChanges();
        }
        public static  int Edit(CT_MANZANA oCT_MANZANA)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            db.Entry(oCT_MANZANA).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public static int Del(CT_MANZANA oCT_MANZANA)
        {
            db2f833638c20949ff9238a2f301222db5Entities db = new db2f833638c20949ff9238a2f301222db5Entities();
            var customer = db.CT_MANZANA.Include(c => c.CT_GEOLOCALIZACION ).First(c => c.int_IdManzana == oCT_MANZANA.int_IdManzana);
            db.Entry(customer).State = EntityState.Deleted;
            return db.SaveChanges();
        }
    }
}
