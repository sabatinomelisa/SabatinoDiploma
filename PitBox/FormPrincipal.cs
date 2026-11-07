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
    public partial class FormPrincipal : Form, IObserverIdioma
    {
        List<IdiomaBE> idiomas_675MS = new List<IdiomaBE>();
        IdiomaBLL idiomaBLL_675MS = new IdiomaBLL();
        List<TraduccionBE> traducciones_675MS = new List<TraduccionBE>();
        private bool cargandoIdiomas_675MS = false;
        public FormPrincipal()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.BienvenidoPitBox;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {

            // Instamcio el formulario al que voy a pasar
            FormRegistro frmRegistro = new FormRegistro();

            // Oculto el FormPrincipal
            this.Hide();

            // Mostraar el nuevo formulario
            frmRegistro.ShowDialog(); 

        }

        private void btnSalir_675MS_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnIngresar_675MS_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();

            try
            {
                //Realizo el login con las credenciales ingresadas por el usuario
                usuarioBLL_675MS.Login_675MS(txtUsuario_675MS.Text, txtPassword_675MS.Text);
                // Instamcio el formulario al que voy a pasar
                FormMenuEmpleado frmMenu = new FormMenuEmpleado();

                // Oculto el FormPrincipal
                this.Hide();

                // Mostraar el nuevo formulario
                frmMenu.ShowDialog();

            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            //Suscribo al evento del observer
            TratamientoIdioma.Instancia.Suscribir(this);
  
            //Blanqueo campos
            txtPassword_675MS.Text=string.Empty;
            txtUsuario_675MS.Text = string.Empty;
            
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
                    case "lblUsuario_675MS":
                        lblUsuario_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "lblPassword_675MS":
                        lblPassword_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnIngresar_675MS":
                        btnIngresar_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "btnSalir_675MS":
                        btnSalir_675MS.Text = traduccion.Traduccion_675MS;
                        break;
                    case "bntRegistrar_675MS":
                        bntRegistrar_675MS.Text = traduccion.Traduccion_675MS;
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
    }
}
