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

            // Cerramos el menú actual
            this.Close();
        }
    }
}
