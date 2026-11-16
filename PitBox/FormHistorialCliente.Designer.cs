namespace PitBox
{
    partial class FormHistorialCliente
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
            this.lblIdioma_675MS = new System.Windows.Forms.Label();
            this.cmbIdiomas_675MS = new System.Windows.Forms.ComboBox();
            this.btnVolver_675MS = new System.Windows.Forms.Button();
            this.bntConsultar_675MS = new System.Windows.Forms.Button();
            this.lblhistorial_675MS = new System.Windows.Forms.Label();
            this.dgvHistorial_675MS = new System.Windows.Forms.DataGridView();
            this.txtDNI_675MS = new System.Windows.Forms.TextBox();
            this.lblDNI_675MS = new System.Windows.Forms.Label();
            this.lblHasta_675MS = new System.Windows.Forms.Label();
            this.lblDesde_675MS = new System.Windows.Forms.Label();
            this.dtpDesde_675MS = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta_675MS = new System.Windows.Forms.DateTimePicker();
            this.btnLimpiar_675MS = new System.Windows.Forms.Button();
            this.btnActivar_675MS = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial_675MS)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIdioma_675MS
            // 
            this.lblIdioma_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblIdioma_675MS.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdioma_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblIdioma_675MS.Location = new System.Drawing.Point(794, 14);
            this.lblIdioma_675MS.Name = "lblIdioma_675MS";
            this.lblIdioma_675MS.Size = new System.Drawing.Size(155, 35);
            this.lblIdioma_675MS.TabIndex = 121;
            this.lblIdioma_675MS.Text = "IDIOMA";
            this.lblIdioma_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbIdiomas_675MS
            // 
            this.cmbIdiomas_675MS.FormattingEnabled = true;
            this.cmbIdiomas_675MS.Location = new System.Drawing.Point(972, 23);
            this.cmbIdiomas_675MS.Name = "cmbIdiomas_675MS";
            this.cmbIdiomas_675MS.Size = new System.Drawing.Size(141, 24);
            this.cmbIdiomas_675MS.TabIndex = 120;
            this.cmbIdiomas_675MS.SelectedIndexChanged += new System.EventHandler(this.cmbIdiomas_675MS_SelectedIndexChanged);
            // 
            // btnVolver_675MS
            // 
            this.btnVolver_675MS.BackColor = System.Drawing.Color.Salmon;
            this.btnVolver_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver_675MS.Location = new System.Drawing.Point(909, 489);
            this.btnVolver_675MS.Name = "btnVolver_675MS";
            this.btnVolver_675MS.Size = new System.Drawing.Size(204, 30);
            this.btnVolver_675MS.TabIndex = 119;
            this.btnVolver_675MS.Text = "Volver";
            this.btnVolver_675MS.UseVisualStyleBackColor = false;
            this.btnVolver_675MS.Click += new System.EventHandler(this.btnVolver_675MS_Click);
            // 
            // bntConsultar_675MS
            // 
            this.bntConsultar_675MS.BackColor = System.Drawing.Color.PaleTurquoise;
            this.bntConsultar_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntConsultar_675MS.Location = new System.Drawing.Point(909, 384);
            this.bntConsultar_675MS.Name = "bntConsultar_675MS";
            this.bntConsultar_675MS.Size = new System.Drawing.Size(204, 30);
            this.bntConsultar_675MS.TabIndex = 117;
            this.bntConsultar_675MS.Text = "Consultar";
            this.bntConsultar_675MS.UseVisualStyleBackColor = false;
            this.bntConsultar_675MS.Click += new System.EventHandler(this.bntConsultar_675MS_Click);
            // 
            // lblhistorial_675MS
            // 
            this.lblhistorial_675MS.AutoSize = true;
            this.lblhistorial_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblhistorial_675MS.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhistorial_675MS.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblhistorial_675MS.Location = new System.Drawing.Point(794, 187);
            this.lblhistorial_675MS.Name = "lblhistorial_675MS";
            this.lblhistorial_675MS.Size = new System.Drawing.Size(316, 35);
            this.lblhistorial_675MS.TabIndex = 116;
            this.lblhistorial_675MS.Text = "HISTORIAL DE CLIENTES";
            this.lblhistorial_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dgvHistorial_675MS
            // 
            this.dgvHistorial_675MS.AllowUserToAddRows = false;
            this.dgvHistorial_675MS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial_675MS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvHistorial_675MS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial_675MS.Location = new System.Drawing.Point(12, 251);
            this.dgvHistorial_675MS.Name = "dgvHistorial_675MS";
            this.dgvHistorial_675MS.ReadOnly = true;
            this.dgvHistorial_675MS.RowHeadersWidth = 51;
            this.dgvHistorial_675MS.RowTemplate.Height = 24;
            this.dgvHistorial_675MS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial_675MS.Size = new System.Drawing.Size(709, 311);
            this.dgvHistorial_675MS.TabIndex = 115;
            this.dgvHistorial_675MS.SelectionChanged += new System.EventHandler(this.dgvHistorial_675MS_SelectionChanged);
            // 
            // txtDNI_675MS
            // 
            this.txtDNI_675MS.Location = new System.Drawing.Point(910, 251);
            this.txtDNI_675MS.Name = "txtDNI_675MS";
            this.txtDNI_675MS.Size = new System.Drawing.Size(199, 22);
            this.txtDNI_675MS.TabIndex = 122;
            // 
            // lblDNI_675MS
            // 
            this.lblDNI_675MS.AutoSize = true;
            this.lblDNI_675MS.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI_675MS.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDNI_675MS.Location = new System.Drawing.Point(748, 251);
            this.lblDNI_675MS.Name = "lblDNI_675MS";
            this.lblDNI_675MS.Size = new System.Drawing.Size(155, 25);
            this.lblDNI_675MS.TabIndex = 123;
            this.lblDNI_675MS.Text = "DNI (opcional):";
            this.lblDNI_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblHasta_675MS
            // 
            this.lblHasta_675MS.AutoSize = true;
            this.lblHasta_675MS.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta_675MS.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblHasta_675MS.Location = new System.Drawing.Point(730, 343);
            this.lblHasta_675MS.Name = "lblHasta_675MS";
            this.lblHasta_675MS.Size = new System.Drawing.Size(173, 25);
            this.lblHasta_675MS.TabIndex = 124;
            this.lblHasta_675MS.Text = "Hasta (opcional):";
            this.lblHasta_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDesde_675MS
            // 
            this.lblDesde_675MS.AutoSize = true;
            this.lblDesde_675MS.Font = new System.Drawing.Font("Segoe UI Black", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde_675MS.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDesde_675MS.Location = new System.Drawing.Point(727, 294);
            this.lblDesde_675MS.Name = "lblDesde_675MS";
            this.lblDesde_675MS.Size = new System.Drawing.Size(176, 25);
            this.lblDesde_675MS.TabIndex = 125;
            this.lblDesde_675MS.Text = "Desde (opcional):";
            this.lblDesde_675MS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dtpDesde_675MS
            // 
            this.dtpDesde_675MS.Location = new System.Drawing.Point(909, 297);
            this.dtpDesde_675MS.Name = "dtpDesde_675MS";
            this.dtpDesde_675MS.Size = new System.Drawing.Size(200, 22);
            this.dtpDesde_675MS.TabIndex = 126;
            // 
            // dtpHasta_675MS
            // 
            this.dtpHasta_675MS.Location = new System.Drawing.Point(910, 346);
            this.dtpHasta_675MS.Name = "dtpHasta_675MS";
            this.dtpHasta_675MS.Size = new System.Drawing.Size(200, 22);
            this.dtpHasta_675MS.TabIndex = 127;
            // 
            // btnLimpiar_675MS
            // 
            this.btnLimpiar_675MS.BackColor = System.Drawing.Color.Cornsilk;
            this.btnLimpiar_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar_675MS.Location = new System.Drawing.Point(910, 455);
            this.btnLimpiar_675MS.Name = "btnLimpiar_675MS";
            this.btnLimpiar_675MS.Size = new System.Drawing.Size(204, 30);
            this.btnLimpiar_675MS.TabIndex = 128;
            this.btnLimpiar_675MS.Text = "Limpiar Filtros";
            this.btnLimpiar_675MS.UseVisualStyleBackColor = false;
            this.btnLimpiar_675MS.Click += new System.EventHandler(this.btnLimpiar_675MS_Click);
            // 
            // btnActivar_675MS
            // 
            this.btnActivar_675MS.BackColor = System.Drawing.Color.Aquamarine;
            this.btnActivar_675MS.Enabled = false;
            this.btnActivar_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivar_675MS.Location = new System.Drawing.Point(910, 420);
            this.btnActivar_675MS.Name = "btnActivar_675MS";
            this.btnActivar_675MS.Size = new System.Drawing.Size(204, 30);
            this.btnActivar_675MS.TabIndex = 129;
            this.btnActivar_675MS.Text = "Activar";
            this.btnActivar_675MS.UseVisualStyleBackColor = false;
            this.btnActivar_675MS.Click += new System.EventHandler(this.btnActivar_675MS_Click);
            // 
            // FormHistorialCliente
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1134, 711);
            this.Controls.Add(this.btnActivar_675MS);
            this.Controls.Add(this.btnLimpiar_675MS);
            this.Controls.Add(this.dtpHasta_675MS);
            this.Controls.Add(this.dtpDesde_675MS);
            this.Controls.Add(this.lblDesde_675MS);
            this.Controls.Add(this.lblHasta_675MS);
            this.Controls.Add(this.lblDNI_675MS);
            this.Controls.Add(this.txtDNI_675MS);
            this.Controls.Add(this.lblIdioma_675MS);
            this.Controls.Add(this.cmbIdiomas_675MS);
            this.Controls.Add(this.btnVolver_675MS);
            this.Controls.Add(this.bntConsultar_675MS);
            this.Controls.Add(this.lblhistorial_675MS);
            this.Controls.Add(this.dgvHistorial_675MS);
            this.Name = "FormHistorialCliente";
            this.Text = "FormHistorialCliente";
            this.Load += new System.EventHandler(this.FormHistorialCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial_675MS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIdioma_675MS;
        private System.Windows.Forms.ComboBox cmbIdiomas_675MS;
        private System.Windows.Forms.Button btnVolver_675MS;
        private System.Windows.Forms.Button bntConsultar_675MS;
        private System.Windows.Forms.Label lblhistorial_675MS;
        private System.Windows.Forms.DataGridView dgvHistorial_675MS;
        private System.Windows.Forms.TextBox txtDNI_675MS;
        private System.Windows.Forms.Label lblDNI_675MS;
        private System.Windows.Forms.Label lblHasta_675MS;
        private System.Windows.Forms.Label lblDesde_675MS;
        private System.Windows.Forms.DateTimePicker dtpDesde_675MS;
        private System.Windows.Forms.DateTimePicker dtpHasta_675MS;
        private System.Windows.Forms.Button btnLimpiar_675MS;
        private System.Windows.Forms.Button btnActivar_675MS;
    }
}