using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitBox
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                InicializarBaseDeDatos_675MS();
                Application.Run(new FormPrincipal());
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar PitBox.\n\n" + ex.Message,"Error de inicialización",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void InicializarBaseDeDatos_675MS()
        {
            string[] servidoresCandidatos =
            {
                ".",
                @".\SQLEXPRESS",
                @".\SQLEXPRESS01",
                @"(localdb)\MSSQLLocalDB"
            };


            string servidorActivo = null;


            // Primero buscar una instancia que ya tenga PITBOX.
            foreach (string servidor in servidoresCandidatos)
            {
                try
                {
                    string cadena =
                        $"Server={servidor};Database=master;Integrated Security=True;Connect Timeout=3;";

                    using (SqlConnection conexion = new SqlConnection(cadena))
                    {
                        conexion.Open();

                        using (SqlCommand comando = new SqlCommand(
                            "SELECT DB_ID(N'PITBOX')", conexion))
                        {
                            object resultado = comando.ExecuteScalar();

                            if (resultado != null && resultado != DBNull.Value)
                            {
                                servidorActivo = servidor;
                                break;
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                    // Continuar con otra instancia.
                }
            }

            // Si PITBOX no existe, buscar una instancia disponible.
            if (servidorActivo == null)
            {
                foreach (string servidor in servidoresCandidatos)
                {
                    try
                    {
                        string cadena =
                            $"Server={servidor};Database=master;Integrated Security=True;Connect Timeout=3;";

                        using (SqlConnection conexion = new SqlConnection(cadena))
                        {
                            conexion.Open();
                            servidorActivo = servidor;
                            break;
                        }
                    }
                    catch (SqlException)
                    {
                        // Continuar con otra instancia.
                    }
                }
            }

            // Compartir la instancia detectada con la DAL
            if (servidorActivo != null)
            {
                System.Configuration.ConfigurationManager.AppSettings["ServidorSQL"] = servidorActivo;
            }

            if (servidorActivo == null)
            {
                throw new Exception( "No se encontró SQL Server. Es necesario instalar SQL Server Express.");
            }

            string conexionMasterActiva = $"Server={servidorActivo};Database=master;Integrated Security=True;";

            // 2. Crear PITBOX únicamente si no existe.
            using (SqlConnection conexion = new SqlConnection(conexionMasterActiva))
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "IF DB_ID(N'PITBOX') IS NULL CREATE DATABASE [PITBOX];",
                    conexion))
                {
                    comando.ExecuteNonQuery();
                }
            }

            string conexionPitbox =
                $"Server={servidorActivo};Database=PITBOX;Integrated Security=True;";

            // 3. Comprobar si la base ya tiene objetos.
            // No modificar automáticamente bases existentes o incompletas.
            using (SqlConnection conexion = new SqlConnection(conexionPitbox))
            {
                conexion.Open();

                using (SqlCommand comando = new SqlCommand(
                    "SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped = 0;",
                    conexion))
                {
                    int cantidadTablas = Convert.ToInt32(comando.ExecuteScalar());

                    if (cantidadTablas > 0)
                    {
                        return;
                    }
                }
            }

            // 4. Ejecutar el script completo de instalación.
            string carpeta = AppDomain.CurrentDomain.BaseDirectory;
            string rutaScript = Path.Combine(carpeta, "script.sql");

            if (!File.Exists(rutaScript))
            {
                throw new Exception("No se encontró script.sql en la carpeta de instalación.");
            }

            EjecutarScriptSql_675MS(rutaScript, conexionPitbox);
        }

        private static void EjecutarScriptSql_675MS(string rutaArchivo, string cadenaConexion)
        {
            if (!File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo SQL requerido.", rutaArchivo);
            }

            string contenido = File.ReadAllText(rutaArchivo);

            // GO es un separador de lotes de SQL Server.
            string[] bloques = System.Text.RegularExpressions.Regex.Split(
                contenido,
                @"^\s*GO\s*(?:--[^\r\n]*)?$",
                System.Text.RegularExpressions.RegexOptions.Multiline |
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                foreach (string bloque in bloques)
                {
                    string sql = bloque.Trim();

                    if (string.IsNullOrWhiteSpace(sql))
                        continue;

                    using (SqlCommand comando = new SqlCommand(sql, conexion))
                    {
                        comando.CommandTimeout = 120;
                        comando.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}