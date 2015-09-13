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
    public class ADPredio
    {
        

        public static  IEnumerable<CT_PREDIO> getAll()
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_PREDIO.ToList();

        }
        public static  IEnumerable<CT_PREDIO> getAll(int int_IdLote=0)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_PREDIO.ToList().Where(x => x.int_IdLote == int_IdLote);

        }
        public static CT_PREDIO getOne(int id = 0)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_PREDIO.Find(id);
        }
        public static int Add(CT_PREDIO otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.CT_PREDIO.Add(otbSolicitudCatastro);
            return db.SaveChanges();
        }
        public static  int Edit(CT_PREDIO otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.Entry(otbSolicitudCatastro).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public static  int Del(CT_PREDIO otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            var customer = db.CT_PREDIO.First(c => c.int_IdPredio == otbSolicitudCatastro.int_IdPredio);
            db.Entry(customer).State = EntityState.Deleted;
            return db.SaveChanges();
        }
    }
}
