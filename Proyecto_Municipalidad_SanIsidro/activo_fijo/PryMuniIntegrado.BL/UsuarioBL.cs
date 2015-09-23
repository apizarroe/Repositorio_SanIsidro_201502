using PryMuniIntegrado.DAL;
using PryMuniIntegrado.ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PryMuniIntegrado.BL
{
    public class UsuarioBL
    {
        public bool ValidarUsuario(string usuario, string contraseña)
        {
            return UsuarioDAL.ValidarUsuario(usuario, contraseña);
        }
        public Usuario ObtenerUsuario(string noUsuario)
        {
            return UsuarioDAL.ObtenerUsuario(noUsuario);
        }
    }
}
