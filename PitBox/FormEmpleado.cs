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
    public partial class FormEmpleado : Form, IObserverIdioma
    {
        public FormEmpleado()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        // Instancio a la BLL
        EmpleadoBLL empleadoBLL_675MS = new EmpleadoBLL();
        //Manejo de Idiomas
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        private void lblDNI_675MS_Click(object sender, EventArgs e)
        {

        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtDNI_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtNombre_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtApellido_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtMail_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtDomicilio_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono_675MS.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valido que el DNI sea un número entero positivo
            if (!int.TryParse(txtDNI_675MS.Text, out int dniValidado_675MS) || dniValidado_675MS <= 0)
            {
                MessageBox.Show("El DNI ingresado no es válido. Ingrese solo números.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Valido que el teléfono sea un número entero positivo
            if (!int.TryParse(txtTelefono_675MS.Text, out int telefonoValidado_675MS) || telefonoValidado_675MS <= 0)
            {
                MessageBox.Show("El teléfono debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Instancio el objeto ClienteBE para registrar
            EmpleadoBE nuevoEmpleado_675MS = new EmpleadoBE();

            nuevoEmpleado_675MS.DniEmpleado_675MS = dniValidado_675MS;
            nuevoEmpleado_675MS.Nombre_675MS = txtNombre_675MS.Text.Trim();
            nuevoEmpleado_675MS.Apellido_675MS = txtApellido_675MS.Text.Trim();
            nuevoEmpleado_675MS.FechaNacimiento_675MS = dateTimePicker1.Value.Date;
            nuevoEmpleado_675MS.Mail_675MS = txtMail_675MS.Text.Trim();
            nuevoEmpleado_675MS.Domicilio_675MS = txtDomicilio_675MS.Text.Trim();
            nuevoEmpleado_675MS.Telefono_675MS = telefonoValidado_675MS;


            int resultado_675MS = empleadoBLL_675MS.RegistrarEmpleado_675MS(nuevoEmpleado_675MS);

            if (resultado_675MS > 0)
            {
                MessageBox.Show("El cliente " + nuevoEmpleado_675MS.Nombre_675MS + " se registró correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarPantalla_675MS();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar registrar el cliente en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarPantalla_675MS()
        {
            List<EmpleadoBE> empleados_675MS = empleadoBLL_675MS.ListarEmpleados_675MS();

            // Asigno la lista al DataGridView
            dgvEmpleados_675MS.DataSource = null;
            dgvEmpleados_675MS.DataSource = empleados_675MS;

            // Oculto columnas
            dgvEmpleados_675MS.Columns["DigitoVerificadorHorizontal_675MS"].Visible = false;
            dgvEmpleados_675MS.Columns["IdEmpleado_675MS"].Visible = false;


            // Cambio titulos de columnas
            dgvEmpleados_675MS.Columns["DniEmpleado_675MS"].HeaderText = "DNI";
            dgvEmpleados_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre";
            dgvEmpleados_675MS.Columns["Apellido_675MS"].HeaderText = "Apellido";
            dgvEmpleados_675MS.Columns["FechaNacimiento_675MS"].HeaderText = "Fecha de Nacimiento";
            dgvEmpleados_675MS.Columns["Mail_675MS"].HeaderText = "Correo Electrónico";
            dgvEmpleados_675MS.Columns["Domicilio_675MS"].HeaderText = "Domicilio";
            dgvEmpleados_675MS.Columns["Telefono_675MS"].HeaderText = "Teléfono";
            dgvEmpleados_675MS.Columns["Baja_675MS"].HeaderText = "Baja";

        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {
            // Le saco los bordes y la barra de título superior
            this.FormBorderStyle = FormBorderStyle.None;

            // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
            this.Dock = DockStyle.Fill;

            //Suscribo al evento CellFormatting para mostrar el campo BAJA con el estado correspondiente
            dgvEmpleados_675MS.CellFormatting += dgvEmpleados_675MS_CellFormatting;

            //Suscribo al evento del observer
            TratamientoIdioma.Instancia.Suscribir(this);
            //Cargo combo con los idiomas del sistema
            CargarComboIdiomas_675MS();

            ActualizarPantalla_675MS();
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
                    case "lblApellido_675MS":
                        lblApellido_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblDNI_675MS":
                        lblDNI_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblDomicilio_675MS":
                        lblDomicilio_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblFechadeNacimiento_675MS":
                        lblFechadeNacimiento_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblGestionEmpleados_675MS":
                        lblGestionEmpleados_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblNombre_675MS":
                        lblNombre_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblMail_675MS":
                        lblMail_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblTelefono_675MS":
                        lblTelefono_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "bntRegistrar_675MS":
                        bntRegistrar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnBaja_675MS":
                        btnBaja_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnModificar_675MS":
                        btnModificar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnReactivar_675MS":
                        btnReactivar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                }
            }
        }


        private void dgvEmpleados_675MS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Nos aseguramos de que sea la columna de Baja y que no sea la cabecera
            if (e.ColumnIndex >= 0 && dgvEmpleados_675MS.Columns[e.ColumnIndex].Name == "Baja_675MS" && e.Value != null)
            {
                int valorBaja = Convert.ToInt32(e.Value);

                if (valorBaja == 0)
                {
                    e.Value = "Activo";
                    e.FormattingApplied = true;
                }
                else if (valorBaja == 1)
                {
                    e.Value = "Inactivo";
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_675MS_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un empleado de la grilla para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtDNI_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtNombre_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtApellido_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtMail_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtDomicilio_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono_675MS.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDNI_675MS.Text, out int dniValidado_675MS) || dniValidado_675MS <= 0)
            {
                MessageBox.Show("El DNI ingresado no es válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtTelefono_675MS.Text, out int telefonoValidado_675MS) || telefonoValidado_675MS <= 0)
            {
                MessageBox.Show("El teléfono debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EmpleadoBE empleadoModificado_675MS = new EmpleadoBE();
            empleadoModificado_675MS.IdEmpleado_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["IdEmpleado_675MS"].Value);
            empleadoModificado_675MS.DniEmpleado_675MS = dniValidado_675MS;
            empleadoModificado_675MS.Nombre_675MS = txtNombre_675MS.Text.Trim();
            empleadoModificado_675MS.Apellido_675MS = txtApellido_675MS.Text.Trim();
            empleadoModificado_675MS.FechaNacimiento_675MS = dateTimePicker1.Value.Date;
            empleadoModificado_675MS.Mail_675MS = txtMail_675MS.Text.Trim();
            empleadoModificado_675MS.Domicilio_675MS = txtDomicilio_675MS.Text.Trim();
            empleadoModificado_675MS.Telefono_675MS = telefonoValidado_675MS;
            empleadoModificado_675MS.Baja_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["Baja_675MS"].Value); 

            empleadoBLL_675MS = new EmpleadoBLL();
            int resultado_675MS = empleadoBLL_675MS.ModificarEmpleado_675MS(empleadoModificado_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la modificación del empleado.");
            }
            else if (resultado_675MS > 0 || resultado_675MS == 1)
            {
                ActualizarPantalla_675MS();
                MessageBox.Show("El empleado se ha modificado de manera correcta.");
            }
        }

        private void dgvEmpleados_675MS_SelectionChanged(object sender, EventArgs e)
        {
            EmpleadoBE empleadoSeleccionado_675MS = new EmpleadoBE();

            if (dgvEmpleados_675MS.CurrentRow != null)
            {
                // Recupero los valores del registro seleccionado en el Data Grid View de Clientes
                empleadoSeleccionado_675MS.DniEmpleado_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["DniEmpleado_675MS"].Value);
                empleadoSeleccionado_675MS.Nombre_675MS = dgvEmpleados_675MS.CurrentRow.Cells["Nombre_675MS"].Value.ToString();
                empleadoSeleccionado_675MS.Apellido_675MS = dgvEmpleados_675MS.CurrentRow.Cells["Apellido_675MS"].Value.ToString();
                empleadoSeleccionado_675MS.FechaNacimiento_675MS = Convert.ToDateTime(dgvEmpleados_675MS.CurrentRow.Cells["FechaNacimiento_675MS"].Value);
                empleadoSeleccionado_675MS.Mail_675MS = dgvEmpleados_675MS.CurrentRow.Cells["Mail_675MS"].Value.ToString();
                empleadoSeleccionado_675MS.Domicilio_675MS = dgvEmpleados_675MS.CurrentRow.Cells["Domicilio_675MS"].Value.ToString();
                empleadoSeleccionado_675MS.Telefono_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["Telefono_675MS"].Value);
                empleadoSeleccionado_675MS.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["DigitoVerificadorHorizontal_675MS"].Value);
                empleadoSeleccionado_675MS.Baja_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["Baja_675MS"].Value);
                empleadoSeleccionado_675MS.IdEmpleado_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["IdEmpleado_675MS"].Value);

                // Vuelco los datos en los TextBox del panel izquierdo de la interfaz
                txtDNI_675MS.Text = empleadoSeleccionado_675MS.DniEmpleado_675MS.ToString();
                txtApellido_675MS.Text = empleadoSeleccionado_675MS.Apellido_675MS;
                txtNombre_675MS.Text = empleadoSeleccionado_675MS.Nombre_675MS;
                txtMail_675MS.Text = empleadoSeleccionado_675MS.Mail_675MS;
                txtTelefono_675MS.Text = empleadoSeleccionado_675MS.Telefono_675MS.ToString();
                txtDomicilio_675MS.Text = empleadoSeleccionado_675MS.Domicilio_675MS;
                dateTimePicker1.Value = empleadoSeleccionado_675MS.FechaNacimiento_675MS;
            }

        }

        private void btnBaja_675MS_Click(object sender, EventArgs e)
        {
            // Validamos que realmente haya una fila seleccionada en la grilla
            if (dgvEmpleados_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un empleado de la grilla para inactivar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEmpleadoBaja_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["IdEmpleado_675MS"].Value);
            empleadoBLL_675MS = new EmpleadoBLL();
            empleadoBLL_675MS = new EmpleadoBLL();

            int resultado_675MS = empleadoBLL_675MS.BajaEmpleado_675MS(idEmpleadoBaja_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la baja del empleado.");
            }
            else if (resultado_675MS > 0 || resultado_675MS == 1)
            {
                ActualizarPantalla_675MS();
                MessageBox.Show("El empleado se ha dado de baja de manera correcta.");
            }
        }

        private void btnReactivar_675MS_Click(object sender, EventArgs e)
        {
            // Validamos que realmente haya una fila seleccionada en la grilla
            if (dgvEmpleados_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un empleado de la grilla para reactivar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEmpleadoReactivar_675MS = Convert.ToInt32(dgvEmpleados_675MS.CurrentRow.Cells["IdEmpleado_675MS"].Value);
            
            empleadoBLL_675MS = new EmpleadoBLL();
            int resultado_675MS = empleadoBLL_675MS.ReactivarEmpleado_675MS(idEmpleadoReactivar_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la reactivación del empleado.");
            }
            else if (resultado_675MS > 0 || resultado_675MS == 1)
            {
                ActualizarPantalla_675MS();
                MessageBox.Show("El empleado se ha reactivado de manera correcta.");
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
