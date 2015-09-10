using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.Models
{
    public class Usuario
    {
        private int CodVia;
        private int CodUsuario;
        private Usuario miUsuario;
        private bool flIniciado = false;

        private Usuario() { }

        public Usuario GetObject()
        {
            if (!flIniciado)
            {
                flIniciado = true;
                miUsuario = new Usuario();
            }
            return miUsuario;
        }
        //GetObject
    }
}