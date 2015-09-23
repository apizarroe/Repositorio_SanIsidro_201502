using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PryMuniIntegrado.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cuenta", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "InventarioRouter",
                url: "{controller}/{action}/{codigo}",
                defaults: new { controller = "Inventario", action = "DetalleInventario", codigo = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EvaluacionRouter",
                url: "{controller}/{action}/{codigo}",
                defaults: new { controller = "Evaluacion", action = "DetalleEvaluacion", codigo = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "InformeRouter",
                url: "{controller}/{action}/{codigo}",
                defaults: new { controller = "Informe", action = "DetalleInforme", codigo = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PecosaRouter",
                url: "{controller}/{action}/{codigo}",
                defaults: new { controller = "Pecosa", action = "DetallePecosa", codigo = UrlParameter.Optional }
            );

             routes.MapRoute(
                name: "ActualizaActivoRouter",
                url: "{controller}/{action}/{codigoActivo}/{codigoInventario}/{iEvaluar}",
                defaults: new { controller = "Inventario", action = "ActualizaActivo", codigoActivo = UrlParameter.Optional, codigoInventario = UrlParameter.Optional, iEvaluar = UrlParameter.Optional }                        
                );

             routes.MapRoute(
                 name: "EliminaActivoRouter",
                 url: "{controller}/{action}/{codigoActivo}/{codigoInventario}",
                 defaults: new { controller = "Inventario", action = "EliminaActivo", codigoActivo = UrlParameter.Optional, codigoInventario = UrlParameter.Optional }
                 );
        }
    }
}