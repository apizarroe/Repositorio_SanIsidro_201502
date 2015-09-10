using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.GSM;
using bus = Models.GSM.ServicioBUS;
namespace Principal.Controllers.GSM
{
    public class GSMmaestroController : Controller
    {

        public JsonResult ListaTipoServicio()
        {
            List<Combo> lst = (from x in bus.GetObject().GetTipoCategoria() select new Combo { value = x.CodigoCategoriaServicio.ToString(), text = x.nombreCategoria }).ToList();
             
            var vjson = Json(lst, JsonRequestBehavior.AllowGet);
            vjson.MaxJsonLength = int.MaxValue;
            return vjson;
        }

    }
}
