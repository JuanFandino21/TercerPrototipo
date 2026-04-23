namespace POCSuscripcionCliente
{
    partial class GUISearchSuscripcion
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
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.Suscripcion = new System.Windows.Forms.GroupBox();
            this.chkActiva = new System.Windows.Forms.CheckBox();
            this.fechai = new System.Windows.Forms.Label();
            this.Dispositivos = new System.Windows.Forms.Label();
            this.Nombre = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.Label();
            this.Fecha = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Suscripcion.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(259, 6);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 1;
            this.txtID.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(397, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Suscripcion
            // 
            this.Suscripcion.Controls.Add(this.chkActiva);
            this.Suscripcion.Controls.Add(this.fechai);
            this.Suscripcion.Controls.Add(this.Dispositivos);
            this.Suscripcion.Controls.Add(this.Nombre);
            this.Suscripcion.Controls.Add(this.ID);
            this.Suscripcion.Controls.Add(this.Fecha);
            this.Suscripcion.Controls.Add(this.label5);
            this.Suscripcion.Controls.Add(this.label4);
            this.Suscripcion.Controls.Add(this.label3);
            this.Suscripcion.Controls.Add(this.label2);
            this.Suscripcion.Location = new System.Drawing.Point(12, 61);
            this.Suscripcion.Name = "Suscripcion";
            this.Suscripcion.Size = new System.Drawing.Size(617, 175);
            this.Suscripcion.TabIndex = 3;
            this.Suscripcion.TabStop = false;
            this.Suscripcion.Text = "Suscripción";
            this.Suscripcion.Enter += new System.EventHandler(this.Suscripcion_Enter);
            // 
            // chkActiva
            // 
            this.chkActiva.Enabled = false;
            this.chkActiva.Location = new System.Drawing.Point(117, 79);
            this.chkActiva.Name = "chkActiva";
            this.chkActiva.Size = new System.Drawing.Size(104, 24);
            this.chkActiva.TabIndex = 10;
            this.chkActiva.UseVisualStyleBackColor = true;
            // 
            // fechai
            // 
            this.fechai.AutoSize = true;
            this.fechai.Location = new System.Drawing.Point(114, 130);
            this.fechai.Name = "fechai";
            this.fechai.Size = new System.Drawing.Size(0, 13);
            this.fechai.TabIndex = 9;
            // 
            // Dispositivos
            // 
            this.Dispositivos.AutoSize = true;
            this.Dispositivos.Location = new System.Drawing.Point(114, 102);
            this.Dispositivos.Name = "Dispositivos";
            this.Dispositivos.Size = new System.Drawing.Size(0, 13);
            this.Dispositivos.TabIndex = 8;
            // 
            // Nombre
            // 
            this.Nombre.AutoSize = true;
            this.Nombre.Location = new System.Drawing.Point(114, 56);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(0, 13);
            this.Nombre.TabIndex = 6;
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Location = new System.Drawing.Point(114, 28);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(0, 13);
            this.ID.TabIndex = 5;
            // 
            // Fecha
            // 
            this.Fecha.AutoSize = true;
            this.Fecha.Location = new System.Drawing.Point(35, 130);
            this.Fecha.Name = "Fecha";
            this.Fecha.Size = new System.Drawing.Size(37, 13);
            this.Fecha.TabIndex = 4;
            this.Fecha.Text = "Fecha";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Dispositivos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Activa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID:";
            // 
            // GUISearchSuscripcion
            // 
            this.ClientSize = new System.Drawing.Size(641, 261);
            this.Controls.Add(this.Suscripcion);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Name = "GUISearchSuscripcion";
            this.Text = "GUISearchSuscripcion";
            this.Suscripcion.ResumeLayout(false);
            this.Suscripcion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox Suscripcion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkActiva;
        private System.Windows.Forms.Label fechai;
        private System.Windows.Forms.Label Dispositivos;
        private System.Windows.Forms.Label Nombre;
        private System.Windows.Forms.Label ID;
        private System.Windows.Forms.Label Fecha;
    }
}