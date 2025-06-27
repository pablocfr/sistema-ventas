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
    public class ClientesNegocio
    {
        ClienteDAO clienteDAO = new ClienteDAO();

        public List<Cliente> ListarClientes()
        {
            return clienteDAO.ListarClientes();
        }

        public string GrabarCliente(Cliente obj)
        {
            return clienteDAO.GrabarCliente(obj);
        }

        public string ActualizarCliente(Cliente obj)
        {
            return clienteDAO.ActualizarCliente(obj);
        }

        public string EliminarCliente(string cod_cli)
        {
            return clienteDAO.EliminarCliente(cod_cli);
        }

        public List<Distrito> ListarDistritos()
        {
            return clienteDAO.ListarDistritos();
        }
    }
}
