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
    }
}
