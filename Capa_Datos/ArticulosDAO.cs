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
    public class ArticuloDAO
    {
        public List<Articulo> ListarArticulos()
        {
            var lista = new List<Articulo>();

            DataTable dt = DBHelper.RetornaDataTable("LISTAR_ARTICULOS_CC");

            string cad_jsonb = JsonConvert.SerializeObject(dt);

            lista = JsonConvert.DeserializeObject<List<Articulo>>(cad_jsonb);

            return lista;
        }


        public string GrabarArticulo(Articulo obj)
        {
            try
            {
                DBHelper.EjecutarSP("PA_GRABAR_ARTICULO",
                    obj.nom_art, obj.uni_med,
                    obj.pre_art, obj.stk_art);

                return $"Se registro el nuevo Articulo: {obj.nom_art}";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ActualizarArticulo(Articulo obj)
        {
            try
            {
                DBHelper.EjecutarSP("PA_ACTUALIZAR_ARTICULO",
                    obj.cod_art, obj.nom_art, obj.uni_med,
                    obj.pre_art, obj.stk_art);

                return $"Se actualiz al Articulo: {obj.nom_art}";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string EliminarArticulo(string cod_art)
        {
            string nombre = ListarArticulos().Find(c => c.cod_art.Equals(cod_art)).nom_art;

            try
            {
                DBHelper.EjecutarSP("PA_ELIMINAR_ARTICULO", cod_art);

                return $"Se elimino al articulo: {nombre}";
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
