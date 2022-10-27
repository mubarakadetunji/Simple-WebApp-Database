using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Models;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly SchedulerContext _context;

        private User _user;
        private List<Concentration> _concentrations;
        public LoginController(SchedulerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ProcessLogin(User user)
        {
            
            UsersController uc = new UsersController(_context);
            User result = uc.Login(user);

            if (result != null)
            {
                return View("LoginSuccess", result);
            }
            else {
                return View("LoginFailure", user);
            }
            
        }

        public IActionResult CreateUser()
        {
            return View("Create");
        }

        public IActionResult UserDetails(User user)
        {
            ConcentrationsController cc = new ConcentrationsController(_context);
            _concentrations = new List<Concentration>();
            if (user.Concentrations != null)
            {
                string[] concs = user.Concentrations.Split(',');

                foreach (string name in concs)
                {
                    Concentration concentration = cc.GetConcentration(name).Result.Value;
                    if (concentration != null)
                    {
                        _concentrations.Add(concentration);
                    }


                }
            }
            return View("UserDetails", (_concentrations,user));
            
        }

        public IActionResult ProcessCreate(User user)
        {
            
            user.Registered = true;
            user.isAdmin = false;
            DateTime date = DateTime.Now;
            user.DateCreated = date;


            UsersController uc = new UsersController(_context);
            User result = uc.Create(user);

            if (result != null)
            {
                return View("CreateSuccess", result);
            }
            else
            {
                return View("CreateFailure", user);
            }

        }
        public IActionResult ProcessGuest(User user)
        {

            user.Registered = false;
            user.isAdmin = false;
            DateTime date = DateTime.Now;
            user.DateCreated = date;

            user.Username = "guest";
            user.Password = "guest";
            user.SchoolEmail = "guest";
            UsersController uc = new UsersController(_context);
            User result = uc.Create(user);

            if (result != null)
            {
                return View("UserDetails", result);
            }
            else
            {
                return View("CreateFailure",user);
            }

        }

        public IActionResult Guest()
        {

            return View("Guest");

        }






    }
}
