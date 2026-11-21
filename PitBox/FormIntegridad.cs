using BE;
using BLL;
using Servicios.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PitBox
{
    public partial class FormIntegridad : Form, IObserverIdioma
    {
        public FormIntegridad()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        //Manejo de Idiomas
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        private void bntVerificar_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                IntegridadBLL integridad_675MS = new IntegridadBLL();
                List<ResultadoIntegridadBE> resultados_675MS = integridad_675MS.VerificarIntegridad_675MS();
                dgvIntegridad_675MS.DataSource = null;
                dgvIntegridad_675MS.DataSource = resultados_675MS;
                bool hayErrores_675MS = resultados_675MS.Exists(r => !r.Correcto_675MS);
                btnReparar_675MS.Enabled = hayErrores_675MS;
                MessageBox.Show(hayErrores_675MS ? "Se detectaron inconsistencias de integridad." : "La integridad es correcta.",
                    "Verificación de integridad", MessageBoxButtons.OK,
                    hayErrores_675MS ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                btnReparar_675MS.Enabled = false;
                MessageBox.Show("Error al verificar la integridad: " + ex.Message,"PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormIntegridad_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            ProtegerIntegridad();
            TratamientoIdioma.Instancia.Suscribir(this);
            CargarComboIdiomas_675MS();
        }

        private void ProtegerIntegridad()
        {
            try
            {
                if (!Servicios.SessionManager.SesionIniciada_675MS)
                    throw new UnauthorizedAccessException("Debe iniciar sesión.");

                UsuarioBE usuario_675MS = Servicios.SessionManager.ObtenerUsuarioActual_675MS();

                if (usuario_675MS == null || usuario_675MS.Rol_675MS == null || !new RolBLL().TienePermiso_675MS(usuario_675MS.Rol_675MS, "GestionIntegridad"))
                    throw new UnauthorizedAccessException("No tiene permiso para gestionar la integridad.");
                
                btnReparar_675MS.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new Action(() => this.Close()));
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
                    cmbIdiomas_675MS.SelectedValue = TratamientoIdioma.Instancia.IdiomaActual_675MS.Id_675MS;
                else if (idiomas_675MS.Count > 0)
                {
                    cmbIdiomas_675MS.SelectedIndex = 0;
                    TratamientoIdioma.Instancia.IdiomaActual_675MS = idiomas_675MS[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los idiomas: " + ex.Message);
            }
            finally
            {
                cargandoIdiomas_675MS = false;
            }
            ActualizarIdioma_675MS();
        }

        public void ActualizarIdioma_675MS()
        {
            if (TratamientoIdioma.Instancia.IdiomaActual_675MS == null) return;
            TraduccionBLL traduccionBLL_675MS = new TraduccionBLL();
            int idiomaSeleccionado_675MS = TratamientoIdioma.Instancia.IdiomaActual_675MS.Id_675MS;
            List<TraduccionBE> traducciones_675MS = traduccionBLL_675MS.Listar_675MS(idiomaSeleccionado_675MS);
            foreach (TraduccionBE traduccion_675MS in traducciones_675MS)
            {
                switch (traduccion_675MS.NombreControl_675MS)
                {
                    case "lblGestionIntegridad_675MS":
                        lblGestionIntegridad_675MS.Text = traduccion_675MS.Traduccion_675MS;
                        break;

                    case "bntVerificar_675MS":
                        bntVerificar_675MS.Text = traduccion_675MS.Traduccion_675MS;
                        break;

                    case "btnReparar_675MS":
                        btnReparar_675MS.Text = traduccion_675MS.Traduccion_675MS;
                        break;
                    case "btnVolver_675MS":
                        btnVolver_675MS.Text = traduccion_675MS.Traduccion_675MS;
                        break;
                    case "lblIdioma_675MS":
                        lblIdioma_675MS.Text = traduccion_675MS.Traduccion_675MS;
                        break;
                }
            }
        }

        private void dgvIntegridad_675MS_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void cmbIdiomas_675MS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoIdiomas_675MS || cmbIdiomas_675MS.SelectedItem == null) return;
            IdiomaBE idioma_675MS = cmbIdiomas_675MS.SelectedItem as IdiomaBE;
            if (idioma_675MS == null) return;
            TratamientoIdioma.Instancia.IdiomaActual_675MS = idioma_675MS;
            TratamientoIdioma.Instancia.Notificar();
        }

        private void btnReparar_675MS_Click(object sender, EventArgs e)
        {
            try
            {
                ResultadoIntegridadBE seleccionado_675MS = dgvIntegridad_675MS.CurrentRow == null ? null : dgvIntegridad_675MS.CurrentRow.DataBoundItem as ResultadoIntegridadBE;
                if (seleccionado_675MS == null || seleccionado_675MS.Correcto_675MS)
                {
                    MessageBox.Show("Seleccioná una fila con un error de integridad.", "PitBox");
                    return;
                }

                IntegridadBLL integridadBLL_675MS = new IntegridadBLL();
                BitacoraBLL bitacoraBLL_675MS = new BitacoraBLL();
                string usuario_675MS = Servicios.SessionManager.ObtenerUsuarioActual_675MS().Username_675MS;

                if (seleccionado_675MS.TipoDigito_675MS == "DVH")
                {
                    int dni_675MS;
                    if (!int.TryParse(seleccionado_675MS.Identificador_675MS, out dni_675MS))
                        throw new Exception("El DNI seleccionado no es válido.");

                    ClienteBE original_675MS = integridadBLL_675MS.BuscarClienteOriginal_675MS(dni_675MS);
 
                    DialogResult respuesta_675MS = MessageBox.Show("Se restaurarán los datos del cliente DNI " + dni_675MS + " desde su historial y se recalcularán los DVV.\n" +  "Nombre recuperado: " + original_675MS.Nombre_675MS + " " + original_675MS.Apellido_675MS +  "\n¿Confirmás la restauración?", "Recuperar cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                   
                    if (respuesta_675MS != DialogResult.Yes) return;

                    integridadBLL_675MS.RestaurarClienteDesdeHistorial_675MS(dni_675MS);
                    bitacoraBLL_675MS.Registrar_675MS(usuario_675MS, "RESTAURACION_CLIENTE", "Seguridad", "Se restauró el cliente DNI " + dni_675MS + " desde el historial y se actualizaron sus DVV.");
                    MessageBox.Show("Cliente restaurado y DVV actualizados correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (seleccionado_675MS.TipoDigito_675MS == "DVV")
                {
                    DialogResult respuesta_675MS = MessageBox.Show("¿Confirmás que revisaste los datos de los clientes?\n" +"Se recalcularán únicamente los DVV, sin modificar los DVH.", "Recuperar integridad", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (respuesta_675MS != DialogResult.Yes) return;

                    integridadBLL_675MS.RecuperarDigitosVerticalesCliente_675MS();
                    bitacoraBLL_675MS.Registrar_675MS(usuario_675MS, "REPARACION_INTEGRIDAD", "Seguridad","El administrador recuperó los DVV de Cliente sin modificar los DVH.");
                    MessageBox.Show("Los DVV fueron recuperados correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Seleccioná un error DVH o DVV.", "PitBox");
                    return;
                }
                btnReparar_675MS.Enabled = false;
                bntVerificar_675MS.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reparar la integridad: " + ex.Message,"PitBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
