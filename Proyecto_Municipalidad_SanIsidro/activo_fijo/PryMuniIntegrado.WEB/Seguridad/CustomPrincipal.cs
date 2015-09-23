using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

using PryMuniIntegrado.ET;
using PryMuniIntegrado.BL;

namespace PryMuniIntegrado.Seguridad
{
    public class CustomPrincipal : IPrincipal
    {
        #region Campos Privados
        private Usuario Usuario;
        #endregion

        public CustomPrincipal(Usuario usuario)
        {
            this.Usuario = usuario;
            this.Identity = new GenericIdentity(usuario.NombreUsuario);
        }

        #region Implementacion de IPrincipal
        public IIdentity Identity
        {
            get;
            set;
        }
        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => this.Usuario.Roles.Contains(r));
        }
        #endregion
    }
}