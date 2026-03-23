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

        /// <summary>
        /// Inicializa una nueva instancia del formulario principal de gestión de clientes.
        /// </summary>
        public ClientsUI()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Se ejecuta al cargar el formulario.
        /// Comprueba si existe el fichero de persistencia local y, en caso contrario, lo crea
        /// con la cabecera correspondiente. Después carga automáticamente los datos en la interfaz.
        /// </summary>
        /// <param name="sender">Objeto que lanza el evento.</param>
        /// <param name="e">Datos asociados al evento de carga.</param>
        /// <returns>No devuelve ningún valor.</returns>
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

        /// <summary>
        /// Abre una ventana de selección de fichero para importar clientes
        /// en el formato actualmente seleccionado en la interfaz.
        /// </summary>
        /// <param name="sender">Objeto que lanza el evento.</param>
        /// <param name="e">Datos asociados al evento de clic.</param>
        /// <returns>No devuelve ningún valor.</returns>
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

        /// <summary>
        /// Cambia el formato de trabajo seleccionado entre CSV y JSON.
        /// </summary>
        /// <param name="sender">Objeto que lanza el evento.</param>
        /// <param name="e">Datos asociados al evento de clic.</param>
        /// <returns>No devuelve ningún valor.</returns>
        private void Format_Click(object sender, EventArgs e)
        {
            Format.Text = Format.Text == "CSV" ? "JSON" : "CSV";
        }

        /// <summary>
        /// Obtiene el factory correspondiente del formato indicado.
        /// </summary>
        /// <param name="format">Formato de fichero a utilizar. Puede ser CSV o JSON.</param>
        /// <returns>
        /// Una instancia de <see cref="IClienteFactory"/> compatible con el formato indicado,
        /// o null si el formato no está soportado.
        /// </returns>
        private IClienteFactory LoadFactory(string format)
        {
            IClienteFactory importer = null;

            string extension = System.IO.Path.GetExtension(FileRoute)?.ToLower();

            if ((format == "CSV" && extension != ".csv") || (format == "JSON" && extension != ".json"))
            {
                MessageBox.Show("El formato seleccionado no coincide con la extensión del fichero.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

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

        /// <summary>
        /// Carga los datos de clientes desde el fichero indicado y los muestra en la interfaz.
        /// Utiliza el importador correspondiente según el formato seleccionado (CSV o JSON).
        /// </summary>
        /// <param name="fileRoute">Ruta del fichero desde el que se cargarán los datos.</param>
        /// <returns>No devuelve ningún valor.</returns>
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

        /// <summary>
        /// Valida todos los clientes recibidos comprobando que sus datos cumplan
        /// las reglas básicas definidas por la aplicación.
        /// </summary>
        /// <param name="clientes">Lista de clientes a validar.</param>
        /// <returns>
        /// True si todos los clientes son válidos; en caso contrario, false.
        /// </returns>
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

        /// <summary>
        /// Guarda en el fichero de persistencia los datos actualmente mostrados en la tabla.
        /// Primero elimina los registros existentes y posteriormente inserta los nuevos datos validados.
        /// </summary>
        /// <param name="fileRoute">Ruta del fichero donde se guardarán los datos.</param>
        /// <returns>No devuelve ningún valor.</returns>
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

        /// <summary>
        /// Obtiene la lista de clientes a partir de los datos actualmente visibles
        /// en la rejilla de la interfaz.
        /// </summary>
        /// <returns>
        /// Una lista de objetos <see cref="Cliente"/> generada a partir del contenido
        /// actual del DataGridView.
        /// </returns>
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

        /// <summary>
        /// Rellena la tabla de la interfaz con la lista de clientes proporcionada,
        /// actualizando además la barra de progreso y los indicadores de estado.
        /// </summary>
        /// <param name="info">Lista de clientes a mostrar en la tabla.</param>
        /// <returns>No devuelve ningún valor.</returns>
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

        /// <summary>
        /// Guarda manualmente los datos actuales de la rejilla en el fichero.
        /// </summary>
        /// <param name="sender">Objeto que lanza el evento.</param>
        /// <returns>No devuelve ningún valor.</returns>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save(FileRoute);
        }

        /// <summary>
        /// Recarga los datos desde el fichero actual y actualiza la tabla de la interfaz
        /// con la información más reciente almacenada.
        /// </summary>
        /// <param name="sender">Objeto que lanza el evento.</param>
        /// <returns>No devuelve ningún valor.</returns>
        private void Refresh_Click(object sender, EventArgs e)
        {
            IClienteFactory importer = LoadFactory(Format.Text);

            if(importer != null)
                FillInfo(importer.Import(FileRoute));
        }
    }
}
