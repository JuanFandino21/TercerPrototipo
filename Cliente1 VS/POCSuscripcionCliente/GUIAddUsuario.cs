using POCSuscripcionCliente.Models;
using POCSuscripcionCliente.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIAddUsuario : Form
    {
        private ApiService apiService = new ApiService();

        public GUIAddUsuario()
        {
            InitializeComponent();
            CargarCombos();

            buttonGuardar.Click += buttonGuardar_Click;
        }

        private void CargarCombos()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("CC");
            comboBox1.Items.Add("TI");
            comboBox1.Items.Add("PAS");
            comboBox1.Items.Add("CE");

            comboBox2.Items.Clear();
            comboBox2.Items.Add("AC - Activo");
            comboBox2.Items.Add("IN - Inactivo");

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = 0;
        }

        private async void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            Usuario usuario = new Usuario
            {
                codigo = int.Parse(textCodigo.Text.Trim()),
                numDocumento = long.Parse(textDocumento.Text.Trim()),
                tipoDoc = comboBox1.SelectedItem.ToString(),
                nombre = textBox4.Text.Trim(),
                email = textBox5.Text.Trim().ToLower(),
                estado = ObtenerEstadoSeleccionado()
            };

            string respuesta = await apiService.CrearUsuario(usuario);

            if (respuesta == "OK")
            {
                MessageBox.Show(
                    "Usuario creado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show(
                    respuesta,
                    "No se pudo guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(textCodigo.Text))
            {
                MessageBox.Show("Ingresa el código del usuario.");
                textCodigo.Focus();
                return false;
            }

            if (!int.TryParse(textCodigo.Text.Trim(), out int codigo) || codigo <= 0)
            {
                MessageBox.Show("El código debe ser un número mayor a 0.");
                textCodigo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textDocumento.Text))
            {
                MessageBox.Show("Ingresa el número de documento.");
                textDocumento.Focus();
                return false;
            }

            if (!long.TryParse(textDocumento.Text.Trim(), out long documento))
            {
                MessageBox.Show("El documento solo debe contener números.");
                textDocumento.Focus();
                return false;
            }

            if (textDocumento.Text.Trim().Length < 6 || textDocumento.Text.Trim().Length > 15)
            {
                MessageBox.Show("El documento debe tener entre 6 y 15 dígitos.");
                textDocumento.Focus();
                return false;
            }

            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona el tipo de documento.");
                comboBox1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Ingresa el nombre completo.");
                textBox4.Focus();
                return false;
            }

            if (textBox4.Text.Trim().Length < 3)
            {
                MessageBox.Show("El nombre debe tener mínimo 3 caracteres.");
                textBox4.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Ingresa el email.");
                textBox5.Focus();
                return false;
            }

            if (!Regex.IsMatch(textBox5.Text.Trim(), @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                MessageBox.Show("Ingresa un email válido. Ejemplo: usuario@test.com");
                textBox5.Focus();
                return false;
            }

            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona el estado del usuario.");
                comboBox2.Focus();
                return false;
            }

            return true;
        }

        private string ObtenerEstadoSeleccionado()
        {
            if (comboBox2.SelectedItem.ToString().StartsWith("AC"))
            {
                return "AC";
            }

            return "IN";
        }

        private void LimpiarCampos()
        {
            textCodigo.Clear();
            textDocumento.Clear();
            textBox4.Clear();
            textBox5.Clear();

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = 0;

            textCodigo.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}