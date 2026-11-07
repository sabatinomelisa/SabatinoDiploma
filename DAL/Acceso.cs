using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;

namespace DAL
{
    //CLASE QUE ENCAPSULA LAS FUNCIONALIDADES BASICAS DE CONEXION Y ACCESO A BASE DE DATOS
    public class Acceso
    {
        SqlConnection conexion_675MS;
        SqlTransaction tx_675MS;

        //Conectar a la Base de Datos
        public void Conectar_675MS()
        {
            conexion_675MS = new SqlConnection();
            // conexion_675MS.ConnectionString = "DATA SOURCE = DESKTOP-BJDMH9N\\SQLEXPRESS;" + "Initial Catalog=PITBOX;" + "Integrated Security=true;";
            conexion_675MS.ConnectionString = "Data Source=.\\SQLEXPRESS;" + "Initial Catalog=PITBOX;" + "Integrated Security=True;";
            conexion_675MS.Open();
        }
        //Desconectar de la Base de Datos
        public void Desconectar_675MS()
        {
            conexion_675MS.Close();
            conexion_675MS = null;
            GC.Collect();
        }

        public void IniciarTx_675MS()
        {
            tx_675MS = conexion_675MS.BeginTransaction();
        }

        public void ConfirmarTx_675MS()
        {
            if (tx_675MS != null)
            {
                tx_675MS.Commit();
                tx_675MS = null; 
            }
        }

        public void RevertirTx_675MS()
        {
            if (tx_675MS != null)
            {
                try { 
                    tx_675MS.Rollback(); 
                } catch { 

                }
                tx_675MS = null;
            }
        }

        public SqlCommand CrearComando_675MS(string sql_675MS, List<SqlParameter> parametros_675MS = null)
        {
            SqlCommand comando_675MS = new SqlCommand(sql_675MS,conexion_675MS);

            comando_675MS.CommandText = sql_675MS;
            comando_675MS.CommandType = System.Data.CommandType.StoredProcedure;

            if (tx_675MS != null)
            {
                comando_675MS.Transaction = tx_675MS;
            }

            if (parametros_675MS != null)
            {
                comando_675MS.Parameters.AddRange(parametros_675MS.ToArray());
            }

            return comando_675MS;
        }

        public SqlParameter CrearParametro_675MS(string nombre_675MS, int valor_675MS)
        {
            SqlParameter parametro_675MS = new SqlParameter();

            parametro_675MS.ParameterName = nombre_675MS;
            parametro_675MS.Value = valor_675MS;
            parametro_675MS.DbType = System.Data.DbType.Int32;

            return parametro_675MS;

        }

        public SqlParameter CrearParametro_675MS(string nombre_675MS, string valor_675MS)
        {
            SqlParameter parametro_675MS = new SqlParameter();

            parametro_675MS.ParameterName = nombre_675MS;
            parametro_675MS.Value = valor_675MS;
            parametro_675MS.DbType = System.Data.DbType.String;

            return parametro_675MS;
        }

        public SqlParameter CrearParametro_675MS(string nombre_675MS, DateTime valor_675MS)
        {
            SqlParameter parametro_675MS = new SqlParameter();

            parametro_675MS.ParameterName = nombre_675MS;
            parametro_675MS.Value = valor_675MS;
            parametro_675MS.DbType = System.Data.DbType.DateTime;

            return parametro_675MS;
        }
        public SqlParameter CrearParametroDateSolo_675MS(string nombre_675MS, DateTime valor_675MS)
        {
            SqlParameter parametro_675MS = new SqlParameter();
            parametro_675MS .ParameterName = nombre_675MS;

            // Si la fecha es DateTime.MinValue (0001-01-01), mandamos DBNull a la base para evitar problemas
            if (valor_675MS == DateTime.MinValue)
            {
                parametro_675MS.Value = DBNull.Value;
            }
            else
            {
                parametro_675MS.Value = valor_675MS;
                parametro_675MS.SqlDbType = SqlDbType.Date; // Forzamos explícitamente el tipo Date de SQL Server
            }

            return parametro_675MS;
        }
        public DataTable Leer_675MS(string sql_675MS, List<SqlParameter> parametros_675MS = null)
        {
            DataTable tabla_675MS = new DataTable();
            SqlDataAdapter adapter_675MS = new SqlDataAdapter();

            adapter_675MS.SelectCommand = CrearComando_675MS(sql_675MS, parametros_675MS);
            adapter_675MS.Fill(tabla_675MS);

            return tabla_675MS;
        }

        public int Escribir_675MS(string sql_675MS, List<SqlParameter> parametros_675MS = null)
        {
            int filasAfectadas_675MS = 0;

            SqlCommand comando = CrearComando_675MS(sql_675MS, parametros_675MS);

            try
            {
                filasAfectadas_675MS = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                filasAfectadas_675MS = -1;
            }

            return filasAfectadas_675MS;
        }

        public int DevolverEscalar_675MS(string sql_675MS, List<SqlParameter> parameters_675MS = null)
        {
            SqlCommand comando = CrearComando_675MS(sql_675MS, parameters_675MS);

            return int.Parse(comando.ExecuteScalar().ToString());
        }

        public string DevolverEscalarString_675MS(string sql_675MS, List<SqlParameter> parameters_675MS = null)
        {
            SqlCommand comando_675MS = CrearComando_675MS(sql_675MS, parameters_675MS);

            object resultado_675MS = comando_675MS.ExecuteScalar();

            if (resultado_675MS == null)
            {
                return string.Empty;
            }

            return resultado_675MS.ToString();

        }

        public object DevolverEscalarObjeto_675MS(string sql_675MS, List<SqlParameter> parameters_675MS = null)
        {
            SqlCommand comando_675MS = CrearComando_675MS(sql_675MS, parameters_675MS);

            object resultado_675MS = comando_675MS.ExecuteScalar();

            if (resultado_675MS == null || resultado_675MS == DBNull.Value)
            {
                return null;
            }

            return resultado_675MS;
        }

    }
}
