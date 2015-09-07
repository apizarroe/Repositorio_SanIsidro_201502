using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent = Entidades.GSM;

namespace AccesoDatos.GSM
{
    public class ServicioDAO
    {
        public List<Ent.USP_GSM_BuscarServicio> GetServicio(Ent.SM_PARAMETRO oParametro)
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {

                var lst = cn.USP_GSM_BuscarServicio(oParametro.Id,oParametro.name,oParametro.Tipo,oParametro.Pagina,oParametro.Paginacion);
                 
                //var data = lst.ToList();
                List<Ent.USP_GSM_BuscarServicio> lista = (from x in lst
                                                          select new Ent.USP_GSM_BuscarServicio
                                          {
                                              CodigoServicio=x.CodigoServicio,
                                              NombreServicio=x.NombreServicio,
                                              CodigoCategoriaServicio=x.CodigoCategoriaServicio.Value,
                                              nombreCategoria=x.nombreCategoria,
                                              Filas=x.Filas.Value
                                          }).ToList();

                return lista;
            }
        }


        public List<Ent.SM_CATEGORIA_SERVICIO> GetTipoCategoria()
        {
            using (var cn = new Entity.MUNI_INTEGRADOEntities())
            {

                var lst = (from x in cn.SM_CATEGORIA_SERVICIO select new Ent.SM_CATEGORIA_SERVICIO {CodigoCategoriaServicio=x.CodigoCategoriaServicio,
                nombreCategoria=x.nombreCategoria}).ToList();


                return lst;
            }
        }

    }
}
