
using DAL;
using System;
using System.IO;

namespace BLL
{
    public class BackupRestoreBLL
    {
        private readonly BackupRestoreDAL backupRestoreDAL_675MS = new BackupRestoreDAL();

        public void RealizarBackup_675MS(string ruta_675MS)
        {
            if (string.IsNullOrWhiteSpace(ruta_675MS)) throw new ArgumentException("Debe seleccionar una ruta para guardar el backup.");
            if (!string.Equals(Path.GetExtension(ruta_675MS), ".bak", StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("El archivo de backup debe tener extensión .bak.");

            backupRestoreDAL_675MS.RealizarBackup_675MS(ruta_675MS);
        }

        public string PrepararRestauracion_675MS(string ruta_675MS, string carpetaPreventivo_675MS)
        {
            if (string.IsNullOrWhiteSpace(ruta_675MS)) throw new ArgumentException("Debe seleccionar un archivo de backup.");
            if (!string.Equals(Path.GetExtension(ruta_675MS), ".bak", StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("El archivo seleccionado debe tener extensión .bak.");
            if (string.IsNullOrWhiteSpace(carpetaPreventivo_675MS)) throw new ArgumentException("Debe seleccionar una carpeta para el backup preventivo.");

            backupRestoreDAL_675MS.ValidarBackup_675MS(ruta_675MS);

            string rutaPreventivo_675MS = backupRestoreDAL_675MS.RealizarBackupPreventivo_675MS(carpetaPreventivo_675MS);

            return rutaPreventivo_675MS;
        }
        public void RestaurarBackup_675MS(string ruta_675MS, string carpetaPreventivo_675MS)
        {
            if (string.IsNullOrWhiteSpace(ruta_675MS)) throw new ArgumentException("Debe seleccionar un archivo de backup.");
            if (!string.Equals(Path.GetExtension(ruta_675MS), ".bak", StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("El archivo seleccionado debe tener extensión .bak.");
            if (string.IsNullOrWhiteSpace(carpetaPreventivo_675MS)) throw new ArgumentException("Debe seleccionar una carpeta para el backup preventivo.");

            backupRestoreDAL_675MS.ValidarBackup_675MS(ruta_675MS);
            backupRestoreDAL_675MS.ValidarRutasRestauracion_675MS(ruta_675MS);

            string rutaPreventivo_675MS = backupRestoreDAL_675MS.RealizarBackupPreventivo_675MS(carpetaPreventivo_675MS);

            backupRestoreDAL_675MS.RestaurarBackup_675MS(ruta_675MS);
        }
    }
}
