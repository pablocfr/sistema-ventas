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
    public class ClienteDAO
    {
        public List<Cliente> ListarClientes()
        {
            var lista = new List<Cliente>();

            DataTable dt = DBHelper.RetornaDataTable("PA_CLIENTES_CC");

            string cad_jsonb = JsonConvert.SerializeObject(dt);

            lista = JsonConvert.DeserializeObject<List<Cliente>>(cad_jsonb);

            return lista;
        }

        public string GrabarCliente(Cliente obj)
        {
            try
            {
                DBHelper.EjecutarSP("PA_GRABAR_CLIENTE",
                    obj.nom_cli, obj.tel_cli,
                    obj.cor_cli, obj.dir_cli, obj.cred_cli,
                    obj.fec_nac, obj.cod_dist);

                return $"Se registro al Nuevo Cliente: {obj.nom_cli}";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ActualizarCliente(Cliente obj)
        {
            try
            {
                DBHelper.EjecutarSP("PA_ACTUALIZAR_CLIENTE",
                    obj.cod_cli, obj.nom_cli, obj.tel_cli,
                    obj.cor_cli, obj.dir_cli, obj.cred_cli,
                    obj.fec_nac, obj.cod_dist);

                return $"Se actualiz al Cliente: {obj.nom_cli}";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string EliminarCliente(string cod_cli)
        {
            string nombre = ListarClientes().Find(c => c.cod_cli.Equals(cod_cli)).nom_cli;

            try
            {
                DBHelper.EjecutarSP("PA_ELIMINAR_CLIENTE", cod_cli);

                return $"Se elimino al Cliente: {nombre}";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Distrito> ListarDistritos()
        {
            var lista = new List<Distrito>();

            DataTable dt = DBHelper.RetornaDataTable("PA_DISTRITOS_CC");

            string cad_jsonb = JsonConvert.SerializeObject(dt);

            lista = JsonConvert.DeserializeObject<List<Distrito>>(cad_jsonb);

            return lista;
        }

    }
}
