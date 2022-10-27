#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Data;
using Scheduler.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcentrationsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public ConcentrationsController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/Concentrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concentration>>> GetConcentrations()
        {
            return await _context.Concentrations.ToListAsync();
        }

        // GET: api/Concentrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Concentration>> GetConcentration(string id)
        {
            var concentration = await _context.Concentrations.FindAsync(id);

            if (concentration == null)
            {
                return NotFound();
            }

            return concentration;
        }

        // PUT: api/Concentrations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcentration(string id, Concentration concentration)
        {
            if (id != concentration.ConcentrationName)
            {
                return BadRequest();
            }

            _context.Entry(concentration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConcentrationExists(id))
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

        // POST: api/Concentrations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Concentration>> PostConcentration(Concentration concentration)
        {
            _context.Concentrations.Add(concentration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ConcentrationExists(concentration.ConcentrationName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetConcentration", new { id = concentration.ConcentrationName }, concentration);
        }

        // DELETE: api/Concentrations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcentration(string id)
        {
            var concentration = await _context.Concentrations.FindAsync(id);
            if (concentration == null)
            {
                return NotFound();
            }

            _context.Concentrations.Remove(concentration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConcentrationExists(string id)
        {
            return _context.Concentrations.Any(e => e.ConcentrationName == id);
        }

        public Concentration GetConc(string name)
        {
            ActionResult<Concentration> result = GetConcentration(name).Result;
            System.Diagnostics.Debug.WriteLine("----------------------------------");
            System.Diagnostics.Debug.WriteLine(result.Value);
            System.Diagnostics.Debug.WriteLine(result.GetType());
            System.Diagnostics.Debug.WriteLine(result.Result);
            System.Diagnostics.Debug.WriteLine("----------------------------------");

            Concentration test = result.Value;
            if (test != null & test.ConcentrationName == name)
            {
                return test;
            }
            else
            {
                return null;
            }

        }
    }
}
