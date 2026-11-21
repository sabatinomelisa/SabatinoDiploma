using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Linq;

namespace DAL
{
    public class IntegridadDAL
    {
        private ClienteDAL clienteDAL;


        public List<ClienteBE> ListarClientesParaIntegridad_675MS()
        {
            clienteDAL = new ClienteDAL();
            return clienteDAL.ListarClientes_675MS();
        }

        public void ActualizarDigitoHorizontalCliente_675MS(int dni_675MS, int digitoVerificadorHorizontal_675MS)
        {
            clienteDAL = new ClienteDAL();
            clienteDAL.ActualizarDigitoVerificadorHorizontal_675MS(dni_675MS, digitoVerificadorHorizontal_675MS);
        }

        public Dictionary<string, int> ObtenerDigitosVerticales_675MS(string entidad_675MS)
        {
            Dictionary<string, int> digitos_675MS = new Dictionary<string, int>();

            Acceso acceso_675MS = new Acceso();

            try
            {
                acceso_675MS.Conectar_675MS();

                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@entidad", entidad_675MS));

                DataTable tabla_675MS = acceso_675MS.Leer_675MS("CONSULTAR_DIGITOS_VERTICALES", parametros_675MS);

                foreach (DataRow fila_675MS in tabla_675MS.Rows)
                {
                    string campo_675MS = fila_675MS["Campo"].ToString();

                    int valor_675MS = Convert.ToInt32(fila_675MS["Valor"]);

                    digitos_675MS[campo_675MS] = valor_675MS;
                }
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }

            return digitos_675MS;
        }

        public void GuardarDigitosVerticales_675MS(string entidad_675MS, Dictionary<string, int> digitos_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            try
            {
                //Abro la conexión e inicio la transacción
                acceso_675MS.Conectar_675MS();
                acceso_675MS.IniciarTx_675MS();

                //Guardo todos los dígitos en una única operación
                GuardarDigitosVerticalesInterno_675MS(entidad_675MS, digitos_675MS, acceso_675MS);
                acceso_675MS.ConfirmarTx_675MS();
            }
            catch
            {
                acceso_675MS.RevertirTx_675MS();
                throw;
            }
            finally
            {
                acceso_675MS.Desconectar_675MS();
            }
        }

        public bool ExistenDigitosVerticales_675MS(string entidad_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            try
            {
                acceso_675MS.Conectar_675MS();

                List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@entidad", entidad_675MS));

                int resultado_675MS = acceso_675MS.DevolverEscalar_675MS("EXISTE_DIGITO_VERTICAL", parametros_675MS);

                return resultado_675MS > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                //Me aseguro de cerrar la conexion
                acceso_675MS.Desconectar_675MS();
            }
        }

        private void GuardarDigitosVerticalesInterno_675MS(string entidad_675MS, Dictionary<string, int> digitos_675MS, Acceso acceso_675MS)
        {
            //Método interno: comparte la transacción de esta DAL, no se expone a la BLL
            if (digitos_675MS == null || digitos_675MS.Count == 0)
                throw new Exception("No hay dígitos verticales para guardar.");

            foreach (var digito_675MS in digitos_675MS)
            {
                //Creo los parámetros del dígito vertical
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@entidad", entidad_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@campo", digito_675MS.Key));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@valor", digito_675MS.Value));

                //Guardo el dígito utilizando la transacción activa
                int resultado_675MS = acceso_675MS.Escribir_675MS("GUARDAR_DIGITO_VERTICAL", parametros_675MS);

