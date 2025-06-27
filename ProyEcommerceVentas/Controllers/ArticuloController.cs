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
    public class ArticuloController : Controller
    {
        ArticuloNegocio negocio = new ArticuloNegocio();

        // GET: Articulo
        public ActionResult Index()
        {
            ViewBag.Usuario = Session["usuario"];
            ViewBag.Rol = Session["rol"];
            return View(negocio.ListarArticulos());
        }

        Articulo BuscarArticulo(string cod_art)
        {
            return negocio.ListarArticulos().Find(x => x.cod_art.Equals(cod_art));
        }

        // GET: Articulo/Details/5
        public JsonResult DetailsAjax(string id)
        {
            var articulo = BuscarArticulo(id); // Cambia según tu lógica
            return Json(articulo, JsonRequestBehavior.AllowGet);
        }

        // GET: Articulo/Create
        public ActionResult Create()
        {
            return View(new Articulo());
        }

        // POST: Articulo/Create
        [HttpPost]
        public ActionResult Create(Articulo articulo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = negocio.GrabarArticulo(articulo);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View(articulo);
        }

        // GET: Articulo/Edit/5
        public ActionResult Edit(string id)
        {
            return View(BuscarArticulo(id));
        }

        // POST: Articulo/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Articulo articulo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = negocio.ActualizarArticulo(articulo);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View(articulo);
        }

        // GET: Articulo/Delete/5
        
        public ActionResult Delete(string id)
        {
            return View(BuscarArticulo(id));
        }

        // POST: Articulo/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = negocio.EliminarArticulo(id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            return View(BuscarArticulo(id));
        }
    }
}
