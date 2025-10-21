using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SociedadesAPI.Data;
using SociedadesAPI.DTO;
using SociedadesAPI.Models;

namespace SociedadesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SociedadesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SociedadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sociedad>>> GetAll()
        {
            return await _context.Sociedades.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sociedad>> GetById(int id)
        {
            var sociedad = await _context.Sociedades.FindAsync(id);
            if (sociedad == null) return NotFound( new {message = $"No se encontro el {id} id de la sociedad"});

            return sociedad;
        }

        [HttpGet("buscar/{claveCasfim}")]
        public async Task<ActionResult<Sociedad>> FindByClaveCasfim (string claveCasfim)
        {
            var sociedad = await _context.Sociedades.FirstOrDefaultAsync(
                s => s.ClaveCasfim == claveCasfim);

            if (sociedad == null)
            {
                return NotFound(new { message = $"No se encontró la sociedad con clave {claveCasfim}" });
            }

            return Ok(sociedad);
        }

        [HttpPost("importar-json")]
        public async Task<IActionResult> ImportarJson([FromBody] List<DataWrapper> input)
        {
            if (input == null || input.Count == 0)
            {
                return BadRequest(new { message = "El JSOn esta vacion o no tiene el formato correcto" });
            }

            var sociedades = new List<Sociedad>();

            foreach (var data in input)
            {
                foreach (var res in data.data)
                {
                    foreach (var item in res.result)
                    {

                        if (await _context.Sociedades.AnyAsync(s => s.ClaveCasfim == item.clave_casfim))
                            continue;

                        var sociedad = new Sociedad
                        {
                            Consecutivo = item.consecutivo,
                            ClaveCasfim = item.clave_casfim,
                            SociedadNombre = item.sociedad,
                            CapitalNeto = item.capital_neto,
                            RequerimientoCapital = item.requerimiento_capital,
                            Nicap = item.nicap,
                            Categoria = item.categoria,
                            Federacion = item.federacion

                        };

                        sociedades.Add(sociedad);
                        
                    }
                }
            }

            _context.Sociedades.AddRange(sociedades);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"{sociedades.Count()} registros importados correctamente" });
        }

        [HttpPost("importar-json-archivo")]
        public async Task<IActionResult> ImportarJsonArchivo(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest(new { message = "Debes subir un archivo .json válido" });
            }

            using var stream = new StreamReader(archivo.OpenReadStream());
            var contenido = await stream.ReadToEndAsync();

            List<DataWrapper>? input;
            try
            {
                input = JsonConvert.DeserializeObject<List<DataWrapper>>(contenido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al leer el archivo JSON", error = ex.Message });
            };

            if (input == null || input.Count == 0)
                return BadRequest(new { message = "El archivo no contiene datos válidos." });

            var sociedades = new List<Sociedad>();

            foreach (var data in input)
            {
                foreach (var res in data.data)
                {
                    foreach (var item in res.result)
                    {
                        if (await _context.Sociedades.AnyAsync(s => s.ClaveCasfim == item.clave_casfim))
                            continue;

                        var sociedad = new Sociedad
                        {
                            Consecutivo = item.consecutivo,
                            ClaveCasfim = item.clave_casfim,
                            SociedadNombre = item.sociedad,
                            CapitalNeto = item.capital_neto,
                            RequerimientoCapital = item.requerimiento_capital,
                            Nicap = item.nicap,
                            Categoria = item.categoria,
                            Federacion = item.federacion
                        };

                        sociedades.Add(sociedad);
                    }
                }
            }

            _context.Sociedades.AddRange(sociedades);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"{sociedades.Count()} registros importados correctamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSociedad(int id, Sociedad sociedad)
        {
            if (id != sociedad.Id) return BadRequest();

            _context.Entry(sociedad).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Se realizaron las modificaciones de manera correcta."});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sociedad = await _context.Sociedades.FindAsync(id);
            if (sociedad == null) return NotFound();

            _context.Sociedades.Remove(sociedad);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Se elimino la sociedad con {id} id de manera correcta." });

        }
    }
}


//clave_casfim