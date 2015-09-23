using PryMuniIntegrado.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PryMuniIntegrado.Seguridad
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(string.IsNullOrEmpty(SessionPersister.NombreUsuario))
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { Controller = "Account", action = "Index" }));
            }
            else
            {
                var usuarioBL = new UsuarioBL();
                var customPrincipal = new CustomPrincipal(
                    usuarioBL.ObtenerUsuario(SessionPersister.NombreUsuario));
                if (!customPrincipal.IsInRole(Roles))
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "AccesoDeneid", action = "Index" }));

            }
        }
    }
}