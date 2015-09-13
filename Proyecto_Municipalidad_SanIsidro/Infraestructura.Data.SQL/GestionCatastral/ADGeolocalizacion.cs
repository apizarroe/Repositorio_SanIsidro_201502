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
    public class ADGeolocalizacion
    {
        

        public static IEnumerable<CT_GEOLOCALIZACION> getAll()
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_GEOLOCALIZACION.ToList();

        }
        public static IEnumerable<CT_GEOLOCALIZACION> getAllPorZona(int id)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_GEOLOCALIZACION.ToList().Where(x => x.int_IdZona ==id );
        }
        public static  IEnumerable<CT_GEOLOCALIZACION> getAllPorManzana(int id)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_GEOLOCALIZACION.ToList().Where(x => x.int_IdManzana == id);
        }
        public static IEnumerable<CT_GEOLOCALIZACION> getAllPorLote(int id)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_GEOLOCALIZACION.ToList().Where(x => x.int_IdLote == id);
        }
        public static CT_GEOLOCALIZACION getOne(int id = 0)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.CT_GEOLOCALIZACION.Find(id);
        }
        public static int Add(CT_GEOLOCALIZACION otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.CT_GEOLOCALIZACION.Add(otbSolicitudCatastro);
            return db.SaveChanges();
        }
        public static int Edit(CT_GEOLOCALIZACION otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.Entry(otbSolicitudCatastro).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public static int Del(CT_GEOLOCALIZACION otbSolicitudCatastro)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.CT_GEOLOCALIZACION.Remove(otbSolicitudCatastro);
            return db.SaveChanges();
        }
        public static  int DelPorZona(int IdZona)
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            db.CT_GEOLOCALIZACION.ToList().RemoveAll(x => x.int_IdZona == IdZona);
            return db.SaveChanges();
        }

    }
}
