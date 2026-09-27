using BE;
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
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
        }

        // Instancio a la BLL
        ClienteBLL clienteBLL_675MS = new ClienteBLL();
        private void FormClientes_Load(object sender, EventArgs e)
        {
            {
                // Le saco los bordes y la barra de título superior
                this.FormBorderStyle = FormBorderStyle.None;

                // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
                this.Dock = DockStyle.Fill;

                ActualizarPantalla();

            }
        }

        private void ActualizarPantalla()
        {

            // Traigo la lista de clientes desde la base de datos
            List<ClienteBE> clientes_675MS = clienteBLL_675MS.ListarClientes_675MS();

            // Asigno la lista al DataGridView
            dgvClientes_675MS.DataSource = null;
            dgvClientes_675MS.DataSource = clientes_675MS;

            // Oculto columnas
            dgvClientes_675MS.Columns["DigitoVerificadorHorizontal_675MS"].Visible = false;

            // Cambio titulos de columnas
            dgvClientes_675MS.Columns["Dni_675MS"].HeaderText = "DNI";
            dgvClientes_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre";
            dgvClientes_675MS.Columns["Apellido_675MS"].HeaderText = "Apellido";
            dgvClientes_675MS.Columns["FechaNacimiento_675MS"].HeaderText = "Fecha de Nacimiento";
            dgvClientes_675MS.Columns["Mail_675MS"].HeaderText = "Correo Electrónico";
            dgvClientes_675MS.Columns["Domicilio_675MS"].HeaderText = "Domicilio";
            dgvClientes_675MS.Columns["Telefono_675MS"].HeaderText = "Teléfono";
        }

        private void btnSerializacion_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSerializacion_675MS.Text))
                {
                    MessageBox.Show("Por favor, seleccione una ubicación y un nombre de archivo primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Recupero la lista directamente desde la grilla (DataGridView)
                List<ClienteBE> clientes_675MS = dgvClientes_675MS.DataSource as List<ClienteBE>;

                if (clientes_675MS == null || clientes_675MS.Count == 0)
                {
                    MessageBox.Show("No hay datos en la grilla para serializar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Invoco al método de la BLL
                clienteBLL_675MS.GuardarClientesXML_675MS(clientes_675MS, txtSerializacion_675MS.Text);

                MessageBox.Show("Clientes serializados en formato XML con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Error al serializar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCarpeta1_675MS_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog_675MS = new SaveFileDialog();
            saveFileDialog_675MS.Filter = "XML Files|*.xml";
            saveFileDialog_675MS.Title = "Guardar archivo XML de Clientes";

            if (saveFileDialog_675MS.ShowDialog() == DialogResult.OK)
            {
                // Mostramos la ruta seleccionada en el TextBox correspondiente
                txtSerializacion_675MS.Text = saveFileDialog_675MS.FileName;
            }
        }

        private void btnDeserializacion_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDeserializacion_675MS.Text))
                {
                    MessageBox.Show("Por favor, seleccione un archivo XML primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Invoco a la BLL para que lea el archivo y devuelva la lista
                List<ClienteBE> listaClientes = clienteBLL_675MS.CargarClientesXML_675MS(txtDeserializacion_675MS.Text);

                // Subo los datos directamente a la matriz de datos de la pantalla (grilla)
                dgvClientes_675MS.DataSource = null;
                dgvClientes_675MS.DataSource = listaClientes;

                MessageBox.Show("Archivo XML des-serializado y cargado en la grilla con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al deserializar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCarpeta2_675MS_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog_675MS = new OpenFileDialog();
            openFileDialog_675MS.Filter = "XML Files|*.xml";
            openFileDialog_675MS.Title = "Seleccionar archivo XML de Clientes";

            if (openFileDialog_675MS.ShowDialog() == DialogResult.OK)
            {
                // Mostramos la ruta seleccionada en el TextBox correspondiente
                txtDeserializacion_675MS.Text = openFileDialog_675MS.FileName;
            }
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvClientes_675MS_SelectionChanged(object sender, EventArgs e)
        {
            ClienteBE clienteSeleccionado = new ClienteBE();

            if (dgvClientes_675MS.CurrentRow != null)
            {
                // Recupero los valores del registro seleccionado en el Data Grid View de Clientes
                clienteSeleccionado.Dni_675MS = Convert.ToInt32(dgvClientes_675MS.CurrentRow.Cells["Dni_675MS"].Value);
                clienteSeleccionado.Nombre_675MS = dgvClientes_675MS.CurrentRow.Cells["Nombre_675MS"].Value.ToString();
                clienteSeleccionado.Apellido_675MS = dgvClientes_675MS.CurrentRow.Cells["Apellido_675MS"].Value.ToString();
                clienteSeleccionado.FechaNacimiento_675MS = Convert.ToDateTime(dgvClientes_675MS.CurrentRow.Cells["FechaNacimiento_675MS"].Value);
                clienteSeleccionado.Mail_675MS = dgvClientes_675MS.CurrentRow.Cells["Mail_675MS"].Value.ToString();
                clienteSeleccionado.Domicilio_675MS = dgvClientes_675MS.CurrentRow.Cells["Domicilio_675MS"].Value.ToString();
                clienteSeleccionado.Telefono_675MS = Convert.ToInt32(dgvClientes_675MS.CurrentRow.Cells["Telefono_675MS"].Value);
                clienteSeleccionado.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(dgvClientes_675MS.CurrentRow.Cells["DigitoVerificadorHorizontal_675MS"].Value);

                // Vuelco los datos en los TextBox del panel izquierdo de la interfaz
                txtDNI_675MS.Text = clienteSeleccionado.Dni_675MS.ToString();
                txtApellido_675MS.Text = clienteSeleccionado.Apellido_675MS;
                txtNombre_675MS.Text = clienteSeleccionado.Nombre_675MS;
                txtMail_675MS.Text = clienteSeleccionado.Mail_675MS;
                txtTelefono_675MS.Text = clienteSeleccionado.Telefono_675MS.ToString();
                txtDomicilio_675MS.Text = clienteSeleccionado.Domicilio_675MS;
                dateTimePicker1.Value = clienteSeleccionado.FechaNacimiento_675MS;
            }
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
            ClienteBE nuevoCliente_675MS = new ClienteBE();

            nuevoCliente_675MS.Dni_675MS = dniValidado_675MS;
            nuevoCliente_675MS.Nombre_675MS = txtNombre_675MS.Text.Trim();
            nuevoCliente_675MS.Apellido_675MS = txtApellido_675MS.Text.Trim();
            nuevoCliente_675MS.FechaNacimiento_675MS = dateTimePicker1.Value.Date;
            nuevoCliente_675MS.Mail_675MS = txtMail_675MS.Text.Trim();
            nuevoCliente_675MS.Domicilio_675MS = txtDomicilio_675MS.Text.Trim();
            nuevoCliente_675MS.Telefono_675MS = telefonoValidado_675MS;


            int resultado_675MS = clienteBLL_675MS.RegistrarCliente_675MS(nuevoCliente_675MS);

            if (resultado_675MS > 0)
            {
                MessageBox.Show("El cliente " + nuevoCliente_675MS.Nombre_675MS + " se registró correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarPantalla();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar registrar el cliente en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_675MS_Click(object sender, EventArgs e)
        {
            if (dgvClientes_675MS.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un cliente de la grilla para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            ClienteBE clienteModificado_675MS = new ClienteBE();
            clienteModificado_675MS.Dni_675MS = dniValidado_675MS;
            clienteModificado_675MS.Nombre_675MS = txtNombre_675MS.Text.Trim();
            clienteModificado_675MS.Apellido_675MS = txtApellido_675MS.Text.Trim();
            clienteModificado_675MS.FechaNacimiento_675MS = dateTimePicker1.Value.Date;
            clienteModificado_675MS.Mail_675MS = txtMail_675MS.Text.Trim();
            clienteModificado_675MS.Domicilio_675MS = txtDomicilio_675MS.Text.Trim();
            clienteModificado_675MS.Telefono_675MS = telefonoValidado_675MS;

            ClienteBLL clienteBLL_675MS = new ClienteBLL();
            int resultado_675MS = clienteBLL_675MS.ModificarCliente_675MS(clienteModificado_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la modificación del cliente.");
            }
            else if (resultado_675MS > 0 || resultado_675MS == 1)
            {
                ActualizarPantalla();
                MessageBox.Show("El cliente se ha modificado de manera correcta.");
            }
        }
    }
}
