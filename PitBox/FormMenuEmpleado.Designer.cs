namespace PitBox
{
    partial class FormMenuEmpleado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gesionarUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desbloquearUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarRolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altaEmpleadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altaDeClienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vehiculosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altaVehículoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarVTVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altaDeTurnoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idiomasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gesionarIdiomasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuariosToolStripMenuItem,
            this.empleadosToolStripMenuItem,
            this.clientesToolStripMenuItem,
            this.vehiculosToolStripMenuItem,
            this.turnosToolStripMenuItem,
            this.idiomasToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1134, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // usuariosToolStripMenuItem
            // 
            this.usuariosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gesionarUsuariosToolStripMenuItem,
            this.desbloquearUsuarioToolStripMenuItem,
            this.gestionarRolesToolStripMenuItem});
            this.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            this.usuariosToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.usuariosToolStripMenuItem.Text = "Usuarios";
            // 
            // gesionarUsuariosToolStripMenuItem
            // 
            this.gesionarUsuariosToolStripMenuItem.Name = "gesionarUsuariosToolStripMenuItem";
            this.gesionarUsuariosToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.gesionarUsuariosToolStripMenuItem.Text = "Gestionar Usuarios";
            this.gesionarUsuariosToolStripMenuItem.Click += new System.EventHandler(this.gesionarUsuariosToolStripMenuItem_Click);
            // 
            // desbloquearUsuarioToolStripMenuItem
            // 
            this.desbloquearUsuarioToolStripMenuItem.Name = "desbloquearUsuarioToolStripMenuItem";
            this.desbloquearUsuarioToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.desbloquearUsuarioToolStripMenuItem.Text = "Desbloquear Usuario";
            this.desbloquearUsuarioToolStripMenuItem.Click += new System.EventHandler(this.desbloquearUsuarioToolStripMenuItem_Click);
            // 
            // gestionarRolesToolStripMenuItem
            // 
            this.gestionarRolesToolStripMenuItem.Name = "gestionarRolesToolStripMenuItem";
            this.gestionarRolesToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.gestionarRolesToolStripMenuItem.Text = "Gestionar Roles";
            this.gestionarRolesToolStripMenuItem.Click += new System.EventHandler(this.gestionarRolesToolStripMenuItem_Click);
            // 
            // empleadosToolStripMenuItem
            // 
            this.empleadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.altaEmpleadoToolStripMenuItem});
            this.empleadosToolStripMenuItem.Name = "empleadosToolStripMenuItem";
            this.empleadosToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.empleadosToolStripMenuItem.Text = "Empleados";
            // 
            // altaEmpleadoToolStripMenuItem
            // 
            this.altaEmpleadoToolStripMenuItem.Name = "altaEmpleadoToolStripMenuItem";
            this.altaEmpleadoToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.altaEmpleadoToolStripMenuItem.Text = "Gestionar Empleados";
            this.altaEmpleadoToolStripMenuItem.Click += new System.EventHandler(this.altaEmpleadoToolStripMenuItem_Click);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.altaDeClienteToolStripMenuItem});
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.clientesToolStripMenuItem.Text = "Clientes";
            // 
            // altaDeClienteToolStripMenuItem
            // 
            this.altaDeClienteToolStripMenuItem.Name = "altaDeClienteToolStripMenuItem";
            this.altaDeClienteToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.altaDeClienteToolStripMenuItem.Text = "Gestionar Clientes";
            this.altaDeClienteToolStripMenuItem.Click += new System.EventHandler(this.altaDeClienteToolStripMenuItem_Click);
            // 
            // vehiculosToolStripMenuItem
            // 
            this.vehiculosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.altaVehículoToolStripMenuItem,
            this.gestionarVTVToolStripMenuItem});
            this.vehiculosToolStripMenuItem.Name = "vehiculosToolStripMenuItem";
            this.vehiculosToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.vehiculosToolStripMenuItem.Text = "Vehiculos";
            // 
            // altaVehículoToolStripMenuItem
            // 
            this.altaVehículoToolStripMenuItem.Name = "altaVehículoToolStripMenuItem";
            this.altaVehículoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.altaVehículoToolStripMenuItem.Text = "Gestionar Vehículo";
            this.altaVehículoToolStripMenuItem.Click += new System.EventHandler(this.altaVehículoToolStripMenuItem_Click);
            // 
            // gestionarVTVToolStripMenuItem
            // 
            this.gestionarVTVToolStripMenuItem.Name = "gestionarVTVToolStripMenuItem";
            this.gestionarVTVToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.gestionarVTVToolStripMenuItem.Text = "Gestionar VTV";
            this.gestionarVTVToolStripMenuItem.Click += new System.EventHandler(this.gestionarVTVToolStripMenuItem_Click);
            // 
            // turnosToolStripMenuItem
            // 
            this.turnosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.altaDeTurnoToolStripMenuItem});
            this.turnosToolStripMenuItem.Name = "turnosToolStripMenuItem";
            this.turnosToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.turnosToolStripMenuItem.Text = "Turnos";
            // 
            // altaDeTurnoToolStripMenuItem
            // 
            this.altaDeTurnoToolStripMenuItem.Name = "altaDeTurnoToolStripMenuItem";
            this.altaDeTurnoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.altaDeTurnoToolStripMenuItem.Text = "Gestionar Turnos";
            this.altaDeTurnoToolStripMenuItem.Click += new System.EventHandler(this.altaDeTurnoToolStripMenuItem_Click);
            // 
            // idiomasToolStripMenuItem
            // 
            this.idiomasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gesionarIdiomasToolStripMenuItem});
            this.idiomasToolStripMenuItem.Name = "idiomasToolStripMenuItem";
            this.idiomasToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.idiomasToolStripMenuItem.Text = "Idiomas";
            // 
            // gesionarIdiomasToolStripMenuItem
            // 
            this.gesionarIdiomasToolStripMenuItem.Name = "gesionarIdiomasToolStripMenuItem";
            this.gesionarIdiomasToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.gesionarIdiomasToolStripMenuItem.Text = "Gesionar Idiomas";
            this.gesionarIdiomasToolStripMenuItem.Click += new System.EventHandler(this.gesionarIdiomasToolStripMenuItem_Click);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // FormMenuEmpleado
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1134, 711);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMenuEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PitBox";
            this.Load += new System.EventHandler(this.FormMenuEmpleado_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem empleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vehiculosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altaEmpleadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altaDeClienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altaVehículoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altaDeTurnoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gesionarUsuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desbloquearUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarRolesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idiomasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gesionarIdiomasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarVTVToolStripMenuItem;
    }
}