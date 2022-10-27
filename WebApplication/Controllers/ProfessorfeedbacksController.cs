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
    public class ProfessorfeedbacksController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public ProfessorfeedbacksController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/Professorfeedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professorfeedback>>> GetProfessorfeedbacks()
        {
            return await _context.Professorfeedbacks.ToListAsync();
        }

        // GET: api/Professorfeedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professorfeedback>> GetProfessorfeedback(string id)
        {
            var professorfeedback = await _context.Professorfeedbacks.FindAsync(id);

            if (professorfeedback == null)
            {
                return NotFound();
            }

            return professorfeedback;
        }

        // PUT: api/Professorfeedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessorfeedback(string id, Professorfeedback professorfeedback)
        {
            if (id != professorfeedback.ProfessorName)
            {
                return BadRequest();
            }

            _context.Entry(professorfeedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorfeedbackExists(id))
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

        // POST: api/Professorfeedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Professorfeedback>> PostProfessorfeedback(Professorfeedback professorfeedback)
        {
            _context.Professorfeedbacks.Add(professorfeedback);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfessorfeedbackExists(professorfeedback.ProfessorName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProfessorfeedback", new { id = professorfeedback.ProfessorName }, professorfeedback);
        }

        // DELETE: api/Professorfeedbacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessorfeedback(string id)
        {
            var professorfeedback = await _context.Professorfeedbacks.FindAsync(id);
            if (professorfeedback == null)
            {
                return NotFound();
            }

            _context.Professorfeedbacks.Remove(professorfeedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfessorfeedbackExists(string id)
        {
            return _context.Professorfeedbacks.Any(e => e.ProfessorName == id);
        }
    }
}
