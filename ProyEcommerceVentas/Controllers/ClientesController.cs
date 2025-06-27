using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Capa_Entidad;
using Capa_Negocio;
using TuProyectoWeb.Filtros;

namespace ProyEcommerceVentas.Controllers
{
    [FiltroSesion]
    public class ClientesController : Controller
    {
        ClientesNegocio clientesNegocio = new ClientesNegocio();

        // GET: Clientes
        public ActionResult Index()
        {
            ViewBag.Usuario = Session["usuario"];
            ViewBag.Rol = Session["rol"];
            ViewBag.distritos = new SelectList(clientesNegocio.ListarDistritos(), "cod_dist", "nom_dist");
            return View(clientesNegocio.ListarClientes());
        }

        Cliente BuscarCliente(string codigo)
        {
            Cliente buscado = clientesNegocio.ListarClientes().Find(x => x.cod_cli.Equals(codigo));
            return buscado;
        }



        // GET: Clientes/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.distritos = new SelectList(clientesNegocio.ListarDistritos(), "cod_dist", "nom_dist");

            return View(BuscarCliente(id));
        }

        // GET: Clientes/Create
        public ActionResult CreateCliente()
        {   
            ViewBag.distritos = new SelectList(clientesNegocio.ListarDistritos(), "cod_dist", "nom_dist");
            //
            return View(new Cliente());
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult CreateCliente(Cliente obj)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    TempData["mensaje"] = clientesNegocio.GrabarCliente(obj);
                    return RedirectToAction("Index");
                }  
            }
            catch(Exception ex) 
            {
                ViewBag.mensaje = ex.Message;
            }

            ViewBag.distritos = new SelectList(clientesNegocio.ListarDistritos(), "cod_dist", "nom_dist");
            return View(obj);
        }

        // GET: Clientes/Edit/5
        public ActionResult EditCliente(string id)
        {
            ViewBag.distritos = new SelectList(clientesNegocio.ListarDistritos(), "cod_dist", "nom_dist");

            return View(BuscarCliente(id));
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult EditCliente(string id, Cliente obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = clientesNegocio.ActualizarCliente(obj);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }

            ViewBag.distritos = new SelectList(clientesNegocio.ListarDistritos(), "cod_dist", "nom_dist");
            return View(obj);
        }

        // GET: Clientes/Delete/5
        public JsonResult DetailsAjax(string id)
        {
            var cliente = BuscarCliente(id); // Cambia según tu lógica
            return Json(cliente, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCliente(string id)
        {
            return View(BuscarCliente(id));
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        public ActionResult DeleteCliente(string id, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = clientesNegocio.EliminarCliente(id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }

            return View(BuscarCliente(id));
        }
    }
}
