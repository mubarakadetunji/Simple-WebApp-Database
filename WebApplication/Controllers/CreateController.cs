using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Models;

namespace WebApplication.Controllers
{
    public class CreateController : Controller
    {
        private readonly SchedulerContext _context;

        public CreateController(SchedulerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessCreate(User user)
        {
            DateTime now = DateTime.Now;
            user.DateCreated = now;
            
            UsersController uc = new UsersController(_context);
            User result = uc.Create(user);
            if (result != null)
            {
                return View("CreateSucces", result);
            }
            else
            {
                return View("CreateFailure", user);
            }

        }

        public void Login() 
        {
            Response.Redirect(HttpContext.Request.PathBase + "/login");
        }
    }
}
