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
    public class ADZona
    {
        
        

        public static  IEnumerable<CT_ZONA> getAll()
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_ZONA.ToList();

        }
        public static IEnumerable<CT_ZONA> getAllPorSolicitud(int int_IdSolicitud=0)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_ZONA.ToList().Where(x=> x.int_IdSolicitud==int_IdSolicitud);

        }
        public static CT_ZONA getOne(int id = 0)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_ZONA.Find(id);
        }
        public static int Add(CT_ZONA oZona)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.CT_ZONA.Add(oZona);
            return db.SaveChanges();
        }
        public static  int Edit(CT_ZONA oZona)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.Entry(oZona).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public static  int Del(CT_ZONA oZona)
        {
            try
            {
                db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();

                var customer = db.CT_ZONA.Include(c => c.CT_GEOLOCALIZACION ).First(c => c.int_IdZona == oZona.int_IdZona);

                db.Entry(customer).State = EntityState.Deleted;
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           // db.CT_ZONA.Remove(oZona);
            
        }
    }
}
