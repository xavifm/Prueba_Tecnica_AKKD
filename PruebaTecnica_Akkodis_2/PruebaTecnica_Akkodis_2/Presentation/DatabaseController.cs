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

        public DatabaseController()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "clientes_store.json");

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, "[]");
            }

            _service = new DatabaseApplicationServices(filePath);
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _service.GetAllClientes();
            return Ok(clientes);
        }

        [HttpGet("clientes/{dni}")]
        public async Task<IActionResult> GetByDni(string dni)
        {
            var cliente = await _service.GetClienteByDni(dni);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost("clientes")]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            var result = await _service.AddCliente(cliente);

            if (!result.Ok)
                return BadRequest(new { message = result.Error });

            return Created($"/database/clientes/{cliente.DNI}", cliente);
        }

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