                //Verifico que el dígito vertical se haya guardado correctamente
                if (resultado_675MS <= 0)
                {
                    throw new Exception("No se pudo guardar el dígito vertical del campo: " + digito_675MS.Key);
                }
            }
        }

        public void RestaurarCliente_675MS(ClienteBE cliente_675MS, ClienteBE clienteActual_675MS, Dictionary<string, int> digitosVerticales_675MS)
        {
            if (cliente_675MS == null || clienteActual_675MS == null)
                throw new Exception("Faltan los datos del cliente para restaurar.");
            if (cliente_675MS.Dni_675MS != clienteActual_675MS.Dni_675MS)
                throw new Exception("El DNI del cliente no coincide con los datos consultados.");

            Acceso acceso_675MS = new Acceso();

            try
            {
                //Abro la conexión a la base de datos
                acceso_675MS.Conectar_675MS();

                //Inicio la transacción
                acceso_675MS.IniciarTx_675MS();

                //Creo los parámetros para restaurar el cliente
                List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DNI", cliente_675MS.Dni_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Nombre", cliente_675MS.Nombre_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Apellido", cliente_675MS.Apellido_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametroDateSolo_675MS("@FechaNacimiento", cliente_675MS.FechaNacimiento_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Mail", cliente_675MS.Mail_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Domicilio", cliente_675MS.Domicilio_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Telefono", cliente_675MS.Telefono_675MS));


                //Verifico que los datos actuales no hayan cambiado desde la consulta
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@NombreActual", clienteActual_675MS.Nombre_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@ApellidoActual", clienteActual_675MS.Apellido_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametroDateSolo_675MS("@FechaNacimientoActual", clienteActual_675MS.FechaNacimiento_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@MailActual", clienteActual_675MS.Mail_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DomicilioActual", clienteActual_675MS.Domicilio_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@TelefonoActual", clienteActual_675MS.Telefono_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DVHActual", clienteActual_675MS.DigitoVerificadorHorizontal_675MS));
                parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DVHOriginal", cliente_675MS.DigitoVerificadorHorizontal_675MS));

                //Restauro los datos del cliente sin modificar su DVH
                int resultado_675MS = acceso_675MS.Escribir_675MS("RESTAURAR_CLIENTE", parametros_675MS);

                if (resultado_675MS <= 0)
                {
                    throw new Exception("No se pudo restaurar el cliente.");
                }

                //Guardo los DVV utilizando la misma conexión y transacción
                GuardarDigitosVerticalesInterno_675MS("Cliente", digitosVerticales_675MS, acceso_675MS);

                //Confirmo todos los cambios
                acceso_675MS.ConfirmarTx_675MS();
            }
            catch (Exception)
            {
                //Si ocurre un error, revierto todos los cambios
                acceso_675MS.RevertirTx_675MS();
                throw;
            }
            finally
            {
                //Cierro la conexión
                acceso_675MS.Desconectar_675MS();
            }
        }


        public void GuardarIntegridadConsistente_675MS(ClienteBE clienteOriginal_675MS, ClienteBE clienteActual_675MS,Func<List<ClienteBE>, Dictionary<string, int>> calcularDVV_675MS)
        {
            Acceso acceso_675MS = new Acceso();

            try
            {
                //Abro la conexión e inicio la transacción
                acceso_675MS.Conectar_675MS();
                acceso_675MS.IniciarTx_675MS();

                //Bloqueo la tabla y obtengo los clientes actualizados
                DataTable tabla_675MS = acceso_675MS.Leer_675MS("BLOQUEAR_CLIENTES_INTEGRIDAD");

                List<ClienteBE> clientes_675MS = new List<ClienteBE>();

                foreach (DataRow fila_675MS in tabla_675MS.Rows)
                {
                    ClienteBE cliente_675MS = new ClienteBE();

                    cliente_675MS.Dni_675MS = Convert.ToInt32(fila_675MS["DNI"]);
                    cliente_675MS.Nombre_675MS = fila_675MS["Nombre"].ToString();
                    cliente_675MS.Apellido_675MS = fila_675MS["Apellido"].ToString();
                    cliente_675MS.FechaNacimiento_675MS = Convert.ToDateTime(fila_675MS["FechaNacimiento"]);
                    cliente_675MS.Mail_675MS = fila_675MS["Mail"].ToString();
                    cliente_675MS.Domicilio_675MS = fila_675MS["Domicilio"].ToString();
                    cliente_675MS.Telefono_675MS = Convert.ToInt32(fila_675MS["Telefono"]);
                    cliente_675MS.DigitoVerificadorHorizontal_675MS =  Convert.ToInt32(fila_675MS["DigitoVerificadorHorizontal"]);

                    clientes_675MS.Add(cliente_675MS);
                }

                //Si recibí un cliente, debo restaurarlo
                if (clienteOriginal_675MS != null)
                {
                    if (clienteActual_675MS == null ||
                        clienteOriginal_675MS.Dni_675MS != clienteActual_675MS.Dni_675MS)
                    {
                        throw new Exception("Los datos del cliente no coinciden.");
                    }

                    ClienteBE vigente_675MS = clientes_675MS.FirstOrDefault(c => c.Dni_675MS == clienteActual_675MS.Dni_675MS);

                    if (vigente_675MS == null ||
                        vigente_675MS.Nombre_675MS != clienteActual_675MS.Nombre_675MS ||
                        vigente_675MS.Apellido_675MS != clienteActual_675MS.Apellido_675MS ||
                        vigente_675MS.FechaNacimiento_675MS.Date != clienteActual_675MS.FechaNacimiento_675MS.Date ||
                        vigente_675MS.Mail_675MS != clienteActual_675MS.Mail_675MS ||
                        vigente_675MS.Domicilio_675MS != clienteActual_675MS.Domicilio_675MS ||
                        vigente_675MS.Telefono_675MS != clienteActual_675MS.Telefono_675MS ||
                        vigente_675MS.DigitoVerificadorHorizontal_675MS !=
                            clienteActual_675MS.DigitoVerificadorHorizontal_675MS)
                    {
                        throw new Exception("El cliente cambió durante la recuperación.");
                    }

                    //Preparo los parámetros del procedimiento de restauración
                    List<SqlParameter> parametros_675MS = new List<SqlParameter>();

                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DNI", clienteOriginal_675MS.Dni_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Nombre", clienteOriginal_675MS.Nombre_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Apellido", clienteOriginal_675MS.Apellido_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametroDateSolo_675MS("@FechaNacimiento", clienteOriginal_675MS.FechaNacimiento_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Mail", clienteOriginal_675MS.Mail_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Domicilio", clienteOriginal_675MS.Domicilio_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@Telefono", clienteOriginal_675MS.Telefono_675MS));

                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@NombreActual", clienteActual_675MS.Nombre_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@ApellidoActual", clienteActual_675MS.Apellido_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametroDateSolo_675MS("@FechaNacimientoActual", clienteActual_675MS.FechaNacimiento_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@MailActual", clienteActual_675MS.Mail_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DomicilioActual", clienteActual_675MS.Domicilio_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@TelefonoActual", clienteActual_675MS.Telefono_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DVHActual", clienteActual_675MS.DigitoVerificadorHorizontal_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@DVHOriginal", clienteOriginal_675MS.DigitoVerificadorHorizontal_675MS));

                    //Restauro el cliente sin modificar su DVH
                    int resultado_675MS = acceso_675MS.Escribir_675MS("RESTAURAR_CLIENTE", parametros_675MS);

                    if (resultado_675MS <= 0)
                    {
                        throw new Exception("No se pudo restaurar el cliente.");
                    }

                    //Actualizo la lista utilizada para calcular los DVV
                    clientes_675MS.Remove(vigente_675MS);
                    clientes_675MS.Add(clienteOriginal_675MS);
                }

                //Calculo los DVV mientras la tabla continúa bloqueada
                Dictionary<string, int> digitos_675MS = calcularDVV_675MS(clientes_675MS);

                //Guardo los DVV en la misma transacción
                GuardarDigitosVerticalesInterno_675MS("Cliente", digitos_675MS, acceso_675MS);

                //Confirmo todos los cambios
                acceso_675MS.ConfirmarTx_675MS();
            }
            catch
            {
                //Si ocurre un error, revierto los cambios
                acceso_675MS.RevertirTx_675MS();
                throw;
            }
            finally
            {
                //Cierro la conexión
                acceso_675MS.Desconectar_675MS();
            }
        }

    }

}
