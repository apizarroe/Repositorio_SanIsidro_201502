using Principal.Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using bus = Models.GSM.PersonaBUS;
using ent = Dominio.Core.Entities.GSM;

namespace Principal.Controllers.GSM
{
    public class GSMPersonaController : Controller
    {
        //
        // GET: /GSMPersona/

        public JsonResult ListaBusqueda(String keyname)
        {


            /*******************************************************/

            var lst = new List<Persona>();

            ent.SM_PARAMETRO prm = new ent.SM_PARAMETRO();
            prm.name = keyname;
            prm.value = keyname;
            prm.value2 = keyname;

            List<ent.MA_PERSONANATURAL> obj = bus.GetObject().GetEmpleado(prm);

            if (obj != null && obj.Count > 0)
            {
                var strList = new
                {
                    data = (from x in obj select new Persona { Codigo = x.idPersona.ToString(), DNI = x.NroDocIdentidad, Nombre = x.Nombres }).ToList(),
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
