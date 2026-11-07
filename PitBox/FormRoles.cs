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
        }

        private void ActualizarPantalla_675MS()
        {
            CargarRoles();
            CargarPermisos();
            //CargarRolPermiso();
        }

        //private void CargarRolPermiso()
        //{
        //    componenteBLL_675MS= new ComponenteBLL();
        //    List<ComponenteBE> componentes_675MS = componenteBLL_675MS.ListarComponentes_675MS();

        //    dgvComponentes_675MS.DataSource = null;
        //    dgvComponentes_675MS.DataSource = componentes_675MS;

        //    dgvRoles_675MS.Columns["IdRol_675MS"].HeaderText = "Código de Rol";
        //    dgvRoles_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre del Rol";
        //}

        private void CargarPermisos()
        {
            permisoBLL_675MS = new PermisoBLL();
            List<PermisoBE> permisos_675MS = new List<PermisoBE>();
            permisos_675MS= permisoBLL_675MS.ListarPermisos_675MS();

            dgvPermisos_675MS.DataSource = null;
            dgvPermisos_675MS.DataSource = permisoBLL_675MS;
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

        private void CargarRoles()
        {
            rolBLL_675MS = new RolBLL();

            List<RolBE> roles_675MS = new List<RolBE>(); 
            roles_675MS= rolBLL_675MS.ListarRoles_675MS();

             dgvRoles_675MS.DataSource = null;
             dgvRoles_675MS.DataSource = roles_675MS;

            dgvRoles_675MS.Columns["Id_675MS"].HeaderText = "Código de Rol";
            dgvRoles_675MS.Columns["Nombre_675MS"].HeaderText = "Nombre del Rol";
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

        private void tabRoles_675MS_Click(object sender, EventArgs e)
        {

        }

        private void btnVolver_675MS_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
