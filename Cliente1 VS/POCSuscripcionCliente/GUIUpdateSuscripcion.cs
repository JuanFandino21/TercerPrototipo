using RestSharp;
using System;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIUpdateSuscripcion : Form
    {
        public GUIUpdateSuscripcion()
        {
            InitializeComponent();

            
            txtNombre.Enabled = false;
            chkActiva.Enabled = false;
            txtDispositivos.Enabled = false;
            dtpFecha.Enabled = false;
            btnUpdate.Enabled = false;
        }

        
        private void limpiarCampos()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            chkActiva.Checked = false;
            txtDispositivos.Text = "";
            dtpFecha.Value = DateTime.Now;

            txtNombre.Enabled = false;
            chkActiva.Enabled = false;
            txtDispositivos.Enabled = false;
            dtpFecha.Enabled = false;
            btnUpdate.Enabled = false;
        }

        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Debe ingresar un ID");
                return;
            }

            var client = new RestClient("http://localhost:31230/streaming");
            var request = new RestRequest("/" + txtID.Text, Method.Get);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                MessageBox.Show("Error al consultar");
                return;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("No existe la suscripción");
                limpiarCampos();
                return;
            }

            string contenido = response.Content;

            contenido = contenido.Replace("{", "").Replace("}", "");

            string[] campos = contenido.Split(',');

            foreach (var campo in campos)
            {
                string valor = campo.Substring(campo.IndexOf(":") + 1).Replace("\"", "");

                if (campo.Contains("nombreUsuario"))
                    txtNombre.Text = valor;

                if (campo.Contains("activa"))
                    chkActiva.Checked = valor.Trim() == "true";

                if (campo.Contains("dispositivosSimultaneos"))
                    txtDispositivos.Text = valor;

                if (campo.Contains("fechaInicio"))
                {
                    valor = valor.Replace("T", " ");
                    dtpFecha.Value = DateTime.Parse(valor);
                }
            }

            
            txtNombre.Enabled = true;
            chkActiva.Enabled = true;
            txtDispositivos.Enabled = true;
            dtpFecha.Enabled = true;
            btnUpdate.Enabled = true;
        }

        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("ID inválido");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Nombre requerido");
                return;
            }

            if (!int.TryParse(txtDispositivos.Text, out int dispositivos))
            {
                MessageBox.Show("Dispositivos inválido");
                return;
            }

            var client = new RestClient("http://localhost:31230/streaming");
            var request = new RestRequest("/" + txtID.Text, Method.Put);

            string fecha = dtpFecha.Value.ToString("yyyy-MM-ddTHH:mm:ss");

            request.AddJsonBody(new
            {
                id = id,
                nombreUsuario = txtNombre.Text,
                activa = chkActiva.Checked,
                dispositivosSimultaneos = dispositivos,
                fechaInicio = fecha
            });

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                MessageBox.Show("Actualizado correctamente");
                limpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar");
            }
        }
    }
}