
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public class BackupRestoreDAL
    {
        private string ObtenerCadenaMaster_675MS()
        {
            string servidor_675MS = ConfigurationManager.AppSettings["ServidorSQL"];

            if (string.IsNullOrWhiteSpace(servidor_675MS))
            {
                throw new Exception("No está configurado el servidor SQL de PitBox.");
            }

            SqlConnectionStringBuilder cadena_675MS = new SqlConnectionStringBuilder();
            cadena_675MS.DataSource = servidor_675MS;
            cadena_675MS.InitialCatalog = "master";
            cadena_675MS.IntegratedSecurity = true;
            cadena_675MS.ConnectTimeout = 15;

            return cadena_675MS.ConnectionString;
        }

        public void RealizarBackup_675MS(string ruta_675MS)
        {
            using (SqlConnection conexion_675MS = new SqlConnection(ObtenerCadenaMaster_675MS()))
            {
                conexion_675MS.Open();

                using (SqlCommand comando_675MS = new SqlCommand("BACKUP DATABASE [PITBOX] TO DISK = @ruta WITH INIT, CHECKSUM", conexion_675MS))
                {
                    comando_675MS.CommandType = CommandType.Text;
                    comando_675MS.CommandTimeout = 300;
                    comando_675MS.Parameters.Add("@ruta", SqlDbType.NVarChar, 4000).Value = ruta_675MS;
                    comando_675MS.ExecuteNonQuery();
                }
            }
        }

        public string RealizarBackupPreventivo_675MS(string carpeta_675MS)
        {
            if (string.IsNullOrWhiteSpace(carpeta_675MS)) throw new ArgumentException("Debe indicar una carpeta para el backup preventivo.");

            string ruta_675MS = Path.Combine(carpeta_675MS, "PITBOX_ANTES_RESTORE_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak");

            RealizarBackup_675MS(ruta_675MS);
            ValidarBackup_675MS(ruta_675MS);

            return ruta_675MS;
        }


        public void RestaurarBackup_675MS(string ruta_675MS)
        {
            using (SqlConnection conexion_675MS = new SqlConnection(ObtenerCadenaMaster_675MS()))
            {
                conexion_675MS.Open();

                try
                {
                    string sql_675MS = @"
                            USE [master];
                            ALTER DATABASE [PITBOX] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                            RESTORE DATABASE [PITBOX] FROM DISK = @ruta WITH REPLACE, RECOVERY, FILE = 1;
                            ALTER DATABASE [PITBOX] SET MULTI_USER;";

                    using (SqlCommand comando_675MS = new SqlCommand(sql_675MS, conexion_675MS))
                    {
                        comando_675MS.CommandTimeout = 600;
                        comando_675MS.Parameters.Add("@ruta", SqlDbType.NVarChar, 4000).Value = ruta_675MS;
                        comando_675MS.ExecuteNonQuery();
                    }
                }
                finally
                {
                    try
                    {
                        if (conexion_675MS.State != ConnectionState.Open) conexion_675MS.Open();

                        using (SqlCommand comando_675MS = new SqlCommand("IF DB_ID('PITBOX') IS NOT NULL ALTER DATABASE [PITBOX] SET MULTI_USER", conexion_675MS))
                        {
                            comando_675MS.CommandTimeout = 60;
                            comando_675MS.ExecuteNonQuery();
                        }
                    }
                    finally
                    {
                        SqlConnection.ClearAllPools();
                    }
                }
            }
        }


        public void ValidarBackup_675MS(string ruta_675MS)
        {
            using (SqlConnection conexion_675MS = new SqlConnection(ObtenerCadenaMaster_675MS()))
            {
                conexion_675MS.Open();

                using (SqlCommand comando_675MS = new SqlCommand("RESTORE HEADERONLY FROM DISK = @ruta", conexion_675MS))
                {
                    comando_675MS.CommandTimeout = 300;
                    comando_675MS.Parameters.Add("@ruta", SqlDbType.NVarChar, 4000).Value = ruta_675MS;

                    using (SqlDataReader lector_675MS = comando_675MS.ExecuteReader())
                    {
                        if (!lector_675MS.Read())
                        {
                            throw new Exception("El archivo no contiene un respaldo válido.");
                        }

                        string baseDatos_675MS = Convert.ToString(lector_675MS["DatabaseName"]);
                        int tipoBackup_675MS = Convert.ToInt32(lector_675MS["BackupType"]);

                        if (!string.Equals(baseDatos_675MS, "PITBOX", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new Exception("El respaldo seleccionado no pertenece a PITBOX.");
                        }

                        if (tipoBackup_675MS != 1)
                        {
                            throw new Exception("Debe seleccionar un respaldo completo de PITBOX.");
                        }
                    }
                }

                using (SqlCommand comando_675MS = new SqlCommand("RESTORE VERIFYONLY FROM DISK = @ruta WITH CHECKSUM", conexion_675MS))
                {
                    comando_675MS.CommandTimeout = 600;
                    comando_675MS.Parameters.Add("@ruta", SqlDbType.NVarChar, 4000).Value = ruta_675MS;
                    comando_675MS.ExecuteNonQuery();
                }
            }
        }


        public void ValidarRutasRestauracion_675MS(string ruta_675MS)
        {
            using (SqlConnection conexion_675MS = new SqlConnection(ObtenerCadenaMaster_675MS()))
            {
                conexion_675MS.Open();

                System.Collections.Generic.Dictionary<string, string> archivosActuales_675MS = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                using (SqlCommand comando_675MS = new SqlCommand("SELECT name, physical_name FROM sys.master_files WHERE database_id = DB_ID('PITBOX')", conexion_675MS))
                using (SqlDataReader lector_675MS = comando_675MS.ExecuteReader())
                {
                    while (lector_675MS.Read()) archivosActuales_675MS.Add(lector_675MS.GetString(0), lector_675MS.GetString(1));
                }

                if (archivosActuales_675MS.Count == 0) throw new Exception("No se encontró la base PITBOX.");

                int cantidad_675MS = 0;

                using (SqlCommand comando_675MS = new SqlCommand("RESTORE FILELISTONLY FROM DISK = @ruta WITH FILE = 1", conexion_675MS))
                {
                    comando_675MS.CommandTimeout = 300;
                    comando_675MS.Parameters.Add("@ruta", SqlDbType.NVarChar, 4000).Value = ruta_675MS;

                    using (SqlDataReader lector_675MS = comando_675MS.ExecuteReader())
                    {
                        while (lector_675MS.Read())
                        {
                            string nombre_675MS = Convert.ToString(lector_675MS["LogicalName"]);
                            string rutaArchivo_675MS = Convert.ToString(lector_675MS["PhysicalName"]);

                            if (!archivosActuales_675MS.ContainsKey(nombre_675MS)) throw new Exception("El respaldo contiene un archivo lógico diferente: " + nombre_675MS);
                            if (!string.Equals(archivosActuales_675MS[nombre_675MS], rutaArchivo_675MS, StringComparison.OrdinalIgnoreCase)) throw new Exception("Las rutas físicas del respaldo no coinciden con PITBOX. Se requiere una restauración con MOVE.");

                            cantidad_675MS++;
                        }
                    }
                }

                if (cantidad_675MS != archivosActuales_675MS.Count) throw new Exception("La cantidad de archivos del respaldo no coincide con PITBOX.");
            }
        }

    }
}
