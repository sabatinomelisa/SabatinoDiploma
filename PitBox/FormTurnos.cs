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
using System.Xml.Schema;

namespace PitBox
{
    public partial class FormTurnos : Form, IObserverIdioma
    {
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        public FormTurnos()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
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
            CargoPatentes_675MS();
            CargoHorarios_675MS();
            ActualizarPantalla_675MS();

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
                    case "lblDia_675MS":
                        lblDia_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblDominio_675MS":
                        lblDominio_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblDuracion_675MS":
                        lblDuracion_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblGestorTurnos_675MS":
                        lblGestorTurnos_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblHora_675MS":
                        lblHora_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnBaja_675MS":
                        btnBaja_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnModificar_675MS":
                        btnModificar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "bntRegistrar_675MS":
                        bntRegistrar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                }
            }
        }

        public void ActualizarPantalla_675MS()
        {

            TurnoBLL turnoBLL_675MS = new TurnoBLL();

            turnos_675MS =  turnoBLL_675MS.ListarTurnost_6755MS();

            dgvTurnos_675MS.DataSource = null;
            dgvTurnos_675MS.DataSource = turnos_675MS;
            dgvTurnos_675MS.Columns["IdTurno_675MS"].Visible = false;
            dgvTurnos_675MS.Columns["DigitoVerificadorHorizontal_675MS"].Visible = false;
            dgvTurnos_675MS.Columns["Activo_675MS"].Visible = false;
            dgvTurnos_675MS.Columns["FechaBaja_675MS"].Visible = false;
            dgvTurnos_675MS.Columns["IdEmpleado_675MS"].Visible = false;
            dgvTurnos_675MS.Columns["Dominio_675MS"].HeaderText = "Patente";
            dgvTurnos_675MS.Columns["FechaHora_675MS"].HeaderText = "Fecha/Hora";
            dgvTurnos_675MS.Columns["DuracionEstimada_675MS"].HeaderText = "Duración (minutos)";
        }

        private void CargoPatentes_675MS()
        {
            cmbVehiculos_675MS.Items.Clear();

            // Instancio la BLL
            VehiculoBLL vehiculoBLL_675MS = new VehiculoBLL();

            List<string> patentes_675MS = vehiculoBLL_675MS.ListarPatentes_675MS();

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
                    turnoSeleccionado.Dominio_675MS = dgvTurnos_675MS.CurrentRow.Cells["Dominio_675MS"].Value.ToString();
                    turnoSeleccionado.FechaHora_675MS = Convert.ToDateTime(dgvTurnos_675MS.CurrentRow.Cells["FechaHora_675MS"].Value);
                    turnoSeleccionado.DuracionEstimada_675MS = Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["DuracionEstimada_675MS"].Value);
                    turnoSeleccionado.DigitoVerificadorHorizontal_675MS = Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["DigitoVerificadorHorizontal_675MS"].Value);
                    turnoSeleccionado.FechaBaja_675MS = Convert.ToDateTime(dgvTurnos_675MS.CurrentRow.Cells["FechaBaja_675MS"].Value);
                    turnoSeleccionado.Activo_675MS = Convert.ToBoolean(dgvTurnos_675MS.CurrentRow.Cells["Activo_675MS"].Value);
                    turnoSeleccionado.IdEmpleado_675MS = Convert.ToInt32(dgvTurnos_675MS.CurrentRow.Cells["IdEmpleado_675MS"].Value);

                    dateTimePicker1.Value= turnoSeleccionado.FechaHora_675MS;
                    cmbVehiculos_675MS.Text = turnoSeleccionado.Dominio_675MS.ToString();
                    txtDuracion_675MS.Text = turnoSeleccionado.DuracionEstimada_675MS.ToString();
                    string horaStr_675MS = turnoSeleccionado.FechaHora_675MS.ToString("HH:mm");

                    cmbHorario_675MS.SelectedItem = horaStr_675MS;

                    if (cmbHorario_675MS.Items.Contains(horaStr_675MS))
                    {
                        cmbHorario_675MS.SelectedItem = horaStr_675MS;
                    }
                    else
                    {
                        // Como no está en la lista precargada, lo agrego temporalmente para que se muestre
                        cmbHorario_675MS.Items.Add(horaStr_675MS);
                        cmbHorario_675MS.SelectedItem = horaStr_675MS;
                    }
            }
      }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            CargoHorarios_675MS();

        }

        private void CargoHorarios_675MS()
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
                    // Refrescar la grilla 
                    ActualizarPantalla_675MS();
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
                    ActualizarPantalla_675MS();
                    MessageBox.Show("El turno se ha reprogramado de manera Correcta");
                }
            }
        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(cmbVehiculos_675MS.Text) ||
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
                ActualizarPantalla_675MS();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar registrar el turno en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbIdiomas_675MS_SelectedIndexChanged_1(object sender, EventArgs e)
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
