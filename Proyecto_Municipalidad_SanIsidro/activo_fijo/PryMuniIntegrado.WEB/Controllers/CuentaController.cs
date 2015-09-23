using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PryMuniIntegrado.ET;
using PryMuniIntegrado.BL;
using PryMuniIntegrado.Seguridad;

namespace PryMuniIntegrado.WEB.Controllers
{
    public class CuentaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var usuarioBL = new UsuarioBL(); 

            if(string.IsNullOrEmpty(usuario.NombreUsuario) || string.IsNullOrEmpty(usuario.Contraseña)
                || !usuarioBL.ValidarUsuario(usuario.NombreUsuario, usuario.Contraseña))
            {
                ViewBag.Error = "Usuario Invalido";
                return View("Index");
            }
            SessionPersister.NombreUsuario = usuario.NombreUsuario;
            return View("Success");
        }

        public ActionResult Logout()
        {
            SessionPersister.NombreUsuario = string.Empty;
            return RedirectToAction("Index");
        }
    }
}
