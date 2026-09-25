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
using System.Xml.Schema;

namespace PitBox
{
    public partial class FormTurnos : Form
    {
        public FormTurnos()
        {
            InitializeComponent();
        }

        List<TurnoBE> turnos_675MS = new List<TurnoBE>();
        private void FormTurnos_Load(object sender, EventArgs e)
        {

            // Habilita el autocompletado que despliega sugerencias y completa el texto
            cmbVehiculos_675MS.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // Le indica al ComboBox que use los elementos que ya tiene cargados para filtrar
            cmbVehiculos_675MS.AutoCompleteSource = AutoCompleteSource.ListItems;
            // Le saco los bordes y la barra de título superior
            this.FormBorderStyle = FormBorderStyle.None;

            // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
            this.Dock = DockStyle.Fill;
            // Refrescar la grilla para que aparezca el nuevo turno
            ActualizarPantalla();
        }

        public void ActualizarPantalla()
        {

            TurnoBLL turnoBLL_675MS = new TurnoBLL();

            turnos_675MS = turnoBLL_675MS.ListarTurnosturnos_675MS();

            dgvTurnos_675MS.DataSource = null;
            dgvTurnos_675MS.DataSource = turnos_675MS;

            // Ocultar las columnas que no es necesario mostrar
            dgvTurnos_675MS.Columns["IdTurno_675MS"].Visible = false;
            dgvTurnos_675MS.Columns["DigitoVerificadorHorizontal_675MS"].Visible = false;
            dgvTurnos_675MS.Columns["Activo_675MS"].Visible = false;
            dgvTurnos_675MS.Columns["FechaBaja_675MS"].Visible = false;


            // Cambiar los títulos para que la interfaz quede prolija
            dgvTurnos_675MS.Columns["Dni_675MS"].HeaderText = "DNI Cliente";
            dgvTurnos_675MS.Columns["Dominio_675MS"].HeaderText = "Patente";
            dgvTurnos_675MS.Columns["FechaHora_675MS"].HeaderText = "Fecha/Hora";
            dgvTurnos_675MS.Columns["DuracionEstimada_675MS"].HeaderText = "Duracion(')";

            //Cargo el combo de horarioc
            CargoHorarios();
            CargoPatentes();
        }

        private void CargoPatentes()
        {
            cmbVehiculos_675MS.Items.Clear();

            // Instancio la BLL
            VehiculoBLL vehiculoBLL_675MS = new VehiculoBLL();

            List<string> patentes_675MS = vehiculoBLL_675MS.ListarPatentes();

            // Llenamos el combo con la lista procesada
            foreach (string dominio in patentes_675MS)
            {
                cmbVehiculos_675MS.Items.Add(dominio);
            }


        }

