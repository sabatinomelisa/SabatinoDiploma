using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class IntegridadBLL
    {
        private const string EntidadCliente_675MS = "Cliente";
        private readonly IntegridadDAL integridadDAL_675MS;
        private readonly CalculadorDigitoVerificador calculadorDigitoVerificador_675MS;

        public IntegridadBLL()
        {
            integridadDAL_675MS = new IntegridadDAL();
            calculadorDigitoVerificador_675MS = new CalculadorDigitoVerificador();
        }

        public void InicializarSiCorresponde_675MS()
        {
            List<ClienteBE> clientes_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS();

            // Si no hay clientes, no hay integridad que verificar
            if (clientes_675MS.Count == 0)
            {
                return;
            }

            if (!integridadDAL_675MS.ExistenDigitosVerticales_675MS(EntidadCliente_675MS))
            {
                throw new Exception("No existen los dígitos verificadores verticales de Cliente. " + "Se requiere una revisión y recuperación de integridad por parte del administrador.");
            }
        }

        public void ValidarIntegridadParaLogin_675MS()
        {
            InicializarSiCorresponde_675MS();
            ValidarIntegridadObligatoria_675MS("login");
        }

        public void ActualizarIntegridadParaLogout_675MS()
        {
            InicializarSiCorresponde_675MS();
            ValidarIntegridadObligatoria_675MS("logout");
        }

        private void ValidarIntegridadObligatoria_675MS(string operacion_675MS)
        {
            List<ResultadoIntegridadBE> resultados_675MS = VerificarIntegridad_675MS();

            foreach (ResultadoIntegridadBE resultado_675MS in resultados_675MS)
            {
                if (!resultado_675MS.Correcto_675MS)
                {
                    throw new Exception(
                        "Error de integridad en " + operacion_675MS +
                        ". Entidad: " + resultado_675MS.Entidad_675MS +
                        ". Tipo: " + resultado_675MS.TipoDigito_675MS +
                        ". Identificador: " + resultado_675MS.Identificador_675MS +
                        ". Valor esperado: " + resultado_675MS.ValorEsperado_675MS +
                        ". Valor actual: " + resultado_675MS.ValorActual_675MS
                    );
                }
            }
        }

        public List<ResultadoIntegridadBE> VerificarIntegridad_675MS()
        {
            List<ResultadoIntegridadBE> resultados_675MS = new List<ResultadoIntegridadBE>();
            List<ClienteBE> clientes_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS();

            if (clientes_675MS.Count == 0)
            {
                resultados_675MS.Add(new ResultadoIntegridadBE
                {
                    Entidad_675MS = EntidadCliente_675MS,
                    Identificador_675MS = "GENERAL",
                    TipoDigito_675MS = "DVH/DVV",
                    ValorEsperado_675MS = 0,
                    ValorActual_675MS = 0,
                    Correcto_675MS = true,
                    Mensaje_675MS = "No hay clientes registrados. No hay integridad que verificar."
                });

                return resultados_675MS;
            }

            foreach (ClienteBE cliente_675MS in clientes_675MS)
            {

                int digitoEsperado = CalcularDigitoHorizontalCliente_675MS(cliente_675MS);

                bool correcto = digitoEsperado == cliente_675MS.DigitoVerificadorHorizontal_675MS;

                if (!correcto)
                {
                    resultados_675MS.Add(new ResultadoIntegridadBE
                    {
                        Entidad_675MS = EntidadCliente_675MS,
                        Identificador_675MS = cliente_675MS.Dni_675MS.ToString(),
                        TipoDigito_675MS = "DVH",
                        ValorEsperado_675MS = digitoEsperado,
                        ValorActual_675MS = cliente_675MS.DigitoVerificadorHorizontal_675MS,
                        Correcto_675MS = false,
                        Mensaje_675MS = "El registro fue modificado o alterado por fuera del sistema."
                    });
                }
            }

            // Verificamos si existen los dígitos verificadores verticales
            if (!integridadDAL_675MS.ExistenDigitosVerticales_675MS(EntidadCliente_675MS))
            {
                resultados_675MS.Add(new ResultadoIntegridadBE
                {
                    Entidad_675MS = EntidadCliente_675MS,
                    Identificador_675MS = "GENERAL",
                    TipoDigito_675MS = "DVV",
                    ValorEsperado_675MS = 0,
                    ValorActual_675MS = -1,
                    Correcto_675MS = false,
                    Mensaje_675MS = "No existen los dígitos verificadores verticales. Se requiere recuperación de integridad."
                });
            }



            Dictionary<string, int> verticalesCalculados = CalcularDigitosVerticales_675MS(clientes_675MS);
            Dictionary<string, int> verticalesGuardados = integridadDAL_675MS.ObtenerDigitosVerticales_675MS(EntidadCliente_675MS);

            foreach (KeyValuePair<string, int> verticalCalculado in verticalesCalculados)
            {
                int valorGuardado = verticalesGuardados.ContainsKey(verticalCalculado.Key) ? verticalesGuardados[verticalCalculado.Key] : -1;

                if (valorGuardado != verticalCalculado.Value)
                {
                    resultados_675MS.Add(new ResultadoIntegridadBE
                    {
                        Entidad_675MS = EntidadCliente_675MS,
                        Identificador_675MS = verticalCalculado.Key,
                        TipoDigito_675MS = "DVV",
                        ValorEsperado_675MS = verticalCalculado.Value,
                        ValorActual_675MS = valorGuardado,
                        Correcto_675MS = false,
                        Mensaje_675MS = "La columna presenta altas, bajas o intercambios no registrados por la aplicación."
                    });
                }
            }

            if (resultados_675MS.Count == 0)
            {
                resultados_675MS.Add(new ResultadoIntegridadBE
                {
                    Entidad_675MS = EntidadCliente_675MS,
                    Identificador_675MS = "GENERAL",
                    TipoDigito_675MS = "DVH/DVV",
                    ValorEsperado_675MS = 0,
                    ValorActual_675MS = 0,
                    Correcto_675MS = true,
                    Mensaje_675MS = "La integridad de la entidad Cliente es correcta."
                });
            }

            return resultados_675MS;
        }

        public void RecalcularIntegridad_675MS()
        {
            // Verifico que el usuario tenga permiso para gestionar integridad
            ValidarPermisoIntegridad_675MS();

            // Verifico la integridad antes de recalcular los dígitos
            List<ResultadoIntegridadBE> resultados_675MS = VerificarIntegridad_675MS();

            foreach (ResultadoIntegridadBE resultado_675MS in resultados_675MS)
            {
                // Si existe un DVH incorrecto, no permito recalcularlo
                if (!resultado_675MS.Correcto_675MS && resultado_675MS.TipoDigito_675MS == "DVH")
                {
                    throw new Exception("No se puede recalcular la integridad porque existen datos " + "posiblemente modificados. Consulte el Historial de Clientes.");
                }
            }

            // Recalculo solamente si los DVH existentes son correctos
            CalcularYGuardarIntegridad_675MS();
        }

        private void CalcularYGuardarIntegridad_675MS()
        {
            List<ClienteBE> clientes_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS();

            foreach (ClienteBE cliente_675MS in clientes_675MS)
            {
                int digitoHorizontal = CalcularDigitoHorizontalCliente_675MS(cliente_675MS);

                integridadDAL_675MS.ActualizarDigitoHorizontalCliente_675MS(cliente_675MS.Dni_675MS, digitoHorizontal);

                cliente_675MS.DigitoVerificadorHorizontal_675MS = digitoHorizontal;
            }

            Dictionary<string, int> digitosVerticales = CalcularDigitosVerticales_675MS(clientes_675MS);

            integridadDAL_675MS.GuardarDigitosVerticales_675MS(EntidadCliente_675MS, digitosVerticales);
        }

        public void ActualizarDigitosVerticalesCliente_675MS()
        {

            // Obtengo todos los clientes actualizados desde la base de datos
            List<ClienteBE> clientes_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS();

            // Calculo los DVV de todas las columnas
            Dictionary<string, int> digitosVerticales_675MS = CalcularDigitosVerticales_675MS(clientes_675MS);

            // Guardo los nuevos DVV
            integridadDAL_675MS.GuardarDigitosVerticales_675MS(EntidadCliente_675MS, digitosVerticales_675MS);
        }

        public void RecuperarDigitosVerticalesCliente_675MS()
        {
            // Verifico que el usuario tenga permiso para gestionar integridad
            ValidarPermisoIntegridad_675MS();

            // Verifico primero que no existan errores de DVH
            List<ResultadoIntegridadBE> resultados_675MS = VerificarIntegridad_675MS();

            foreach (ResultadoIntegridadBE resultado_675MS in resultados_675MS)
            {
                if (!resultado_675MS.Correcto_675MS &&
                    resultado_675MS.TipoDigito_675MS == "DVH")
                {
                    throw new Exception(
                        "No se pueden recuperar los DVV porque existen errores de DVH.");
                }
            }


            integridadDAL_675MS.GuardarIntegridadConsistente_675MS(
                null,
                null,
                clientesBloqueados_675MS =>
                {
                    foreach (ClienteBE cliente_675MS in clientesBloqueados_675MS)
                    {
                        int digitoCalculado_675MS =
                            CalcularDigitoHorizontalCliente_675MS(cliente_675MS);

                        if (digitoCalculado_675MS !=
                            cliente_675MS.DigitoVerificadorHorizontal_675MS)
                        {
                            throw new Exception(
                                "No se pueden recuperar los DVV porque existen errores de DVH.");
                        }
                    }

                    return CalcularDigitosVerticales_675MS(clientesBloqueados_675MS);
                });

        }
        
        public int CalcularDigitoHorizontalCliente_675MS(ClienteBE cliente_675MS)
        {
            return calculadorDigitoVerificador_675MS.CalcularHorizontal_675MS(
                cliente_675MS.Dni_675MS.ToString(),
                cliente_675MS.Apellido_675MS,
                cliente_675MS.Nombre_675MS,
                cliente_675MS.FechaNacimiento_675MS.ToString("yyyyMMdd"),
                cliente_675MS.Mail_675MS,
                cliente_675MS.Domicilio_675MS,
                cliente_675MS.Telefono_675MS.ToString()
            );
        }
        
        private Dictionary<string, int> CalcularDigitosVerticales_675MS(List<ClienteBE> clientes_675MS)
        {
            List<Dictionary<string, string>> filas_675MS = new List<Dictionary<string, string>>();

            foreach (ClienteBE cliente_675MS in clientes_675MS.OrderBy(c => c.Dni_675MS))
            {
                Dictionary<string, string> fila_675MS = new Dictionary<string, string>();
                fila_675MS.Add("DNI", cliente_675MS.Dni_675MS.ToString());
                fila_675MS.Add("Nombre", cliente_675MS.Nombre_675MS);
                fila_675MS.Add("Apellido", cliente_675MS.Apellido_675MS);
                fila_675MS.Add("FechaNacimiento", cliente_675MS.FechaNacimiento_675MS.ToString("yyyyMMdd"));
                fila_675MS.Add("Email", cliente_675MS.Mail_675MS);
                fila_675MS.Add("Domicilio", cliente_675MS.Domicilio_675MS);
                fila_675MS.Add("Telefono", cliente_675MS.Telefono_675MS.ToString());
                fila_675MS.Add("DVH", cliente_675MS.DigitoVerificadorHorizontal_675MS.ToString(CultureInfo.InvariantCulture));
                filas_675MS.Add(fila_675MS);
            }

            return calculadorDigitoVerificador_675MS.CalcularVertical(filas_675MS);
        }

        public List<HistorialClienteBE> ConsultarHistorialCliente_675MS(int dni_675MS)
        {
            ValidarPermisoIntegridad_675MS();

            // Consulto los cambios registrados para el cliente
            HistorialClienteDAL historialDAL_675MS = new HistorialClienteDAL();

            List<HistorialClienteBE> historial_675MS = historialDAL_675MS.ObtenerHistorialPorDNI_675MS(dni_675MS);

            return historial_675MS;
        }

        public List<HistorialClienteBE> ObtenerCambiosParaRecuperar_675MS(int dni_675MS)
        {
            ValidarPermisoIntegridad_675MS();

            // Obtengo el historial del cliente
            List<HistorialClienteBE> historial_675MS = ConsultarHistorialCliente_675MS(dni_675MS);

            // Verifico que existan modificaciones registradas
            if (historial_675MS.Count == 0)
            {
                throw new Exception("No existen modificaciones registradas para este cliente.");
            }

            return historial_675MS;
        }

        private void ValidarPermisoIntegridad_675MS()
        {
            // Verifico que exista una sesión iniciada
            if (!SessionManager.SesionIniciada_675MS)
            {
                throw new UnauthorizedAccessException("Debe iniciar sesión para gestionar la integridad.");
            }

            UsuarioBE usuario_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            // Verifico que el usuario tenga un rol asignado
            if (usuario_675MS == null || usuario_675MS.Rol_675MS == null)
            {
                throw new UnauthorizedAccessException("El usuario no tiene un rol asignado.");
            }

            // Consulto los permisos mediante el Composite
            RolBLL rolBLL_675MS = new RolBLL();

            if (!rolBLL_675MS.TienePermiso_675MS(usuario_675MS.Rol_675MS, "GestionIntegridad"))
            {
                throw new UnauthorizedAccessException("No tiene permiso para gestionar la integridad.");
            }
        }

        private ClienteBE CopiarCliente_675MS(ClienteBE cliente_675MS)
        {
            // Creo una copia del cliente para no modificar el original
            ClienteBE copia_675MS = new ClienteBE();

            copia_675MS.Dni_675MS = cliente_675MS.Dni_675MS;
            copia_675MS.Nombre_675MS = cliente_675MS.Nombre_675MS;
            copia_675MS.Apellido_675MS = cliente_675MS.Apellido_675MS;
            copia_675MS.FechaNacimiento_675MS = cliente_675MS.FechaNacimiento_675MS;
            copia_675MS.Mail_675MS = cliente_675MS.Mail_675MS;
            copia_675MS.Domicilio_675MS = cliente_675MS.Domicilio_675MS;
            copia_675MS.Telefono_675MS = cliente_675MS.Telefono_675MS;
            copia_675MS.DigitoVerificadorHorizontal_675MS = cliente_675MS.DigitoVerificadorHorizontal_675MS;

            return copia_675MS;
        }

        private void AplicarValorAnterior_675MS(ClienteBE cliente_675MS, HistorialClienteBE historial_675MS)
        {
            if (cliente_675MS == null) throw new ArgumentNullException(nameof(cliente_675MS));
            if (historial_675MS == null) throw new ArgumentNullException(nameof(historial_675MS));
            if (cliente_675MS.Dni_675MS != historial_675MS.DNI_675MS) throw new Exception("El historial seleccionado no corresponde al cliente.");

            cliente_675MS.Nombre_675MS = historial_675MS.Nombre_675MS;
            cliente_675MS.Apellido_675MS = historial_675MS.Apellido_675MS;
            cliente_675MS.FechaNacimiento_675MS = historial_675MS.FechaNacimiento_675MS;
            cliente_675MS.Mail_675MS = historial_675MS.Mail_675MS;
            cliente_675MS.Domicilio_675MS = historial_675MS.Domicilio_675MS;
            cliente_675MS.Telefono_675MS = historial_675MS.Telefono_675MS ?? 0;
        }

        public ClienteBE BuscarClienteOriginal_675MS(int dni_675MS)
        {
            // Verifico que el usuario tenga permiso para gestionar integridad
            ValidarPermisoIntegridad_675MS();

            // Obtengo el cliente actual desde la base de datos
            List<ClienteBE> clientes_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS();

            ClienteBE clienteActual_675MS = clientes_675MS.FirstOrDefault(c => c.Dni_675MS == dni_675MS);

            if (clienteActual_675MS == null)
            {
                throw new Exception("No se encontró el cliente.");
            }

            //Verifico si el cliente actual ya tiene un DVH correcto
            int digitoActual_675MS = CalcularDigitoHorizontalCliente_675MS(clienteActual_675MS);

            if (digitoActual_675MS == clienteActual_675MS.DigitoVerificadorHorizontal_675MS)
            {
                throw new Exception("El cliente no presenta errores de DVH. No necesita restauración.");
            }

            // Creo una copia para no modificar los datos originales
            ClienteBE copia_675MS = CopiarCliente_675MS(clienteActual_675MS);

            // Obtengo los cambios registrados en el historial
            List<HistorialClienteBE> historial_675MS =
                ObtenerCambiosParaRecuperar_675MS(dni_675MS);

            // Recorro los cambios desde el más reciente
            foreach (HistorialClienteBE cambio_675MS in historial_675MS)
            {
                // Verifico que el historial coincida con el estado actual de la copia
                ValidarCambioHistorial_675MS(copia_675MS, cambio_675MS);

                // Aplico el valor anterior solamente sobre la copia
                AplicarValorAnterior_675MS(copia_675MS, cambio_675MS);

                // Calculo el DVH de la copia
                int digitoCalculado_675MS = CalcularDigitoHorizontalCliente_675MS(copia_675MS);

                // Comparo con el DVH almacenado originalmente
                if (digitoCalculado_675MS == clienteActual_675MS.DigitoVerificadorHorizontal_675MS)
                {
                    return copia_675MS;
                }
            }

            throw new Exception("No se encontró una versión anterior compatible con el DVH almacenado. " + "Es necesario revisar el historial y una copia de seguridad.");
        }

        private void ValidarCambioHistorial_675MS(ClienteBE cliente_675MS, HistorialClienteBE historial_675MS)
        {
            if (cliente_675MS == null) throw new ArgumentNullException(nameof(cliente_675MS));
            if (historial_675MS == null) throw new ArgumentNullException(nameof(historial_675MS));
            if (cliente_675MS.Dni_675MS != historial_675MS.DNI_675MS) throw new Exception("El historial seleccionado no corresponde al cliente.");
            if (historial_675MS.IdHistorial_675MS <= 0) throw new Exception("Debe seleccionar una versión histórica válida.");
            if (historial_675MS.FechaNacimiento_675MS == DateTime.MinValue) throw new Exception("La versión histórica tiene una fecha de nacimiento inválida.");

            bool mismoEstado_675MS =
                string.Equals(cliente_675MS.Nombre_675MS, historial_675MS.Nombre_675MS, StringComparison.Ordinal) &&
                string.Equals(cliente_675MS.Apellido_675MS, historial_675MS.Apellido_675MS, StringComparison.Ordinal) &&
                cliente_675MS.FechaNacimiento_675MS.Date == historial_675MS.FechaNacimiento_675MS.Date &&
                string.Equals(cliente_675MS.Mail_675MS, historial_675MS.Mail_675MS, StringComparison.Ordinal) &&
                string.Equals(cliente_675MS.Domicilio_675MS, historial_675MS.Domicilio_675MS, StringComparison.Ordinal) &&
                cliente_675MS.Telefono_675MS == historial_675MS.Telefono_675MS.GetValueOrDefault();

            if (mismoEstado_675MS) throw new Exception("El cliente ya tiene los mismos datos que la versión seleccionada.");
        }

        public void RestaurarClienteDesdeHistorial_675MS(int dni_675MS)
        {
            //Verifico que el usuario tenga permiso para gestionar integridad
            ValidarPermisoIntegridad_675MS();

            //Obtengo los datos actuales para comprobarlos antes de restaurar
            List<ClienteBE> clientes_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS();
            ClienteBE clienteActual_675MS = clientes_675MS.FirstOrDefault(c => c.Dni_675MS == dni_675MS);

            if (clienteActual_675MS == null)
            {
                throw new Exception("No se encontró el cliente.");
            }

            clienteActual_675MS = CopiarCliente_675MS(clienteActual_675MS);

            //Busco los datos originales del cliente utilizando el historial
            ClienteBE clienteOriginal_675MS = BuscarClienteOriginal_675MS(dni_675MS);

            //Verifico que los datos recuperados coincidan con el DVH almacenado
            int digitoCalculado_675MS = CalcularDigitoHorizontalCliente_675MS(clienteOriginal_675MS);

            if (digitoCalculado_675MS != clienteOriginal_675MS.DigitoVerificadorHorizontal_675MS)
            {
                throw new Exception("Los datos recuperados no coinciden con el DVH original.");
            }

            //Compruebo que la búsqueda del historial no haya partido de otro estado
            ClienteBE clienteVerificado_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS()
                .FirstOrDefault(c => c.Dni_675MS == dni_675MS);

            if (clienteVerificado_675MS == null ||
                clienteVerificado_675MS.Nombre_675MS != clienteActual_675MS.Nombre_675MS ||
                clienteVerificado_675MS.Apellido_675MS != clienteActual_675MS.Apellido_675MS ||
                clienteVerificado_675MS.FechaNacimiento_675MS.Date != clienteActual_675MS.FechaNacimiento_675MS.Date ||
                clienteVerificado_675MS.Mail_675MS != clienteActual_675MS.Mail_675MS ||
                clienteVerificado_675MS.Domicilio_675MS != clienteActual_675MS.Domicilio_675MS ||
                clienteVerificado_675MS.Telefono_675MS != clienteActual_675MS.Telefono_675MS ||
                clienteVerificado_675MS.DigitoVerificadorHorizontal_675MS != clienteActual_675MS.DigitoVerificadorHorizontal_675MS)
            {
                throw new Exception("Los datos del cliente cambiaron durante la recuperación. Volvé a verificar la integridad.");
            }

            //Restauro el cliente y recalculo los DVV dentro de una misma transacción
            integridadDAL_675MS.GuardarIntegridadConsistente_675MS(
                clienteOriginal_675MS,
                clienteActual_675MS,
                clientesBloqueados_675MS =>  CalcularDigitosVerticales_675MS(clientesBloqueados_675MS));
        }

        public Dictionary<string, int> CalcularDVVConClienteRestaurado_675MS(ClienteBE clienteRestaurado_675MS)
        {
            //Obtengo los clientes actuales de la base de datos
            List<ClienteBE> clientes_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS();

            //Busco el cliente que voy a restaurar
            ClienteBE clienteActual_675MS = clientes_675MS.FirstOrDefault(c => c.Dni_675MS == clienteRestaurado_675MS.Dni_675MS);

            if (clienteActual_675MS == null)
            {
                throw new Exception("No se encontró el cliente para calcular los DVV.");
            }

            //Reemplazo el cliente actual por los datos recuperados del historial
            clientes_675MS.Remove(clienteActual_675MS);
            clientes_675MS.Add(CopiarCliente_675MS(clienteRestaurado_675MS));

            //Calculo los DVV considerando los datos que se van a restaurar
            return CalcularDigitosVerticales_675MS(clientes_675MS);
        }

    }
}
