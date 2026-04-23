using RestSharp;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIAddSuscripcion : Form
    {
        public GUIAddSuscripcion()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtID.Text) || int.Parse(txtID.Text) <= 0)
                {
                    MessageBox.Show("El ID debe ser mayor a 0");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
                {
                    MessageBox.Show("El nombre de usuario es obligatorio");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDispositivos.Text))
                {
                    MessageBox.Show("Debe ingresar dispositivos");
                    return;
                }

                int dispositivos = int.Parse(txtDispositivos.Text);

                if (dispositivos < 1 || dispositivos > 6)
                {
                    MessageBox.Show("Los dispositivos deben estar entre 1 y 6");
                    return;
                }

              
                var options = new RestClientOptions("http://localhost:31230/streaming");
                var client = new RestClient(options);

                var request = new RestRequest("/", Method.Post);

                string fecha = dtpFechaInicio.Value.ToString("yyyy-MM-ddTHH:mm:ss");

                request.AddJsonBody(new
                {
                    id = int.Parse(txtID.Text),
                    nombreUsuario = txtNombreUsuario.Text.Trim(),
                    activa = chkActiva.Checked,
                    dispositivosSimultaneos = dispositivos,
                    fechaInicio = fecha
                });

                var response = client.Execute(request);

                
                if (!response.IsSuccessful)
                {
                    MessageBox.Show(response.Content, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                MessageBox.Show("Suscripción creada correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                LimpiarFormulario();
            }
            catch (FormatException)
            {
                MessageBox.Show("Verifica que los campos numéricos sean válidos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }
        }

        
        private void LimpiarFormulario()
        {
            txtID.Text = "";
            txtNombreUsuario.Text = "";
            txtDispositivos.Text = "";
            chkActiva.Checked = false;

            dtpFechaInicio.Value = DateTime.Now;

            txtNombreUsuario.Focus();
        }
    }
}