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
    public class DashboardController : Controller
    {
        ArticuloNegocio articuloNegocio = new ArticuloNegocio();
        ClientesNegocio clientesNegocio = new ClientesNegocio();
        VentasNegocio ventasNegocio = new VentasNegocio();

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.Usuario = Session["usuario"];
            ViewBag.Rol = Session["rol"];

            var clientesTotal = clientesNegocio.ListarClientes();
            var articulosTotal = articuloNegocio.ListarArticulos();

            var articulosStockBajo = articulosTotal
                .Where(a => a.stk_art < 40)
                .ToList();

            var ventasTotales = ventasNegocio.TotalesVentas();
            var montoTotal = ventasNegocio.MontoTotalVentas();

            ViewBag.totalClientes = clientesTotal.Count;
            ViewBag.articulosTotal = articulosTotal.Count;
            ViewBag.productosStockBajo = articulosStockBajo;
            ViewBag.ventasTotales = ventasTotales;
            ViewBag.montoTotal = montoTotal;



            return View();
        }
    }
}