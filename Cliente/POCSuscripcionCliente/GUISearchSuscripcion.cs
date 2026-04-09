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
    public partial class GUISearchSuscripcion : Form
    {
        public GUISearchSuscripcion()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;

            var options = new RestClientOptions("http://localhost:31230/streaming");
            var client = new RestClient(options);

            var request = new RestRequest("/" + id, Method.Get);

            var response = client.Execute(request);

            txtRespuesta.Text = response.Content;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

