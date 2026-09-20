namespace PitBox
{
    partial class FormRegistro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistro));
            this.lblUsuario_675MS = new System.Windows.Forms.Label();
            this.lblPassword_675MS = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIdEmpleado_675MS = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblUsuario_675MS
            // 
            this.lblUsuario_675MS.AutoSize = true;
            this.lblUsuario_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuario_675MS.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblUsuario_675MS.Location = new System.Drawing.Point(472, 206);
            this.lblUsuario_675MS.Name = "lblUsuario_675MS";
            this.lblUsuario_675MS.Size = new System.Drawing.Size(131, 35);
            this.lblUsuario_675MS.TabIndex = 4;
            this.lblUsuario_675MS.Text = "USUARIO";
            // 
            // lblPassword_675MS
            // 
            this.lblPassword_675MS.AutoSize = true;
            this.lblPassword_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword_675MS.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPassword_675MS.Location = new System.Drawing.Point(417, 249);
            this.lblPassword_675MS.Name = "lblPassword_675MS";
            this.lblPassword_675MS.Size = new System.Drawing.Size(189, 35);
            this.lblPassword_675MS.TabIndex = 5;
            this.lblPassword_675MS.Text = "CONTRASEÑA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(494, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(420, 47);
            this.label1.TabIndex = 6;
            this.label1.Text = "REGISTRO DE USUARIO";
            // 
            // lblIdEmpleado_675MS
            // 
            this.lblIdEmpleado_675MS.AutoSize = true;
            this.lblIdEmpleado_675MS.BackColor = System.Drawing.Color.Transparent;
            this.lblIdEmpleado_675MS.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdEmpleado_675MS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblIdEmpleado_675MS.Location = new System.Drawing.Point(396, 293);
            this.lblIdEmpleado_675MS.Name = "lblIdEmpleado_675MS";
            this.lblIdEmpleado_675MS.Size = new System.Drawing.Size(210, 35);
            this.lblIdEmpleado_675MS.TabIndex = 7;
            this.lblIdEmpleado_675MS.Text = "DNI EMPLEADO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(633, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 35);
            this.label2.TabIndex = 10;
            this.label2.Text = "IDIOMA";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(773, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(141, 24);
            this.comboBox1.TabIndex = 9;
            // 
            // FormRegistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(926, 563);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblIdEmpleado_675MS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPassword_675MS);
            this.Controls.Add(this.lblUsuario_675MS);
            this.Name = "FormRegistro";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PitBox -Registro de Usuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuario_675MS;
        private System.Windows.Forms.Label lblPassword_675MS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIdEmpleado_675MS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}