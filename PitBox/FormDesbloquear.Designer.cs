namespace PitBox
{
    partial class FormDesbloquear
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
            this.dgvUsuarios_675MS = new System.Windows.Forms.DataGridView();
            this.btnVolver_675MS = new System.Windows.Forms.Button();
            this.bntRegistrar_675MS = new System.Windows.Forms.Button();
            this.lblDNI_675MS = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsuario_675MS = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios_675MS)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUsuarios_675MS
            // 
            this.dgvUsuarios_675MS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios_675MS.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvUsuarios_675MS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios_675MS.Location = new System.Drawing.Point(34, 198);
            this.dgvUsuarios_675MS.Name = "dgvUsuarios_675MS";
            this.dgvUsuarios_675MS.RowHeadersWidth = 51;
            this.dgvUsuarios_675MS.RowTemplate.Height = 24;
            this.dgvUsuarios_675MS.Size = new System.Drawing.Size(706, 460);
            this.dgvUsuarios_675MS.TabIndex = 49;
            // 
            // btnVolver_675MS
            // 
            this.btnVolver_675MS.BackColor = System.Drawing.Color.MistyRose;
            this.btnVolver_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver_675MS.Location = new System.Drawing.Point(854, 379);
            this.btnVolver_675MS.Name = "btnVolver_675MS";
            this.btnVolver_675MS.Size = new System.Drawing.Size(255, 30);
            this.btnVolver_675MS.TabIndex = 62;
            this.btnVolver_675MS.Text = "Volver";
            this.btnVolver_675MS.UseVisualStyleBackColor = false;
            this.btnVolver_675MS.Click += new System.EventHandler(this.btnVolver_675MS_Click);
            // 
            // bntRegistrar_675MS
            // 
            this.bntRegistrar_675MS.BackColor = System.Drawing.Color.Aquamarine;
            this.bntRegistrar_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntRegistrar_675MS.Location = new System.Drawing.Point(854, 343);
            this.bntRegistrar_675MS.Name = "bntRegistrar_675MS";
            this.bntRegistrar_675MS.Size = new System.Drawing.Size(255, 30);
            this.bntRegistrar_675MS.TabIndex = 60;
            this.bntRegistrar_675MS.Text = "Desbloquear";
            this.bntRegistrar_675MS.UseVisualStyleBackColor = false;
            this.bntRegistrar_675MS.Click += new System.EventHandler(this.bntRegistrar_675MS_Click);
            // 
            // lblDNI_675MS
            // 
            this.lblDNI_675MS.AutoSize = true;
            this.lblDNI_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblDNI_675MS.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDNI_675MS.Location = new System.Drawing.Point(773, 269);
            this.lblDNI_675MS.Name = "lblDNI_675MS";
            this.lblDNI_675MS.Size = new System.Drawing.Size(75, 23);
            this.lblDNI_675MS.TabIndex = 71;
            this.lblDNI_675MS.Text = "Usuario";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(767, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 35);
            this.label1.TabIndex = 69;
            this.label1.Text = "DESBLOQUEO DE USUARIOS";
            // 
            // txtUsuario_675MS
            // 
            this.txtUsuario_675MS.Location = new System.Drawing.Point(854, 271);
            this.txtUsuario_675MS.Name = "txtUsuario_675MS";
            this.txtUsuario_675MS.Size = new System.Drawing.Size(255, 22);
            this.txtUsuario_675MS.TabIndex = 68;
            // 
            // FormDesbloquear
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1134, 711);
            this.Controls.Add(this.lblDNI_675MS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUsuario_675MS);
            this.Controls.Add(this.btnVolver_675MS);
            this.Controls.Add(this.bntRegistrar_675MS);
            this.Controls.Add(this.dgvUsuarios_675MS);
            this.Name = "FormDesbloquear";
            this.Text = "Desbloquear Usuario";
            this.Load += new System.EventHandler(this.FormDesbloquear_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios_675MS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsuarios_675MS;
        private System.Windows.Forms.Button btnVolver_675MS;
        private System.Windows.Forms.Button bntRegistrar_675MS;
        private System.Windows.Forms.Label lblDNI_675MS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsuario_675MS;
    }
}