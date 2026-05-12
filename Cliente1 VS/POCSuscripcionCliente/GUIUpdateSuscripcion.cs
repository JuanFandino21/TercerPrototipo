using RestSharp;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIUpdateSuscripcion : Form
    {
        private string plataformaActual = "";
        private double costoMensualActual = 0;

        public GUIUpdateSuscripcion()
        {
            InitializeComponent();

            PrepararPantalla();

        }

        private void PrepararPantalla()
        {
            txtCodigoUsuario.ReadOnly = true;
            txtCodigoUsuario.Enabled = false;

            chkActiva.Enabled = false;
            txtDispositivos.Enabled = false;
            dtpFecha.Enabled = false;
            btnUpdate.Enabled = false;

            txtID.Focus();
        }

        private void limpiarCampos()
        {
            txtID.Clear();
            txtCodigoUsuario.Clear();
            chkActiva.Checked = false;
            txtDispositivos.Clear();
            dtpFecha.Value = DateTime.Now;

            plataformaActual = "";
            costoMensualActual = 0;

            txtCodigoUsuario.ReadOnly = true;
            txtCodigoUsuario.Enabled = false;

            chkActiva.Enabled = false;
            txtDispositivos.Enabled = false;
            dtpFecha.Enabled = false;
            btnUpdate.Enabled = false;

            txtID.Focus();
        }

        private void limpiarDatosSuscripcion()
        {
            txtCodigoUsuario.Clear();
            chkActiva.Checked = false;
            txtDispositivos.Clear();
            dtpFecha.Value = DateTime.Now;

            plataformaActual = "";
            costoMensualActual = 0;

            txtCodigoUsuario.ReadOnly = true;
            txtCodigoUsuario.Enabled = false;

            chkActiva.Enabled = false;
            txtDispositivos.Enabled = false;
            dtpFecha.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarDatosSuscripcion();

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

                var client = new RestClient("http://localhost:8090/streaming");
                var request = new RestRequest("/find/" + id, Method.Get);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    MessageBox.Show("No existe una suscripción con ID: " + id);
                    return;
                }

                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    MessageBox.Show("El servidor no devolvió información.");
                    return;
                }

                JsonDocument json = JsonDocument.Parse(response.Content);
                JsonElement root = json.RootElement;

                if (root.TryGetProperty("activa", out JsonElement activaElement))
                {
                    chkActiva.Checked = activaElement.GetBoolean();
                }

                if (root.TryGetProperty("dispositivosSimultaneos", out JsonElement dispositivosElement))
                {
                    txtDispositivos.Text = dispositivosElement.GetInt32().ToString();
                }

                if (root.TryGetProperty("fechaInicio", out JsonElement fechaElement))
                {
                    string fecha = fechaElement.GetString();

                    if (!string.IsNullOrWhiteSpace(fecha))
                    {
                        dtpFecha.Value = DateTime.Parse(fecha.Replace("T", " "));
                    }
                }

                if (root.TryGetProperty("plataforma", out JsonElement plataformaElement))
                {
                    plataformaActual = plataformaElement.GetString();
                }

                if (root.TryGetProperty("costoMensual", out JsonElement costoElement))
                {
                    costoMensualActual = costoElement.GetDouble();
                }

                if (root.TryGetProperty("usuario", out JsonElement usuarioElement))
                {
                    if (usuarioElement.TryGetProperty("codigo", out JsonElement codigoUsuarioElement))
                    {
                        txtCodigoUsuario.Text = codigoUsuarioElement.GetInt32().ToString();
                    }
                }

                // El código del usuario se muestra pero no se modifica aca
                txtCodigoUsuario.ReadOnly = true;
                txtCodigoUsuario.Enabled = true;

                chkActiva.Enabled = true;
                txtDispositivos.Enabled = true;
                dtpFecha.Enabled = true;
                btnUpdate.Enabled = true;

                txtDispositivos.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtID.Text.Trim(), out int id) || id <= 0)
                {
                    MessageBox.Show("ID inválido.");
                    txtID.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCodigoUsuario.Text))
                {
                    MessageBox.Show("La suscripción no tiene un usuario asociado.");
                    return;
                }

                if (!int.TryParse(txtCodigoUsuario.Text.Trim(), out int codigoUsuario) || codigoUsuario <= 0)
                {
                    MessageBox.Show("El código del usuario asociado no es válido.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDispositivos.Text))
                {
                    MessageBox.Show("Debe ingresar la cantidad de dispositivos.");
                    txtDispositivos.Focus();
                    return;
                }

                if (!int.TryParse(txtDispositivos.Text.Trim(), out int dispositivos))
                {
                    MessageBox.Show("Dispositivos inválido.");
                    txtDispositivos.Focus();
                    return;
                }

                if (dispositivos < 1 || dispositivos > 6)
                {
                    MessageBox.Show("Los dispositivos deben estar entre 1 y 6.");
                    txtDispositivos.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(plataformaActual))
                {
                    MessageBox.Show("No se pudo identificar la plataforma de la suscripción.");
                    return;
                }

                double costo = CalcularCosto(dispositivos);

                var client = new RestClient("http://localhost:8090/streaming");
                var request = new RestRequest("/update/" + id, Method.Put);

                string fecha = dtpFecha.Value.ToString("yyyy-MM-ddTHH:mm:ss");

                request.AddJsonBody(new
                {
                    id = id,
                    fechaInicio = fecha,
                    activa = chkActiva.Checked,
                    dispositivosSimultaneos = dispositivos,
                    costoMensual = costo,
                    plataforma = plataformaActual,
                    usuario = new
                    {
                        codigo = codigoUsuario
                    }
                });

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show(
                        "Suscripción actualizada correctamente.",
                        "Actualización exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    limpiarCampos();
                }
                else
                {
                    MessageBox.Show(
                        "Error al actualizar: " + response.Content,
                        "No se pudo actualizar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private double CalcularCosto(int dispositivos)
        {
            if (dispositivos == 1) return 16000;
            if (dispositivos == 2) return 18000;
            if (dispositivos == 3) return 20000;
            if (dispositivos == 4) return 22000;
            if (dispositivos == 5) return 24000;
            if (dispositivos == 6) return 26000;

            return 0;
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtCodigoUsuario_TextChanged(object sender, EventArgs e)
        {
        }
    }
}