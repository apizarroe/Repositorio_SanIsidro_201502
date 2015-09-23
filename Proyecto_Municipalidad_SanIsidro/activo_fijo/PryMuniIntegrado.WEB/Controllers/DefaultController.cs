using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PryMuniIntegrado.Seguridad;
using PryMuniIntegrado.ET;

namespace PryMuniIntegrado.WEB.Controllers
{
    public class DefaultController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Roles="Administrador")]
        public ActionResult Work1()
        {
            return View("Work1");
        }

        [CustomAuthorize(Roles = "Administrador, Secretaria")]
        public ActionResult Work2()
        {
            return View("Work2");
        }

        [CustomAuthorize(Roles = "Administrador, Secretaria")]
        public ActionResult Work3()
        {
            return View("Work3");
        }
    }
}
