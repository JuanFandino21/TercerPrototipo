using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

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

            var request = new RestRequest("/", Method.Post);

            string fecha = dtpFechaInicio.Value.ToString("yyyy-MM-ddTHH:mm:ss");

            request.AddJsonBody(new
            {
                id = int.Parse(txtID.Text),
                nombreUsuario = txtNombreUsuario.Text,
                activa = chkActiva.Checked,
                dispositivosSimultaneos = int.Parse(txtDispositivos.Text),
                fechaInicio = fecha
            });

            var response = client.Execute(request);

            

            var json = JsonSerializer.Deserialize<object>(response.Content);
            string bonito = JsonSerializer.Serialize(json, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            MessageBox.Show(bonito);

            Dispose();

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void chkActiva_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
