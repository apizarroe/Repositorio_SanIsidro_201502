using GSM.Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
namespace GSM.Controllers.GSM
{
    public class GSMmaestroController : Controller
    {

        public JsonResult ListaTipoServicio()
        {
            List<Combo> lst = Servicio.GetTipoCategoria();
            var vjson = Json(lst, JsonRequestBehavior.AllowGet);
            vjson.MaxJsonLength = int.MaxValue;
            return vjson;
        }


        public JsonResult ListaServicio()
        {
            List<Combo> lst = Servicio.GetServicioList();
            var vjson = Json(lst, JsonRequestBehavior.AllowGet);
            vjson.MaxJsonLength = int.MaxValue;
            return vjson;
        }
         
    }
}
