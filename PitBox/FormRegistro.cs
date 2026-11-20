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

    public partial class FormRegistro : Form , IObserverIdioma
    {
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        public FormRegistro()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }


        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            // Validar nombre de usuario
            if (string.IsNullOrWhiteSpace(txtUsuario_675MS.Text))
            {
                MessageBox.Show("Por favor, ingrese el usuario.","Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario_675MS.Focus();
                return;
            }

            // Validar contraseña
            if (string.IsNullOrWhiteSpace(txtPassword_675MS.Text))
            {
                MessageBox.Show("Por favor, ingrese su contraseña.","Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword_675MS.Focus();
                return;
            }

            // Validar repetición de contraseña
            if (string.IsNullOrWhiteSpace(txtxRepetirPass_675MS.Text))
            {
                MessageBox.Show("Por favor, repita la contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtxRepetirPass_675MS.Focus();
                return;
            }

            // Verificar que las contraseñas coincidan
            if (txtPassword_675MS.Text != txtxRepetirPass_675MS.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtxRepetirPass_675MS.Focus();
                return;
            }

            // Validar empleado seleccionado
            if (cmbDNIEmpleado_675MS.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un empleado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDNIEmpleado_675MS.Focus();
                return;
            }

            EmpleadoBE empleadoSeleccionado_675MS =  cmbDNIEmpleado_675MS.SelectedItem as EmpleadoBE;

            if (empleadoSeleccionado_675MS == null)
            {
                MessageBox.Show("El empleado seleccionado no es válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UsuarioBE usuario_675MS = new UsuarioBE();

                usuario_675MS.Username_675MS = txtUsuario_675MS.Text.Trim();

                usuario_675MS.Password_675MS = txtPassword_675MS.Text;

                usuario_675MS.FechaCreacion_675MS = DateTime.Now;

                usuario_675MS.Bloqueado_675MS = "0";
                usuario_675MS.IntentosFallidos_675MS = 0;

                // ID real del empleado seleccionado
                usuario_675MS.IdEmpleado_675MS = empleadoSeleccionado_675MS.IdEmpleado_675MS;

                // Rol inicial: Empleado
                usuario_675MS.IdRol_675MS = 2;

                UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();

                //Verifico que el empleado no tenga ya un usuario 
                if (usuarioBLL_675MS.EmpleadoTieneUsuario_675MS(usuario_675MS.IdEmpleado_675MS))
                {
                    MessageBox.Show("El empleado seleccionado ya tiene un usuario registrado.","Usuario existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // La BLL realiza el hash, calcula DVH y registra
                int resultado_675MS =  usuarioBLL_675MS.AltaUsuario_675MS(usuario_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show( "El usuario se registró correctamente.\n" + "El administrador debe aprobar su acceso.", "PitBox",MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtUsuario_675MS.Clear();
                    txtPassword_675MS.Clear();
                    txtxRepetirPass_675MS.Clear();
                    cmbDNIEmpleado_675MS.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario. " + "Verifique los datos e intente nuevamente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el usuario:\n" + ex.Message,  "Error",  MessageBoxButtons.OK,   MessageBoxIcon.Error);
            }
        }

        private void FormRegistro_Load(object sender, EventArgs e)
        {
            //Suscribo al evento del observer
            TratamientoIdioma.Instancia.Suscribir(this);
            //Cargo combo con los idiomas del sistema
            CargarComboIdiomas_675MS();
            CargarComboDNI_675MS();
        }


        private void CargarComboDNI_675MS()
        {
            try
            {
                EmpleadoBLL empleadoBLL_675MS = new EmpleadoBLL();

                List<EmpleadoBE> empleados_675MS =  empleadoBLL_675MS.ListarEmpleados_675MS();

                cmbDNIEmpleado_675MS.DataSource = null;

                cmbDNIEmpleado_675MS.DisplayMember = "DniEmpleado_675MS";
                cmbDNIEmpleado_675MS.ValueMember = "IdEmpleado_675MS";

                cmbDNIEmpleado_675MS.DataSource = empleados_675MS;
                cmbDNIEmpleado_675MS.SelectedIndex = -1;
                cmbDNIEmpleado_675MS.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
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
                    case "lblUsuario_675MS":
                        lblUsuario_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblPassword_675MS":
                        lblPassword_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblIdEmpleado_675MS":
                        lblIdEmpleado_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblRepetirPass_675MS":
                        lblRepetirPass_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "bntRegistrar_675MS":
                        bntRegistrar_675MS .Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                }
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

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
