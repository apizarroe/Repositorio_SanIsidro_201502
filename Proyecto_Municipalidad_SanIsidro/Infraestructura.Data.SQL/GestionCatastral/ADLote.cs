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
    public class ADLote
    {
        

        public static IEnumerable<CT_LOTE> getAll()
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_LOTE.ToList();

        }
        public static  IEnumerable<CT_LOTE> getAll(int int_IdManzana)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_LOTE.ToList().Where(x => x.int_IdManzana == int_IdManzana);

        }
        public static CT_LOTE getOne(int id = 0)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_LOTE.Find(id);
        }
        public static  int Add(CT_LOTE oCT_LOTE)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.CT_LOTE.Add(oCT_LOTE);
            return db.SaveChanges();
        }
        public static  int Edit(CT_LOTE oCT_LOTE)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.Entry(oCT_LOTE).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public static  int Del(CT_LOTE oCT_LOTE)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            var customer = db.CT_LOTE.Include(c => c.CT_GEOLOCALIZACION).First(c => c.int_IdLote == oCT_LOTE.int_IdLote);
            db.Entry(customer).State = EntityState.Deleted;
            return db.SaveChanges();
        }
    }
}
