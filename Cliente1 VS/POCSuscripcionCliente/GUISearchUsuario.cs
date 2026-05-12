using POCSuscripcionCliente.Models;
using POCSuscripcionCliente.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUISearchUsuario : Form
    {
        private ApiService apiService = new ApiService();

        public GUISearchUsuario()
        {
            InitializeComponent();

            ConfigurarTabla();

            buttonCodigo.Click += btnBuscarCodigo_Click;
            buttonNombre.Click += btnBuscarNombre_Click;
        }

        private void ConfigurarTabla()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Código",
                DataPropertyName = "codigo",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Documento",
                DataPropertyName = "numDocumento",
                Width = 120
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tipo Doc",
                DataPropertyName = "tipoDoc",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "nombre",
                Width = 150
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Email",
                DataPropertyName = "email",
                Width = 180
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                DataPropertyName = "EstadoTexto",
                Width = 90
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Registro",
                DataPropertyName = "fechaRegistro",
                Width = 150
            });

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private async void btnBuscarCodigo_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            if (string.IsNullOrWhiteSpace(textCodigo.Text))
            {
                MessageBox.Show(
                    "Ingresa el código del usuario.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                textCodigo.Focus();
                return;
            }

            if (!int.TryParse(textCodigo.Text.Trim(), out int codigo) || codigo <= 0)
            {
                MessageBox.Show(
                    "El código debe ser un número mayor a 0.",
                    "Dato inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                textCodigo.Focus();
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

                textCodigo.Focus();
                return;
            }

            List<Usuario> resultado = new List<Usuario>();
            resultado.Add(usuario);

            MostrarUsuarios(resultado);
        }

        private async void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            if (string.IsNullOrWhiteSpace(textNombre.Text))
            {
                MessageBox.Show(
                    "Ingresa el nombre del usuario.",
                    "Campo obligatorio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                textNombre.Focus();
                return;
            }

            if (textNombre.Text.Trim().Length < 2)
            {
                MessageBox.Show(
                    "Escribe al menos 2 letras del nombre.",
                    "Dato insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                textNombre.Focus();
                return;
            }

            List<Usuario> usuarios = await apiService.BuscarUsuarioPorNombre(textNombre.Text.Trim());

            if (usuarios == null || usuarios.Count == 0)
            {
                MessageBox.Show(
                    "No se encontraron usuarios con ese nombre.",
                    "Sin resultados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                textNombre.Focus();
                return;
            }

            MostrarUsuarios(usuarios);
        }

        private void MostrarUsuarios(List<Usuario> usuarios)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = usuarios;
        }

        private void GUISearchUsuario_Load(object sender, EventArgs e)
        {
        }

        private void textCodigo_TextChanged(object sender, EventArgs e)
        {
        }
    }
}