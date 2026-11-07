using BE;
using BLL;
using Servicios.Observer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PitBox
{
    public partial class FormIdiomas : Form, IObserverIdioma
    {
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS;
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        int id = 0;

        public FormIdiomas()
        {
            InitializeComponent();
            //Agrego fondo de pantalla
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void FormIdiomas_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            TratamientoIdioma.Instancia.Suscribir(this);

            ActualizarPantalla_675MS();

            CargarComboIdiomas_675MS();
        }



        private void ActualizarPantalla_675MS()
        {
            ActualizarIdiomas();

            if (dgvIdiomas_675MS.Rows.Count > 0)
            {
                dgvIdiomas_675MS.Rows[0].Selected = true;

                id = Convert.ToInt32(dgvIdiomas_675MS.Rows[0].Cells["Id_675MS"].Value);

                ActualizarTraducciones();
            }
            else
            {
                dgvtraducciones_675MS.DataSource = null;
            }
        }

        //Actualizo grilla idiomas
        private void ActualizarIdiomas()
        {
            idiomaBLL_675MS = new IdiomaBLL();

            idiomas_675MS = idiomaBLL_675MS.ListarIdiomas_675MS();

            dgvIdiomas_675MS.DataSource = null;
            dgvIdiomas_675MS.AutoGenerateColumns = true;
            dgvIdiomas_675MS.DataSource = idiomas_675MS;

            if (dgvIdiomas_675MS.Columns["Id_675MS"] != null)
                dgvIdiomas_675MS.Columns["Nombre_675MS"].HeaderText = "Id de Idioma";

            if (dgvIdiomas_675MS.Columns["Nombre_675MS"] != null)
                dgvIdiomas_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre de Idioma";
        }

        //Actualizo grilla traducciones
        private void ActualizarTraducciones()
        {
            TraduccionBLL traduccionBLL_675MS = new TraduccionBLL();

            traducciones_675MS = traduccionBLL_675MS.Listar_675MS(id);

            dgvtraducciones_675MS.DataSource = null;
            dgvtraducciones_675MS.AutoGenerateColumns = true;
            dgvtraducciones_675MS.DataSource = traducciones_675MS;

            if (dgvtraducciones_675MS.Columns["Id_675MS"] != null)
                dgvtraducciones_675MS.Columns["Id_675MS"].Visible = false;

            if (dgvtraducciones_675MS.Columns["IdIdioma_675MS"] != null)
                dgvtraducciones_675MS.Columns["IdIdioma_675MS"].Visible = false;

            if (dgvtraducciones_675MS.Columns["Idioma_675MS"] != null)
                dgvtraducciones_675MS.Columns["Idioma_675MS"].Visible = false;

            if (dgvtraducciones_675MS.Columns["NombreControl_675MS"] != null)
                dgvtraducciones_675MS.Columns["NombreControl_675MS"].HeaderText = "Nombre de Control";

            if (dgvtraducciones_675MS.Columns["Traduccion_675MS"] != null)
                dgvtraducciones_675MS.Columns["Traduccion_675MS"].HeaderText = "Traducción";
        }

        private void dgvIdiomas_675MS_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvIdiomas_675MS.CurrentRow != null &&
                dgvIdiomas_675MS.CurrentRow.Cells["Id_675MS"].Value != null)
            {
                id = Convert.ToInt32(dgvIdiomas_675MS.CurrentRow.Cells["Id_675MS"].Value);

                ActualizarTraducciones();
            }
        }
        //Observer
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
                    case "lblNombreControl_675MS":
                        lblNombreControl_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblTraduccion":
                        lblTraduccion.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblNombreIdioma_675MS":
                        lblNombreIdioma_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "bntRegistrar_675MS":
                        bntRegistrar_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "btnModificar_675MS":
                        btnModificar_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "btnBorrar_675MS":
                        btnBorrar_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblIdioma675MS":
                        lblIdioma675MS.Text = traduccion.Traduccion_675MS;
                        break;
                }
            }
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void dgvIdiomas_675MS_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvIdiomas_675MS.CurrentRow == null || dgvIdiomas_675MS.CurrentRow.IsNewRow)
                return;

            if (dgvIdiomas_675MS.CurrentRow.Cells["Id_675MS"].Value == null)
                return;

            id = Convert.ToInt32(dgvIdiomas_675MS.CurrentRow.Cells["Id_675MS"].Value            );

            txtNombreIdioma_675MS.Text = dgvIdiomas_675MS.CurrentRow.Cells["Nombre_675MS"].Value.ToString();

            ActualizarTraducciones();
        }

        private void dgvtraducciones_675MS_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvtraducciones_675MS.CurrentRow == null || dgvtraducciones_675MS.CurrentRow.IsNewRow)
                return;

            if (dgvtraducciones_675MS.CurrentRow.Cells["Id_675MS"].Value == null)
                return;

            txtNombreControl_675MS.Text = dgvtraducciones_675MS.CurrentRow.Cells["NombreControl_675MS"].Value.ToString();

            txtTraduccion_675MS.Text =  dgvtraducciones_675MS.CurrentRow.Cells["Traduccion_675MS"].Value.ToString();

            
        }

        //Cambio el nombre por btnRegistrarIdioma_675MS
        private void button3_Click(object sender, EventArgs e)
        {

            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtNombreIdioma_675MS.Text))
            {
                MessageBox.Show("Por favor, completa el campo Nombre Idioma.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nuevoIdioma_675MS;
            nuevoIdioma_675MS = txtNombreIdioma_675MS.Text.Trim();


            idiomaBLL_675MS = new IdiomaBLL();
            int resultado_675MS = idiomaBLL_675MS.AltaIdioma_675MS(nuevoIdioma_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en el alta del idioma.");
            }
            else if (resultado_675MS > 0)
            {
                ActualizarPantalla_675MS();
                MessageBox.Show("El idioma se ha registrado de manera correcta.");
            }
        }

        //Cambio el nombre por btnModificarIdioma_675MS
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvIdiomas_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un idioma de la grilla para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtNombreIdioma_675MS.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IdiomaBE idiomaModificado_675MS = new IdiomaBE();
            idiomaModificado_675MS.Nombre_675MS = txtNombreIdioma_675MS.Text.Trim();
            idiomaModificado_675MS.Id_675MS = Convert.ToInt32(dgvIdiomas_675MS.CurrentRow.Cells["Id_675MS"].Value);


            idiomaBLL_675MS = new IdiomaBLL();
            int resultado_675MS = idiomaBLL_675MS.ModificarIdioma_675MS(idiomaModificado_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la modificación del idioma.");
            }
            else if (resultado_675MS > 0 )
            {
                ActualizarPantalla_675MS();
                MessageBox.Show("El idioma se ha modificado de manera correcta.");
            }

        }
    


        //Cambio el nombre por btnBorrarIdioma_675MS
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvIdiomas_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un idioma de la grilla para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtNombreIdioma_675MS.Text))
            {
                MessageBox.Show("Por favor, seleccione un idioma a eliminar.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado_675MS = MessageBox.Show("¿Estás seguro? Si borrás este idioma se eliminarán todas las traducciones cargadas para el mismo.",
                "Advertencia",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (resultado_675MS == DialogResult.Yes)
            {
                int idiomaEliminar_675MS = 0;
                idiomaEliminar_675MS = Convert.ToInt32(dgvIdiomas_675MS.CurrentRow.Cells["Id_675MS"].Value);


                idiomaBLL_675MS = new IdiomaBLL();
                int resultadoBorrar_675MS = idiomaBLL_675MS.BorrarIdioma_675MS(idiomaEliminar_675MS);

                if (resultadoBorrar_675MS == -1)
                {
                    MessageBox.Show("Error en el borrado del idioma.");
                }
                else if (resultadoBorrar_675MS > 0)
                {
                    ActualizarPantalla_675MS();
                    MessageBox.Show("El idioma se ha eliminado de manera correcta.");
                }

            }


        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtNombreControl_675MS.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre de control válido.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtTraduccion_675MS.Text))
            {
                MessageBox.Show("Por favor, ingrese una traducción válida.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TraduccionBE nuevaTraduccion_675MS = new TraduccionBE();
            nuevaTraduccion_675MS.NombreControl_675MS = txtNombreControl_675MS.Text.Trim();
            nuevaTraduccion_675MS.Traduccion_675MS = txtTraduccion_675MS.Text.Trim();
            nuevaTraduccion_675MS.IdIdioma_675MS = Convert.ToInt32(dgvIdiomas_675MS.CurrentRow.Cells["Id_675MS"].Value);

            TraduccionBLL traduccionBLL_675MS = new TraduccionBLL();
            int resultado_675MS = traduccionBLL_675MS.AltaTraduccion_675MS(nuevaTraduccion_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en el alta de la traducción.");
            }
            else if (resultado_675MS > 0)
            {
                ActualizarPantalla_675MS();
                MessageBox.Show("La traducción se ha registrado de manera correcta.");
            }
        }

        private void btnModificar_675MS_Click(object sender, EventArgs e)
        {
            if (dgvtraducciones_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una traducción de la grilla para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtNombreControl_675MS.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre de control válido.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtTraduccion_675MS.Text))
            {
                MessageBox.Show("Por favor, ingrese una traducción válida.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TraduccionBE traduccionModificada_675MS = new TraduccionBE();
            traduccionModificada_675MS.NombreControl_675MS = txtNombreControl_675MS.Text.Trim();
            traduccionModificada_675MS.Traduccion_675MS = txtTraduccion_675MS.Text.Trim();
            traduccionModificada_675MS.Id_675MS = Convert.ToInt32(dgvIdiomas_675MS.CurrentRow.Cells["Id_675MS"].Value);

            TraduccionBLL traduccionBLL_675MS = new TraduccionBLL();
            int resultado_675MS = traduccionBLL_675MS.ModificarTraduccion_675MS(traduccionModificada_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la modificación de la traducción.");
            }
            else if (resultado_675MS > 0)
            {
                ActualizarPantalla_675MS();
                MessageBox.Show("La traducción se ha modificado de manera correcta.");
            }


        }

        private void btnBorrar_675MS_Click(object sender, EventArgs e)
        {
            if (dgvtraducciones_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una traducción de la grilla para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvtraducciones_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una traducción de la grilla para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int traduccionEliminar_675MS = 0;
            traduccionEliminar_675MS = Convert.ToInt32(dgvtraducciones_675MS.CurrentRow.Cells["Id_675MS"].Value);

            TraduccionBLL traduccionBLL_675MS = new TraduccionBLL();
           
            int resultadoBorrar_675MS = traduccionBLL_675MS.BorrarTraduccion_675MS(traduccionEliminar_675MS);

            if (resultadoBorrar_675MS == -1)
            {
                MessageBox.Show("Error en el borrado de la traducción.");
            }
            else if (resultadoBorrar_675MS > 0)
            {
                ActualizarPantalla_675MS();
                MessageBox.Show("La traducción se ha eliminado de manera correcta.");
            }
            
        }
    }
}