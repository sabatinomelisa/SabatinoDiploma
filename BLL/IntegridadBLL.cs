using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!integridadDAL_675MS.ExistenDigitosVerticales_675MS(EntidadCliente_675MS))
            {
                RecalcularIntegridad_675MS();
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
            RecalcularIntegridad_675MS();
        }

        private void ValidarIntegridadObligatoria_675MS(string operacion_675MS)
        {
            List<ResultadoIntegridadBE> resultados_675MS = VerificarIntegridad_675MS();

            foreach (ResultadoIntegridadBE resultado_675MS in resultados_675MS)
            {
                if (!resultado_675MS.Correcto_675MS)
                {
                    throw new Exception("Error de integridad en " + operacion_675MS + ": " + resultado_675MS.Mensaje_675MS);
                }
            }
        }

        public List<ResultadoIntegridadBE> VerificarIntegridad_675MS()
        {
            List<ResultadoIntegridadBE> resultados_675MS = new List<ResultadoIntegridadBE>();
            List<ClienteBE> clientes_675MS = integridadDAL_675MS.ListarClientesParaIntegridad_675MS();

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

            foreach (ClienteBE cliente_675MS in clientes_675MS)
            {
                Dictionary<string, string> fila_675MS = new Dictionary<string, string>();
                fila_675MS.Add("DNI",cliente_675MS.Dni_675MS.ToString());
                fila_675MS.Add("Nombre", cliente_675MS.Nombre_675MS);
                fila_675MS.Add("Apellido", cliente_675MS.Apellido_675MS);
                fila_675MS.Add("FechaNacimiento", cliente_675MS.FechaNacimiento_675MS.ToString("yyyyMMdd"));
                fila_675MS.Add("Email", cliente_675MS.Mail_675MS);
                fila_675MS.Add("Domicilio",cliente_675MS.Domicilio_675MS);
                fila_675MS.Add("Telefono", cliente_675MS.Telefono_675MS.ToString());
                fila_675MS.Add("DVH",cliente_675MS.DigitoVerificadorHorizontal_675MS.ToString(CultureInfo.InvariantCulture));
                filas_675MS.Add(fila_675MS);
            }

            return calculadorDigitoVerificador_675MS.CalcularVertical(filas_675MS);
        }

    }
}
