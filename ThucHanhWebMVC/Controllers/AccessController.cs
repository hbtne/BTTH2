using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using ThucHanhWebMVC.Models;

namespace ThucHanhWebMVC.Controllers
{
	public class AccessController : Controller
	{
		private QlbanVaLiContext db = new QlbanVaLiContext();

		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public IActionResult Login(TUser user)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				var u = db.TUsers.Where(x => x.Username.Equals(user.Username) &&
				x.Password.Equals(user.Password)).FirstOrDefault();
				if (u != null)
				{
					HttpContext.Session.SetString("UserName", u.Username.ToString());
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Login", "Access");
		}
        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // Register POST
        [HttpPost]
        public IActionResult Register(TUser user)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                var existingUser = db.TUsers.FirstOrDefault(x => x.Username == user.Username);
                if (existingUser == null)
                {
                    // Add new user to the database
                    db.TUsers.Add(user);
                    db.SaveChanges();

                    // Set session and redirect
                    HttpContext.Session.SetString("UserName", user.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Show error message if username is taken
                    ModelState.AddModelError("Username", "Username already exists.");
                }
            }
            return View(user);
        }
    }
}