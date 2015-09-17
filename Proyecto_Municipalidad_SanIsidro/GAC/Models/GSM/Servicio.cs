using GSM.Models.GSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSM.Models.GSM
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public String Nombre { get; set; }
        public int IdTipoServicio { get; set; }
        public String TipoServicio { get; set; }

        public int Filas { get; set; }

        #region "Acceso a datos"

        public static List<Servicio> GetServicio(Parametro oParametro)
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {

                var lst = cn.USP_GSM_BuscarServicio(oParametro.Id, oParametro.name, oParametro.Tipo, oParametro.Pagina, oParametro.Paginacion);

                //var data = lst.ToList();
                List<Servicio> lista = (from x in lst
                                        select new Servicio
                                                          {
                                                              IdServicio = x.CodigoServicio,
                                                              Nombre = x.NombreServicio,
                                                              IdTipoServicio = x.CodigoCategoriaServicio.Value,
                                                              TipoServicio = x.nombreCategoria,
                                                              Filas = x.Filas.Value
                                                          }).ToList();

                return lista;
            }
        }

        public static List<Combo> GetTipoCategoria()
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {

                var lst = (from x in cn.SM_CATEGORIA_SERVICIO select x);

                var combo = new List<Combo>();
                foreach (var x in lst)
                {
                    Combo xcombo = new Combo();
                    xcombo.value = x.CodigoCategoriaServicio.ToString();
                    xcombo.text = x.nombreCategoria;
                    combo.Add(xcombo);
                }


                return combo;//lst;
            }
        }

        public static List<Combo> GetServicioList()
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {

                var lst = (from x in cn.SM_SERVICIO select x);

                var combo = new List<Combo>();
                foreach (var x in lst)
                {
                    Combo xcombo = new Combo();
                    xcombo.value = x.CodigoServicio.ToString();
                    xcombo.text = x.NombreServicio;
                    combo.Add(xcombo);
                }


                return combo;//lst;
            }
        }

        #endregion


    }
}