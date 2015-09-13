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
    public class ADUsuario
    {
        
        

        //public static  IEnumerable<CT_ZONA> getAll()
        //{
        //    db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
        //    return db.CT_ZONA.ToList();

        //}
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

        public static MA_USUARIO getOneUsuario(String user = "",string pwd="")
        {
            db2f833638c20949ff9238a2f301222db5Entities11 db = new db2f833638c20949ff9238a2f301222db5Entities11();
            return db.MA_USUARIO.ToList().Where(x => x.noLoginUsuario == user && x.noClaveUsu == pwd).First();
        }
        
    }
}
