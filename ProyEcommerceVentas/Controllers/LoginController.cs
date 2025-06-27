using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capa_Negocio;

namespace ProyEcommerceVentas.Controllers
{
    public class LoginController : Controller
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usuario, string clave)
        {
            var user = usuarioNegocio.ValidarLogin(usuario, clave);
            if (user != null)
            {
                Session["usuario"] = user.Nombre;
                Session["rol"] = user.Rol;
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Mensaje = "Usuario o clave incorrectos";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}