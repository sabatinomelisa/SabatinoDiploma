using BE;
using BLL;
using Servicios.Observer;
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
    public partial class FormHistorialCliente : Form, IObserverIdioma
    {

        //Gestion del idioma
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;


        public FormHistorialCliente()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void FormHistorialCliente_Load(object sender, EventArgs e)
        {
            // Le saco los bordes y la barra de título superior
            this.FormBorderStyle = FormBorderStyle.None;

            // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
            this.Dock = DockStyle.Fill;

            //Suscribo al evento del observer
            TratamientoIdioma.Instancia.Suscribir(this);
            //Cargo combo con los idiomas del sistema
            CargarComboIdiomas_675MS();

        }

        private void CargarComboIdiomas_675MS()
        {
            try
            {
                cargandoIdiomas_675MS = true;

                idiomas_675MS = idiomaBLL_675MS.ListarIdiomas_675MS();

                cmbIdiomas_675MS.DataSource = null;
                cmbIdiomas_675MS.DisplayMember = "Nombre_675MS";
                cmbIdiomas_675MS.ValueMember = "Id_675MS";
                cmbIdiomas_675MS.DataSource = idiomas_675MS;

                if (TratamientoIdioma.Instancia.IdiomaActual_675MS != null)
                {
                    cmbIdiomas_675MS.SelectedValue =
                        TratamientoIdioma.Instancia.IdiomaActual_675MS.Id_675MS;
                }
                else if (idiomas_675MS.Count > 0)
                {
                    cmbIdiomas_675MS.SelectedIndex = 0;

                    TratamientoIdioma.Instancia.IdiomaActual_675MS =
                        idiomas_675MS[0];
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("No se pudieron cargar los idiomas: " + exception.Message);
            }
            finally
            {
                cargandoIdiomas_675MS = false;
            }

            ActualizarIdioma_675MS();
        }

        public void ActualizarIdioma_675MS()
        {
            if (TratamientoIdioma.Instancia.IdiomaActual_675MS == null)
                return;

            TraduccionBLL traduccionBLL_675MS = new TraduccionBLL();

            int idiomaSeleccionado =
                TratamientoIdioma.Instancia.IdiomaActual_675MS.Id_675MS;

            List<TraduccionBE> traducciones =
                traduccionBLL_675MS.Listar_675MS(idiomaSeleccionado);

            foreach (TraduccionBE traduccion in traducciones)
            {
                switch (traduccion.NombreControl_675MS)
                {
                    case "lblIdioma_675MS":
                        lblIdioma_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblhistorial_675MS":
                        lblhistorial_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "bntConsultar_675MS":
                        bntConsultar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                }
            }
        }

        private void cmbIdiomas_675MS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoIdiomas_675MS)
                return;

            if (cmbIdiomas_675MS.SelectedItem == null)
                return;

            IdiomaBE idioma = cmbIdiomas_675MS.SelectedItem as IdiomaBE;

            if (idioma == null)
                return;

            TratamientoIdioma.Instancia.IdiomaActual_675MS = idioma;

            TratamientoIdioma.Instancia.Notificar();
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void bntConsultar_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                HistorialClienteBLL historialBLL_675MS = new HistorialClienteBLL();
                List<HistorialClienteBE> historial_675MS;
                string dniTexto_675MS = txtDNI_675MS.Text.Trim();

                if (string.IsNullOrWhiteSpace(dniTexto_675MS))
                {
                    historial_675MS = historialBLL_675MS.ListarHistorial_675MS();
                }
                else
                {
                    int dni_675MS;
                    if (!int.TryParse(dniTexto_675MS, out dni_675MS) || dni_675MS <= 0)
                    {
                        MessageBox.Show("Ingresá un DNI válido.", "Historial de clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    historial_675MS = historialBLL_675MS.ObtenerHistorialPorDNI_675MS(dni_675MS);
                }

                if (dtpDesde_675MS.Checked && dtpHasta_675MS.Checked && dtpDesde_675MS.Value.Date > dtpHasta_675MS.Value.Date)
                {
                    MessageBox.Show("La fecha Desde no puede ser posterior a Hasta.", "Historial de clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtpDesde_675MS.Checked)
                    historial_675MS = historial_675MS.Where(h => h.FechaCambio_675MS >= dtpDesde_675MS.Value.Date).ToList();

                if (dtpHasta_675MS.Checked)
                    historial_675MS = historial_675MS.Where(h => h.FechaCambio_675MS < dtpHasta_675MS.Value.Date.AddDays(1)).ToList();

                dgvHistorial_675MS.DataSource = null;
                dgvHistorial_675MS.AutoGenerateColumns = true;
                dgvHistorial_675MS.DataSource = historial_675MS;
                dgvHistorial_675MS.ReadOnly = true;
                dgvHistorial_675MS.AllowUserToAddRows = false;
                dgvHistorial_675MS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewColumn columna_675MS in dgvHistorial_675MS.Columns)
                {
                    columna_675MS.HeaderText = columna_675MS.HeaderText.Replace("_675MS", "");
                }

                if (dgvHistorial_675MS.Columns.Contains("IdHistorial_675MS"))
                    dgvHistorial_675MS.Columns["IdHistorial_675MS"].HeaderText = "ID Cambio";

                if (dgvHistorial_675MS.Columns.Contains("FechaCambio_675MS"))
                {
                    dgvHistorial_675MS.Columns["FechaCambio_675MS"].HeaderText = "Fecha del cambio";
                    dgvHistorial_675MS.Columns["FechaCambio_675MS"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                }

                if (dgvHistorial_675MS.Columns.Contains("Operacion_675MS"))
                    dgvHistorial_675MS.Columns["Operacion_675MS"].HeaderText = "Operación";

                // Mostrar el estado activo como texto
                if (dgvHistorial_675MS.Columns.Contains("Act_675MS"))
                    dgvHistorial_675MS.Columns["Act_675MS"].Visible = false;

                if (dgvHistorial_675MS.Columns.Contains("EstadoVersion_675MS"))
                    dgvHistorial_675MS.Columns.Remove("EstadoVersion_675MS");

                dgvHistorial_675MS.Columns.Add("EstadoVersion_675MS", "Versión activa");

                foreach (DataGridViewRow fila_675MS in dgvHistorial_675MS.Rows)
                {
                    HistorialClienteBE version_675MS = fila_675MS.DataBoundItem as HistorialClienteBE;

                    if (version_675MS != null)
                        fila_675MS.Cells["EstadoVersion_675MS"].Value = version_675MS.Act_675MS ? "Sí" : "No";
                }

                dgvHistorial_675MS.ClearSelection();
                btnActivar_675MS.Enabled = false;

                if (historial_675MS.Count == 0)
                    MessageBox.Show("No hay cambios registrados.", "Historial de clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar historial", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnLimpiar_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                txtDNI_675MS.Clear();
                dtpDesde_675MS.Checked = false;
                dtpHasta_675MS.Checked = false;
                dtpDesde_675MS.Value = DateTime.Today;
                dtpHasta_675MS.Value = DateTime.Today;
                bntConsultar_675MS_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al limpiar filtros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActivar_675MS_Click(object sender, EventArgs e)
        {
            if (dgvHistorial_675MS.SelectedRows.Count == 0) return;

            HistorialClienteBE historial_675MS = dgvHistorial_675MS.SelectedRows[0].DataBoundItem as HistorialClienteBE;

            if (historial_675MS == null || historial_675MS.Act_675MS) return;

            DialogResult respuesta_675MS = MessageBox.Show(
                "¿Querés recuperar esta versión del cliente?",
                "Recuperar versión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta_675MS != DialogResult.Yes) return;

            try
            {
                ClienteBLL clienteBLL_675MS = new ClienteBLL();
                int resultado_675MS = clienteBLL_675MS.RecuperarCliente_675MS(historial_675MS);

                if (resultado_675MS <= 0)
                    throw new Exception("No se pudo recuperar la versión del cliente.");

                MessageBox.Show("Versión recuperada correctamente.");
                bntConsultar_675MS_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al recuperar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHistorial_675MS_SelectionChanged(object sender, EventArgs e)
        {
            btnActivar_675MS.Enabled = false;
            if (dgvHistorial_675MS.SelectedRows.Count == 0) return;
            HistorialClienteBE historial_675MS = dgvHistorial_675MS.SelectedRows[0].DataBoundItem as HistorialClienteBE;
            if (historial_675MS == null) return;
            btnActivar_675MS.Enabled = !historial_675MS.Act_675MS;
        }
    }
}
