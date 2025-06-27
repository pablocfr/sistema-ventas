using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Entidad;
using Capa_Datos;
using System.Data;

namespace Capa_Negocio
{
    public class VentasNegocio
    {
        VentasDAO dao = new VentasDAO();

        public List<Articulo> ListarArticulos(string nombre)
        {
            return dao.ListarArticulos(nombre);
        }

        public List<Cliente> ListarClientes()
        {
            return dao.ListarClientes();
        }

        public string GrabarVenta(string cod_cli, decimal total, List<Carrito> listaCarrito)
        {
            try
            {
                return dao.GrabarVenta(cod_cli, total, listaCarrito);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Detalle> ListarDetalleVenta(string codigo)
        {
            return dao.ListarDetalleVenta(codigo);
        }

        public string RetornarUltimaVenta()
        {
            return dao.RetornarUltimaVenta();
        }

        public int TotalesVentas()
        {
            return dao.TotalesVentas();
        }

        public double MontoTotalVentas()
        {
            return dao.MontoTotalVentas();
        }

    }// end class
}// end namespace
