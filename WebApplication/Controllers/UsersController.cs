#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Data;
using Scheduler.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public UsersController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Username)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Username }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Username == id);
        }

        public User Login(User user)
        {
            ActionResult<User> result = GetUser(user.Username).Result;
            /*System.Diagnostics.Debug.WriteLine("----------------------------------");
            System.Diagnostics.Debug.WriteLine(GetUser(user.Username).Status);
            System.Diagnostics.Debug.WriteLine(GetUser(user.Username).Result);
            System.Diagnostics.Debug.WriteLine(result.Value);
            System.Diagnostics.Debug.WriteLine(result.GetType());
            System.Diagnostics.Debug.WriteLine(result.Result);
            System.Diagnostics.Debug.WriteLine("----------------------------------");
            */
            User test = result.Value;
            if (test != null & test.Username == user.Username & test.Password == user.Password)
            {
                return test;
            }
            else
            {
                return null;
            }

        }
        public User Create(User user)
        {
            var action = PostUser(user);
            /*
            System.Diagnostics.Debug.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            System.Diagnostics.Debug.WriteLine(action);
            System.Diagnostics.Debug.WriteLine(action.GetType());
            System.Diagnostics.Debug.WriteLine(action.Result);
            System.Diagnostics.Debug.WriteLine(action.Result.GetType());
            System.Diagnostics.Debug.WriteLine(action.Result.Value);
            System.Diagnostics.Debug.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            */
            var result = action.Result;

            if (result.GetType() != Conflict().GetType())
            {
                User test = result.Value;
                return test;

            }else
            {
                return null;
            }
        }
    }
}
