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
    public partial class FormUsuarios : Form, IObserverIdioma
    {

        //Manejo de idiomas
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        public FormUsuarios()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            TratamientoIdioma.Instancia.Suscribir(this);
            CargarComboIdiomas_675MS();

            CargarRoles_675MS();
            CargarUsuarios_675MS();
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
                    cmbIdiomas_675MS.SelectedValue = TratamientoIdioma.Instancia.IdiomaActual_675MS.Id_675MS;
                }
                else if (idiomas_675MS.Count > 0)
                {
                    cmbIdiomas_675MS.SelectedIndex = 0;

                    TratamientoIdioma.Instancia.IdiomaActual_675MS =  idiomas_675MS[0];
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
                    case "lblGestionUsuarios_675MS":
                        lblGestionUsuarios_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "bntActivar_675MS":
                        bntActivar_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblEmpleado_675MS":
                        lblEmpleado_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "btnDesactivar_675MS":
                        btnDesactivar_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "btnGuardarRol_675MS":
                        btnGuardarRol_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "chkActivo_675MS":
                        chkActivo_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblRol_675MS":
                        lblRol_675MS.Text = traduccion.Traduccion_675MS;
                        break;

                    case "lblActivo_675MS":
                        lblActivo_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                }
            }
        }

        private void cmbIdiomas_675MS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoIdiomas_675MS) return;

            IdiomaBE idioma_675MS = cmbIdiomas_675MS.SelectedItem as IdiomaBE;
            if (idioma_675MS == null) return;

            TratamientoIdioma.Instancia.IdiomaActual_675MS = idioma_675MS;
            TratamientoIdioma.Instancia.Notificar();
            ActualizarIdioma_675MS();
        }

        private void CargarUsuarios_675MS()
        {
            try
            {
                UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();
                dgvUsuarios_675MS.DataSource = null;
                dgvUsuarios_675MS.AutoGenerateColumns = true;
                dgvUsuarios_675MS.DataSource = usuarioBLL_675MS.ListarUsuarios_675MS();

                dgvUsuarios_675MS.Columns["IdUsuario"].Visible = false;
                dgvUsuarios_675MS.Columns["IdEmpleado"].Visible = false;
                dgvUsuarios_675MS.Columns["IdRol"].Visible = false;

                dgvUsuarios_675MS.Columns["NombreUsuario"].HeaderText = "Usuario";
                dgvUsuarios_675MS.Columns["NombreEmpleado"].HeaderText = "Empleado";
                dgvUsuarios_675MS.Columns["NombreRol"].HeaderText = "Rol";
                dgvUsuarios_675MS.Columns["Activo"].HeaderText = "Activo";
                dgvUsuarios_675MS.Columns["Bloqueado"].HeaderText = "Bloqueado";
                dgvUsuarios_675MS.Columns["IntentosFallidos"].HeaderText = "Intentos fallidos";

                dgvUsuarios_675MS.ReadOnly = true;
                dgvUsuarios_675MS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsuarios_675MS.MultiSelect = false;
                dgvUsuarios_675MS.AllowUserToAddRows = false;
                dgvUsuarios_675MS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvUsuarios_675MS.ClearSelection();
                txtUsuario_675MS.Clear();
                txtEmpleado_675MS.Clear();
                chkActivo_675MS.Checked = false;
                cmbRol_675MS.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarRoles_675MS()
        {
            try
            {
                RolBLL rolBLL_675MS = new RolBLL();
                List<RolBE> roles_675MS = rolBLL_675MS.ListarRoles_675MS();

                cmbRol_675MS.DataSource = null;
                cmbRol_675MS.DisplayMember = "Nombre_675MS";
                cmbRol_675MS.ValueMember = "Id_675MS";
                cmbRol_675MS.DataSource = roles_675MS;
                cmbRol_675MS.SelectedIndex = -1;
                cmbRol_675MS.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnGuardarRol_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios_675MS.CurrentRow == null || cmbRol_675MS.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un usuario y un rol.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idUsuario_675MS = Convert.ToInt32(dgvUsuarios_675MS.CurrentRow.Cells["IdUsuario"].Value);
                int idRol_675MS = Convert.ToInt32(cmbRol_675MS.SelectedValue);

                DialogResult respuesta_675MS = MessageBox.Show("¿Confirmás que querés modificar el rol de este usuario?", "Confirmar cambio de rol", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta_675MS != DialogResult.Yes) return;

                UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();
                usuarioBLL_675MS.AsignarRolUsuario_675MS(idUsuario_675MS, idRol_675MS);

                MessageBox.Show("El rol del usuario se actualizó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios_675MS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_675MS_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvUsuarios_675MS.CurrentRow == null) return;
            if (dgvUsuarios_675MS.CurrentRow.IsNewRow) return;

            DataGridViewRow fila_675MS = dgvUsuarios_675MS.CurrentRow;

            txtUsuario_675MS.Text = Convert.ToString(fila_675MS.Cells["NombreUsuario"].Value);
            txtEmpleado_675MS.Text = Convert.ToString(fila_675MS.Cells["NombreEmpleado"].Value);
            chkActivo_675MS.Checked = Convert.ToInt32(fila_675MS.Cells["Activo"].Value) == 1;
            cmbRol_675MS.SelectedValue = Convert.ToInt32(fila_675MS.Cells["IdRol"].Value);
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void bntActivar_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios_675MS.CurrentRow == null || dgvUsuarios_675MS.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un usuario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow fila_675MS = dgvUsuarios_675MS.CurrentRow;
                int idUsuario_675MS = Convert.ToInt32(fila_675MS.Cells["IdUsuario"].Value);
                string nombreUsuario_675MS = Convert.ToString(fila_675MS.Cells["NombreUsuario"].Value);
                bool activo_675MS = Convert.ToInt32(fila_675MS.Cells["Activo"].Value) == 1;

                if (activo_675MS)
                {
                    MessageBox.Show("El usuario seleccionado ya está activo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult respuesta_675MS = MessageBox.Show("¿Confirmás que querés activar al usuario " + nombreUsuario_675MS + "?", "Confirmar activación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta_675MS != DialogResult.Yes) return;

                UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();
                usuarioBLL_675MS.ActivarCuentaUsuario_675MS(idUsuario_675MS);

                MessageBox.Show("El usuario se activó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios_675MS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesactivar_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios_675MS.CurrentRow == null || dgvUsuarios_675MS.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un usuario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow fila_675MS = dgvUsuarios_675MS.CurrentRow;
                int idUsuario_675MS = Convert.ToInt32(fila_675MS.Cells["IdUsuario"].Value);
                string nombreUsuario_675MS = Convert.ToString(fila_675MS.Cells["NombreUsuario"].Value);
                bool activo_675MS = Convert.ToInt32(fila_675MS.Cells["Activo"].Value) == 1;

                if (!activo_675MS)
                {
                    MessageBox.Show("El usuario seleccionado ya está inactivo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult respuesta_675MS = MessageBox.Show("¿Confirmás que querés desactivar al usuario " + nombreUsuario_675MS + "?", "Confirmar desactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta_675MS != DialogResult.Yes) return;

                UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();
                usuarioBLL_675MS.DesactivarCuentaUsuario_675MS(idUsuario_675MS);

                MessageBox.Show("El usuario se desactivó correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios_675MS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbIdiomas_675MS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cargandoIdiomas_675MS) return;

            IdiomaBE idioma_675MS = cmbIdiomas_675MS.SelectedItem as IdiomaBE;
            if (idioma_675MS == null) return;

            TratamientoIdioma.Instancia.IdiomaActual_675MS = idioma_675MS;
            TratamientoIdioma.Instancia.Notificar();
            ActualizarIdioma_675MS();
        }
    }
}
