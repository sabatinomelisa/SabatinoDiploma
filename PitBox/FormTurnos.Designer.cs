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
            this.dgvTurnos_675MS = new System.Windows.Forms.DataGridView();
            this.lblIdioma_675MS = new System.Windows.Forms.Label();
            this.cmbIdiomas_675MS = new System.Windows.Forms.ComboBox();
            this.lblGestorTurnos_675MS = new System.Windows.Forms.Label();
            this.lblDominio_675MS = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblDia_675MS = new System.Windows.Forms.Label();
            this.lblDuracion_675MS = new System.Windows.Forms.Label();
            this.txtDuracion_675MS = new System.Windows.Forms.TextBox();
            this.bntRegistrar_675MS = new System.Windows.Forms.Button();
            this.btnModificar_675MS = new System.Windows.Forms.Button();
            this.btnBaja_675MS = new System.Windows.Forms.Button();
            this.lblHora_675MS = new System.Windows.Forms.Label();
            this.cmbHorario_675MS = new System.Windows.Forms.ComboBox();
            this.cmbVehiculos_675MS = new System.Windows.Forms.ComboBox();
            this.btnVolver_675MS = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos_675MS)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTurnos_675MS
            // 
            this.dgvTurnos_675MS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTurnos_675MS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurnos_675MS.Location = new System.Drawing.Point(38, 225);
            this.dgvTurnos_675MS.Name = "dgvTurnos_675MS";
            this.dgvTurnos_675MS.RowHeadersWidth = 51;
            this.dgvTurnos_675MS.RowTemplate.Height = 24;
            this.dgvTurnos_675MS.Size = new System.Drawing.Size(661, 436);
            this.dgvTurnos_675MS.TabIndex = 0;
            this.dgvTurnos_675MS.SelectionChanged += new System.EventHandler(this.dgvTurnos_675MS_SelectionChanged);
            // 
            // lblIdioma_675MS
            // 
            this.lblIdioma_675MS.AutoSize = true;
            this.lblIdioma_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblIdioma_675MS.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdioma_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblIdioma_675MS.Location = new System.Drawing.Point(872, 17);
            this.lblIdioma_675MS.Name = "lblIdioma_675MS";
            this.lblIdioma_675MS.Size = new System.Drawing.Size(115, 35);
            this.lblIdioma_675MS.TabIndex = 13;
            this.lblIdioma_675MS.Text = "IDIOMA";
            // 
            // cmbIdiomas_675MS
            // 
            this.cmbIdiomas_675MS.FormattingEnabled = true;
            this.cmbIdiomas_675MS.Location = new System.Drawing.Point(969, 21);
            this.cmbIdiomas_675MS.Name = "cmbIdiomas_675MS";
            this.cmbIdiomas_675MS.Size = new System.Drawing.Size(141, 24);
            this.cmbIdiomas_675MS.TabIndex = 12;
            this.cmbIdiomas_675MS.SelectedIndexChanged += new System.EventHandler(this.cmbIdiomas_675MS_SelectedIndexChanged_1);
            // 
            // lblGestorTurnos_675MS
            // 
            this.lblGestorTurnos_675MS.AutoSize = true;
            this.lblGestorTurnos_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblGestorTurnos_675MS.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGestorTurnos_675MS.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblGestorTurnos_675MS.Location = new System.Drawing.Point(832, 225);
            this.lblGestorTurnos_675MS.Name = "lblGestorTurnos_675MS";
            this.lblGestorTurnos_675MS.Size = new System.Drawing.Size(265, 35);
            this.lblGestorTurnos_675MS.TabIndex = 11;
            this.lblGestorTurnos_675MS.Text = "GESTOR DE TURNOS";
            // 
            // lblDominio_675MS
            // 
            this.lblDominio_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblDominio_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDominio_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDominio_675MS.Location = new System.Drawing.Point(789, 315);
            this.lblDominio_675MS.Name = "lblDominio_675MS";
            this.lblDominio_675MS.Size = new System.Drawing.Size(82, 23);
            this.lblDominio_675MS.TabIndex = 16;
            this.lblDominio_675MS.Text = "Dominio";
            this.lblDominio_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(877, 344);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(220, 25);
            this.dateTimePicker1.TabIndex = 17;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // lblDia_675MS
            // 
            this.lblDia_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblDia_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDia_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDia_675MS.Location = new System.Drawing.Point(833, 344);
            this.lblDia_675MS.Name = "lblDia_675MS";
            this.lblDia_675MS.Size = new System.Drawing.Size(38, 23);
            this.lblDia_675MS.TabIndex = 18;
            this.lblDia_675MS.Text = "Día";
            this.lblDia_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDuracion_675MS
            // 
            this.lblDuracion_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblDuracion_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuracion_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDuracion_675MS.Location = new System.Drawing.Point(729, 370);
            this.lblDuracion_675MS.Name = "lblDuracion_675MS";
            this.lblDuracion_675MS.Size = new System.Drawing.Size(142, 23);
            this.lblDuracion_675MS.TabIndex = 20;
            this.lblDuracion_675MS.Text = "Duración Turno";
            this.lblDuracion_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // lblHora_675MS
            // 
            this.lblHora_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblHora_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblHora_675MS.Location = new System.Drawing.Point(768, 399);
            this.lblHora_675MS.Name = "lblHora_675MS";
            this.lblHora_675MS.Size = new System.Drawing.Size(103, 23);
            this.lblHora_675MS.TabIndex = 25;
            this.lblHora_675MS.Text = "Hora Inicio";
            this.lblHora_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // btnVolver_675MS
            // 
            this.btnVolver_675MS.BackColor = System.Drawing.Color.Plum;
            this.btnVolver_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver_675MS.Location = new System.Drawing.Point(877, 600);
            this.btnVolver_675MS.Name = "btnVolver_675MS";
            this.btnVolver_675MS.Size = new System.Drawing.Size(217, 30);
            this.btnVolver_675MS.TabIndex = 28;
            this.btnVolver_675MS.Text = "Volver";
            this.btnVolver_675MS.UseVisualStyleBackColor = false;
            this.btnVolver_675MS.Click += new System.EventHandler(this.btnVolver_675MS_Click);
            // 
            // FormTurnos
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1134, 711);
            this.Controls.Add(this.btnVolver_675MS);
            this.Controls.Add(this.cmbVehiculos_675MS);
            this.Controls.Add(this.cmbHorario_675MS);
            this.Controls.Add(this.lblHora_675MS);
            this.Controls.Add(this.btnBaja_675MS);
            this.Controls.Add(this.btnModificar_675MS);
            this.Controls.Add(this.bntRegistrar_675MS);
            this.Controls.Add(this.lblDuracion_675MS);
            this.Controls.Add(this.txtDuracion_675MS);
            this.Controls.Add(this.lblDia_675MS);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblDominio_675MS);
            this.Controls.Add(this.lblIdioma_675MS);
            this.Controls.Add(this.cmbIdiomas_675MS);
            this.Controls.Add(this.lblGestorTurnos_675MS);
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
        private System.Windows.Forms.Label lblIdioma_675MS;
        private System.Windows.Forms.ComboBox cmbIdiomas_675MS;
        private System.Windows.Forms.Label lblGestorTurnos_675MS;
        private System.Windows.Forms.Label lblDominio_675MS;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblDia_675MS;
        private System.Windows.Forms.Label lblDuracion_675MS;
        private System.Windows.Forms.TextBox txtDuracion_675MS;
        private System.Windows.Forms.Button bntRegistrar_675MS;
        private System.Windows.Forms.Button btnModificar_675MS;
        private System.Windows.Forms.Button btnBaja_675MS;
        private System.Windows.Forms.Label lblHora_675MS;
        private System.Windows.Forms.ComboBox cmbHorario_675MS;
        private System.Windows.Forms.ComboBox cmbVehiculos_675MS;
        private System.Windows.Forms.Button btnVolver_675MS;
    }
}