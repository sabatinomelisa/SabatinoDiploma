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
    public partial class FormRoles : Form , IObserverIdioma
    {
        public FormRoles()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;
        RolBLL rolBLL_675MS;
        PermisoBLL permisoBLL_675MS;
        ComponenteBLL componenteBLL_675MS;

        //Manejo de idiomas

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

            int idiomaSeleccionado = TratamientoIdioma.Instancia.IdiomaActual_675MS.Id_675MS;

            List<TraduccionBE> traducciones = traduccionBLL_675MS.Listar_675MS(idiomaSeleccionado);

            foreach (TraduccionBE traduccion in traducciones)
            {
                switch (traduccion.NombreControl_675MS)
                {
                    case "lblIdioma675MS":
                        lblIdioma675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblGestorRoles_675MS":
                        lblGestorRoles_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblNombreRol_675MS":
                        lblNombreRol_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblNombrePermiso_675MS":
                        lblNombrePermiso_675MS.Text = traduccion.Traduccion_675MS;
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

        //Eventos del Form y controles de usuario
        private void FormRoles_Load(object sender, EventArgs e)
        {
            // Le saco los bordes y la barra de título superior
            this.FormBorderStyle = FormBorderStyle.None;

            // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
            this.Dock = DockStyle.Fill;

            // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
            this.Dock = DockStyle.Fill;

            ActualizarPantalla_675MS();

            TratamientoIdioma.Instancia.Suscribir(this);

            CargarComboIdiomas_675MS();

            CargarComboRoles_675MS();

            CargarEmpleadosRoles_675MS();

            ActualizarBotones_675MS();

            CargarRolPermisos_675MS();
        }

        private void dgvRolEmp_675MS_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRolEmp_675MS.CurrentRow == null)
            {
                txtRolEmpleado_675MS.Clear();
                cmbRolEmpleado_675MS.SelectedIndex = -1;
                return;
            }

            DataGridViewRow fila_675MS = dgvRolEmp_675MS.CurrentRow;

            string nombre_675MS = Convert.ToString(fila_675MS.Cells["Nombre"].Value);

            string apellido_675MS =   Convert.ToString(fila_675MS.Cells["Apellido"].Value);

            txtRolEmpleado_675MS.Text =  nombre_675MS + " " + apellido_675MS;

            object idRol_675MS = fila_675MS.Cells["IdRol"].Value;

            if (idRol_675MS != null && idRol_675MS != DBNull.Value)
            {
                cmbRolEmpleado_675MS.SelectedValue = Convert.ToInt32(idRol_675MS);
            }
            else
            {
                cmbRolEmpleado_675MS.SelectedIndex = -1;
            }
        }
        private void tabRoles_675MS_Click(object sender, EventArgs e)
        {

        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            //Alta de nuevo rol
            if (tabRolesPermisos_675MS.SelectedTab == tabRoles_675MS)
            {
                AltaRol_675MS();
            }
            //Alta de nuevo permiso
            else if (tabRolesPermisos_675MS.SelectedTab == tabPermisos_675MS)
            {
                AltaPermiso_675MS();
            }
            //Relacionar el rol+permiso
            else if (tabRolesPermisos_675MS.SelectedTab == TabPermisoEmpleado_675MS)
            {
                AsignarRolEmpleado_675MS();
            }
            //Asignar rol al empleado
            else if (tabRolesPermisos_675MS.SelectedTab == tabRolPer_675MS)
            {
                AltaRolPermiso_675MS();
            }
        }

        private void btnModificar_675MS_Click(object sender, EventArgs e)
        {
            //Modificar rol
            if (tabRolesPermisos_675MS.SelectedTab == tabRoles_675MS)
            {
                ModificarRol_675MS();
            }
            //Modificar permiso
            else if (tabRolesPermisos_675MS.SelectedTab == tabPermisos_675MS)
            {
                ModificarPermiso_675MS();
            }
            //Modificar rol+permiso
            else if (tabRolesPermisos_675MS.SelectedTab == TabPermisoEmpleado_675MS)
            {
                ModificarRolEmpleado_675MS();
            }
            //Modificar rol + empleado
            else if (tabRolesPermisos_675MS.SelectedTab == tabRolPer_675MS)
            {
                ModificarRolPermiso_675MS();
            }
        }

        private void btnBaja_675MS_Click(object sender, EventArgs e)
        {
            if (tabRolesPermisos_675MS.SelectedTab == tabRoles_675MS)
            {
                BajaRol_675MS();
            }
            else if (tabRolesPermisos_675MS.SelectedTab == tabPermisos_675MS)
            {
                BajaPermiso_675MS();
            }
            else if (tabRolesPermisos_675MS.SelectedTab == tabRolPer_675MS)
            {
                BajaRolPermiso_675MS();
            }
        }

        private void dgvPermisos_675MS_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPermisos_675MS.CurrentRow != null)
            {
                txtNombrePermiso_675MS.Text = dgvPermisos_675MS.CurrentRow.Cells["Nombre_675MS"].Value?.ToString() ?? "";
            }
        }

        private void dgvRolEmp_675MS_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvRolEmp_675MS.CurrentRow == null)
                return;

            DataGridViewRow fila_675MS = dgvRolEmp_675MS.CurrentRow;

            string nombre_675MS = Convert.ToString(fila_675MS.Cells["Nombre"].Value);

            string apellido_675MS = Convert.ToString(fila_675MS.Cells["Apellido"].Value);

            txtRolEmpleado_675MS.Text = nombre_675MS + " " + apellido_675MS;

            object idRol_675MS = fila_675MS.Cells["IdRol"].Value;

            if (idRol_675MS != null && idRol_675MS != DBNull.Value)
            {
                cmbRolEmpleado_675MS.SelectedValue = Convert.ToInt32(idRol_675MS);
            }
            else
            {
                cmbRolEmpleado_675MS.SelectedIndex = -1;
            }
        }


        private void tabRolesPermisos_675MS_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            ActualizarBotones_675MS();

            try
            {
                if (tabRolesPermisos_675MS.SelectedTab == TabPermisoEmpleado_675MS)
                {
                    CargarComboRoles_675MS();
                    CargarEmpleadosRoles_675MS();
                }
                else if (tabRolesPermisos_675MS.SelectedTab == tabRolPer_675MS)
                {
                    CargarRolPermisos_675MS();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvRolPermiso_675MS_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvRolPermiso_675MS.CurrentRow == null)
                return;

            if (dgvRolPermiso_675MS.SelectedRows.Count == 0)
                return;

            if (cmbRolPermisos_675MS.DataSource == null || cmbPermisoRol_675MS.DataSource == null)
                return;

            DataGridViewRow fila_675MS = dgvRolPermiso_675MS.CurrentRow;

            if (fila_675MS.Cells["IdRolPadre"].Value == DBNull.Value || fila_675MS.Cells["IdPermiso"].Value == DBNull.Value)
                return;

            cmbRolPermisos_675MS.SelectedValue = Convert.ToInt32(fila_675MS.Cells["IdRolPadre"].Value);

            cmbPermisoRol_675MS.SelectedValue = Convert.ToInt32(fila_675MS.Cells["IdPermiso"].Value);
        }

        private void dgvRoles_675MS_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRoles_675MS.CurrentRow == null)
            {
                txtNombreRol_675MS.Clear();
                return;
            }

            object nombre_675MS = dgvRoles_675MS.CurrentRow.Cells["Nombre_675MS"].Value;

            txtNombreRol_675MS.Text = Convert.ToString(nombre_675MS);
        }

        //Metodos de refresh de pantalla
        private void ActualizarPantalla_675MS()
        {
            CargarRoles_675MS();
            CargarPermisos_675MS();
        }

        private void CargarPermisos_675MS()
        {
            permisoBLL_675MS = new PermisoBLL();
            List<PermisoBE> permisos_675MS = new List<PermisoBE>();
            permisos_675MS= permisoBLL_675MS.ListarPermisos_675MS();

            dgvPermisos_675MS.DataSource = null;
            dgvPermisos_675MS.DataSource = permisos_675MS;
            dgvPermisos_675MS.AutoGenerateColumns = true;

            // Verificamos que la grilla realmente tenga al menos 2 columnas generadas antes de tocarlas
            if (dgvPermisos_675MS.Columns.Count >= 2)
            {
                dgvPermisos_675MS.Columns[0].Visible = true;
                dgvPermisos_675MS.Columns[0].HeaderText = "Id Permiso";

                dgvPermisos_675MS.Columns[1].Visible = true;
                dgvPermisos_675MS.Columns[1].HeaderText = "Permiso / Familia";
            }

        }

        private void CargarRoles_675MS()
        {
            rolBLL_675MS = new RolBLL();

            List<RolBE> roles_675MS = new List<RolBE>(); 
            roles_675MS= rolBLL_675MS.ListarRoles_675MS();

             dgvRoles_675MS.DataSource = null;
             dgvRoles_675MS.DataSource = roles_675MS;

            dgvRoles_675MS.Columns["Id_675MS"].HeaderText = "Código de Rol";
            dgvRoles_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre del Rol";
        }

        private void CargarEmpleadosRoles_675MS()
        {
            UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();

            dgvRolEmp_675MS.DataSource = usuarioBLL_675MS.ListarEmpleadosRoles_675MS();

            dgvRolEmp_675MS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRolEmp_675MS.ReadOnly = true;
            dgvRolEmp_675MS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRolEmp_675MS.MultiSelect = false;
            dgvRolEmp_675MS.AllowUserToAddRows = false;
            dgvRolEmp_675MS.Columns["IdEmpleado"].Visible = false;
            dgvRolEmp_675MS.Columns["IdUsuario"].Visible = false;
            dgvRolEmp_675MS.Columns["IdRol"].Visible = false;
        }

        private void CargarRolPermisos_675MS()
        {
            try
            {
                RolBLL rolBLL_675MS = new RolBLL();
                PermisoBLL permisoBLL_675MS = new PermisoBLL();

                // Cargar ComboBox de roles
                cmbRolPermisos_675MS.DataSource = null;
                cmbRolPermisos_675MS.DisplayMember = "Nombre_675MS";
                cmbRolPermisos_675MS.ValueMember = "Id_675MS";
                cmbRolPermisos_675MS.DataSource = rolBLL_675MS.ListarRoles_675MS();

                // Cargar ComboBox de permisos
                cmbPermisoRol_675MS.DataSource = null;
                cmbPermisoRol_675MS.DisplayMember = "Nombre_675MS";
                cmbPermisoRol_675MS.ValueMember = "Id_675MS";
                cmbPermisoRol_675MS.DataSource = permisoBLL_675MS.ListarPermisos_675MS();

                // Cargar grilla
                dgvRolPermiso_675MS.DataSource = rolBLL_675MS.ListarRolPermisos_675MS();

                // Configurar grilla
                dgvRolPermiso_675MS.ReadOnly = true;
                dgvRolPermiso_675MS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRolPermiso_675MS.MultiSelect = false;
                dgvRolPermiso_675MS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Ocultar identificadores
                dgvRolPermiso_675MS.Columns["IdRolComponente"].Visible = false;
                dgvRolPermiso_675MS.Columns["IdRolPadre"].Visible = false;
                dgvRolPermiso_675MS.Columns["IdPermiso"].Visible = false;

                // Limpiar selección
                dgvRolPermiso_675MS.ClearSelection();
                cmbRolPermisos_675MS.SelectedIndex = -1;
                cmbPermisoRol_675MS.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar roles y permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboRoles_675MS()
        {
            RolBLL rolBLL_675MS = new RolBLL();

            cmbRolEmpleado_675MS.DataSource = null;

            cmbRolEmpleado_675MS.DisplayMember = "Nombre_675MS";
            cmbRolEmpleado_675MS.ValueMember = "Id_675MS";

            cmbRolEmpleado_675MS.DataSource = rolBLL_675MS.ListarRoles_675MS();

            cmbRolEmpleado_675MS.SelectedIndex = -1;
        }

        private void ActualizarBotones_675MS()
        {
            // Deshabilitar Baja en Rol + Empleado
            btnBaja_675MS.Enabled = tabRolesPermisos_675MS.SelectedIndex != 2;
        }


        //Pestaña Permisos
        private void AltaPermiso_675MS()
        {
            try
            {
                string nombre_675MS = txtNombrePermiso_675MS.Text.Trim();

                PermisoBLL permisoBLL_675MS = new PermisoBLL();

                int resultado_675MS = permisoBLL_675MS.AltaPermiso_675MS(nombre_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Permiso registrado correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNombrePermiso_675MS.Clear();

                    CargarPermisos_675MS();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BajaPermiso_675MS()
        {
            try
            {
                if (dgvPermisos_675MS.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un permiso.");
                    return;
                }

                int idPermiso_675MS = Convert.ToInt32(dgvPermisos_675MS.CurrentRow.Cells["Id_675MS"].Value);

                DialogResult respuesta_675MS = MessageBox.Show("¿Está seguro de que desea eliminar el permiso seleccionado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta_675MS != DialogResult.Yes)
                    return;

                PermisoBLL permisoBLL_675MS = new PermisoBLL();

                int resultado_675MS = permisoBLL_675MS.BajaPermiso_675MS(idPermiso_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Permiso eliminado correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombrePermiso_675MS.Clear();
                    CargarPermisos_675MS();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el permiso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarPermiso_675MS()
        {
            try
            {
                if (dgvPermisos_675MS.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un permiso.");
                    return;
                }

                int idPermiso_675MS = Convert.ToInt32(dgvPermisos_675MS.CurrentRow.Cells["Id_675MS"].Value);

                string nombre_675MS = txtNombrePermiso_675MS.Text.Trim();

                PermisoBLL permisoBLL_675MS = new PermisoBLL();

                int resultado_675MS = permisoBLL_675MS.ModificarPermiso_675MS(idPermiso_675MS, nombre_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Permiso modificado correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNombrePermiso_675MS.Clear();
                    CargarPermisos_675MS();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el permiso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Pestaña Rol + Empleado

        private void AsignarRolEmpleado_675MS()
        {
            try
            {
                if (dgvRolEmp_675MS.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un empleado.");
                    return;
                }

                object idUsuarioValor_675MS = dgvRolEmp_675MS.CurrentRow.Cells["IdUsuario"].Value;

                if (idUsuarioValor_675MS == null || idUsuarioValor_675MS == DBNull.Value)
                {
                    MessageBox.Show("El empleado seleccionado no tiene usuario registrado. " + "Primero debe crearle una cuenta desde Usuarios.");
                    return;
                }

                if (cmbRolEmpleado_675MS.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un rol.");
                    return;
                }

                int idUsuario_675MS = Convert.ToInt32(idUsuarioValor_675MS);

                int idRol_675MS = Convert.ToInt32(cmbRolEmpleado_675MS.SelectedValue);

                UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();

                int resultado_675MS = usuarioBLL_675MS.AsignarRolUsuario_675MS(idUsuario_675MS, idRol_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Rol asignado correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarEmpleadosRoles_675MS();
                }
                else
                {
                    MessageBox.Show("No se pudo asignar el rol.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarRolEmpleado_675MS()
        {
            AsignarRolEmpleado_675MS();
        }

        //Pestaña Rol + Permiso
        private void AltaRolPermiso_675MS()
        {
            try
            {
                if (cmbRolPermisos_675MS.SelectedIndex < 0 || cmbPermisoRol_675MS.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccioná un rol y un permiso.");
                    return;
                }

                int idRol_675MS =  Convert.ToInt32(cmbRolPermisos_675MS.SelectedValue);

                int idPermiso_675MS = Convert.ToInt32(cmbPermisoRol_675MS.SelectedValue);

                RolBLL rolBLL_675MS = new RolBLL();

                int resultado_675MS = rolBLL_675MS.AltaRolPermiso_675MS(idRol_675MS, idPermiso_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Permiso asignado correctamente.");
                    CargarRolPermisos_675MS();
                }
                else
                {
                    MessageBox.Show("No se pudo asignar el permiso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarRolPermiso_675MS()
        {
            try
            {
                if (dgvRolPermiso_675MS.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccioná una relación.");
                    return;
                }

                if (cmbRolPermisos_675MS.SelectedIndex < 0 ||  cmbPermisoRol_675MS.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccioná un rol y un permiso.");
                    return;
                }

                int idRelacion_675MS = Convert.ToInt32(dgvRolPermiso_675MS.SelectedRows[0].Cells["IdRolComponente"].Value);

                int idRol_675MS = Convert.ToInt32(cmbRolPermisos_675MS.SelectedValue);

                int idPermiso_675MS = Convert.ToInt32(cmbPermisoRol_675MS.SelectedValue);

                RolBLL rolBLL_675MS = new RolBLL();

                int resultado_675MS = rolBLL_675MS.ModificarRolPermiso_675MS(idRelacion_675MS, idRol_675MS, idPermiso_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Relación modificada correctamente.");
                    CargarRolPermisos_675MS();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar la relación.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BajaRolPermiso_675MS()
        {
            try
            {
                if (dgvRolPermiso_675MS.CurrentRow == null)
                {
                    MessageBox.Show("Seleccioná una relación para eliminar.");
                    return;
                }

                int idRelacion_675MS = Convert.ToInt32(dgvRolPermiso_675MS.CurrentRow.Cells["IdRolComponente"].Value);

                DialogResult respuesta_675MS = MessageBox.Show("¿Querés quitar este permiso del rol?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta_675MS != DialogResult.Yes)
                    return;

                RolBLL rolBLL_675MS = new RolBLL();

                int resultado_675MS = rolBLL_675MS.BajaRolPermiso_675MS(idRelacion_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Permiso quitado correctamente.");
                    CargarRolPermisos_675MS();
                }
                else
                {
                    MessageBox.Show("No se pudo quitar el permiso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Pestaña Roles

        private void AltaRol_675MS()
        {
            try
            {
                string nombre_675MS = txtNombreRol_675MS.Text.Trim();

                RolBLL rolBLL_675MS = new RolBLL();

                int resultado_675MS = rolBLL_675MS.AltaRol_675MS(nombre_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Rol registrado correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNombreRol_675MS.Clear();

                    CargarRoles_675MS();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarRol_675MS()
        {
            try
            {
                if (dgvRoles_675MS.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un rol.");
                    return;
                }

                int idRol_675MS = Convert.ToInt32(dgvRoles_675MS.CurrentRow.Cells["Id_675MS"].Value);

                string nombre_675MS = txtNombreRol_675MS.Text.Trim();

                RolBLL rolBLL_675MS = new RolBLL();

                int resultado_675MS = rolBLL_675MS.ModificarRol_675MS(idRol_675MS, nombre_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Rol modificado correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNombreRol_675MS.Clear();

                    CargarRoles_675MS();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el rol.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BajaRol_675MS()
        {
            try
            {
                if (dgvRoles_675MS.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un rol.");
                    return;
                }

                int idRol_675MS = Convert.ToInt32(dgvRoles_675MS.CurrentRow.Cells["Id_675MS"].Value);

                DialogResult respuesta_675MS = MessageBox.Show("¿Está seguro de que desea eliminar el rol seleccionado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta_675MS != DialogResult.Yes)
                    return;

                RolBLL rolBLL_675MS = new RolBLL();

                int resultado_675MS = rolBLL_675MS.BajaRol_675MS(idRol_675MS);

                if (resultado_675MS > 0)
                {
                    MessageBox.Show("Rol eliminado correctamente.");

                    txtNombreRol_675MS.Clear();
                    CargarRoles_675MS();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }


}
    

