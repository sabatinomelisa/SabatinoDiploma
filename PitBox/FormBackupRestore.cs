using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitBox
{
    public partial class FormBackupRestore : Form
    {
        public FormBackupRestore()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoPrincipal;
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void FormBackupRestore_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            txtRuta_675MS.ReadOnly = true;
            btnRestore_675MS.Text = "Restaurar base de datos";
            btnRestore_675MS.Enabled = true;
        }

        private readonly BackupRestoreBLL backupRestoreBLL_675MS = new BackupRestoreBLL();



        private void btnVolver_675MS_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBackup_675MS_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRuta_675MS.Text))
                {
                    MessageBox.Show("Primero seleccioná dónde guardar el backup.");
                    return;
                }

                btnBackup_675MS.Enabled = false;
                backupRestoreBLL_675MS.RealizarBackup_675MS(txtRuta_675MS.Text);
                MessageBox.Show("Backup generado correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar el backup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBackup_675MS.Enabled = true;
            }
        }

        private void btnExaminar_675MS_Click_1(object sender, EventArgs e)
        {
            using (SaveFileDialog dialogo_675MS = new SaveFileDialog())
            {
                dialogo_675MS.Filter = "Backup SQL Server (*.bak)|*.bak";
                dialogo_675MS.FileName = "PITBOX_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";
                dialogo_675MS.OverwritePrompt = true;

                if (dialogo_675MS.ShowDialog() == DialogResult.OK)
                {
                    txtRuta_675MS.Text = dialogo_675MS.FileName;
                }
            }
        }

        private void btnSeleccionarRestore_675MS_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo_675MS = new OpenFileDialog())
            {
                dialogo_675MS.Title = "Seleccionar backup de PITBOX";
                dialogo_675MS.Filter = "Backup SQL Server (*.bak)|*.bak";
                dialogo_675MS.CheckFileExists = true;

                if (dialogo_675MS.ShowDialog() == DialogResult.OK)
                {
                    txtRuta_675MS.Text = dialogo_675MS.FileName;
                }
            }
        }

        private void btnRestore_675MS_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtRuta_675MS.Text))
            {
                MessageBox.Show("Primero seleccioná el archivo .bak que querés restaurar.");
                return;
            }

            if (!string.Equals(System.IO.Path.GetExtension(txtRuta_675MS.Text), ".bak", StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(txtRuta_675MS.Text))
            {
                MessageBox.Show("Seleccioná un archivo .bak existente y accesible desde esta computadora.", "Archivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion_675MS = MessageBox.Show("ATENCIÓN: se reemplazarán los datos actuales de PITBOX por los del respaldo seleccionado.\n\nLos cambios posteriores al respaldo dejarán de estar en la base restaurada.\n\n¿Confirmás la restauración?", "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (confirmacion_675MS != DialogResult.Yes) return;

            using (FolderBrowserDialog dialogo_675MS = new FolderBrowserDialog())
            {
                dialogo_675MS.Description = "Seleccioná la carpeta donde se guardará el backup preventivo.";

                if (dialogo_675MS.ShowDialog() != DialogResult.OK) return;

                try
                {
                    btnRestore_675MS.Enabled = false;
                    btnBackup_675MS.Enabled = false;
                    this.UseWaitCursor = true;

                    backupRestoreBLL_675MS.RestaurarBackup_675MS(txtRuta_675MS.Text, dialogo_675MS.SelectedPath);

                    MessageBox.Show("PITBOX fue restaurada correctamente.\n\nLa aplicación se cerrará. Iniciá sesión nuevamente.", "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo completar la restauración.\n\n" + ex.Message + "\n\nVerificá el estado de PITBOX en SQL Server antes de volver a utilizarla.", "Error de restauración", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Application.Exit();
                }
                finally
                {
                    this.UseWaitCursor = false;
                }
            }
        }
    
    }
}
