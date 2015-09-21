using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Core.Entities.ModeloGestionCatastral;
using Infraestructura.Data.SQL;

namespace GAC.Controllers
{
    public class GeolocalizacionController : Controller
    {
        //
        // GET: /Geolocalizacion/

        public ActionResult InsertarGeolocalizacion(CT_GEOLOCALIZACION outb_GCT_Geolocalizacion)
        {
            if (ModelState.IsValid)
            {
                ADGeolocalizacion.DelPorZona(outb_GCT_Geolocalizacion.int_IdZona.Value);

                ADGeolocalizacion.Add(outb_GCT_Geolocalizacion);
                return Json(new
                {
                    success = true,
                    outb_GCT_Geolocalizacion.int_IdGeolocalizacion,
                });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public ActionResult GetGeolocalizacion(int int_IdSolicitud = 0,int opt = 0)
        {



            if (opt == 1)
            {
                return this.Json((from obj
                                      in ADGeolocalizacion.getAllPorZona(int_IdSolicitud)
                                  select new
                                  {
                                      flt_Latitud = obj.flt_Latitud,
                                      flt_Longitud = obj.flt_Longitud,
                                      marca = obj.var_Marca
                                  }).OrderBy(x=>x.marca), JsonRequestBehavior.AllowGet);

            }
            if (opt == 2)
            {
                return this.Json((from obj
                                       in ADGeolocalizacion.getAllPorManzana(int_IdSolicitud)
                                  select new
                                  {
                                      flt_Latitud = obj.flt_Latitud,
                                      flt_Longitud = obj.flt_Longitud
                                  }), JsonRequestBehavior.AllowGet);
            }
            if (opt == 3)
            {
                return this.Json((from obj
                                        in ADGeolocalizacion.getAllPorLote(int_IdSolicitud)
                                  select new
                                  {
                                      flt_Latitud = obj.flt_Latitud,
                                      flt_Longitud = obj.flt_Longitud
                                  }), JsonRequestBehavior.AllowGet);
            }


            return null;  
        }


    }
}
