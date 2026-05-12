namespace POCSuscripcionCliente
{
    partial class GUIPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suscripcionStreamingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSuscripToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listSuscripcionStreamingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listSuscripcionStreamingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchSuscripcionStreamingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSuscripcionStreamingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateSuscripcionStreamingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUIAddUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUIDeleteUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUIListUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUISearchUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gUIUpdateUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.usuarioToolStripMenuItem,
            this.suscripcionStreamingToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(475, 24);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // suscripcionStreamingToolStripMenuItem
            // 
            this.suscripcionStreamingToolStripMenuItem.Checked = true;
            this.suscripcionStreamingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.suscripcionStreamingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSuscripToolStripMenuItem,
            this.listSuscripcionStreamingToolStripMenuItem,
            this.listSuscripcionStreamingToolStripMenuItem1,
            this.searchSuscripcionStreamingToolStripMenuItem,
            this.deleteSuscripcionStreamingToolStripMenuItem,
            this.updateSuscripcionStreamingToolStripMenuItem});
            this.suscripcionStreamingToolStripMenuItem.Name = "suscripcionStreamingToolStripMenuItem";
            this.suscripcionStreamingToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.suscripcionStreamingToolStripMenuItem.Text = "SuscripcionStreaming";
            this.suscripcionStreamingToolStripMenuItem.Click += new System.EventHandler(this.suscripcionStreamingToolStripMenuItem_Click);
            // 
            // addSuscripToolStripMenuItem
            // 
            this.addSuscripToolStripMenuItem.Name = "addSuscripToolStripMenuItem";
            this.addSuscripToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.addSuscripToolStripMenuItem.Text = "Agregar Suscripción";
            this.addSuscripToolStripMenuItem.Click += new System.EventHandler(this.addSuscripToolStripMenuItem_Click);
            // 
            // listSuscripcionStreamingToolStripMenuItem
            // 
            this.listSuscripcionStreamingToolStripMenuItem.Name = "listSuscripcionStreamingToolStripMenuItem";
            this.listSuscripcionStreamingToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.listSuscripcionStreamingToolStripMenuItem.Text = "Listar Suscripcion con filtro";
            this.listSuscripcionStreamingToolStripMenuItem.Click += new System.EventHandler(this.listSuscripcionStreamingToolStripMenuItem_Click);
            // 
            // listSuscripcionStreamingToolStripMenuItem1
            // 
            this.listSuscripcionStreamingToolStripMenuItem1.Name = "listSuscripcionStreamingToolStripMenuItem1";
            this.listSuscripcionStreamingToolStripMenuItem1.Size = new System.Drawing.Size(227, 22);
            this.listSuscripcionStreamingToolStripMenuItem1.Text = "Listar Suscripcion ";
            this.listSuscripcionStreamingToolStripMenuItem1.Click += new System.EventHandler(this.listSuscripcionStreamingToolStripMenuItem1_Click);
            // 
            // searchSuscripcionStreamingToolStripMenuItem
            // 
            this.searchSuscripcionStreamingToolStripMenuItem.Name = "searchSuscripcionStreamingToolStripMenuItem";
            this.searchSuscripcionStreamingToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.searchSuscripcionStreamingToolStripMenuItem.Text = "Buscar Suscripción";
            this.searchSuscripcionStreamingToolStripMenuItem.Click += new System.EventHandler(this.searchSuscripcionStreamingToolStripMenuItem_Click);
            // 
            // deleteSuscripcionStreamingToolStripMenuItem
            // 
            this.deleteSuscripcionStreamingToolStripMenuItem.Name = "deleteSuscripcionStreamingToolStripMenuItem";
            this.deleteSuscripcionStreamingToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.deleteSuscripcionStreamingToolStripMenuItem.Text = "Borrar Suscripcion";
            this.deleteSuscripcionStreamingToolStripMenuItem.Click += new System.EventHandler(this.deleteSuscripcionStreamingToolStripMenuItem_Click);
            // 
            // updateSuscripcionStreamingToolStripMenuItem
            // 
            this.updateSuscripcionStreamingToolStripMenuItem.Name = "updateSuscripcionStreamingToolStripMenuItem";
            this.updateSuscripcionStreamingToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.updateSuscripcionStreamingToolStripMenuItem.Text = "Actualizar Suscripcion";
            this.updateSuscripcionStreamingToolStripMenuItem.Click += new System.EventHandler(this.updateSuscripcionStreamingToolStripMenuItem_Click);
            // 
            // usuarioToolStripMenuItem
            // 
            this.usuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gUIAddUsuarioToolStripMenuItem,
            this.gUIDeleteUsuarioToolStripMenuItem,
            this.gUIListUsuarioToolStripMenuItem,
            this.gUISearchUsuarioToolStripMenuItem,
            this.gUIUpdateUsuarioToolStripMenuItem});
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            this.usuarioToolStripMenuItem.Click += new System.EventHandler(this.usuarioToolStripMenuItem_Click);
            // 
            // gUIAddUsuarioToolStripMenuItem
            // 
            this.gUIAddUsuarioToolStripMenuItem.Name = "gUIAddUsuarioToolStripMenuItem";
            this.gUIAddUsuarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gUIAddUsuarioToolStripMenuItem.Text = "Agregar Usuario";
            this.gUIAddUsuarioToolStripMenuItem.Click += new System.EventHandler(this.gUIAddUsuarioToolStripMenuItem_Click);
            // 
            // gUIDeleteUsuarioToolStripMenuItem
            // 
            this.gUIDeleteUsuarioToolStripMenuItem.Name = "gUIDeleteUsuarioToolStripMenuItem";
            this.gUIDeleteUsuarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gUIDeleteUsuarioToolStripMenuItem.Text = "Borrar Usuario";
            this.gUIDeleteUsuarioToolStripMenuItem.Click += new System.EventHandler(this.gUIDeleteUsuarioToolStripMenuItem_Click);
            // 
            // gUIListUsuarioToolStripMenuItem
            // 
            this.gUIListUsuarioToolStripMenuItem.Name = "gUIListUsuarioToolStripMenuItem";
            this.gUIListUsuarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gUIListUsuarioToolStripMenuItem.Text = "Listar Usuario";
            this.gUIListUsuarioToolStripMenuItem.Click += new System.EventHandler(this.gUIListUsuarioToolStripMenuItem_Click);
            // 
            // gUISearchUsuarioToolStripMenuItem
            // 
            this.gUISearchUsuarioToolStripMenuItem.Name = "gUISearchUsuarioToolStripMenuItem";
            this.gUISearchUsuarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gUISearchUsuarioToolStripMenuItem.Text = "Buscar Usuario";
            this.gUISearchUsuarioToolStripMenuItem.Click += new System.EventHandler(this.gUISearchUsuarioToolStripMenuItem_Click);
            // 
            // gUIUpdateUsuarioToolStripMenuItem
            // 
            this.gUIUpdateUsuarioToolStripMenuItem.Name = "gUIUpdateUsuarioToolStripMenuItem";
            this.gUIUpdateUsuarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gUIUpdateUsuarioToolStripMenuItem.Text = "Actualizar Usuario";
            this.gUIUpdateUsuarioToolStripMenuItem.Click += new System.EventHandler(this.gUIUpdateUsuarioToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(80, 20);
            this.toolStripMenuItem1.Text = "Acerca de...";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // GUIPrincipal
            // 
            this.ClientSize = new System.Drawing.Size(475, 279);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "GUIPrincipal";
            this.Text = "GESTOR DE USUARIOS Y SUSCRIPCIONES";
            this.Load += new System.EventHandler(this.GUIPrincipal_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suscripcionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchSuscripcionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchSuscripcionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteSuscripcionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateSuscripcionToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suscripcionStreamingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSuscripToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem listSuscripcionStreamingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchSuscripcionStreamingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSuscripcionStreamingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateSuscripcionStreamingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listSuscripcionStreamingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUIAddUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUIDeleteUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUIListUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUISearchUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gUIUpdateUsuarioToolStripMenuItem;
    }
}

