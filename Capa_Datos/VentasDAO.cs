using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Entidad;
using System.Data;
using Newtonsoft.Json;

namespace Capa_Datos
{
    public class VentasDAO
    {
        public List<Articulo> ListarArticulos(string nombre)
        {
            var lista = new List<Articulo>();

            DataTable dt = DBHelper.RetornaDataTable("PA_ARTICULOS_CC", nombre);
            
            string cad_jsonb = JsonConvert.SerializeObject(dt);
            
            lista = JsonConvert.DeserializeObject<List<Articulo>>(cad_jsonb);

            return lista;
        }

        public List<Cliente> ListarClientes()
        {
            var lista = new List<Cliente>();

            DataTable dt = DBHelper.RetornaDataTable("PA_CLIENTES_CC");

            string cad_jsonb = JsonConvert.SerializeObject(dt);

            lista = JsonConvert.DeserializeObject<List<Cliente>>(cad_jsonb);

            return lista;
        }

        public string GrabarVenta(string cod_cli, decimal total, List<Carrito> listaCarrito)
        {
            string resultado = "";

            try
            {
                //Devolvemos el string que se manda en el procedimiento almacenado:
                //Grabar en la cabecera de la venta PA_GRABAR_VENTAS_CAB_CC
                string num_venta = DBHelper.RetornaValorEscalar("PA_GRABAR_VENTAS_CAB_CC", cod_cli, total).ToString();

                //Grabar en la detalle de la venta: PA_GRABAR_VENTAS_DETA_CC
                foreach (Carrito item in listaCarrito)
                {
                    DBHelper.EjecutarSP("PA_GRABAR_VENTAS_DETA_CC", num_venta, item.Codigo, item.Cantidad, item.Precio);
                }

                //listaCarrito.ForEach(x => DBHelper.EjecutarSP("PA_GRABAR_VENTAS_DETA_CC", num_venta, x.Codigo, x.Cantidad, x.Precio));

                resultado = $"La venta {num_venta} se realizó correctamente";

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public int TotalesVentas()
        {
            DataTable dt = DBHelper.RetornaDataTable("LISTAR_VENTAS_CC");

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }

            return 0; // Retorna 0 si no hay datos
        }

        public double MontoTotalVentas()
        {
            DataTable dt = DBHelper.RetornaDataTable("MONTO_TOTAL_VENTAS");

            if (dt.Rows.Count > 0)
            {
                return Convert.ToDouble(dt.Rows[0][0]);
            }

            return 0.0;
        }


        public List<Detalle> ListarDetalleVenta(string detalle)
        {
            var lista = new List<Detalle>();

            DataTable dt = DBHelper.RetornaDataTable("PA_LISTAR_DETALLE", detalle);

            string cad_jsonb = JsonConvert.SerializeObject(dt);

            lista = JsonConvert.DeserializeObject<List<Detalle>>(cad_jsonb);

            return lista;
        }

        public string RetornarUltimaVenta()
        {
            try
            {
                string num_venta = DBHelper.RetornaValorEscalar("PA_OBTENER_ULTIMA_VENTA").ToString();

                return num_venta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VENTAS_AGRUPADAS> ListaVentas()
        {
            var lista = new List<VENTAS_AGRUPADAS>();

            DataTable dt = DBHelper.RetornaDataTable("PA_VENTAS_AGRUPADAS");

            string cad_jsonb = JsonConvert.SerializeObject(dt);

            lista = JsonConvert.DeserializeObject<List<VENTAS_AGRUPADAS>>(cad_jsonb);

            return lista;
        }
    }
}
