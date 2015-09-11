using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSM.Models
{
    public class Usuario
    {
        public int CodVia{get;set;}
        public int CodUsuario{get;set;}
        private static Usuario miUsuario;
        private static bool flIniciado = false;

        private Usuario() { }

        public static Usuario GetObject()
        {
            if (!flIniciado)
            {
                flIniciado = true;
                miUsuario = new Usuario();
                miUsuario.CodUsuario=7;
                miUsuario.CodVia=2;
            }
            return miUsuario;
        }
        //GetObject
    }
}