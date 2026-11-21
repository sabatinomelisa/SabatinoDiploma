using BE;
using BLL;
using Servicios;
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
    public partial class FormMenuEmpleado : Form
    {
        public FormMenuEmpleado()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoPrincipal;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void vTVToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void consultarPorDNIToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void altaDeTurnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTurnos formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormTurnos)
                {
                    formExistente_675MS = (FormTurnos)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormTurnos formTurnos_675MS = new FormTurnos();

                // Lo vinculo al contenedor MDI 
                formTurnos_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formTurnos_675MS.Show();
            }
        }

        private void altaVehículoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormVehiculos formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormVehiculos)
                {
                    formExistente_675MS = (FormVehiculos)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormVehiculos formVehiculos_675MS = new FormVehiculos();

                // Lo vinculo al contenedor MDI 
                formVehiculos_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formVehiculos_675MS.Show();
            }

        }

        private void gesionarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsuarios formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormUsuarios)
                {
                    formExistente_675MS = (FormUsuarios)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormUsuarios formUsuarios_675MS = new FormUsuarios();

                // Lo vinculo al contenedor MDI 
                formUsuarios_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formUsuarios_675MS.Show();
            }
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPrincipal formPrincipal_675MS = null;

            // Buscar si el FormPrincipal ya está abierto en la memoria
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormPrincipal)
                {
                    formPrincipal_675MS = (FormPrincipal)formulario_675MS;
                    break; // Si lo encuentro se corta la búsqueda
                }
            }

            if (formPrincipal_675MS != null)
            {
                // Si ya existía (estaba oculto), se muestra
                formPrincipal_675MS.Show();
            }
            else
            {
                // Si no existía, se instancia
                FormPrincipal nuevoPrincipal_675MS = new FormPrincipal();
                nuevoPrincipal_675MS.Show();
            }

            try
            {
                // Cierro la instancia de la sesión actual
                SessionManager.Logout_675MS();

                // Cierro el formulario actual
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Cierre de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void altaDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClientes formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormClientes)
                {
                    formExistente_675MS = (FormClientes)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormClientes formUsuarios_675MS = new FormClientes();

                // Lo vinculo al contenedor MDI 
                formUsuarios_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formUsuarios_675MS.Show();
            }
        }

        private void FormMenuEmpleado_Load(object sender, EventArgs e)
        {
            FiltrarMenuPorPermisos_675MS();

        }

        private void gestionarRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRoles formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormRoles)
                {
                    formExistente_675MS = (FormRoles)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormRoles formRoles_675MS = new FormRoles();

                // Lo vinculo al contenedor MDI 
                formRoles_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formRoles_675MS.Show();
            }
        }

        private void desbloquearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDesbloquear formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormDesbloquear)
                {
                    formExistente_675MS = (FormDesbloquear)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormDesbloquear formDesbloquear_675MS = new FormDesbloquear();

                // Lo vinculo al contenedor MDI 
                formDesbloquear_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formDesbloquear_675MS.Show();
            }

        }

        private void altaEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEmpleado formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormClientes)
                {
                    formExistente_675MS = (FormEmpleado)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormEmpleado formUsuarios_675MS = new FormEmpleado();

                // Lo vinculo al contenedor MDI 
                formUsuarios_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formUsuarios_675MS.Show();
            }
        }

        private void gesionarIdiomasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIdiomas formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormIdiomas)
                {
                    formExistente_675MS = (FormIdiomas)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormIdiomas formIdiomas_675MS = new FormIdiomas();

                // Lo vinculo al contenedor MDI 
                formIdiomas_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formIdiomas_675MS.Show();
            }
        }

        private void gestionarVTVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVTV formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormVTV)
                {
                    formExistente_675MS = (FormVTV)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormVTV formVTV_675MS = new FormVTV();

                // Lo vinculo al contenedor MDI 
                formVTV_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formVTV_675MS.Show();
            }
        }



        private void FiltrarMenuPorPermisos_675MS()
        {
            UsuarioBE usuario_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (usuario_675MS == null || usuario_675MS.Rol_675MS == null)
            {
                MessageBox.Show("No se pudo recuperar el rol del usuario.");
                usuariosToolStripMenuItem.Visible = false;
                empleadosToolStripMenuItem.Visible = false;
                clientesToolStripMenuItem.Visible = false;
                vehiculosToolStripMenuItem.Visible = false;
                turnosToolStripMenuItem.Visible = false;
                idiomasToolStripMenuItem.Visible = false;
                gestionarVTVToolStripMenuItem.Visible = false;
                gestionarRolesToolStripMenuItem.Visible = false;
                integridadToolStripMenuItem.Visible = false;
                historialDeCambiosToolStripMenuItem.Visible = false;
                consultaDeBitácoraToolStripMenuItem.Visible = false;
                generarBackupToolStripMenuItem.Visible = false;
                return;
            }

            RolBLL rolBLL_675MS = new RolBLL();
            RolBE rol_675MS = usuario_675MS.Rol_675MS;

            bool permisoUsuarios_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "Usuarios");
            bool permisoEmpleados_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "Empleados");
            bool permisoClientes_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "Clientes");
            bool permisoVehiculos_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "Vehiculos");
            bool permisoTurnos_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "Turnos");
            bool permisoVTV_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "VTV");
            bool permisoIntegridad_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "GestionIntegridad");
            bool permisoIdiomas_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "Idiomas");
            bool permisoRoles_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "GestionRoles");
            bool permisoBitacora_675MS = rolBLL_675MS.TienePermiso_675MS(rol_675MS, "ConsultarBitacora");


            usuariosToolStripMenuItem.Visible = permisoUsuarios_675MS;
            empleadosToolStripMenuItem.Visible = permisoEmpleados_675MS;
            clientesToolStripMenuItem.Visible = permisoClientes_675MS;
            vehiculosToolStripMenuItem.Visible = permisoVehiculos_675MS || permisoVTV_675MS;
            turnosToolStripMenuItem.Visible = permisoTurnos_675MS;
            gestionarVTVToolStripMenuItem.Visible = permisoVTV_675MS;
            altaVehículoToolStripMenuItem.Visible = permisoVehiculos_675MS;
            integridadToolStripMenuItem.Visible = permisoIntegridad_675MS;
            historialDeCambiosToolStripMenuItem.Visible = permisoBitacora_675MS;
            idiomasToolStripMenuItem.Visible = permisoIdiomas_675MS;
            gestionarRolesToolStripMenuItem.Visible = permisoRoles_675MS;
            consultaDeBitácoraToolStripMenuItem.Visible = permisoBitacora_675MS;
            generarBackupToolStripMenuItem.Visible = permisoIntegridad_675MS;
        }

        private void gestionarIntegridadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Verifico que el usuario tenga permiso para gestionar integridad
            UsuarioBE usuario_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (!SessionManager.SesionIniciada_675MS || usuario_675MS == null ||  usuario_675MS.Rol_675MS == null || !new RolBLL().TienePermiso_675MS(usuario_675MS.Rol_675MS, "GestionIntegridad"))
            {
                MessageBox.Show( "No tiene permiso para acceder al control de integridad.", "Acceso denegado",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                return;
            }


            FormIntegridad formExistente_675MS = null;

            // Buscamos si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormIntegridad)
                {
                    formExistente_675MS = (FormIntegridad)formulario_675MS;
                    break;
                }
            }

            if (formExistente_675MS != null)
            {
                // Si ya está abierto, lo traemos al frente
                formExistente_675MS.BringToFront();
            }
            else
            {
                // Si no está abierto, lo creo
                FormIntegridad formIntegridad_675MS = new FormIntegridad();

                // Lo vinculo al contenedor MDI
                formIntegridad_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formIntegridad_675MS.Show();
            }
        }

        private void historialDeCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Verificamos que el usuario tenga permiso para gestionar integridad
            UsuarioBE usuario_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (!SessionManager.SesionIniciada_675MS || usuario_675MS == null || usuario_675MS.Rol_675MS == null || !new RolBLL().TienePermiso_675MS(usuario_675MS.Rol_675MS, "ConsultarBitacora"))
            {
                MessageBox.Show("No tiene permiso para consultar el historial de cambios.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormHistorialCliente formExistente_675MS = null;

            // Buscamos si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormHistorialCliente)
                {
                    formExistente_675MS = (FormHistorialCliente)formulario_675MS;
                    break;
                }
            }

            if (formExistente_675MS != null)
            {
                // Si ya está abierto, lo traemos al frente
                formExistente_675MS.BringToFront();
            }
            else
            {
                // Si no está abierto, lo creo
                FormHistorialCliente formHistorial_675MS = new FormHistorialCliente();

                // Lo vinculo al contenedor MDI
                formHistorial_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formHistorial_675MS.Show();
            }
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta_675MS = System.IO.Path.Combine(Application.StartupPath, "ManualUsuario.pdf");

                if (!System.IO.File.Exists(ruta_675MS))
                {
                    MessageBox.Show("No se encontró el manual de usuario.", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                System.Diagnostics.Process.Start(ruta_675MS);
            }
            catch (Exception ex_675MS)
            {
                MessageBox.Show("No se pudo abrir el manual: " + ex_675MS.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reporteInteligenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReporteInteligente formExistente_675MS = null;
            // Buscamos manualmente si el formulario ya se encuentra abierto
            foreach (Form formulario_675MS in Application.OpenForms)
            {
                if (formulario_675MS is FormReporteInteligente)
                {
                    formExistente_675MS = (FormReporteInteligente)formulario_675MS;
                    break;
                }
            }
            if (formExistente_675MS != null)
            {
                // Si ya hay uno abiert, lo traemos al frente para que el usuario no pierda el foco
                formExistente_675MS.BringToFront();
            }
            else
            {
                //Si no está abierto lo creo
                FormReporteInteligente formReporteInteligente_675MS = new FormReporteInteligente();

                // Lo vinculo al contenedor MDI 
                formReporteInteligente_675MS.MdiParent = this;

                // Mostrar dentro del contenedor
                formReporteInteligente_675MS.Show();
            }
        }

        private void consultaDeBitácoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuarioBE usuario_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (!SessionManager.SesionIniciada_675MS || usuario_675MS == null || usuario_675MS.Rol_675MS == null || !new RolBLL().TienePermiso_675MS(usuario_675MS.Rol_675MS, "ConsultarBitacora"))
            {
                MessageBox.Show("No tiene permiso para consultar la bitácora.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Form formulario_675MS in this.MdiChildren)
            {
                if (formulario_675MS is FormBitacora)
                {
                    formulario_675MS.BringToFront();
                    return;
                }
            }

            FormBitacora formBitacora_675MS = new FormBitacora();
            formBitacora_675MS.MdiParent = this;
            formBitacora_675MS.Show();
        }

        private void generarBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuarioBE usuario_675MS = SessionManager.ObtenerUsuarioActual_675MS();

            if (!SessionManager.SesionIniciada_675MS || usuario_675MS == null || usuario_675MS.Rol_675MS == null || !new RolBLL().TienePermiso_675MS(usuario_675MS.Rol_675MS, "GestionIntegridad"))
            {
                MessageBox.Show("No tiene permiso para gestionar copias de seguridad.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Form formulario_675MS in this.MdiChildren)
            {
                if (formulario_675MS is FormBackupRestore)
                {
                    formulario_675MS.BringToFront();
                    return;
                }
            }

            FormBackupRestore formBackup_675MS = new FormBackupRestore();
            formBackup_675MS.MdiParent = this;
            formBackup_675MS.Show();

        }
    }
    
}
