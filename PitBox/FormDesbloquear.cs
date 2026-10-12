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
    public partial class FormDesbloquear : Form
    {
        // Instancio a la BLL
        UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();
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

            ActualizarPantalla_675MS();
        }

        private void ActualizarPantalla_675MS()
        {
            // Traigo la lista de usuarios bloqueados desde la base de datos
            List<UsuarioBloqueadoBE> listaBloqueados = usuarioBLL_675MS.ObtenerUsuariosBloqueados();

            dgvUsuarios_675MS.DataSource = null;
            dgvUsuarios_675MS.DataSource = listaBloqueados;

            // Ocultas el ID usando el nombre correcto con el sufijo _675MS
            dgvUsuarios_675MS.Columns["IdUsuario_675MS"].Visible = false;

            // Personalizas los títulos usando los nombres de propiedad correctos
            dgvUsuarios_675MS.Columns["Usuario_675MS"].HeaderText = "Usuario";
            dgvUsuarios_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre";
            dgvUsuarios_675MS.Columns["Apellido_675MS"].HeaderText = "Apellido";
            dgvUsuarios_675MS.Columns["Mail_675MS"].HeaderText = "Correo";
            dgvUsuarios_675MS.Columns["DescripcionRol_675MS"].HeaderText = "Rol";
        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {

        }
    }
}
