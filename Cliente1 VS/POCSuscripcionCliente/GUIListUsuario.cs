using POCSuscripcionCliente.Models;
using POCSuscripcionCliente.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace POCSuscripcionCliente
{
    public partial class GUIListUsuario : Form
    {
        private ApiService apiService = new ApiService();
        private List<Usuario> usuarios = new List<Usuario>();

        public GUIListUsuario()
        {
            InitializeComponent();

            CargarCombos();
            ConfigurarTabla();

            this.Load += GUIListUsuario_Load;
        }

        private async void GUIListUsuario_Load(object sender, EventArgs e)
        {
            await CargarUsuarios();
        }

        private void CargarCombos()
        {
            cmbCriterio.Items.Clear();
            cmbCriterio.Items.Add("Código");
            cmbCriterio.Items.Add("Documento");
            cmbCriterio.Items.Add("Nombre");
            cmbCriterio.Items.Add("Email");

            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Todos");
            cmbEstado.Items.Add("Activos");
            cmbEstado.Items.Add("Inactivos");

            cmbCriterio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCriterio.SelectedIndex = 2; // Nombre
            cmbEstado.SelectedIndex = 0;   // Todos
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

        private async System.Threading.Tasks.Task CargarUsuarios()
        {
            usuarios = await apiService.GetUsuarios();

            if (usuarios == null)
            {
                usuarios = new List<Usuario>();
            }

            MostrarUsuarios(usuarios);

            if (usuarios.Count == 0)
            {
                MessageBox.Show(
                    "Aún no hay usuarios registrados.",
                    "Lista vacía",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void MostrarUsuarios(List<Usuario> lista)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (usuarios == null || usuarios.Count == 0)
            {
                MessageBox.Show(
                    "No hay usuarios cargados para buscar.",
                    "Sin datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            string criterio = cmbCriterio.SelectedItem.ToString();
            string texto = textBox1.Text.Trim().ToLower();
            string estado = cmbEstado.SelectedItem.ToString();

            List<Usuario> resultado = usuarios;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                if (criterio == "Código")
                {
                    resultado = resultado
                        .Where(u => u.codigo.ToString() == texto)
                        .ToList();
                }
                else if (criterio == "Documento")
                {
                    resultado = resultado
                        .Where(u => u.numDocumento.ToString().Contains(texto))
                        .ToList();
                }
                else if (criterio == "Nombre")
                {
                    resultado = resultado
                        .Where(u => u.nombre != null && u.nombre.ToLower().Contains(texto))
                        .ToList();
                }
                else if (criterio == "Email")
                {
                    resultado = resultado
                        .Where(u => u.email != null && u.email.ToLower().Contains(texto))
                        .ToList();
                }
            }

            if (estado == "Activos")
            {
                resultado = resultado
                    .Where(u => u.estado == "AC")
                    .ToList();
            }
            else if (estado == "Inactivos")
            {
                resultado = resultado
                    .Where(u => u.estado == "IN")
                    .ToList();
            }

            MostrarUsuarios(resultado);

            if (resultado.Count == 0)
            {
                MessageBox.Show(
                    "No hay usuarios que coincidan con los filtros seleccionados.",
                    "Sin resultados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private async void btnLimpiar_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            cmbCriterio.SelectedIndex = 2;
            cmbEstado.SelectedIndex = 0;

            await CargarUsuarios();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void cmbCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}