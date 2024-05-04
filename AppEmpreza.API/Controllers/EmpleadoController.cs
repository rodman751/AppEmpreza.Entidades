using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppEmpreza.Entidades;

namespace AppEmpreza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbContext _context;

        public EmpleadoController(DbContext context)
        {
            _context = context;
        }

        // GET: api/Empleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleados>>> GetEmpleados()
        {
            return await _context.Empleados.ToListAsync();
        }

        // GET: api/Empleado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleados>> GetEmpleados(int id)
        {
            var empleados = await _context
                .Empleados
                .Include(e => e.Cargos)
                .Include(e => e.Departamento)
                .Where(e => e.Id==id)
                .FirstOrDefaultAsync();

            if (empleados == null)
            {
                return NotFound();
            }

            return empleados;
        }

        // PUT: api/Empleado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleados(int id, Empleados empleados)
        {
            if (id != empleados.Id)
            {
                return BadRequest();
            }

            _context.Entry(empleados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Empleado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empleados>> PostEmpleados(Empleados empleados)
        {
            _context.Empleados.Add(empleados);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpleados", new { id = empleados.Id }, empleados);
        }

        // DELETE: api/Empleado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleados(int id)
        {
            var empleados = await _context.Empleados.FindAsync(id);
            if (empleados == null)
            {
                return NotFound();
            }

            _context.Empleados.Remove(empleados);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpleadosExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
