using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIPrincipal : Form
    {
        public GUIPrincipal()
        {
            InitializeComponent();
        }

        private void GUIPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void addSuscripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIAddSuscripcion gui = new GUIAddSuscripcion();
            gui.Show();

        }

        private void listSuscripcionStreamingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIListFilterSuscripcion gui = new GUIListFilterSuscripcion();
            gui.Show();

        }

        private void searchSuscripcionStreamingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUISearchSuscripcion gui = new GUISearchSuscripcion();
            gui.Show();

        }

        private void deleteSuscripcionStreamingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIDeleteSuscripcion gui = new GUIDeleteSuscripcion();
            gui.Show();

        }

        private void updateSuscripcionStreamingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIUpdateSuscripcion gui = new GUIUpdateSuscripcion();
            gui.Show();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            MessageBox.Show(
                "Sistema de Suscripciones\n\n" +
                "Versión: 4.0\n\n" +
                "Integrantes:\n\n" +
                "Juan David Fandiño Hernandez\n" +
                "Código: 2220221087\n\n" +
                "Ronald Andres Barrios Hernandez\n" +
                "Código: 2220221029\n\n" +
                "Universidad de Ibagué\n" +
                "Desarrollo de Aplicaciones Empresariales",
                "Acerca de...",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );


        }

        private void suscripcionStreamingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listSuscripcionStreamingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GUIListSuscripcion gui = new GUIListSuscripcion();
            gui.Show();

        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gUIAddUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIAddUsuario ventana = new GUIAddUsuario();
            ventana.Show();

        }

        private void gUIListUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIListUsuario ventana = new GUIListUsuario();
            ventana.Show();

        }

        private void gUIDeleteUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIDeleteUsuario ventana = new GUIDeleteUsuario();
            ventana.Show();

        }

        private void gUISearchUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUISearchUsuario ventana = new GUISearchUsuario();
            ventana.Show();

        }

        private void gUIUpdateUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GUIUpdateUsuario ventana = new GUIUpdateUsuario();
            ventana.Show();

        }
    }
}
