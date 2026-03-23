using PruebaTecnica_Akkodis.Controllers.FileImportController;
using PruebaTecnica_Akkodis.Controllers.FileImportController.DTO;
using PruebaTecnica_Akkodis.Controllers.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaTecnica_Akkodis
{
    public partial class ClientsUI : Form
    {
        private string FileRoute = "";

        public ClientsUI()
        {
            InitializeComponent();
        }

        private void ClientsUI_Load(object sender, EventArgs e)
        {
            string persistedFile = System.IO.Path.Combine(Application.StartupPath, "clientes_store.csv");

            if (!System.IO.File.Exists(persistedFile))
            {
                System.IO.File.WriteAllText(persistedFile, "dni,nombre,apellidos,fechaNacimiento,telefono,email\n");
            }

            FileRoute = persistedFile;
            LoadData(FileRoute);
            Estado.Text = "Datos cargados automáticamente";
        }

        private void Import_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string format = Format.Text.ToLower();

                openFileDialog.Filter = $"{format.ToUpper()} files (*.{format})|*.{format}";
                openFileDialog.Title = "Select a file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileRoute = openFileDialog.FileName;
                    LoadData(FileRoute);
                }
            }
        }

        private void Format_Click(object sender, EventArgs e)
        {
            Format.Text = Format.Text == "CSV" ? "JSON" : "CSV";
        }

        private IClienteFactory LoadFactory(string format)
        {
            IClienteFactory importer = null;

            switch (format)
            {
                case "CSV":
                    importer = new CsvClienteFactory();
                    break;
                case "JSON":
                    importer = new JsonClienteFactory();
                    break;
            }

            return importer;
        }

        private void LoadData(string fileRoute)
        {
            IClienteFactory importer = LoadFactory(Format.Text);

            if(importer != null)
            {
                List<Cliente> clientes = importer.Import(fileRoute);
                FillInfo(clientes);
            }
            else
            {
                MessageBox.Show("Error al abrir el importador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateAllFields(List<Cliente> clientes)
        {
            bool query = true;

            foreach (var cliente in clientes)
            {
                string error = "";
                if (!ClienteValidator.Validate(cliente, out error))
                {
                    MessageBox.Show(error, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    query = false;
                    break;
                }
            }

            return query;
        }

        private void Save(string fileRoute)
        {
            IClienteFactory importer = LoadFactory(Format.Text);
            List<Cliente> clientsListFromGrid = GetClientesFromGrid();

            if (importer == null || string.IsNullOrWhiteSpace(FileRoute))
                return;

            var clientes = importer.Import(FileRoute).ToList();
            bool fieldsAreCorrect = ValidateAllFields(clientsListFromGrid);

            if (!fieldsAreCorrect)
                return;

            foreach (var cliente in clientes)
            {
                importer.Delete(FileRoute, cliente.DNI);
            }

            foreach (var cliente in clientsListFromGrid)
            {
                importer.Add(FileRoute, cliente);
            }

            FillInfo(importer.Import(FileRoute));
        }

        private List<Cliente> GetClientesFromGrid()
        {
            List<Cliente> list = new List<Cliente>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                list.Add(new Cliente
                {
                    DNI = row.Cells[0].Value?.ToString(),
                    Nombre = row.Cells[1].Value?.ToString(),
                    Apellidos = row.Cells[2].Value?.ToString(),
                    FechaNacimiento = DateTime.TryParse(row.Cells[3].Value?.ToString(), out DateTime fecha)
                    ? fecha : DateTime.MinValue,
                    Telefono = row.Cells[4].Value?.ToString(),
                    Email = row.Cells[5].Value?.ToString()
                });
            }

            return list;
        }

        private void FillInfo(List<Cliente> info)
        {
            dataGridView1.Rows.Clear();

            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = info.Count;
            ProgressBar.Value = 0;

            foreach (var cliente in info)
            {
                dataGridView1.Rows.Add(
                    cliente.DNI,
                    cliente.Nombre,
                    cliente.Apellidos,
                    cliente.FechaNacimiento.ToString("yyyy-MM-dd"),
                    cliente.Telefono,
                    cliente.Email
                );

                ProgressBar.Value++;
                Estado.Text = $"Cargando {ProgressBar.Value}/{ProgressBar.Maximum}";
                Application.DoEvents();
            }

            TotalClientes.Text = $"Total Clientes: {info.Count}";
            Estado.Text = "Datos cargados";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save(FileRoute);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            IClienteFactory importer = LoadFactory(Format.Text);

            if(importer != null)
                FillInfo(importer.Import(FileRoute));
        }
    }
}
