using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Capa_Entidad;
using Capa_Negocio;
using Newtonsoft.Json;

using System.Transactions; //agregado para usar transacciones

namespace ProyEcommerceVentas.Controllers
{
    public class VentasController : Controller
    {
        VentasNegocio ventasNegocio = new VentasNegocio();

        List<Carrito> listaCarrito = new List<Carrito>();

        void GrabarCarrito()
        {
            //Grabar el carrito en la sesión
            //Session["carrito"] = listaCarrito;
            Session["carrito"] = JsonConvert.SerializeObject(listaCarrito);
        }

        void LeerCarrito()
        {
            //Leer el carrito de la sesión
            //listaCarrito = (List<Carrito>)Session["carrito"]
            listaCarrito = (List<Carrito>)JsonConvert.DeserializeObject<List<Carrito>>(Session["carrito"].ToString());
        }

        // GET: Ventas
        public ActionResult IndexArticulos(string nombre = "")
        {
            if (Session["carrito"] == null)
            {
                GrabarCarrito();
            }
            //
            return View(ventasNegocio.ListarArticulos(nombre));
        }

        // GET: Ventas/AgregarArticulo, usaremos plantilla Delete y el modelo Articulo/5
        public ActionResult AgregarArticulo(string id = "")
        {
            Articulo buscado = ventasNegocio.ListarArticulos("").Find(x => x.cod_art == id);

            return View(buscado);
        }

        // POST: Ventas/AgregarArticulo, usaremos plantilla Delete y el modelo Articulo/5
        [HttpPost]
        public ActionResult AgregarArticulo(string id = "", int cantidad = 0)
        {
            LeerCarrito(); // 1. Cargar la lista del carrito desde la sesión

            // 2. Buscar el artículo en la base de datos usando el ID recibido (ID que figura en la vista)
            Articulo buscado = ventasNegocio.ListarArticulos("").Find(x => x.cod_art == id);

            // 3. Crear un objeto Carrito con los datos del artículo encontrado y la cantidad del formulario
            Carrito car = new Carrito()
            {
                Codigo = buscado.cod_art,
                Nombre = buscado.nom_art,
                Precio = buscado.pre_art,
                Cantidad = cantidad
            };

            // 4. Buscar si ya existe ese artículo en el carrito
            Carrito encontrado = listaCarrito.Find(x => x.Codigo == car.Codigo);

            if (encontrado == null)
            {
                // 5a. Si no existe, lo agregas como nuevo artículo
                listaCarrito.Add(car);
                ViewBag.mensaje = "El nuevo artículo fue agregado al carrito.";
            }
            else
            {
                // 5b. Si ya existe, simplemente sumas la cantidad
                encontrado.Cantidad += car.Cantidad;
                ViewBag.mensaje = "La cantidad del artículo fue actualizada correctamente.";
            }

            // 6. Guardar el carrito actualizado en la sesión
            GrabarCarrito();

            // 7. Retornar la vista con el artículo que se agregó o actualizó
            return View(buscado);
        }

        // GET: Ventas/VerCarrito, plantilla List y el modelo Carrito
        public ActionResult VerCarrito()
        {
            LeerCarrito(); 
            
            if(listaCarrito.Count == 0)
            {
                return RedirectToAction("IndexArticulos");
            }
            
            ViewBag.total = listaCarrito.Sum(x => x.Total);

            return View(listaCarrito);
        }

        // GET: Ventas/Quitar_1_Item
        public ActionResult Quitar_1_Item(string id = "")
        {
            LeerCarrito();

            Carrito encontrado = listaCarrito.Find(x => x.Codigo.Equals(id));
            encontrado.Cantidad -= 1;

            if(encontrado.Cantidad == 0)
            {
                listaCarrito.Remove(encontrado);
            }

            GrabarCarrito();

            return RedirectToAction("VerCarrito");
        }

        // GET: Ventas/PagarCarrito, plantilla List y el modelo es carrito
        public ActionResult PagarCarrito()
        {
            LeerCarrito();

            ViewBag.clientes = new SelectList(ventasNegocio.ListarClientes(), "cod_cli", "nom_cli");

            ViewBag.total = listaCarrito.Sum(x => x.Total);
            return View(listaCarrito);
        }

        // POST: Ventas/PagarCarrito, plantilla List y el modelo es carrito
        [HttpPost]
        public ActionResult PagarCarrito(string codcli = "")
        {
            LeerCarrito();

            //Bloque using para manejar la transaccion
            using (TransactionScope trx = new TransactionScope())
            {
                try
                {
                    decimal total = listaCarrito.Sum(x => x.Total);
                    //
                    //ViewBag.mensaje = ventasNegocio.GrabarVenta(codcli, total, listaCarrito);
                    //
                    //TempData["mensaje"] = ventasNegocio.GrabarVenta(codcli, total, listaCarrito);

                    // Grabar la venta y obtener el número de venta generado
                    TempData["mensaje"] = ventasNegocio.GrabarVenta(codcli, total, listaCarrito);

                    //Si la venta se graba correctamente, se confirma la transacción
                    trx.Complete();

                    //Eliminar la sesión del carrito
                    Session.Remove("carrito");

                    return RedirectToAction("ListarDetalleVenta");
                }
                catch (Exception ex)
                {
                    ViewBag.mensaje = ex.Message;
                }
            }

            //
            ViewBag.clientes = new SelectList(ventasNegocio.ListarClientes(), "cod_cli", "nom_cli");
            ViewBag.total = listaCarrito.Sum(x => x.Total);
            return View(listaCarrito);
        }

        public ActionResult ListarDetalleVenta()
        {
            var ultimaVenta = ventasNegocio.RetornarUltimaVenta();

            var detalle = ventasNegocio.ListarDetalleVenta(ultimaVenta);

            return View(detalle);
        }

    }
}
