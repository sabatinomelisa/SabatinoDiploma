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
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
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
            Application.Exit();

        }

        private void btnIngresar_675MS_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();

            try
            {
                usuarioBLL_675MS.Login_675MS(txtUsuario_675MS.Text, txtPassword_675MS.Text);
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
