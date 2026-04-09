using RestSharp;
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
    public partial class GUIAddSuscripcion : Form
    {
        public GUIAddSuscripcion()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var options = new RestClientOptions("http://localhost:31230/streaming");
            var client = new RestClient(options);

            var request = new RestRequest("");

            request.AddBody(new
            {
                id = int.Parse(txtID.Text),
                nombreUsuario = txtNombreUsuario.Text,
                dispositivosSimultaneos = int.Parse(txtDispositivosSimultaneos.Text)
            });

            var response = client.Post(request);

            MessageBox.Show(response.Content);

        }
    }
}
