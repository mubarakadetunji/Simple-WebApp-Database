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
    public class CoursefeedbacksController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public CoursefeedbacksController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/Coursefeedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coursefeedback>>> GetCoursefeedbacks()
        {
            return await _context.Coursefeedbacks.ToListAsync();
        }

        // GET: api/Coursefeedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coursefeedback>> GetCoursefeedback(string id)
        {
            var coursefeedback = await _context.Coursefeedbacks.FindAsync(id);

            if (coursefeedback == null)
            {
                return NotFound();
            }

            return coursefeedback;
        }

        // PUT: api/Coursefeedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoursefeedback(string id, Coursefeedback coursefeedback)
        {
            if (id != coursefeedback.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(coursefeedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursefeedbackExists(id))
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

        // POST: api/Coursefeedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coursefeedback>> PostCoursefeedback(Coursefeedback coursefeedback)
        {
            _context.Coursefeedbacks.Add(coursefeedback);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CoursefeedbackExists(coursefeedback.CourseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCoursefeedback", new { id = coursefeedback.CourseId }, coursefeedback);
        }

        // DELETE: api/Coursefeedbacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoursefeedback(string id)
        {
            var coursefeedback = await _context.Coursefeedbacks.FindAsync(id);
            if (coursefeedback == null)
            {
                return NotFound();
            }

            _context.Coursefeedbacks.Remove(coursefeedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoursefeedbackExists(string id)
        {
            return _context.Coursefeedbacks.Any(e => e.CourseId == id);
        }
    }
}
