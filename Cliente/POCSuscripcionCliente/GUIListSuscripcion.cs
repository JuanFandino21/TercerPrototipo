using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace POCSuscripcionCliente
{
    public partial class GUIListSuscripcion : Form
    {
        public GUIListSuscripcion()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string activa = chkActiva.Checked ? "true" : "";

            var options = new RestClientOptions("http://localhost:31230/streaming");
            var client = new RestClient(options);

            string url = "/?";

            if (!string.IsNullOrEmpty(nombre))
                url += "nombre=" + nombre + "&";

            if (!string.IsNullOrEmpty(activa))
                url += "activa=" + activa;

            var request = new RestRequest(url, Method.Get);
            var response = client.Execute(request);

            dataGridView1.Rows.Clear();

            string contenido = response.Content;

            contenido = contenido.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "");

            string[] registros = contenido.Split(new string[] { "}," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var reg in registros)
            {
                string[] campos = reg.Split(',');

                string id = "";
                string nombreUsuario = "";
                string activaVal = "";
                string dispositivos = "";
                string fecha = ""; 

                foreach (var campo in campos)
                {
                    if (campo.Contains("id"))
                        id = campo.Split(':')[1];

                    if (campo.Contains("fechaInicio"))
                    {
                        fecha = campo.Split(':')[1].Replace("\"", "");
                        fecha = fecha.Replace("T", " ");
                    }

                    if (campo.Contains("nombreUsuario"))
                        nombreUsuario = campo.Split(':')[1].Replace("\"", "");

                    if (campo.Contains("activa"))
                        activaVal = campo.Split(':')[1];

                    if (campo.Contains("dispositivosSimultaneos"))
                        dispositivos = campo.Split(':')[1];
                }

               
                dataGridView1.Rows.Add(id, nombreUsuario, activaVal, dispositivos, fecha);
            }
        }

        private void txtRespuesta_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

