using BE;
using DAL;
using Servicios;
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
        ClienteBE clienteBE_675MS = new ClienteBE();
        public List<ClienteBE> ListarClientes_675MS()
        {
            //Instancio la DAL
            clienteDAL675MS = new ClienteDAL();

            List<ClienteBE> clientes_675MS = clienteDAL675MS.ListarClientes_675MS();

            return clientes_675MS;
        }


        //Serialización
        private GestorXML gestorXML_675MS = new GestorXML();

        public void GuardarClientesXML_675MS(List<ClienteBE> clientes, string path)
        {
            gestorXML_675MS.Serializar_675MS(clientes, path);
        }

        public List<ClienteBE> CargarClientesXML_675MS(string path)
        {
            return gestorXML_675MS.Deserializar_675MS(path);
        }

        public int RegistrarCliente_675MS(ClienteBE nuevoCliente_675MS)
        {
            int filasAfectadas_675MS = 0;

            // Instancio servicio para calcular dígito verificador horizontal (DVH)
            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            // Calculo y asigno el dígito verificador horizontal al cliente
            clienteBE_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                    clienteBE_675MS.Dni_675MS.ToString(),
                    clienteBE_675MS.Nombre_675MS,
                    clienteBE_675MS.Apellido_675MS,
                    clienteBE_675MS.FechaNacimiento_675MS.ToString("yyyy-MM-dd"), 
                    clienteBE_675MS.Mail_675MS,
                    clienteBE_675MS.Domicilio_675MS,
                    clienteBE_675MS.Telefono_675MS.ToString()
                );            
            
            // Valido que haya una sesión iniciada en el sistema
            if (Servicios.SessionManager.SesionIniciada_675MS)
            {
                ClienteDAL clienteDAL_675MS = new ClienteDAL();
                filasAfectadas_675MS = clienteDAL_675MS.RegistrarClienteDAL_675MS(clienteBE_675MS);
            }
            else
            {
                throw new Exception("No hay ningún empleado logueado en el sistema.");
            }

            return filasAfectadas_675MS;
        }

        public int ModificarCliente_675MS(ClienteBE clienteModificado_675MS)
        {
            // Recalculo el DVH al modificar los datos del cliente
            Servicios.CalculadorDigitoVerificador calculadorDV_675MS = new Servicios.CalculadorDigitoVerificador();

            clienteBE_675MS.DigitoVerificadorHorizontal_675MS = calculadorDV_675MS.CalcularHorizontal_675MS(
                    clienteBE_675MS.Dni_675MS.ToString(),
                    clienteBE_675MS.Nombre_675MS,
                    clienteBE_675MS.Apellido_675MS,
                    clienteBE_675MS.FechaNacimiento_675MS.ToString("yyyy-MM-dd"),
                    clienteBE_675MS.Mail_675MS,
                    clienteBE_675MS.Domicilio_675MS,
                    clienteBE_675MS.Telefono_675MS.ToString()
                );
            ClienteDAL clienteDAL_675MS = new ClienteDAL();
            return clienteDAL_675MS.ModificarClienteDAL_675MS(clienteBE_675MS);
        }
    }
}
