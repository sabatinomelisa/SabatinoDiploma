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
            // Llamamos a la verificación de la base de datos antes de abrir el primer formulario
            InicializarBaseDeDatos_675MS();
            Application.Run(new FormPrincipal());
        }
        private static void InicializarBaseDeDatos_675MS()
        {
            try
            {
                string servidorActivo = null;
                string[] servidoresCandidatos = { ".", "localhost", ".\\SQLEXPRESS", ".\\SQLEXPRESS01", "(localdb)\\MSSQLLocalDB" };

                // 1. Probar automáticamente cuál de los servidores locales responde
                foreach (var servidor in servidoresCandidatos)
                {
                    string conexionMaster = $"Server={servidor};Database=master;Trusted_Connection=True;";

                    using (SqlConnection connMaster = new SqlConnection(conexionMaster))
                    {
                        try
                        {
                            connMaster.Open();
                            servidorActivo = servidor; // ¡Encontró una instancia válida!

                            // Asegurar que la base de datos PITBOX exista en este servidor
                            SqlCommand cmdCreate = new SqlCommand(
                                "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PITBOX') CREATE DATABASE [PITBOX]", connMaster);
                            cmdCreate.ExecuteNonQuery();
                            break; // Salimos del bucle al conectar con éxito
                        }
                        catch
                        {
                            // Si este servidor no responde, el bucle continúa probando con el siguiente
                        }
                    }
                }

                if (string.IsNullOrEmpty(servidorActivo))
                {
                    throw new Exception("No se pudo detectar ninguna instancia activa de SQL Server (ni ., ni localhost, ni SQLEXPRESS).");
                }

                // 2. Conectarse a la base de datos PITBOX usando el servidor detectado
                string conexionPitbox = $"Server={servidorActivo};Database=PITBOX;Trusted_Connection=True;";

                using (SqlConnection connPitbox = new SqlConnection(conexionPitbox))
                {
                    connPitbox.Open();

                    // 3. Verificar si ya existen las tablas para ejecutar el script inicial una única vez
                    SqlCommand cmdTableCheck = new SqlCommand("SELECT OBJECT_ID('dbo.Empleado', 'U')", connPitbox);
                    var tablaExiste = cmdTableCheck.ExecuteScalar();

                    if (tablaExiste == null || tablaExiste == DBNull.Value)
                    {
                        string rutaScript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "script.sql");

                        if (!File.Exists(rutaScript))
                        {
                            throw new Exception("No se encontró el archivo script.sql en la carpeta de instalación.");
                        }

                        string scriptContenido = File.ReadAllText(rutaScript);

                        // Separar los bloques utilizando GO
                        string[] comandos = System.Text.RegularExpressions.Regex.Split(
                            scriptContenido,
                            @"^\s*GO\s*$",
                            System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                        foreach (string comandoSql in comandos)
                        {
                            string sqlLimpio = comandoSql.Trim();
                            if (!string.IsNullOrWhiteSpace(sqlLimpio) &&
                                !sqlLimpio.StartsWith("USE ", StringComparison.OrdinalIgnoreCase))
                            {
                                using (SqlCommand cmd = new SqlCommand(sqlLimpio, connPitbox))
                                {
                                    cmd.CommandTimeout = 120;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al inicializar la base de datos: " + ex.Message,
                    "Error crítico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}