using GSM.Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 

namespace GSM.Controllers.GSM
{
    public class GSMPersonaController : Controller
    {
        //
        // GET: /GSMPersona/

        public JsonResult ListaBusqueda(String keyname)
        {


            /*******************************************************/

            var lst = new List<Persona>();

            Parametro prm = new Parametro();
            prm.name = keyname;
            prm.value = keyname;
            prm.value2 = keyname;

            List<Persona> obj = Persona.GetEmpleado(prm);

            if (obj != null && obj.Count > 0)
            {
                var strList = new
                {
                    data = obj, 
                    total = obj.Count
                };

                var vjson = Json(strList, JsonRequestBehavior.AllowGet);
                vjson.MaxJsonLength = int.MaxValue;
                return vjson;
            }

            var strList2 = new
            {
                data = lst,
                total = 0
            };
            var vjson2 = Json(strList2, JsonRequestBehavior.AllowGet);
            vjson2.MaxJsonLength = int.MaxValue;
            return vjson2;
        }

    }
}
