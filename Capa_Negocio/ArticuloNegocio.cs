using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Datos;
using Capa_Entidad;



namespace Capa_Negocio
{
    public class ArticuloNegocio
    {
        ArticuloDAO articuloDAO = new ArticuloDAO();
        public List<Articulo> ListarArticulos()
        {
            return articuloDAO.ListarArticulos();
        }

        public string GrabarArticulo(Articulo obj)
        {
            return articuloDAO.GrabarArticulo(obj);
        }

        public string ActualizarArticulo(Articulo obj)
        {
            return articuloDAO.ActualizarArticulo(obj);
        }

        public string EliminarArticulo(string cod_art)
        {
            return articuloDAO.EliminarArticulo(cod_art);
        }
    }
}
