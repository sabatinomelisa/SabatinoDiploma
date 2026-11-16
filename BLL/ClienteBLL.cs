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

            // Calculo y asigno el dígito verificador horizontal al cliente
            nuevoCliente_675MS.DigitoVerificadorHorizontal_675MS = CalcularDVHCliente_675MS(nuevoCliente_675MS);

            // Valido que haya una sesión iniciada en el sistema
            if (Servicios.SessionManager.SesionIniciada_675MS)
            {
                ClienteDAL clienteDAL_675MS = new ClienteDAL();
                filasAfectadas_675MS = clienteDAL_675MS.RegistrarClienteDAL_675MS(nuevoCliente_675MS);

                if (filasAfectadas_675MS > 0)
                {
                    IntegridadBLL integridadBLL_675MS = new IntegridadBLL();
                    integridadBLL_675MS.ActualizarDigitosVerticalesCliente_675MS();
                }
            }
            else
            {
                throw new Exception("No hay ningún empleado logueado en el sistema.");
            }

            return filasAfectadas_675MS;
        }

        public int ModificarCliente_675MS(ClienteBE clienteModificado_675MS)
        {
            // Recalculo el DVH con los datos nuevos del cliente
            clienteModificado_675MS.DigitoVerificadorHorizontal_675MS =  CalcularDVHCliente_675MS(clienteModificado_675MS);

            int filasAfectadas_675MS =  clienteDAL675MS.ModificarClienteDAL_675MS(clienteModificado_675MS);

            // Actualizo los DVV si la modificación fue exitosa
            if (filasAfectadas_675MS > 0)
            {
                IntegridadBLL integridadBLL_675MS = new IntegridadBLL();
                integridadBLL_675MS.ActualizarDigitosVerticalesCliente_675MS();
            }

            return filasAfectadas_675MS;
        }

        private int CalcularDVHCliente_675MS(ClienteBE cliente)
        {
            CalculadorDigitoVerificador calculador =  new CalculadorDigitoVerificador();

            return calculador.CalcularHorizontal_675MS(
                cliente.Dni_675MS.ToString(),
                cliente.Apellido_675MS,
                cliente.Nombre_675MS,
                cliente.FechaNacimiento_675MS.ToString("yyyyMMdd"),
                cliente.Mail_675MS,
                cliente.Domicilio_675MS,
                cliente.Telefono_675MS.ToString()
            );
        }

        public bool VerificarExisteDni_675MS(int dniValidado_675MS)
        {
            //Instancio la DAL
            clienteDAL675MS = new ClienteDAL();

            ClienteBE clienteConsultado_675MS = clienteDAL675MS.ConsultaPorDNI_675MS(dniValidado_675MS);

            if(clienteConsultado_675MS==null)
            {
                return false;
            }else return true;
;
        }


        public int RecuperarCliente_675MS(HistorialClienteBE historial_675MS)
        {
            if (historial_675MS == null)
                throw new Exception("No se seleccionó ninguna versión histórica.");

            if (!SessionManager.SesionIniciada_675MS)
                throw new Exception("No hay una sesión iniciada.");


            HistorialClienteBLL historialBLL_675MS = new HistorialClienteBLL();

            historial_675MS = historialBLL_675MS.ValidarVersionParaRecuperar_675MS(historial_675MS.IdHistorial_675MS, historial_675MS.DNI_675MS);

            UsuarioBE usuario_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (usuario_675MS == null || usuario_675MS.Rol_675MS == null)
                throw new Exception("El usuario no tiene un rol asignado.");

            RolBLL rolBLL_675MS = new RolBLL();

            if (!rolBLL_675MS.TienePermiso_675MS(usuario_675MS.Rol_675MS, "GestionIntegridad"))
                throw new Exception("No tenés permiso para recuperar versiones históricas.");

            ClienteBE cliente_675MS = clienteDAL675MS.ConsultaPorDNI_675MS(historial_675MS.DNI_675MS);

            if (cliente_675MS == null)
                throw new Exception("No se encontró el cliente.");

            if (!cliente_675MS.Activo_675MS)
                throw new Exception("No se puede recuperar una versión de un cliente dado de baja.");

            cliente_675MS.Nombre_675MS = historial_675MS.Nombre_675MS;
            cliente_675MS.Apellido_675MS = historial_675MS.Apellido_675MS;
            cliente_675MS.FechaNacimiento_675MS = historial_675MS.FechaNacimiento_675MS;
            cliente_675MS.Mail_675MS = historial_675MS.Mail_675MS;
            cliente_675MS.Domicilio_675MS = historial_675MS.Domicilio_675MS;
            cliente_675MS.Telefono_675MS = historial_675MS.Telefono_675MS ?? 0;

            return ModificarCliente_675MS(cliente_675MS);
        }


        public int BajaCliente_675MS(int dni_675MS)
        {
            if (!Servicios.SessionManager.SesionIniciada_675MS)
                throw new Exception("No hay una sesión iniciada.");

            if (dni_675MS <= 0)
                throw new Exception("DNI inválido.");

            BE.UsuarioBE usuario_675MS = Servicios.SessionManager.ObtenerUsuarioActual_675MS();

            if (usuario_675MS == null || usuario_675MS.Rol_675MS == null || !new RolBLL().TienePermiso_675MS(usuario_675MS.Rol_675MS, "GestionIntegridad"))
                throw new UnauthorizedAccessException("No tenés permiso para dar de baja clientes.");

            int resultado_675MS = new ClienteDAL().BajaClienteDAL_675MS(dni_675MS);

            if (resultado_675MS > 0)
                new IntegridadBLL().ActualizarDigitosVerticalesCliente_675MS();

            return resultado_675MS;
        }


        public int ReactivarCliente_675MS(int dni_675MS)
        {
            if (!Servicios.SessionManager.SesionIniciada_675MS)
                throw new Exception("No hay una sesión iniciada.");

            if (dni_675MS <= 0)
                throw new Exception("DNI inválido.");

            UsuarioBE usuario_675MS = Servicios.SessionManager.ObtenerUsuarioActual_675MS();

            if (usuario_675MS == null || usuario_675MS.Rol_675MS == null ||
                !new RolBLL().TienePermiso_675MS(usuario_675MS.Rol_675MS, "GestionIntegridad"))
                throw new UnauthorizedAccessException("No tenés permiso para reactivar clientes.");

            ClienteBE cliente_675MS = clienteDAL675MS.ConsultaPorDNI_675MS(dni_675MS);

            if (cliente_675MS == null)
                throw new Exception("El cliente no existe.");

            if (cliente_675MS.Activo_675MS)
                throw new Exception("El cliente ya está activo.");

            int resultado_675MS = clienteDAL675MS.ReactivarClienteDAL_675MS(dni_675MS);

            if (resultado_675MS > 0)
                new IntegridadBLL().ActualizarDigitosVerticalesCliente_675MS();

            return resultado_675MS;
        }

    }
}
