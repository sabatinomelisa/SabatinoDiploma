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
    public partial class FormDesbloquear : Form,IObserverIdioma
    {
        // Instancio a la BLL
        UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;

        public FormDesbloquear()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.FondoRepetido;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void FormDesbloquear_Load(object sender, EventArgs e)
        {
            // Le saco los bordes y la barra de título superior
            this.FormBorderStyle = FormBorderStyle.None;

            // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
            this.Dock = DockStyle.Fill;

            //Suscribo al evento del observer
            TratamientoIdioma.Instancia.Suscribir(this);
            //Cargo combo con los idiomas del sistema
            CargarComboIdiomas_675MS();


            ActualizarPantalla_675MS();
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
                    case "lblDesbloqueo_675MS":
                        lblDesbloqueo_675MS.Text = traduccion.Traduccion_675MS;
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

        private void ActualizarPantalla_675MS()
        {
            // Traigo la lista de usuarios bloqueados desde la base de datos
            List<UsuarioBloqueadoBE> listaBloqueados = usuarioBLL_675MS.ObtenerUsuariosBloqueados();

            dgvDesbloquear_675MS.DataSource = null;
            dgvDesbloquear_675MS.DataSource = listaBloqueados;

            // Ocultas el ID usando el nombre correcto con el sufijo _675MS
            dgvDesbloquear_675MS.Columns["IdUsuario_675MS"].Visible = false;

            // Personalizas los títulos usando los nombres de propiedad correctos
            dgvDesbloquear_675MS.Columns["Usuario_675MS"].HeaderText = "Usuario";
            dgvDesbloquear_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre";
            dgvDesbloquear_675MS.Columns["Apellido_675MS"].HeaderText = "Apellido";
            dgvDesbloquear_675MS.Columns["Mail_675MS"].HeaderText = "Correo";
            dgvDesbloquear_675MS.Columns["DescripcionRol_675MS"].HeaderText = "Rol";
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            if (dgvDesbloquear_675MS.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Suponiendo que tu grilla tiene la propiedad IdUsuario_675MS
            int idUsuario_675MS = Convert.ToInt32(dgvDesbloquear_675MS.CurrentRow.Cells["IdUsuario_675MS"].Value);

            // Pasas el ID a la BLL
            int resultado_675MS = usuarioBLL_675MS.ActivarUsuario(idUsuario_675MS);

            if (resultado_675MS > 0)
            {
                MessageBox.Show("El usuario se reactivó correctamente.", "PitBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarPantalla_675MS();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar activar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
