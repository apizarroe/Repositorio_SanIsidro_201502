using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Util
    {
        //Obtiene Titulo de la Pagina
        public string getTitle()
        {
            return "Municipalidad San Isidro";
        }

        //Obtiene Ruta Pagina
        public string getPath()
        {
            return HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + ((HttpContext.Current.Request.ApplicationPath.EndsWith("/")) ? "" : "/");
        }

        //Obtenemos la ruta fisica
        public string getPhysicalPath()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }

        //Obtenemos la ruta de la imagen
        public string getImage(string image)
        {
            return this.getPath() + "Content/img/" + image;
        }
    }
}