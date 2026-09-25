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
    public partial class FormUsuarios : Form
    {
        public FormUsuarios()
        {
            InitializeComponent();
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            // Le saco los bordes y la barra de título superior
            this.FormBorderStyle = FormBorderStyle.None;

            // Hago que ocupe todo el espacio disponible dentro del contenedor MDI sin salirse
            this.Dock = DockStyle.Fill;
        }
    }
}
