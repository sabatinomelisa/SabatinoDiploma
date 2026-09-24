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
        private readonly string connectionString;
        private readonly ClienteDAL clienteDAL;


        public List<ClienteBE> ListarClientesParaIntegridad_675MS()
        {
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

        //public void GuardarDigitosVerticales_675MS(string entidad_675MS, Dictionary<string, int> digitos_675MS)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        foreach (KeyValuePair<string, int> digito_675MS in digitos_675MS)
        //        {
        //            using (SqlCommand command = new SqlCommand("GuardarDigitoVertical", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@entidad", SqlDbType.NVarChar, 100).Value = entidad;
        //                command.Parameters.Add("@campo", SqlDbType.NVarChar, 100).Value = digito.Key;
        //                command.Parameters.Add("@valor", SqlDbType.Int).Value = digito.Value;
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}

        //public bool ExistenDigitosVerticales_675MS(string entidad_675MS)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand("ExisteDigitoVertical", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.Add("@entidad", SqlDbType.NVarChar, 100).Value = entidad;
        //        connection.Open();
        //        return Convert.ToInt32(command.ExecuteScalar()) > 0;
        //    }
        //}
    }
}
