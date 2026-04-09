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
            GUIListSuscripcion gui = new GUIListSuscripcion();
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
    }
}
