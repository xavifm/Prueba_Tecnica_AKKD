using Microsoft.AspNetCore.Mvc;
using PruebaTecnica_Akkodis_2.ApplicationServices;
using PruebaTecnica_Akkodis_2.CrossCutting.DTO;

namespace PruebaTecnica_Akkodis_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseApplicationServices _service;

        /// <summary>
        /// Inicializa el controlador y configura el servicio de aplicación.
        /// Garantiza la existencia del fichero de persistencia en disco.
        /// </summary>
        public DatabaseController()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "clientes_store.json");

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, "[]");
            }

            _service = new DatabaseApplicationServices(filePath);
        }

        /// <summary>
        /// Obtiene la lista completa de clientes almacenados.
        /// </summary>
        /// <returns>
        /// Resultado HTTP 200 con la lista de clientes.
        /// </returns>
        [HttpGet("clientes")]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _service.GetAllClientes();
            return Ok(clientes);
        }

        /// <summary>
        /// Obtiene un cliente concreto a partir de su DNI.
        /// </summary>
        /// <param name="dni">DNI del cliente a buscar.</param>
        /// <returns>
        /// 200 OK con el cliente si existe; 404 NotFound en caso contrario.
        /// </returns>
        [HttpGet("clientes/{dni}")]
        public async Task<IActionResult> GetByDni(string dni)
        {
            var cliente = await _service.GetClienteByDni(dni);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        /// <summary>
        /// Crea un nuevo cliente a partir de los datos recibidos en el cuerpo de la petición.
        /// </summary>
        /// <param name="cliente">Objeto cliente recibido en formato JSON.</param>
        /// <returns>
        /// 201 Created si el cliente se crea correctamente;
        /// 400 BadRequest si hay errores de validación o duplicidad.
        /// </returns>
        [HttpPost("clientes")]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            var result = await _service.AddCliente(cliente);

            if (!result.Ok)
                return BadRequest(new { message = result.Error });

            return Created($"/database/clientes/{cliente.DNI}", cliente);
        }

        /// <summary>
        /// Elimina un cliente existente a partir de su DNI.
        /// </summary>
        /// <param name="dni">DNI del cliente a eliminar.</param>
        /// <returns>
        /// 204 NoContent si se elimina correctamente;
        /// 404 NotFound si el cliente no existe.
        /// </returns>
        [HttpDelete("clientes/{dni}")]
        public async Task<IActionResult> Delete(string dni)
        {
            bool deleted = await _service.DeleteCliente(dni);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}