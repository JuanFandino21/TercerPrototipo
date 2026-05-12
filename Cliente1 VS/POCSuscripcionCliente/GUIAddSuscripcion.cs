using RestSharp;
using System;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIAddSuscripcion : Form
    {
        public GUIAddSuscripcion()
        {
            InitializeComponent();

            CargarCombos();
            PrepararFormulario();
        }

        private void CargarCombos()
        {
            cmbPlataforma.Items.Clear();
            cmbPlataforma.Items.Add("Netflix");
            cmbPlataforma.Items.Add("Disney+");
            cmbPlataforma.Items.Add("HBO Max");
            cmbPlataforma.Items.Add("Amazon Prime Video");
            cmbPlataforma.Items.Add("Paramount+");
            cmbPlataforma.Items.Add("Apple TV+");
            cmbPlataforma.Items.Add("Crunchyroll");
            cmbPlataforma.Items.Add("Star+");

            cmbDispositivos.Items.Clear();
            cmbDispositivos.Items.Add("1");
            cmbDispositivos.Items.Add("2");
            cmbDispositivos.Items.Add("3");
            cmbDispositivos.Items.Add("4");
            cmbDispositivos.Items.Add("5");
            cmbDispositivos.Items.Add("6");

            cmbPlataforma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDispositivos.DropDownStyle = ComboBoxStyle.DropDownList;

            // Para evitar doble ejecución si el evento ya quedó conectado desde el diseñador.
            cmbDispositivos.SelectedIndexChanged -= cmbDispositivos_SelectedIndexChanged;
            cmbDispositivos.SelectedIndexChanged += cmbDispositivos_SelectedIndexChanged;
        }

        private void PrepararFormulario()
        {
            txtCostoMensual.ReadOnly = true;
            txtCostoMensual.Text = "";
            chkActiva.Checked = true;
            dtpFechaInicio.Value = DateTime.Now;
            txtID.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }

                int id = int.Parse(txtID.Text.Trim());
                int codigoUsuario = int.Parse(txtCodigoUsuario.Text.Trim());
                int dispositivos = int.Parse(cmbDispositivos.SelectedItem.ToString());
                double costo = CalcularCosto(dispositivos);

                var client = new RestClient("http://localhost:8090/streaming");

                /*
                 * Funciona porque el backend ahora acepta:
                 * POST /streaming
                 * POST /streaming/
                 */
                var request = new RestRequest("", Method.Post);

                string fecha = dtpFechaInicio.Value.ToString("yyyy-MM-ddTHH:mm:ss");

                request.AddJsonBody(new
                {
                    id = id,
                    fechaInicio = fecha,
                    activa = chkActiva.Checked,
                    dispositivosSimultaneos = dispositivos,
                    costoMensual = costo,
                    plataforma = cmbPlataforma.SelectedItem.ToString(),
                    usuario = new
                    {
                        codigo = codigoUsuario
                    }
                });

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show(
                        "Suscripción creada correctamente.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo guardar.\n\n" + response.Content,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Ingresa el número de suscripción.");
                txtID.Focus();
                return false;
            }

            if (!int.TryParse(txtID.Text.Trim(), out int id) || id <= 0)
            {
                MessageBox.Show("El número de suscripción debe ser mayor a 0.");
                txtID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodigoUsuario.Text))
            {
                MessageBox.Show("Ingresa el código del usuario.");
                txtCodigoUsuario.Focus();
                return false;
            }

            if (!int.TryParse(txtCodigoUsuario.Text.Trim(), out int codigoUsuario) || codigoUsuario <= 0)
            {
                MessageBox.Show("El código del usuario debe ser mayor a 0.");
                txtCodigoUsuario.Focus();
                return false;
            }

            if (cmbPlataforma.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona una plataforma.");
                cmbPlataforma.Focus();
                return false;
            }

            if (cmbDispositivos.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona la cantidad de dispositivos.");
                cmbDispositivos.Focus();
                return false;
            }

            return true;
        }

        private void cmbDispositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDispositivos.SelectedIndex >= 0)
            {
                int dispositivos = int.Parse(cmbDispositivos.SelectedItem.ToString());
                double costo = CalcularCosto(dispositivos);
                txtCostoMensual.Text = costo.ToString("0");
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

        private void LimpiarFormulario()
        {
            txtID.Clear();
            txtCodigoUsuario.Clear();
            txtCostoMensual.Clear();

            cmbPlataforma.SelectedIndex = -1;
            cmbDispositivos.SelectedIndex = -1;

            chkActiva.Checked = true;
            dtpFechaInicio.Value = DateTime.Now;

            txtID.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
        }
    }
}