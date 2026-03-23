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
        /// <summary>
        /// Valida un objeto cliente comprobando que sus campos cumplan
        /// las reglas básicas de formato y consistencia.
        /// </summary>
        /// <param name="cliente">Cliente a validar.</param>
        /// <param name="error">
        /// Mensaje descriptivo del error encontrado en caso de validación fallida.
        /// </param>
        /// <returns>
        /// True si el cliente es válido; en caso contrario, false.
        /// </returns>
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

        /// <summary>
        /// Comprueba si el email proporcionado tiene un formato válido.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico a validar.</param>
        /// <returns>
        /// True si el formato del email es válido; en caso contrario, false.
        /// </returns>
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

        /// <summary>
        /// Comprueba si el número de teléfono contiene únicamente dígitos
        /// y cumple una longitud razonable.
        /// </summary>
        /// <param name="telefono">Número de teléfono a validar.</param>
        /// <returns>
        /// True si el teléfono es válido; en caso contrario, false.
        /// </returns>
        private static bool IsValidPhone(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            string limpio = telefono.Replace(" ", "").Replace("-", "");

            return limpio.All(char.IsDigit) && limpio.Length >= 9 && limpio.Length <= 15;
        }
    }
}
