using POCSuscripcionCliente.Models;
using POCSuscripcionCliente.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIUpdateUsuario : Form
    {
        private ApiService apiService = new ApiService();
        private Usuario usuarioEncontrado = null;

        public GUIUpdateUsuario()
        {
            InitializeComponent();

            CargarCombos();
            PrepararPantalla();

            btnBuscar.Click += btnBuscar_Click;
            btnActualizar.Click += btnActualizar_Click;
        }

        private void CargarCombos()
        {
            cmbTipoDoc.Items.Clear();
            cmbTipoDoc.Items.Add("CC");
            cmbTipoDoc.Items.Add("TI");
            cmbTipoDoc.Items.Add("PAS");
            cmbTipoDoc.Items.Add("CE");

            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("AC - Activo");
            cmbEstado.Items.Add("IN - Inactivo");

            cmbTipoDoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PrepararPantalla()
        {
            txtCodigo.ReadOnly = true;

            HabilitarCampos(false);
            LimpiarCamposDatos();

            txtBuscarCodigo.Focus();
        }

        private void HabilitarCampos(bool habilitar)
        {
            txtCodigo.Enabled = habilitar;
            txtDocumento.Enabled = habilitar;
            cmbTipoDoc.Enabled = habilitar;
            txtNombre.Enabled = habilitar;
            txtEmail.Enabled = habilitar;
            cmbEstado.Enabled = habilitar;
            btnActualizar.Enabled = habilitar;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            usuarioEncontrado = null;
            LimpiarCamposDatos();
            HabilitarCampos(false);

            if (string.IsNullOrWhiteSpace(txtBuscarCodigo.Text))
            {
                MessageBox.Show(
                    "Ingresa el código del usuario que deseas buscar.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtBuscarCodigo.Focus();
                return;
            }

            if (!int.TryParse(txtBuscarCodigo.Text.Trim(), out int codigo) || codigo <= 0)
            {
                MessageBox.Show(
                    "El código debe ser un número mayor a 0.",
                    "Dato inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtBuscarCodigo.Focus();
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

                txtBuscarCodigo.Focus();
                return;
            }

            usuarioEncontrado = usuario;
            MostrarUsuario(usuarioEncontrado);
            HabilitarCampos(true);

            txtCodigo.ReadOnly = true;
            txtCodigo.Enabled = true;
            txtDocumento.Focus();
        }

        private void MostrarUsuario(Usuario usuario)
        {
            txtCodigo.Text = usuario.codigo.ToString();
            txtDocumento.Text = usuario.numDocumento.ToString();
            txtNombre.Text = usuario.nombre;
            txtEmail.Text = usuario.email;

            cmbTipoDoc.SelectedItem = usuario.tipoDoc;

            if (usuario.estado == "AC")
            {
                cmbEstado.SelectedItem = "AC - Activo";
            }
            else
            {
                cmbEstado.SelectedItem = "IN - Inactivo";
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
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

            if (!ValidarCampos())
            {
                return;
            }

            Usuario usuarioActualizado = new Usuario
            {
                codigo = usuarioEncontrado.codigo,
                numDocumento = long.Parse(txtDocumento.Text.Trim()),
                tipoDoc = cmbTipoDoc.SelectedItem.ToString(),
                nombre = txtNombre.Text.Trim(),
                email = txtEmail.Text.Trim().ToLower(),
                estado = ObtenerEstadoSeleccionado(),
                fechaRegistro = usuarioEncontrado.fechaRegistro
            };

            string respuesta = await apiService.ActualizarUsuario(usuarioEncontrado.codigo, usuarioActualizado);

            if (respuesta == "OK")
            {
                MessageBox.Show(
                    "Usuario actualizado correctamente.",
                    "Actualización exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtBuscarCodigo.Clear();
                usuarioEncontrado = null;
                PrepararPantalla();
            }
            else
            {
                MessageBox.Show(
                    respuesta,
                    "No se pudo actualizar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Ingresa el número de documento.");
                txtDocumento.Focus();
                return false;
            }

            if (!long.TryParse(txtDocumento.Text.Trim(), out long documento))
            {
                MessageBox.Show("El documento solo debe contener números.");
                txtDocumento.Focus();
                return false;
            }

            if (txtDocumento.Text.Trim().Length < 6 || txtDocumento.Text.Trim().Length > 15)
            {
                MessageBox.Show("El documento debe tener entre 6 y 15 dígitos.");
                txtDocumento.Focus();
                return false;
            }

            if (cmbTipoDoc.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona el tipo de documento.");
                cmbTipoDoc.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingresa el nombre completo.");
                txtNombre.Focus();
                return false;
            }

            if (txtNombre.Text.Trim().Length < 3)
            {
                MessageBox.Show("El nombre debe tener mínimo 3 caracteres.");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Ingresa el email.");
                txtEmail.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                MessageBox.Show("Ingresa un email válido. Ejemplo: usuario@test.com");
                txtEmail.Focus();
                return false;
            }

            if (cmbEstado.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona el estado del usuario.");
                cmbEstado.Focus();
                return false;
            }

            return true;
        }

        private string ObtenerEstadoSeleccionado()
        {
            if (cmbEstado.SelectedItem.ToString().StartsWith("AC"))
            {
                return "AC";
            }

            return "IN";
        }

        private void LimpiarCamposDatos()
        {
            txtCodigo.Clear();
            txtDocumento.Clear();
            txtNombre.Clear();
            txtEmail.Clear();

            cmbTipoDoc.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}