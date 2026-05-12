using RestSharp;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUISearchSuscripcion : Form
    {
        public GUISearchSuscripcion()
        {
            InitializeComponent();

            btnSearch.Click += btnSearch_Click;

            chkActiva.Enabled = false;
            limpiarLabels();
        }

        private void limpiarLabels()
        {
            ID.Text = "";
            Nombre.Text = "";
            chkActiva.Checked = false;
            Dispositivos.Text = "";
            fechai.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarLabels();

                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Debe ingresar el ID de la suscripción.");
                    txtID.Focus();
                    return;
                }

                if (!int.TryParse(txtID.Text.Trim(), out int id) || id <= 0)
                {
                    MessageBox.Show("El ID debe ser un número mayor a 0.");
                    txtID.Focus();
                    return;
                }

                var options = new RestClientOptions("http://localhost:8090/streaming");
                var client = new RestClient(options);

                // Ruta actual recomendada del backend
                var request = new RestRequest("/find/" + id, Method.Get);

                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("No existe suscripción con ID: " + id);
                    limpiarLabels();
                    return;
                }

                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    MessageBox.Show("El servidor no devolvió información.");
                    limpiarLabels();
                    return;
                }

                JsonDocument json = JsonDocument.Parse(response.Content);
                JsonElement root = json.RootElement;

                if (root.TryGetProperty("id", out JsonElement idElement))
                {
                    ID.Text = idElement.GetInt32().ToString();
                }

                if (root.TryGetProperty("activa", out JsonElement activaElement))
                {
                    chkActiva.Checked = activaElement.GetBoolean();
                }

                if (root.TryGetProperty("dispositivosSimultaneos", out JsonElement dispositivosElement))
                {
                    Dispositivos.Text = dispositivosElement.GetInt32().ToString();
                }

                if (root.TryGetProperty("fechaInicio", out JsonElement fechaElement))
                {
                    string fecha = fechaElement.GetString();

                    if (!string.IsNullOrWhiteSpace(fecha))
                    {
                        fechai.Text = fecha.Replace("T", " ");
                    }
                }

                if (root.TryGetProperty("usuario", out JsonElement usuarioElement))
                {
                    string nombreUsuario = "";
                    string codigoUsuario = "";

                    if (usuarioElement.TryGetProperty("nombre", out JsonElement nombreElement))
                    {
                        nombreUsuario = nombreElement.GetString();
                    }

                    if (usuarioElement.TryGetProperty("codigo", out JsonElement codigoElement))
                    {
                        codigoUsuario = codigoElement.GetInt32().ToString();
                    }

                    Nombre.Text = nombreUsuario + " (Código: " + codigoUsuario + ")";
                }
                else
                {
                    Nombre.Text = "Sin usuario asociado";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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