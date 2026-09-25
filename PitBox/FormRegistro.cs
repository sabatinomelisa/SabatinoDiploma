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
    public partial class FormRegistro : Form
    {
        public FormRegistro()
        {
            InitializeComponent();
        }

        private void bntRegistrar_675MS_Click(object sender, EventArgs e)
        {
            UsuarioBE usuario_675MS = new UsuarioBE();
            usuario_675MS.Empleado_675MS = new EmpleadoBE();

            bool errorValidacion = false;


            if (txtPassword_675MS.Text == string.Empty && !errorValidacion)
            {
                lblResultado_675MS.Text = "Ingresar Contraseña";
                errorValidacion = true;
            }

            if (txtxRepetirPass_675MS.Text == string.Empty)
            {
                lblResultado_675MS.Text = "Repetir Contraseña";
                errorValidacion = true;
            }

            if (txtPassword_675MS.Text != txtxRepetirPass_675MS.Text && !errorValidacion)
            {
                lblResultado_675MS.Text = "Las contraseñas no coinciden";
                errorValidacion = true;
            }

            if (txtDni_675MS.Text==string.Empty && !errorValidacion)
            {
                lblResultado_675MS.Text = "Ingresar DNI";
                errorValidacion = true;

            }

            if (txtUsuario_675MS.Text == string.Empty && !errorValidacion)
            {
                lblResultado_675MS.Text = "Ingresar Usuario";
                errorValidacion = true;

            }


            if(!errorValidacion)
            {
                usuario_675MS.Username_675MS = txtUsuario_675MS.Text;
                usuario_675MS.Password_675MS = txtPassword_675MS.Text;
                usuario_675MS.Empleado_675MS.DniEmpleado = Convert.ToInt32(txtDni_675MS.Text);

                UsuarioBLL usuarioBLL_675MS = new UsuarioBLL();

                int resultado = 0;
                try
                {
                    resultado = usuarioBLL_675MS.AltaUsuario_675MS(usuario_675MS);
                }
                catch (Exception ex)
                {
                    lblResultado_675MS.Text = ex.Message;
                }

                if (resultado == -1)
                {
                    lblResultado_675MS.Text = "Alta Incorrecta";
                }
                else
                {
                    lblResultado_675MS.Text = "Alta correcta";
                }
            }


        }
    }
}
