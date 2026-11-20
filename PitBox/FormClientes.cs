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
    public partial class FormClientes : Form,IObserverIdioma
    {
        //Manejo de Idiomas
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        // Instancio a la BLL
        ClienteBLL clienteBLL_675MS = new ClienteBLL();
        public FormClientes()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        //Metodos de manejo de idioma
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
                    case "lblGestorClientes_675MS":
                        lblGestorClientes_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblMail_675MS":
                        lblMail_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblNombre_675MS":
                        lblNombre_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblTelefono_675MS":
                        lblTelefono_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnDeserializacion_675MS":
                        btnDeserializacion_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnModificar_675MS":
                        btnModificar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnSerializacion_675MS":
                        btnSerializacion_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "bntRegistrar_675MS":
                        bntRegistrar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "groupBox1":
                        groupBox1.Text = traduccion.Traduccion_675MS;
                        break;

                }
            }
        }

        private void ActualizarPantalla_675MS()
        {
            // Conservo el DNI seleccionado, si existe
            int dniSeleccionado_675MS = 0;

            if (dgvClientes_675MS.CurrentRow != null)
            {
                ClienteBE seleccionado_675MS = dgvClientes_675MS.CurrentRow.DataBoundItem as ClienteBE;

                if (seleccionado_675MS != null)
                    dniSeleccionado_675MS = seleccionado_675MS.Dni_675MS;
            }

            // Recupero los clientes desde la BLL
            List<ClienteBE> clientes_675MS = clienteBLL_675MS.ListarClientes_675MS();

            dgvClientes_675MS.DataSource = null;
            dgvClientes_675MS.Columns.Clear();
            dgvClientes_675MS.AutoGenerateColumns = true;
            dgvClientes_675MS.AllowUserToAddRows = false;
            dgvClientes_675MS.DataSource = clientes_675MS;
            dgvClientes_675MS.Columns["Activo_675MS"].Visible = false;

            DataGridViewTextBoxColumn columnaEstado_675MS = new DataGridViewTextBoxColumn();

            columnaEstado_675MS.Name = "EstadoCliente_675MS";
            columnaEstado_675MS.HeaderText = "Activo";
            columnaEstado_675MS.DataPropertyName = "Activo_675MS";
            columnaEstado_675MS.ReadOnly = true;

            dgvClientes_675MS.Columns.Add(columnaEstado_675MS);

            dgvClientes_675MS.CellFormatting -= dgvClientes_675MS_CellFormatting;
            dgvClientes_675MS.CellFormatting += dgvClientes_675MS_CellFormatting;

            // La ubico al final
            columnaEstado_675MS.DisplayIndex = dgvClientes_675MS.Columns.Count - 1;

            // Encabezados
            dgvClientes_675MS.Columns["Dni_675MS"].HeaderText = "DNI";
            dgvClientes_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre";
            dgvClientes_675MS.Columns["Apellido_675MS"].HeaderText = "Apellido";
            dgvClientes_675MS.Columns["FechaNacimiento_675MS"].HeaderText = "Fecha de Nacimiento";
            dgvClientes_675MS.Columns["Mail_675MS"].HeaderText = "Correo Electrónico";
            dgvClientes_675MS.Columns["Domicilio_675MS"].HeaderText = "Domicilio";
            dgvClientes_675MS.Columns["Telefono_675MS"].HeaderText = "Teléfono";
            dgvClientes_675MS.Columns["DigitoVerificadorHorizontal_675MS"].Visible = false;

            // Restablezco la selección
            dgvClientes_675MS.ClearSelection();
            dgvClientes_675MS.CurrentCell = null;

            btnBaja_675MS.Enabled = false;
            btnReactivar_675MS.Enabled = false;

            foreach (DataGridViewRow fila_675MS in dgvClientes_675MS.Rows)
            {
                ClienteBE cliente_675MS =
                    fila_675MS.DataBoundItem as ClienteBE;

                if (cliente_675MS != null &&
                    cliente_675MS.Dni_675MS == dniSeleccionado_675MS)
                {
                    dgvClientes_675MS.CurrentCell = fila_675MS.Cells["Dni_675MS"];

                    fila_675MS.Selected = true;

                    btnBaja_675MS.Enabled = cliente_675MS.Activo_675MS;
                    btnReactivar_675MS.Enabled = !cliente_675MS.Activo_675MS;

                    break;
                }
            }
        }

        //Eventos del form y controles de usuario
        private void FormClientes_Load(object sender, EventArgs e)
        {
            {
                // Le saco los bordes y la barra de título superior
                this.FormBorderStyle = FormBorderStyle.None;

                // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
                this.Dock = DockStyle.Fill;

                ActualizarPantalla_675MS();

                //Suscribo al evento del observer
                TratamientoIdioma.Instancia.Suscribir(this);
                //Cargo combo con los idiomas del sistema
                CargarComboIdiomas_675MS();

            }
        }

        private void dgvClientes_675MS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvClientes_675MS.Columns[e.ColumnIndex].Name == "EstadoCliente_675MS"
                && e.Value != null
                && e.Value != DBNull.Value)
            {
                bool activo_675MS = Convert.ToBoolean(e.Value);

                e.Value = activo_675MS ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
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
            btnBaja_675MS.Enabled = false;
            btnReactivar_675MS.Enabled = false;

            if (dgvClientes_675MS.CurrentRow == null)
                return;

            ClienteBE clienteSeleccionado_675MS =  dgvClientes_675MS.CurrentRow.DataBoundItem as ClienteBE;

            if (clienteSeleccionado_675MS == null)
                return;

            // Cargo los datos del cliente seleccionado
            txtDNI_675MS.Text = clienteSeleccionado_675MS.Dni_675MS.ToString();
            txtApellido_675MS.Text = clienteSeleccionado_675MS.Apellido_675MS;
            txtNombre_675MS.Text = clienteSeleccionado_675MS.Nombre_675MS;
            txtMail_675MS.Text = clienteSeleccionado_675MS.Mail_675MS;
            txtTelefono_675MS.Text = clienteSeleccionado_675MS.Telefono_675MS.ToString();
            txtDomicilio_675MS.Text = clienteSeleccionado_675MS.Domicilio_675MS;
            dateTimePicker1.Value = clienteSeleccionado_675MS.FechaNacimiento_675MS;

            // Habilito el botón correspondiente al estado del cliente
            btnBaja_675MS.Enabled = clienteSeleccionado_675MS.Activo_675MS;
            btnReactivar_675MS.Enabled = !clienteSeleccionado_675MS.Activo_675MS;
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

            // Valido DNI
            bool dniExiste_675MS = clienteBLL_675MS.VerificarExisteDni_675MS(dniValidado_675MS);

            if (dniExiste_675MS)
            {
                MessageBox.Show("Ya existe un cliente registrado con el DNI ingresado.", "DNI Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //Valido Mail
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtMail_675MS.Text.Trim(), @"^[^@\s]+@[^@\s\.]+(\.[^@\s\.]+)+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Mail inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ActualizarPantalla_675MS();
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

            //Valido Mail
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtMail_675MS.Text.Trim(), @"^[^@\s]+@[^@\s\.]+(\.[^@\s\.]+)+$"))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Mail inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ActualizarPantalla_675MS();
                MessageBox.Show("El cliente se ha modificado de manera correcta.");
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

        private void btnBaja_675MS_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDNI_675MS.Text, out int dni_675MS))
            {
                MessageBox.Show("Seleccioná un cliente válido.");
                return;
            }

            DialogResult respuesta_675MS = MessageBox.Show("¿Confirmás que querés dar de baja al cliente?","Baja de cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuesta_675MS != DialogResult.Yes)
                return;

            try
            {
                ClienteBLL clienteBLL_675MS = new ClienteBLL();

                int resultado_675MS = clienteBLL_675MS.BajaCliente_675MS(dni_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Cliente dado de baja correctamente.");
                    btnBaja_675MS.Enabled = false;

                    ActualizarPantalla_675MS();

                }
                else
                {
                    MessageBox.Show("El cliente no existe o ya está dado de baja.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReactivar_675MS_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDNI_675MS.Text, out int dni_675MS))
            {
                MessageBox.Show("Seleccioná un cliente válido.");
                return;
            }

            DialogResult respuesta_675MS = MessageBox.Show("¿Confirmás que querés reactivar al cliente?", "Reactivar cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta_675MS != DialogResult.Yes)
                return;

            try
            {
                ClienteBLL clienteBLL_675MS = new ClienteBLL();

                int resultado_675MS = clienteBLL_675MS.ReactivarCliente_675MS(dni_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Cliente reactivado correctamente.");
                    btnReactivar_675MS.Enabled = false;
                    ActualizarPantalla_675MS();
                }
                else
                {
                    MessageBox.Show("No se pudo reactivar el cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
