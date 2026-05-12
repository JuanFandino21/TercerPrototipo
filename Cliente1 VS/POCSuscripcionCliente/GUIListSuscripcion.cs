using POCSuscripcionCliente.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIListSuscripcion : Form
    {
        public GUIListSuscripcion()
        {
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new RestClientOptions("http://localhost:8090/streaming");
                var client = new RestClient(options);

                var request = new RestRequest("/all", Method.Get);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show(
                        "Error al obtener datos: " + response.Content,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                dataGridView1.Rows.Clear();

                var opcionesJson = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<SuscripcionStreaming> lista =
                    JsonSerializer.Deserialize<List<SuscripcionStreaming>>(response.Content, opcionesJson);

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No hay suscripciones registradas.");
                    return;
                }

                foreach (var s in lista)
                {
                    string nombreUsuario = "";

                    if (s.usuario != null)
                    {
                        nombreUsuario = s.usuario.nombre;
                    }

                    dataGridView1.Rows.Add(
                        s.id,
                        nombreUsuario,
                        s.activa ? "Sí" : "No",
                        s.dispositivosSimultaneos,
                        s.fechaInicio.ToString("yyyy-MM-dd HH:mm:ss")
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