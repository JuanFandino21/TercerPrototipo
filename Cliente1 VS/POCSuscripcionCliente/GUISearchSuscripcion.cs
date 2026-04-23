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

        private void limpiarLabels()
        {
            ID.Text = "";
            Nombre.Text = "";
            chkActiva.Text = "";
            Dispositivos.Text = "";
            fechai.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Debe ingresar un ID");
                return;
            }
            string id = txtID.Text;

            var options = new RestClientOptions("http://localhost:31230/streaming");
            var client = new RestClient(options);

            var request = new RestRequest("/" + id, Method.Get);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("No existe suscripción con id: " + id);

                limpiarLabels();
                return;
            }

            string contenido = response.Content;

          
            contenido = contenido.Replace("{", "").Replace("}", "");

            string[] campos = contenido.Split(',');

   
            foreach (var campo in campos)
            {
                if (campo.Contains("id"))
                    ID.Text = campo.Split(':')[1];

                if (campo.Contains("nombreUsuario"))
                    Nombre.Text = campo.Split(':')[1].Replace("\"", "");

                if (campo.Contains("activa"))
                    chkActiva.Checked = campo.Split(':')[1].Trim() == "true";

                if (campo.Contains("dispositivosSimultaneos"))
                    Dispositivos.Text = campo.Split(':')[1];

                if (campo.Contains("fechaInicio"))
                {
                    string fecha = campo.Split(':')[1].Replace("\"", "");

                    
                    fecha = fecha.Replace("T", " ");

                    fechai.Text = fecha;
                }
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Suscripcion_Enter(object sender, EventArgs e)
        {

        }
    }
}

