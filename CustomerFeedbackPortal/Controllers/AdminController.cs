using Microsoft.AspNetCore.Mvc;
using CustomerFeedbackPortal.Models;
using CustomerFeedbackPortal.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CustomerFeedbackPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly CustomerFeedbackPortalContext _context;

        public AdminController(CustomerFeedbackPortalContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            var existingAdmin = _context.Admin
                .FirstOrDefault(a => admin.Username == admin.Username && admin.Password == admin.Password);

            if (existingAdmin != null)
            {
                TempData["AdminLoggedIn"] = true;
                return RedirectToAction("Dashboard");
            }

            ViewBag.Message = "Invalid Username or Password";
            return View();
        }

        public IActionResult Dashboard()
        {
            if (TempData["AdminLoggedIn"] == null)
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Logout()
        {
            TempData["AdminLoggedIn"] = null;
            return RedirectToAction("Login");
        }
    }
}