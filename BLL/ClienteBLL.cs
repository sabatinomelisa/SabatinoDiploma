using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClienteBLL
    {
        ClienteDAL clienteDAL675MS = new ClienteDAL();
        public List<ClienteBE> ListarClientes_675MS()
        {
            //Instancio la DAL
            clienteDAL675MS = new ClienteDAL();

            List<ClienteBE> clientes_675MS = clienteDAL675MS.ListarClientes_675MS();

            return clientes_675MS;
        }
    }
}
