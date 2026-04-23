using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIListFilterSuscripcion : Form
    {
        public GUIListFilterSuscripcion()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new RestClientOptions("http://localhost:31230/streaming");
                var client = new RestClient(options);

                string url = "";

              
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    url += "?nombre=" + txtNombre.Text;
                }

                
                string activa = chkActiva.Checked ? "true" : "false";

                if (url.Contains("?"))
                    url += "&activa=" + activa;
                else
                    url += "?activa=" + activa;

                var request = new RestRequest(url, Method.Get);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Error al obtener datos");
                    return;
                }

                dataGridView1.Rows.Clear();

                var lista = JsonSerializer.Deserialize<List<SuscripcionDTO>>(response.Content);

                foreach (var s in lista)
                {
                    string fecha = s.fechaInicio.Replace("T", " ");

                    dataGridView1.Rows.Add(
                        s.id,
                        s.nombreUsuario,
                        s.activa ? "Sí" : "No",
                        s.dispositivosSimultaneos,
                        fecha
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }

  

}