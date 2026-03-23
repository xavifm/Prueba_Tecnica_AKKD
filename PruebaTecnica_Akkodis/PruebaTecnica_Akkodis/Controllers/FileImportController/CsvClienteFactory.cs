using PruebaTecnica_Akkodis.Controllers.FileImportController.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PruebaTecnica_Akkodis.Controllers.FileImportController
{
    public class CsvClienteFactory : IClienteFactory
    {
        public void Add(string filePath, Cliente cliente)
        {
            var clientes = Import(filePath);

            if (clientes.Any(c => c.DNI == cliente.DNI))
                return;

            clientes.Add(cliente);
            Save(filePath, clientes);
        }

        public void Delete(string filePath, string dni)
        {
            var clientes = Import(filePath);

            var cliente = clientes.FirstOrDefault(c => c.DNI == dni);
            if (cliente == null)
                return;

            clientes.Remove(cliente);
            Save(filePath, clientes);
        }

        public List<Cliente> Import(string filePath)
        {
            List<Cliente> clientes = new List<Cliente>();
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] parts = lines[i].Split(',');

                if (parts.Length < 6)
                    continue;

                clientes.Add(new Cliente
                {
                    DNI = parts[0].Trim(),
                    Nombre = parts[1].Trim(),
                    Apellidos = parts[2].Trim(),
                    FechaNacimiento = DateTime.Parse(parts[3].Trim(), CultureInfo.InvariantCulture),
                    Telefono = parts[4].Trim(),
                    Email = parts[5].Trim()
                });
            }

            return clientes;
        }

        private void Save(string filePath, List<Cliente> clientes)
        {
            List<string> lines = new List<string>
            {
                "dni,nombre,apellidos,fechaNacimiento,telefono,email"
            };

            foreach (var c in clientes)
            {
                lines.Add(string.Join(",",
                    c.DNI,
                    c.Nombre,
                    c.Apellidos,
                    c.FechaNacimiento.ToString("yyyy-MM-dd"),
                    c.Telefono,
                    c.Email
                ));
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
