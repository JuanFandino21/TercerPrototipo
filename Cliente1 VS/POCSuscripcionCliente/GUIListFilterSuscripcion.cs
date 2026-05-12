using POCSuscripcionCliente.Models;
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
                var options = new RestClientOptions("http://localhost:8090/streaming");
                var client = new RestClient(options);

                string url = "/all";
                bool tieneParametro = false;

                // En esta pantalla txtNombre realmente se usa como código de usuario
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    if (!int.TryParse(txtNombre.Text.Trim(), out int codigoUsuario) || codigoUsuario <= 0)
                    {
                        MessageBox.Show("El código del usuario debe ser un número mayor a 0");
                        return;
                    }

                    url += "?codigoUsuario=" + codigoUsuario;
                    tieneParametro = true;
                }

                string activa = chkActiva.Checked ? "true" : "false";

                if (tieneParametro)
                {
                    url += "&activa=" + activa;
                }
                else
                {
                    url += "?activa=" + activa;
                }

                var request = new RestRequest(url, Method.Get);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("Error al obtener datos: " + response.Content);
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
                    MessageBox.Show("No se encontraron suscripciones con los filtros ingresados.");
                    return;
                }

                foreach (var s in lista)
                {
                    string nombreUsuario = "";
                    string codigoUsuario = "";

                    if (s.usuario != null)
                    {
                        nombreUsuario = s.usuario.nombre;
                        codigoUsuario = s.usuario.codigo.ToString();
                    }

                    dataGridView1.Rows.Add(
                        s.id,
                        nombreUsuario,
                        codigoUsuario,
                        s.plataforma,
                        s.activa ? "Sí" : "No",
                        s.dispositivosSimultaneos,
                        "$" + s.costoMensual.ToString("N0"),
                        s.fechaInicio.ToString("yyyy-MM-dd HH:mm:ss")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
        }

        private void chkActiva_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}