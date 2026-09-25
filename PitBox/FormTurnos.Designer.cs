namespace PitBox
{
    partial class FormTurnos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTurnos));
            this.dgvTurnos_675MS = new System.Windows.Forms.DataGridView();
            this.txtDNI_675MS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDNI_675MS = new System.Windows.Forms.Label();
            this.lblDominio_675MS = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDuracion = new System.Windows.Forms.Label();
            this.txtDuracion_675MS = new System.Windows.Forms.TextBox();
            this.bntRegistrar_675MS = new System.Windows.Forms.Button();
            this.btnModificar_675MS = new System.Windows.Forms.Button();
            this.btnBaja_675MS = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbHorario_675MS = new System.Windows.Forms.ComboBox();
            this.cmbVehiculos_675MS = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos_675MS)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTurnos_675MS
            // 
            this.dgvTurnos_675MS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTurnos_675MS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurnos_675MS.Location = new System.Drawing.Point(38, 225);
            this.dgvTurnos_675MS.Name = "dgvTurnos_675MS";
            this.dgvTurnos_675MS.RowTemplate.Height = 24;
            this.dgvTurnos_675MS.Size = new System.Drawing.Size(661, 436);
            this.dgvTurnos_675MS.TabIndex = 0;
            this.dgvTurnos_675MS.SelectionChanged += new System.EventHandler(this.dgvTurnos_675MS_SelectionChanged);
            // 
            // txtDNI_675MS
            // 
            this.txtDNI_675MS.Location = new System.Drawing.Point(877, 287);
            this.txtDNI_675MS.Name = "txtDNI_675MS";
            this.txtDNI_675MS.Size = new System.Drawing.Size(217, 22);
            this.txtDNI_675MS.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(872, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 28);
            this.label2.TabIndex = 13;
            this.label2.Text = "IDIOMA";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(969, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(141, 24);
            this.comboBox1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(876, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 28);
            this.label1.TabIndex = 11;
            this.label1.Text = "GESTOR DE TURNOS";
            // 
            // lblDNI_675MS
            // 
            this.lblDNI_675MS.AutoSize = true;
            this.lblDNI_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblDNI_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDNI_675MS.Location = new System.Drawing.Point(778, 290);
            this.lblDNI_675MS.Name = "lblDNI_675MS";
            this.lblDNI_675MS.Size = new System.Drawing.Size(88, 19);
            this.lblDNI_675MS.TabIndex = 14;
            this.lblDNI_675MS.Text = "DNI Cliente";
            // 
            // lblDominio_675MS
            // 
            this.lblDominio_675MS.AutoSize = true;
            this.lblDominio_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblDominio_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDominio_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDominio_675MS.Location = new System.Drawing.Point(798, 318);
            this.lblDominio_675MS.Name = "lblDominio_675MS";
            this.lblDominio_675MS.Size = new System.Drawing.Size(68, 19);
            this.lblDominio_675MS.TabIndex = 16;
            this.lblDominio_675MS.Text = "Dominio";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(877, 344);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(220, 22);
            this.dateTimePicker1.TabIndex = 17;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(827, 347);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "Día";
            // 
            // lblDuracion
            // 
            this.lblDuracion.AutoSize = true;
            this.lblDuracion.BackColor = System.Drawing.Color.Transparent;
            this.lblDuracion.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuracion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDuracion.Location = new System.Drawing.Point(748, 373);
            this.lblDuracion.Name = "lblDuracion";
            this.lblDuracion.Size = new System.Drawing.Size(118, 19);
            this.lblDuracion.TabIndex = 20;
            this.lblDuracion.Text = "Duración Turno";
            // 
            // txtDuracion_675MS
            // 
            this.txtDuracion_675MS.Location = new System.Drawing.Point(877, 372);
            this.txtDuracion_675MS.Name = "txtDuracion_675MS";
            this.txtDuracion_675MS.Size = new System.Drawing.Size(217, 22);
            this.txtDuracion_675MS.TabIndex = 19;
            // 
            // bntRegistrar_675MS
            // 
            this.bntRegistrar_675MS.BackColor = System.Drawing.Color.Aquamarine;
            this.bntRegistrar_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntRegistrar_675MS.Location = new System.Drawing.Point(877, 507);
            this.bntRegistrar_675MS.Name = "bntRegistrar_675MS";
            this.bntRegistrar_675MS.Size = new System.Drawing.Size(217, 30);
            this.bntRegistrar_675MS.TabIndex = 21;
            this.bntRegistrar_675MS.Text = "Registrar";
            this.bntRegistrar_675MS.UseVisualStyleBackColor = false;
            this.bntRegistrar_675MS.Click += new System.EventHandler(this.bntRegistrar_675MS_Click);
            // 
            // btnModificar_675MS
            // 
            this.btnModificar_675MS.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnModificar_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar_675MS.Location = new System.Drawing.Point(877, 537);
            this.btnModificar_675MS.Name = "btnModificar_675MS";
            this.btnModificar_675MS.Size = new System.Drawing.Size(217, 30);
            this.btnModificar_675MS.TabIndex = 22;
            this.btnModificar_675MS.Text = "Reprogramar Turno";
            this.btnModificar_675MS.UseVisualStyleBackColor = false;
            this.btnModificar_675MS.Click += new System.EventHandler(this.btnModificar_675MS_Click);
            // 
            // btnBaja_675MS
            // 
            this.btnBaja_675MS.BackColor = System.Drawing.Color.MistyRose;
            this.btnBaja_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaja_675MS.Location = new System.Drawing.Point(877, 568);
            this.btnBaja_675MS.Name = "btnBaja_675MS";
            this.btnBaja_675MS.Size = new System.Drawing.Size(217, 30);
            this.btnBaja_675MS.TabIndex = 23;
            this.btnBaja_675MS.Text = "Baja";
            this.btnBaja_675MS.UseVisualStyleBackColor = false;
            this.btnBaja_675MS.Click += new System.EventHandler(this.btnBaja_675MS_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(781, 402);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 19);
            this.label4.TabIndex = 25;
            this.label4.Text = "Hora Inicio";
            // 
            // cmbHorario_675MS
            // 
            this.cmbHorario_675MS.FormattingEnabled = true;
            this.cmbHorario_675MS.Location = new System.Drawing.Point(877, 399);
            this.cmbHorario_675MS.Name = "cmbHorario_675MS";
            this.cmbHorario_675MS.Size = new System.Drawing.Size(109, 24);
            this.cmbHorario_675MS.TabIndex = 26;
            // 
            // cmbVehiculos_675MS
            // 
            this.cmbVehiculos_675MS.FormattingEnabled = true;
            this.cmbVehiculos_675MS.Location = new System.Drawing.Point(877, 314);
            this.cmbVehiculos_675MS.Name = "cmbVehiculos_675MS";
            this.cmbVehiculos_675MS.Size = new System.Drawing.Size(217, 24);
            this.cmbVehiculos_675MS.TabIndex = 27;
            // 
            // FormTurnos
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1134, 711);
            this.Controls.Add(this.cmbVehiculos_675MS);
            this.Controls.Add(this.cmbHorario_675MS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBaja_675MS);
            this.Controls.Add(this.btnModificar_675MS);
            this.Controls.Add(this.bntRegistrar_675MS);
            this.Controls.Add(this.lblDuracion);
            this.Controls.Add(this.txtDuracion_675MS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblDominio_675MS);
            this.Controls.Add(this.lblDNI_675MS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDNI_675MS);
            this.Controls.Add(this.dgvTurnos_675MS);
            this.Name = "FormTurnos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Turnos";
            this.Load += new System.EventHandler(this.FormTurnos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos_675MS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTurnos_675MS;
        private System.Windows.Forms.TextBox txtDNI_675MS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDNI_675MS;
        private System.Windows.Forms.Label lblDominio_675MS;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDuracion;
        private System.Windows.Forms.TextBox txtDuracion_675MS;
        private System.Windows.Forms.Button bntRegistrar_675MS;
        private System.Windows.Forms.Button btnModificar_675MS;
        private System.Windows.Forms.Button btnBaja_675MS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbHorario_675MS;
        private System.Windows.Forms.ComboBox cmbVehiculos_675MS;
    }
}