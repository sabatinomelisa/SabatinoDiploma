using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IntegridadDAL
    {
        private  string connectionString;
        private  ClienteDAL clienteDAL;


        public List<ClienteBE> ListarClientesParaIntegridad_675MS()
        {
            clienteDAL = new ClienteDAL();
            return clienteDAL.ListarClientes_675MS();
        }

        public void ActualizarDigitoHorizontalCliente_675MS(int dni_675MS, int digitoVerificadorHorizontal_675MS)
        {
            clienteDAL.ActualizarDigitoVerificadorHorizontal_675MS(dni_675MS, digitoVerificadorHorizontal_675MS);
        }

        public Dictionary<string, int> ObtenerDigitosVerticales_675MS(string entidad)
        {
            Dictionary<string, int> digitos = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("ConsultarDigitosVerticales", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@entidad", SqlDbType.NVarChar, 100).Value = entidad;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        digitos[reader["Campo"].ToString()] = Convert.ToInt32(reader["Valor"]);
                    }
                }
            }

            return digitos;
        }

        public void GuardarDigitosVerticales_675MS(string entidad_675MS, Dictionary<string, int> digitos_675MS)
        {
            Acceso acceso_675MS = new Acceso();
            try
            {
                //Abro la conexión al iniciar el proceso
                acceso_675MS.Conectar_675MS();

                // Itero sobre cada dígito del diccionario
                foreach (var digito_675MS in digitos_675MS)
                {
                    // 3. Creamos la lista de parámetros usando los helpers de tu clase Acceso
                    List<SqlParameter> parametros_675MS = new List<SqlParameter>();
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@entidad", entidad_675MS));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@campo", digito_675MS.Key));
                    parametros_675MS.Add(acceso_675MS.CrearParametro_675MS("@valor", digito_675MS.Value));

                    // 4. Ejecutamos el procedimiento almacenado mediante el método de escritura
                    acceso_675MS.Escribir_675MS("GUARDAR_DIGITO_VERTICAL", parametros_675MS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Me aseguro de cerrar la conexion
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Me aseguro de cerrar la conexion
                acceso_675MS.Desconectar_675MS();
            }
        }
    }
}
