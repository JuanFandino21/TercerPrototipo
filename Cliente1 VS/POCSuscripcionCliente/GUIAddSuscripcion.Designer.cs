namespace POCSuscripcionCliente
{
    partial class GUIAddSuscripcion
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbNombreUsuario = new System.Windows.Forms.Label();
            this.lblDispositivos = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtCodigoUsuario = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.cmbPlataforma = new System.Windows.Forms.ComboBox();
            this.cmbDispositivos = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCostoMensual = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkActiva = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID o numero de suscripcion:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbNombreUsuario
            // 
            this.lbNombreUsuario.AutoSize = true;
            this.lbNombreUsuario.Location = new System.Drawing.Point(120, 82);
            this.lbNombreUsuario.Name = "lbNombreUsuario";
            this.lbNombreUsuario.Size = new System.Drawing.Size(97, 13);
            this.lbNombreUsuario.TabIndex = 1;
            this.lbNombreUsuario.Text = "Codigo del usuario:";
            // 
            // lblDispositivos
            // 
            this.lblDispositivos.AutoSize = true;
            this.lblDispositivos.Location = new System.Drawing.Point(120, 147);
            this.lblDispositivos.Name = "lblDispositivos";
            this.lblDispositivos.Size = new System.Drawing.Size(69, 13);
            this.lblDispositivos.TabIndex = 2;
            this.lblDispositivos.Text = "Dispositivos :";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(260, 44);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(51, 20);
            this.txtID.TabIndex = 3;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // txtCodigoUsuario
            // 
            this.txtCodigoUsuario.Location = new System.Drawing.Point(260, 79);
            this.txtCodigoUsuario.Name = "txtCodigoUsuario";
            this.txtCodigoUsuario.Size = new System.Drawing.Size(248, 20);
            this.txtCodigoUsuario.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(238, 311);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(122, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Guardar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Plataforma:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Fecha inicio";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(260, 205);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 10;
            // 
            // cmbPlataforma
            // 
            this.cmbPlataforma.FormattingEnabled = true;
            this.cmbPlataforma.Items.AddRange(new object[] {
            "Netflix",
            "Disney+",
            "HBO Max",
            "Amazon Prime Video",
            "Paramount+",
            "Apple TV+",
            "Crunchyroll",
            "Star+"});
            this.cmbPlataforma.Location = new System.Drawing.Point(260, 107);
            this.cmbPlataforma.Name = "cmbPlataforma";
            this.cmbPlataforma.Size = new System.Drawing.Size(121, 21);
            this.cmbPlataforma.TabIndex = 11;
            // 
            // cmbDispositivos
            // 
            this.cmbDispositivos.FormattingEnabled = true;
            this.cmbDispositivos.Items.AddRange(new object[] {
            "1 dispositivo  → 16.000 COP",
            "2 dispositivos → 18.000 COP",
            "3 dispositivos → 20.000 COP",
            "4 dispositivos → 22.000 COP",
            "5 dispositivos → 24.000 COP",
            "6 dispositivos → 26.000 COP"});
            this.cmbDispositivos.Location = new System.Drawing.Point(260, 139);
            this.cmbDispositivos.Name = "cmbDispositivos";
            this.cmbDispositivos.Size = new System.Drawing.Size(121, 21);
            this.cmbDispositivos.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Costo mensual:";
            // 
            // txtCostoMensual
            // 
            this.txtCostoMensual.Location = new System.Drawing.Point(260, 171);
            this.txtCostoMensual.Name = "txtCostoMensual";
            this.txtCostoMensual.Size = new System.Drawing.Size(248, 20);
            this.txtCostoMensual.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(120, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Estado:";
            // 
            // chkActiva
            // 
            this.chkActiva.AutoSize = true;
            this.chkActiva.Location = new System.Drawing.Point(260, 239);
            this.chkActiva.Name = "chkActiva";
            this.chkActiva.Size = new System.Drawing.Size(56, 17);
            this.chkActiva.TabIndex = 16;
            this.chkActiva.Text = "Activa";
            this.chkActiva.UseVisualStyleBackColor = true;
            // 
            // GUIAddSuscripcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 356);
            this.Controls.Add(this.chkActiva);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCostoMensual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbDispositivos);
            this.Controls.Add(this.cmbPlataforma);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtCodigoUsuario);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblDispositivos);
            this.Controls.Add(this.lbNombreUsuario);
            this.Controls.Add(this.label1);
            this.Name = "GUIAddSuscripcion";
            this.Text = "AGREGAR SUSCRIPCION";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbNombreUsuario;
        private System.Windows.Forms.Label lblDispositivos;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtCodigoUsuario;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.ComboBox cmbPlataforma;
        private System.Windows.Forms.ComboBox cmbDispositivos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCostoMensual;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkActiva;
    }
}