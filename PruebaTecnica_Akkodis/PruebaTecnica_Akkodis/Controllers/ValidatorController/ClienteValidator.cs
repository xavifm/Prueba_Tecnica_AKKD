using PruebaTecnica_Akkodis.Controllers.FileImportController.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_Akkodis.Controllers.Validator
{
    public static class ClienteValidator
    {
        public static bool Validate(Cliente cliente, out string error)
        {
            if (cliente == null)
            {
                error = "El cliente es nulo";
                return false;
            }

            if (string.IsNullOrWhiteSpace(cliente.DNI))
            {
                error = "El DNI no puede estar vacío";
                return false;
            }

            if (cliente.FechaNacimiento == DateTime.MinValue)
            {
                error = "La fecha de nacimiento no es válida";
                return false;
            }

            if (!IsValidEmail(cliente.Email))
            {
                error = "El email no es válido";
                return false;
            }

            if (!IsValidPhone(cliente.Telefono))
            {
                error = "El teléfono no es válido";
                return false;
            }

            error = "";
            return true;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var mail = new MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidPhone(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            string limpio = telefono.Replace(" ", "").Replace("-", "");

            return limpio.All(char.IsDigit) && limpio.Length >= 9 && limpio.Length <= 15;
        }
    }
}