        private void dgvTurnos_675MS_SelectionChanged(object sender, EventArgs e)
        {
            TurnoBE turnoSeleccionado = new TurnoBE();

            if (dgvTurnos_675MS.CurrentRow != null)
            {

                // Recuperamos el valor del registro seleccionado en el Data Grid View
                turnoSeleccionado.IdTurno_675MS = Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["IdTurno_675MS"].Value);
                turnoSeleccionado.Dni_675MS = Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["Dni_675MS"].Value);
                turnoSeleccionado.Dominio_675MS = dgvTurnos_675MS.CurrentRow.Cells["Dominio_675MS"].Value.ToString();
                turnoSeleccionado.FechaHora_675MS = Convert.ToDateTime(dgvTurnos_675MS.CurrentRow.Cells["FechaHora_675MS"].Value);
                turnoSeleccionado.DuracionEstimada_675MS = Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["DuracionEstimada_675MS"].Value);
                turnoSeleccionado.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["DigitoVerificadorHorizontal_675MS"].Value);
                turnoSeleccionado.FechaBaja_675MS = Convert.ToDateTime(dgvTurnos_675MS.CurrentRow.Cells["FechaBaja_675MS"].Value);
                turnoSeleccionado.Activo_675MS = Convert.ToBoolean(dgvTurnos_675MS.CurrentRow.Cells["Activo_675MS"].Value);



                txtDNI_675MS.Text = turnoSeleccionado.Dni_675MS.ToString();
                cmbVehiculos_675MS.Text = turnoSeleccionado.Dominio_675MS.ToString();
                txtDuracion_675MS.Text = turnoSeleccionado.DuracionEstimada_675MS.ToString();
            }
      }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            CargoHorarios();

        }

        private void CargoHorarios()
        {
            cmbHorario_675MS.Items.Clear();
            DateTime fechaSeleccionada = dateTimePicker1.Value.Date;

            // Llamo a la BLL
            TurnoBLL turnoBLL = new TurnoBLL();
            List<string> horarios = turnoBLL.ObtenerHorariosDisponibles_675MS(fechaSeleccionada);

            // Llenamos el combo con la lista procesada
            foreach (string hora in horarios)
            {
                cmbHorario_675MS.Items.Add(hora);
            }

            // Seleccionamos el primero por defecto si hay disponibilidad
            if (cmbHorario_675MS.Items.Count > 0)
            {
                cmbHorario_675MS.SelectedIndex = 0;
            }
        }

        private void btnBaja_675MS_Click(object sender, EventArgs e)
        {

            // Instancio la BLL
            TurnoBLL turnoBLL_675MS = new TurnoBLL();

            int idSeleccionado_675MS= Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["IdTurno_675MS"].Value);

            int resultado_675MS = turnoBLL_675MS.BajaTurno_675MS(idSeleccionado_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la baja del turno");
            }else
            {
                if (resultado_675MS==1)
                {
                    // Refrescar la grilla para que aparezca el nuevo turno
                    ActualizarPantalla();
                    MessageBox.Show("Baja Correcta");
                }
            }
        }

        private void btnModificar_675MS_Click(object sender, EventArgs e)
        {
            // Instancio la BLL
            TurnoBLL turnoBLL_675MS = new TurnoBLL();

            int idSeleccionado_675MS = Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["IdTurno_675MS"].Value);
            DateTime fechaNueva_675MS;

            // Tomamos la fecha pura del calendario y le sumamos el tiempo exacto del ComboBox
            fechaNueva_675MS = dateTimePicker1.Value.Date.Add(TimeSpan.Parse(cmbHorario_675MS.Text));

            int resultado_675MS = turnoBLL_675MS.ReprogramarTurno_675MS(idSeleccionado_675MS, fechaNueva_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la modificación del turno");
            }
            else
            {
                if (resultado_675MS == 1)
                {
                    // Refrescar la grilla para que aparezca el nuevo turno
                    ActualizarPantalla();
                    MessageBox.Show("El turno se ha reprogramado de manera Correcta");
                }
            }
        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(txtDNI_675MS.Text) ||
                string.IsNullOrWhiteSpace(cmbVehiculos_675MS.Text) ||
                string.IsNullOrWhiteSpace(txtDuracion_675MS.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Corto la ejecucion
                return; 
            }

            //Valido que haya seleccionado un horario correcto
            if (cmbHorario_675MS.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un horario disponible del listado.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Corto la ejecucion
                return;
            }

            // Valido que el DNI sea un número entero positivo
            if (!int.TryParse(txtDNI_675MS.Text, out int dniValidado_675MS) || dniValidado_675MS <= 0)
            {
                MessageBox.Show("El DNI ingresado no es válido. Ingrese solo números.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Corto la ejecucion
                return;
            }

            // Valido que la duración sea un número
            if (!int.TryParse(txtDuracion_675MS.Text, out int duracionValidada_675MS) || duracionValidada_675MS <= 0)
            {
                MessageBox.Show("La duración estimada debe ser un número mayor a cero.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Corto la ejecucion
                return;
            }

            // Concateno fecha y hora
            DateTime fechaElegida_675MS = dateTimePicker1.Value.Date.Add(TimeSpan.Parse(cmbHorario_675MS.Text));

            // Instancio el objeto TurnoBE para registrar
            TurnoBE nuevoTurno_675MS = new TurnoBE();

            nuevoTurno_675MS.Dni_675MS = dniValidado_675MS;
            // Guardamos la patente siempre en mayúsculas 
            nuevoTurno_675MS.Dominio_675MS = cmbVehiculos_675MS.Text.Trim().ToUpper();
            nuevoTurno_675MS.FechaHora_675MS = fechaElegida_675MS;
            nuevoTurno_675MS.DuracionEstimada_675MS = duracionValidada_675MS;

            // Al crear un turno nuevo, siempre está activo
            nuevoTurno_675MS.Activo_675MS = true;

            //Instancio la BLL
            TurnoBLL turnoBLL_675MS = new TurnoBLL();

            // Registro el turno
            int resultado_675MS = turnoBLL_675MS.RegistrarTurno_675MS(nuevoTurno_675MS);

            if (resultado_675MS > 0)
            {
                MessageBox.Show("El turno para el vehículo " + nuevoTurno_675MS.Dominio_675MS + " se registró correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar la grilla para que aparezca el nuevo turno
                ActualizarPantalla();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar registrar el turno en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
