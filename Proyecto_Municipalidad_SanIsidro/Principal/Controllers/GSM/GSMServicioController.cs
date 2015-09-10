using Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bus = Models.GSM.ServicioBUS;
using ent = Dominio.Core.Entities.GSM;
namespace Principal.Controllers.GSM
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
                ent.SM_PARAMETRO oparametro = new ent.SM_PARAMETRO();
                oparametro.Pagina = Pagina;
                oparametro.Paginacion = Paginacion;
                oparametro.Id = codint;
                oparametro.name = Nom;
                oparametro.Tipo = tipoint;

                List<Servicio> lst = (from x in bus.GetObject().GetServicio(oparametro)
                                      select new Servicio
                                      {
                                          IdServicio = x.CodigoServicio,
                                          IdTipoServicio = x.CodigoCategoriaServicio,
                                          Nombre = x.NombreServicio,
                                          TipoServicio = x.nombreCategoria,
                                          Filas = x.Filas
                                      }).ToList();

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
