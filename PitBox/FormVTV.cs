using BE;
using BLL;
using Servicios;
using Servicios.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PitBox
{
    public partial class FormVTV : Form, IObserverIdioma
    {
        public FormVTV()
        {
            InitializeComponent();
            //Agrego fondo de pantalla
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        VtvBLL vtvBLL_675MS;
        List<VtvBE> vtvs_675MS = new List<VtvBE>();
        // Empleado a Cargo (Obtenido desde el Singleton de Sesión)
        UsuarioBE usuarioActual_675MS = SessionManager.ObtenerUsuarioActual_675MS();
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;


        private void FormVTV_Load(object sender, EventArgs e)
        {
            // Habilita el autocompletado que despliega sugerencias y completa el texto
            cmbVehiculos_675MS.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // Le indica al ComboBox que use los elementos que ya tiene cargados para filtrar
            cmbVehiculos_675MS.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            TratamientoIdioma.Instancia.Suscribir(this);
            //Cargo combo con los idiomas del sistema
            CargarComboIdiomas_675MS();

            ActualizarPantalla_675MS();
            VerificarPermisoAprobacion_675MS();

        }

        private void VerificarPermisoAprobacion_675MS()
        {
            // Recupero el usuario que inició sesión
            UsuarioBE usuario_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (usuario_675MS == null || usuario_675MS.Rol_675MS == null)
            {
                // Si no hay sesión o rol válido, por seguridad oculto el botón de aprobar
                btnAprobar_675MS.Visible = false;
                return;
            }

            RolBLL rolBLL_675MS = new RolBLL();
            RolBE rol_675MS = usuario_675MS.Rol_675MS;

            //Consulto si el rol actual posee el permiso de aprobar VTV
            bool puedeAprobar_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "AprobarVTV");
            //Consulto si el rol actual posee el permiso de imprimir comprobante
            bool puedeImprimir_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "Imprimir");

            // Mostrar u ocultar el botón según corresponda
            btnAprobar_675MS.Visible = puedeAprobar_675MS;
            btnImprimir_675MS.Visible = puedeImprimir_675MS;
        }

        private void ActualizarPantalla_675MS()
        {
            vtvBLL_675MS = new VtvBLL();

            vtvs_675MS = vtvBLL_675MS.ListarVtvs_675MS();

            dgvVTV_675MS.DataSource = null;
            dgvVTV_675MS.DataSource = vtvs_675MS;

            // Ocultar las columnas que no es necesario mostrar
            dgvVTV_675MS.Columns["IdEmpleado_675MS"].Visible = false;
            dgvVTV_675MS.Columns["Activo_675MS"].Visible = false;

            // Cambiar los títulos para que la interfaz quede prolija
            dgvVTV_675MS.Columns["Dominio_675MS"].HeaderText = "Dominio";
            dgvVTV_675MS.Columns["FechaInspeccion_675MS"].HeaderText = "Fecha Inspección";
            dgvVTV_675MS.Columns["Observaciones_675MS"].HeaderText = "Observaciones";
            dgvVTV_675MS.Columns["RequiereInspeccion_675MS"].HeaderText = "Requiere Reinspección";
            dgvVTV_675MS.Columns["FechaReinspeccion_675MS"].HeaderText = "Fecha Reinspección";
            dgvVTV_675MS.Columns["FechaVencimiento_675MS"].HeaderText = "Fecha Vencimiento";
            dgvVTV_675MS.Columns["Id_675MS"].HeaderText = "Nro Certificado";
            dgvVTV_675MS.Columns["Estado_675MS"].HeaderText = "Estado";


            CargoEmpleado_675MS();
            CargoPatentes_675MS();

        }

        private void CargoEmpleado_675MS()
        {

            if (usuarioActual_675MS != null)
            {
                //Consulto a la BLL de Empleados usando el ID para traer sus datos (Nombre y Apellido)
                EmpleadoBLL empleadoBLL_675MS = new EmpleadoBLL();
                EmpleadoBE empleado_675MS = empleadoBLL_675MS.ObtenerEmpleadoPorId_675MS(usuarioActual_675MS.IdEmpleado_675MS);

                if (empleado_675MS != null)
                {
                    // Mostramos el Nombre y Apellido en el control visual
                    txtEmpleadoaCargo_675MS.Text = empleado_675MS.Nombre_675MS + " " + empleado_675MS.Apellido_675MS;
                }
            }

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

            int idiomaSeleccionado =  TratamientoIdioma.Instancia.IdiomaActual_675MS.Id_675MS;

            List<TraduccionBE> traducciones = traduccionBLL_675MS.Listar_675MS(idiomaSeleccionado);

            foreach (TraduccionBE traduccion in traducciones)
            {
                switch (traduccion.NombreControl_675MS)
                {
                    case "lblDominio_675MS":
                        lblDominio_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblEmpleadoCargo_675MS":
                        lblEmpleadoCargo_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblFechaInspeccion_675MS":
                        lblFechaInspeccion_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblFechaReinspeccion_675MS":
                        lblFechaReinspeccion_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblFechaVencimiento_675MS":
                        lblFechaVencimiento_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblGestorVTV_675MS":
                        lblGestorVTV_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblObservaciones_675MS":
                        lblObservaciones_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblIdioma675MS":
                        lblIdioma675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblRequiere_675MS":
                        lblRequiere_675MS.Text = traduccion.Traduccion_675MS;
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
                    case "bntRegistrar_675MS":
                        bntRegistrar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                }
            }
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(cmbVehiculos_675MS.Text) ||
                (string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
                string.IsNullOrWhiteSpace(txtObservaciones_675MS.Text)))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Corto la ejecucion
                return;
            }

            // Instancio el objeto TurnoBE para registrar
            VtvBE nuevaVTV_675MS = new VtvBE();

            // Guardamos la patente siempre en mayúsculas 
            nuevaVTV_675MS.Dominio_675MS = cmbVehiculos_675MS.Text.Trim().ToUpper();
            nuevaVTV_675MS.FechaInspeccion_675MS = dateTimePicker1.Value.Date;
            nuevaVTV_675MS.Observaciones_675MS = txtObservaciones_675MS.Text;

            // Al crear una nueva VTV, siempre está activa
            nuevaVTV_675MS.Activo_675MS = 0;
            // Asigno el ID del empleado al objeto VTV
            nuevaVTV_675MS.IdEmpleado_675MS = usuarioActual_675MS.IdEmpleado_675MS;
            // Requiere Reinspección (1 si está tildado, 0 si está vacío)
            int requiereReinspeccion_675MS = checkBox1.Checked ? 1 : 0;

            DateTime fechaVencimiento_675MS;

            if (requiereReinspeccion_675MS == 0)
            {
                // Si NO requiere reinspección: Hoy + 1 año
                fechaVencimiento_675MS = DateTime.Today.AddYears(1);
            }
            else
            {
                // Si SÍ requiere reinspección: Fecha mínima (0001-01-01)
                fechaVencimiento_675MS = DateTime.MinValue;
            }

            // Asignao los valores al objeto VTV
            nuevaVTV_675MS.RequiereInspeccion_675MS = requiereReinspeccion_675MS;
            nuevaVTV_675MS.FechaVencimiento_675MS = fechaVencimiento_675MS;
            nuevaVTV_675MS.FechaReinspeccion_675MS = DateTime.MinValue; 

            //Instancio la BLL
            vtvBLL_675MS = new VtvBLL();

            // Registro el turno
            int resultado_675MS = vtvBLL_675MS.RegistrarVTV_675MS(nuevaVTV_675MS);

            if (resultado_675MS > 0)
            {
                MessageBox.Show("La VTV para el vehículo " + nuevaVTV_675MS.Dominio_675MS + " se registró correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar la grilla para que aparezca el nuevo turno
                ActualizarPantalla_675MS();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar registrar la VTV en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_675MS_Click(object sender, EventArgs e)
        {
            if (dgvVTV_675MS.SelectedRows.Count == 0 && dgvVTV_675MS.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un registro de la grilla para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Valido campos vacios
            if (string.IsNullOrWhiteSpace(cmbVehiculos_675MS.Text) ||
                (string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
                string.IsNullOrWhiteSpace(txtObservaciones_675MS.Text)))
            {
                MessageBox.Show("Por favor, complete todos los campos de texto.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Corto la ejecucion
                return;
            }

            // Instancio el objeto TurnoBE para registrar
            VtvBE nuevaVTV_675MS = new VtvBE();

            //Recupero ID de la grilla
            nuevaVTV_675MS.Id_675MS = Convert.ToInt32(dgvVTV_675MS.CurrentRow.Cells["Id_675MS"].Value);

            // Guardamos la patente siempre en mayúsculas 
            nuevaVTV_675MS.Dominio_675MS = cmbVehiculos_675MS.Text.Trim().ToUpper();
            nuevaVTV_675MS.FechaInspeccion_675MS = dateTimePicker1.Value.Date;
            nuevaVTV_675MS.Observaciones_675MS = txtObservaciones_675MS.Text;

            // Asigno el ID del empleado al objeto VTV
            nuevaVTV_675MS.IdEmpleado_675MS = usuarioActual_675MS.IdEmpleado_675MS;
            // Requiere Reinspección (1 si está tildado, 0 si está vacío)
            int requiereReinspeccion_675MS = checkBox1.Checked ? 1 : 0;

            // Asigno los valores al objeto VTV
            nuevaVTV_675MS.RequiereInspeccion_675MS = requiereReinspeccion_675MS;
            nuevaVTV_675MS.FechaReinspeccion_675MS = DateTime.MinValue;
            nuevaVTV_675MS.FechaVencimiento_675MS = dateTimePicker1.Value;

            //Instancio la BLL
            vtvBLL_675MS = new VtvBLL();

            // Registro el turno
            int resultado_675MS = vtvBLL_675MS.ModificarVTV_675MS(nuevaVTV_675MS);

            if (resultado_675MS > 0)
            {
                MessageBox.Show("La VTV para el vehículo " + nuevaVTV_675MS.Dominio_675MS + " se Modificó correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar la grilla para que aparezca el nuevo turno
                ActualizarPantalla_675MS();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar modificar la VTV en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnBaja_675MS_Click(object sender, EventArgs e)
        {
            if (dgvVTV_675MS.SelectedRows.Count == 0 && dgvVTV_675MS.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un registro de la grilla para dar de baja.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Instancio la BLL
            vtvBLL_675MS = new VtvBLL();

            int idSeleccionado_675MS = Convert.ToInt32(dgvVTV_675MS.CurrentRow.Cells["Id_675MS"].Value);

            int resultado_675MS = vtvBLL_675MS.BajaVTV_675MS(idSeleccionado_675MS);

            if (resultado_675MS == -1)
            {
                MessageBox.Show("Error en la baja de la VTV");
            }
            else
            {
                if (resultado_675MS == 1)
                {
                    // Refrescar la grilla 
                    ActualizarPantalla_675MS();
                    MessageBox.Show("Baja Correcta");
                }
            }
        }

        private void dgvVTV_675MS_SelectionChanged(object sender, EventArgs e)
        {
            VtvBE vtvSeleccionada_675MS = new VtvBE();

            if (dgvVTV_675MS.CurrentRow != null)
            {
                vtvSeleccionada_675MS.Id_675MS = Convert.ToInt32(dgvVTV_675MS.CurrentRow.Cells["Id_675MS"].Value);
                vtvSeleccionada_675MS.Dominio_675MS = dgvVTV_675MS.CurrentRow.Cells["Dominio_675MS"].Value.ToString();
                vtvSeleccionada_675MS.FechaInspeccion_675MS = Convert.ToDateTime(dgvVTV_675MS.CurrentRow.Cells["FechaInspeccion_675MS"].Value);

                vtvSeleccionada_675MS.Observaciones_675MS = dgvVTV_675MS.CurrentRow.Cells["Observaciones_675MS"].Value.ToString();
                vtvSeleccionada_675MS.RequiereInspeccion_675MS = Convert.ToInt32(dgvVTV_675MS.CurrentRow.Cells["RequiereInspeccion_675MS"].Value);

                vtvSeleccionada_675MS.FechaReinspeccion_675MS = Convert.ToDateTime(dgvVTV_675MS.CurrentRow.Cells["FechaReinspeccion_675MS"].Value);

                vtvSeleccionada_675MS.FechaVencimiento_675MS = Convert.ToDateTime(dgvVTV_675MS.CurrentRow.Cells["FechaVencimiento_675MS"].Value);

                vtvSeleccionada_675MS.IdEmpleado_675MS = Convert.ToInt32(dgvVTV_675MS.CurrentRow.Cells["IdEmpleado_675MS"].Value);


                vtvSeleccionada_675MS.Activo_675MS = Convert.ToInt32(dgvVTV_675MS.CurrentRow.Cells["Activo_675MS"].Value);

                cmbVehiculos_675MS.Text = vtvSeleccionada_675MS.Dominio_675MS;
                dateTimePicker1.Value = vtvSeleccionada_675MS.FechaInspeccion_675MS;
                txtObservaciones_675MS.Text = vtvSeleccionada_675MS.Observaciones_675MS;

                checkBox1.Checked = (vtvSeleccionada_675MS.RequiereInspeccion_675MS == 1);

                if (vtvSeleccionada_675MS.FechaReinspeccion_675MS != DateTime.MinValue)
                {
                    dateTimePicker2.Value = vtvSeleccionada_675MS.FechaReinspeccion_675MS;
                }


                txtFechaVencimiento_675MS.Text = vtvSeleccionada_675MS.FechaVencimiento_675MS.ToString();
                CargoEmpleadoPorId_675MS(vtvSeleccionada_675MS.IdEmpleado_675MS);
            }
        }

        private void CargoEmpleadoPorId_675MS(int idEmp_675MS)
        {
                //Consulto a la BLL de Empleados usando el ID para traer sus datos (Nombre y Apellido)
                EmpleadoBLL empleadoBLL_675MS = new EmpleadoBLL();
                EmpleadoBE empleado_675MS = empleadoBLL_675MS.ObtenerEmpleadoPorId_675MS(usuarioActual_675MS.IdEmpleado_675MS);

                if (empleado_675MS != null)
                {
                    // Mostramos el Nombre y Apellido en el control visual
                    txtEmpleadoaCargo_675MS.Text = empleado_675MS.Nombre_675MS + " " + empleado_675MS.Apellido_675MS;
                }
        }

        //Boton Aprobar
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvVTV_675MS.SelectedRows.Count == 0 && dgvVTV_675MS.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un registro de la grilla para aprobar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Recupero ID de la grilla
            int Id_675MS = Convert.ToInt32(dgvVTV_675MS.CurrentRow.Cells["Id_675MS"].Value);

            //Instancio la BLL
            vtvBLL_675MS = new VtvBLL();

            // Registro el turno
            int resultado_675MS = vtvBLL_675MS.AprobarVTV_675MS(Id_675MS);

            if (resultado_675MS > 0)
            {
                MessageBox.Show("La VTV fue aprobada.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar la grilla para que aparezca el nuevo turno
                ActualizarPantalla_675MS();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar aprobar la VTV en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_675MS_Click(object sender, EventArgs e)
        {
            if (dgvVTV_675MS.SelectedRows.Count == 0 && dgvVTV_675MS.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un registro de la grilla para imprimir.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string empleadoAprobador_675MS = "Sistema / Desconocido";

            try
            {
                // 1. Obtengo el ID seleccionado en la grilla
                int idVtv_675MS = Convert.ToInt32(dgvVTV_675MS.CurrentRow.Cells["Id_675MS"].Value);

                // 2. Recupero el nombre del empleado aprobador si la variable está disponible
                if (usuarioActual_675MS != null)
                {
                    EmpleadoBLL empleadoBLL_675MS = new EmpleadoBLL();
                    EmpleadoBE empleado_675MS = empleadoBLL_675MS.ObtenerEmpleadoPorId_675MS(usuarioActual_675MS.IdEmpleado_675MS);

                    if (empleado_675MS != null)
                    {
                        empleadoAprobador_675MS = empleado_675MS.Nombre_675MS + " " + empleado_675MS.Apellido_675MS;
                    }
                }

                // 3. Defino ruta genérica segura (Carpeta Descargas)
                string rutaCarpeta_675MS = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string rutaCompleta_675MS = Path.Combine(rutaCarpeta_675MS, $"ComprobanteVTV_{idVtv_675MS}.txt");

                // 4. Llamo a la BLL para procesar la impresión pasando también el nombre del empleado
                vtvBLL_675MS = new VtvBLL();
                vtvBLL_675MS.GenerarComprobanteVTV(idVtv_675MS, rutaCompleta_675MS, empleadoAprobador_675MS);

                MessageBox.Show($"Comprobante generado con éxito en:\n{rutaCompleta_675MS}", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

