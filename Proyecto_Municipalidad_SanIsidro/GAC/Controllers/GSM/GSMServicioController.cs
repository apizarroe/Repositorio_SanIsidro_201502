
using GSM.Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
namespace GSM.Controllers.GSM
{
    public class GSMServicioController : Controller
    {
        //
        // GET: /GSMServicio/

        public JsonResult ListaBusqueda(int Pagina, int Paginacion, String Cod, String Nom, String Tipo)
        {

            try
            {
                int codint = 0;
                int tipoint = 0;
                int.TryParse(Cod, out codint); int.TryParse(Tipo, out tipoint);
                /*******************************************************/
                Parametro oparametro = new Parametro();
                oparametro.Pagina = Pagina;
                oparametro.Paginacion = Paginacion;
                oparametro.Id = codint;
                oparametro.name = Nom;
                oparametro.Tipo = tipoint;

                List<Servicio> lst = Servicio.GetServicio(oparametro);

                var strList = new
                {
                    data = lst,
                    total = lst[0].Filas
                };
                var vjson = Json(strList, JsonRequestBehavior.AllowGet);
                vjson.MaxJsonLength = int.MaxValue;
                return vjson;
            }
            catch (Exception ex)
            {
                var vjson = Json("", JsonRequestBehavior.AllowGet);
                vjson.MaxJsonLength = int.MaxValue;
                return vjson;
            }
        }


    }

}
