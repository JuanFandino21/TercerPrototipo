using POCSuscripcionCliente.Models;
using POCSuscripcionCliente.Services;
using System;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIDeleteSuscripcion : Form
    {
        private ApiService apiService = new ApiService();
        private SuscripcionStreaming suscripcionEncontrada = null;
        private bool eliminando = false;

        public GUIDeleteSuscripcion()
        {
            InitializeComponent();

            // No conectar aquí los eventos si ya están conectados desde el diseñador.
            // btnSearch.Click += btnSearch_Click;
            // btnDelete.Click += btnDelete_Click;

            PrepararPantalla();
        }

        private void PrepararPantalla()
        {
            limpiarCampos();
            btnDelete.Enabled = false;
            chkActiva.Enabled = false;
            txtID.Focus();
        }

        private void limpiarCampos()
        {
            ID.Text = "";
            Nombre.Text = "";
            chkActiva.Checked = false;
            Dispositivos.Text = "";
            fechai.Text = "";
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            suscripcionEncontrada = null;
            btnDelete.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show(
                    "Debe ingresar el número de la suscripción.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtID.Focus();
                return;
            }

            if (!int.TryParse(txtID.Text.Trim(), out int id) || id <= 0)
            {
                MessageBox.Show(
                    "El número de suscripción debe ser mayor a 0.",
                    "Dato inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtID.Focus();
                return;
            }

            SuscripcionStreaming suscripcion = await apiService.GetSuscripcionById(id);

            if (suscripcion == null)
            {
                MessageBox.Show(
                    "No existe una suscripción con ese número.",
                    "Suscripción no encontrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtID.Focus();
                return;
            }

            suscripcionEncontrada = suscripcion;
            MostrarSuscripcion(suscripcionEncontrada);
            btnDelete.Enabled = true;
        }

        private void MostrarSuscripcion(SuscripcionStreaming suscripcion)
        {
            ID.Text = suscripcion.id.ToString();

            if (suscripcion.usuario != null)
            {
                Nombre.Text = suscripcion.usuario.nombre + " (Código: " + suscripcion.usuario.codigo + ")";
            }
            else
            {
                Nombre.Text = "Sin usuario asociado";
            }

            chkActiva.Checked = suscripcion.activa;
            Dispositivos.Text = suscripcion.dispositivosSimultaneos.ToString();
            fechai.Text = suscripcion.fechaInicio.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (eliminando)
            {
                return;
            }

            if (suscripcionEncontrada == null)
            {
                MessageBox.Show(
                    "Primero debe buscar una suscripción existente.",
                    "Sin suscripción seleccionada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Seguro que desea eliminar esta suscripción?\n\n" +
                "ID: " + suscripcionEncontrada.id + "\n" +
                "Usuario: " + (suscripcionEncontrada.usuario != null ? suscripcionEncontrada.usuario.nombre : "Sin usuario") + "\n" +
                "Plataforma: " + suscripcionEncontrada.plataforma,
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            try
            {
                eliminando = true;
                btnDelete.Enabled = false;

                int idEliminar = suscripcionEncontrada.id;

                string respuesta = await apiService.EliminarSuscripcion(idEliminar);

                if (respuesta == "OK")
                {
                    MessageBox.Show(
                        "Suscripción eliminada correctamente.",
                        "Eliminación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    txtID.Clear();
                    suscripcionEncontrada = null;
                    PrepararPantalla();
                }
                else
                {
                    MessageBox.Show(
                        respuesta,
                        "No se pudo eliminar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                btnDelete.Enabled = true;
            }
            finally
            {
                eliminando = false;
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
        }
    }
}