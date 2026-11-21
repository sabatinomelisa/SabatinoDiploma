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
    public partial class FormBitacora : Form, IObserverIdioma
    {
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        public FormBitacora()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void FormBitacora_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            TratamientoIdioma.Instancia.Suscribir(this);
            CargarComboIdiomas_675MS();
        }




        private void FormPrincipal_Load(object sender, EventArgs e)
        {
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
                    case "lblBitacora_675MS":
                        lblBitacora_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblIdioma_675MS":
                        lblIdioma_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblUsuario_675MS":
                        lblUsuario_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblModulo_675MS":
                        lblModulo_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "chkDesde_675MS":
                        chkDesde_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "chkHasta_675MS":
                        chkHasta_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "btnBuscar_675MS":
                        btnBuscar_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "btnLimpiar_675MS":
                        btnLimpiar_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                }
            }
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_675MS_Click(object sender, EventArgs e)
        {
            ConsultarBitacora_675MS();

        }
        private void ConsultarBitacora_675MS()
        {
            try
            {
                BitacoraBLL bitacoraBLL_675MS= new BitacoraBLL();
                string usuario_675MS = txtUsuario_675MS.Text.Trim();
                string modulo_675MS = txtModulo_675MS.Text.Trim();
                DateTime? desde_675MS = chkDesde_675MS.Checked ? dtpDesde_675MS.Value.Date : (DateTime?)null;
                DateTime? hasta_675MS = chkHasta_675MS.Checked ? dtpHasta_675MS.Value.Date : (DateTime?)null;

                DataTable tabla_675MS = bitacoraBLL_675MS.Consultar_675MS(usuario_675MS, modulo_675MS, desde_675MS, hasta_675MS);
                dgvBitacora_675MS.DataSource = tabla_675MS;

                if (dgvBitacora_675MS.Columns.Contains("IdBitacora")) dgvBitacora_675MS.Columns["IdBitacora"].Visible = false;
                if (dgvBitacora_675MS.Columns.Contains("Fecha")) dgvBitacora_675MS.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar la bitácora: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLimpiar_675MS_Click_1(object sender, EventArgs e)
        {
            txtUsuario_675MS.Clear();
            txtModulo_675MS.Clear();
            chkDesde_675MS.Checked = false;
            chkHasta_675MS.Checked = false;
            dtpDesde_675MS.Value = DateTime.Today;
            dtpHasta_675MS.Value = DateTime.Today;
            ConsultarBitacora_675MS();
        }
    }
}
