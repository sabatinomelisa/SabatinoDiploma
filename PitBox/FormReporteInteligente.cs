using System;
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
using BE.BE;

namespace PitBox
{
    public partial class FormReporteInteligente : Form,IObserverIdioma
    {
        //Manejo de Idiomas
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        public FormReporteInteligente()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnGenerar_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.ReporteBLL reporteBLL_675MS = new BLL.ReporteBLL();

                List <ReporteBE> turnos_675MS = reporteBLL_675MS.ObtenerTurnosPosterioresVTV_675MS();
                List<ReporteBE> reinspecciones_675MS = reporteBLL_675MS.ObtenerReinspeccionesPendientes_675MS();

                // Cargar los datos en la grilla
                dgvReporte_675MS.DataSource = null;
                dgvReporte_675MS.Columns.Clear();
                dgvReporte_675MS.AutoGenerateColumns = true;
                dgvReporte_675MS.DataSource = new System.ComponentModel.BindingList<ReporteBE>(reinspecciones_675MS);

                // Aplicar formato una vez cargados los datos
                ConfigurarGrillaReporte_675MS();

                // Mostrar las conclusiones inteligentes
                txtConclusiones_675MS.Text = reporteBLL_675MS.GenerarConclusiones_675MS(turnos_675MS, reinspecciones_675MS);

                // Actualizar indicadores
                lblReinspecciones_675MS.Text = "Reinspecciones pendientes: " + reinspecciones_675MS.Count;
                IndicadoresReporteBE indicadores_675MS = reporteBLL_675MS.ObtenerIndicadores_675MS();

                lblTurnos_675MS.Text = "Turnos activos: " + indicadores_675MS.TurnosActivos_675MS;
                lblAprobadas_675MS.Text = "VTV aprobadas: " + indicadores_675MS.VTVAprobadas_675MS;

                // Ajustar el cuadro de conclusiones
                txtConclusiones_675MS.Multiline = true;
                txtConclusiones_675MS.ScrollBars = ScrollBars.Vertical;
                txtConclusiones_675MS.ReadOnly = true;

                // Verificar resultados
                MessageBox.Show("Reporte generado correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex_675MS)
            {
                MessageBox.Show("Error al generar el reporte: " + ex_675MS.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ConfigurarGrillaReporte_675MS()
        {
            dgvReporte_675MS.AutoGenerateColumns = true;
            dgvReporte_675MS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporte_675MS.ReadOnly = true;
            dgvReporte_675MS.AllowUserToAddRows = false;
            dgvReporte_675MS.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvReporte_675MS.RowHeadersVisible = false;
            dgvReporte_675MS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvReporte_675MS.BackgroundColor = System.Drawing.Color.White;
            dgvReporte_675MS.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvReporte_675MS.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvReporte_675MS.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;
            dgvReporte_675MS.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            dgvReporte_675MS.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White; dgvReporte_675MS.GridColor = System.Drawing.Color.FromArgb(60, 60, 60);
            dgvReporte_675MS.BorderStyle = BorderStyle.None;

            dgvReporte_675MS.EnableHeadersVisualStyles = false;
            dgvReporte_675MS.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            dgvReporte_675MS.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvReporte_675MS.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dgvReporte_675MS.ColumnHeadersHeight = 40;

            dgvReporte_675MS.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            dgvReporte_675MS.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvReporte_675MS.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 120, 180);
            dgvReporte_675MS.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvReporte_675MS.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
            dgvReporte_675MS.RowTemplate.Height = 32;
            dgvReporte_675MS.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(55, 55, 55);

            foreach (DataGridViewColumn columna_675MS in dgvReporte_675MS.Columns)
            {
                columna_675MS.HeaderText = columna_675MS.HeaderText.Replace("_675MS", "");
            }

            if (dgvReporte_675MS.Columns.Contains("TurnosPosteriores_675MS")) dgvReporte_675MS.Columns["TurnosPosteriores_675MS"].Visible = false;
            if (dgvReporte_675MS.Columns.Contains("MinutosReservados_675MS")) dgvReporte_675MS.Columns["MinutosReservados_675MS"].Visible = false;

            if (dgvReporte_675MS.Columns.Contains("FechaInspeccion_675MS")) dgvReporte_675MS.Columns["FechaInspeccion_675MS"].HeaderText = "Fecha VTV";
            if (dgvReporte_675MS.Columns.Contains("FechaReinspeccion_675MS")) dgvReporte_675MS.Columns["FechaReinspeccion_675MS"].HeaderText = "Reinspección";
            if (dgvReporte_675MS.Columns.Contains("SituacionTurno_675MS")) dgvReporte_675MS.Columns["SituacionTurno_675MS"].HeaderText = "Situación del turno";
            if (dgvReporte_675MS.Columns.Contains("Recomendacion_675MS")) dgvReporte_675MS.Columns["Recomendacion_675MS"].HeaderText = "Recomendación";
        }

        private void FormReporteInteligente_Load(object sender, EventArgs e)
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
                //switch (traduccion.NombreControl_675MS)
                //{
                //    case "lblIdioma_675MS":
                //        lblIdioma_675MS.Text = traduccion.Traduccion_675MS;
                //        break;
                //    case "lblUsuario_675MS":
                //        lblUsuario_675MS.Text = traduccion.Traduccion_675MS;
                //        break;
                //    case "lblPassword_675MS":
                //        lblPassword_675MS.Text = traduccion.Traduccion_675MS;
                //        break;
                //    case "txtUsuario_675MS":
                //        txtUsuario_675MS.Text = traduccion.Traduccion_675MS;
                //        break;
                //    case "txtPassword_675MS":
                //        txtPassword_675MS.Text = traduccion.Traduccion_675MS;
                //        break;
                //    case "btnIngresar_675MS":
                //        btnIngresar_675MS.Text = traduccion.Traduccion_675MS;
                //        break;
                //    case "btnSalir_675MS":
                //        btnSalir_675MS.Text = traduccion.Traduccion_675MS;
                //        break;
                //    case "bntRegistrar_675MS":
                //        bntRegistrar_675MS.Text = traduccion.Traduccion_675MS;
                //        break;

                //}
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
    }
}
