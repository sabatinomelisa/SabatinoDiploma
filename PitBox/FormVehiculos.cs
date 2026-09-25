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
    public partial class FormVehiculos : Form
    {
        public FormVehiculos()
        {
            InitializeComponent();
        }
        List<VehiculoBE> vehiculos_675MS = new List<VehiculoBE>();

        private void FormVehiculos_Load(object sender, EventArgs e)
        {
            // Le saco los bordes y la barra de título superior
            this.FormBorderStyle = FormBorderStyle.None;

            // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
            this.Dock = DockStyle.Fill;

            // Habilita el autocompletado que despliega sugerencias y completa el texto
            cmbClientes_675MS.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // Le indica al ComboBox que use los elementos que ya tiene cargados para filtrar
            cmbClientes_675MS.AutoCompleteSource = AutoCompleteSource.ListItems;

            ActualizarPantalla_675MS();

        }

        private void CargoClientes_675MS()
        {
            //Instancio la BLL de Cliente
            ClienteBLL clienteBLL_675MS = new ClienteBLL();


            List<ClienteBE> clientes_675MS= clienteBLL_675MS.ListarClientes_675MS();

            foreach(ClienteBE cliente_675MS in clientes_675MS)
            {
                cmbClientes_675MS.Items.Add(cliente_675MS.Dni_675MS);
            }
        }

        private void ActualizarPantalla_675MS()
        {
            VehiculoBLL vehiculoBLL_675MS = new VehiculoBLL();

            vehiculos_675MS = vehiculoBLL_675MS.ListarVehiculos_675MS();

            dgvVehiculos_675MS.DataSource = null;
            dgvVehiculos_675MS.DataSource = vehiculos_675MS;

            // Ocultar las columnas que no es necesario mostrar
            dgvVehiculos_675MS.Columns["DigitoVerificadorHorizontal_675MS"].Visible = false;

            // Cambiar los títulos para que la interfaz quede prolija
            dgvVehiculos_675MS.Columns["Dominio_675MS"].HeaderText = "Patente";
            dgvVehiculos_675MS.Columns["Dni_675MS"].HeaderText = "DNI Cliente";
            dgvVehiculos_675MS.Columns["Modelo_675MS"].HeaderText = "Modelo";
            dgvVehiculos_675MS.Columns["Anio_675MS"].HeaderText = "Año";
            dgvVehiculos_675MS.Columns["NroChasis_675MS"].HeaderText = "Chasis";
            dgvVehiculos_675MS.Columns["UltimaVTV_675MS"].HeaderText = "Version";

            //Cargo el combo de clientes
            CargoClientes_675MS();
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtDominio_675MS.Text) ||
                string.IsNullOrWhiteSpace(cmbClientes_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtModelo_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtAnio_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtChasis_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtVersion_675MS.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Corto la ejecucion
                return;
            }

            //Valido que haya seleccionado una cliente
            if (cmbClientes_675MS.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un Cliente del listado.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Corto la ejecucion
                return;
            }

            //Valido que el año sea numérico
            if (!int.TryParse(txtAnio_675MS.Text, out int anioValidado_675MS) || anioValidado_675MS < 1900)
            {
                MessageBox.Show("El año ingresado no es válido. Ingrese solo números.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Concateno fecha y hora
            DateTime fechaVTV_675MS = dateTimePicker1.Value.Date;

            // Instancio el objeto TurnoBE para registrar
            VehiculoBE nuevoVehiculo_675MS = new VehiculoBE();
            // Asigno los valores respetando tu estructura de base de datos
            nuevoVehiculo_675MS.Dominio_675MS = txtDominio_675MS.Text.Trim().ToUpper();
            nuevoVehiculo_675MS.Modelo_675MS = txtModelo_675MS.Text.Trim();
            nuevoVehiculo_675MS.Version_675MS = txtVersion_675MS.Text.Trim();
            nuevoVehiculo_675MS.Anio_675MS = anioValidado_675MS;
            nuevoVehiculo_675MS.NroChasis_675MS = txtChasis_675MS.Text.Trim();
            nuevoVehiculo_675MS.UltimaVTV_675MS = fechaVTV_675MS;

            // Asigno el DNI del cliente. 
            nuevoVehiculo_675MS.Dni_675MS = Convert.ToInt32(cmbClientes_675MS.Text);

            // Instancio la BLL de Vehículos
            VehiculoBLL vehiculoBLL_675MS = new VehiculoBLL();

            // Registro el vehículo 
            int resultado_675MS = vehiculoBLL_675MS.RegistrarVehiculo_675MS(nuevoVehiculo_675MS);

            if (resultado_675MS > 0)
            {
                MessageBox.Show("El vehículo con patente " + nuevoVehiculo_675MS.Dominio_675MS + " se registró correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpio los controles de la pantalla
                txtDominio_675MS.Clear();
                txtModelo_675MS.Clear();
                txtVersion_675MS.Clear();
                txtAnio_675MS.Clear();
                txtChasis_675MS.Clear();
                cmbClientes_675MS.SelectedIndex = -1;

                // Refresco la grilla para que aparezca el nuevo vehículo
                ActualizarPantalla_675MS();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar registrar el vehículo en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
