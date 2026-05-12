using POCSuscripcionCliente.Models;
using POCSuscripcionCliente.Services;
using System;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIDeleteUsuario : Form
    {
        private ApiService apiService = new ApiService();
        private Usuario usuarioEncontrado = null;

        public GUIDeleteUsuario()
        {
            InitializeComponent();

            btnBuscar.Click += btnBuscar_Click;
            btnEliminar.Click += btnEliminar_Click;

            PrepararPantalla();
        }

        private void PrepararPantalla()
        {
            LimpiarDatosUsuario();

            btnEliminar.Enabled = false;
            txtCodigo.Focus();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            LimpiarDatosUsuario();
            usuarioEncontrado = null;
            btnEliminar.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(
                    "Ingresa el código del usuario que deseas buscar.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtCodigo.Focus();
                return;
            }

            if (!int.TryParse(txtCodigo.Text.Trim(), out int codigo) || codigo <= 0)
            {
                MessageBox.Show(
                    "El código debe ser un número mayor a 0.",
                    "Dato inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtCodigo.Focus();
                return;
            }

            Usuario usuario = await apiService.GetUsuarioByCodigo(codigo);

            if (usuario == null)
            {
                MessageBox.Show(
                    "No existe un usuario con ese código.",
                    "Usuario no encontrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtCodigo.Focus();
                return;
            }

            usuarioEncontrado = usuario;
            MostrarUsuario(usuarioEncontrado);
            btnEliminar.Enabled = true;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioEncontrado == null)
            {
                MessageBox.Show(
                    "Primero debes buscar un usuario existente.",
                    "Sin usuario seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Seguro que deseas eliminar este usuario?\n\n" +
                "Nombre: " + usuarioEncontrado.nombre + "\n" +
                "Código: " + usuarioEncontrado.codigo,
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            string respuesta = await apiService.EliminarUsuario(usuarioEncontrado.codigo);

            if (respuesta == "OK")
            {
                MessageBox.Show(
                    "Usuario eliminado correctamente.",
                    "Eliminación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtCodigo.Clear();
                usuarioEncontrado = null;
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
            }
        }

        private void MostrarUsuario(Usuario usuario)
        {
            lblCodigo.Text = usuario.codigo.ToString();
            lblCodigoValor.Text = usuario.numDocumento.ToString();
            lblTipoDoc.Text = usuario.tipoDoc;
            lblNombre.Text = usuario.nombre;
            lblEmailValor.Text = usuario.email;
            lblEstadoValor.Text = usuario.estado == "AC" ? "Activo" : "Inactivo";

            if (usuario.fechaRegistro.HasValue)
            {
                lblFechaValor.Text = usuario.fechaRegistro.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                lblFechaValor.Text = "Sin fecha";
            }
        }

        private void LimpiarDatosUsuario()
        {
            lblCodigo.Text = "";
            lblCodigoValor.Text = "";
            lblTipoDoc.Text = "";
            lblNombre.Text = "";
            lblEmailValor.Text = "";
            lblEstadoValor.Text = "";
            lblFechaValor.Text = "";
        }

        private void GUIDeleteUsuario_Load(object sender, EventArgs e)
        {
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
        }

        private void bntEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}