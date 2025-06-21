using Car360.Contexts;
using Car360.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Car360.Controllers
{
    public class SignController : Controller
    {
        private readonly MyContext _context;
        private readonly IWebHostEnvironment _env;

        public SignController(MyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index() => View();

        public IActionResult Gallery() => View();

        public IActionResult Feedback() => View();

        public IActionResult About() => View();

        public IActionResult Sign() => View();

        [HttpPost]
        public async Task<IActionResult> Sign(IFormFile s_image, string s_user, string s_mail, string s_phone, Sign user)
        {
            if (s_image == null || user == null)
            {
                ViewBag.image = "*Image is required";
                return View();
            }

            string extension = Path.GetExtension(s_image.FileName)?.ToLower();
            if (extension != ".jpg" && extension != ".png")
            {
                ViewBag.image = "*Only jpg or png image allowed";
                return View();
            }

            var existingUser = _context.tbl_sign.FirstOrDefault(u =>
                u.s_user == s_user || u.s_mail == s_mail || u.s_phone == s_phone);

            if (existingUser != null)
            {
                ViewBag.user = existingUser.s_user == s_user ? "*Username already taken" : null;
                ViewBag.mail = existingUser.s_mail == s_mail ? "*Email already taken" : null;
                ViewBag.phone = existingUser.s_phone == s_phone ? "*Phone already taken" : null;
                return View();
            }

            string dirPath = Path.Combine(_env.WebRootPath, "User-image", s_mail);
            Directory.CreateDirectory(dirPath);

            string filePath = Path.Combine(dirPath, Path.GetFileName(s_image.FileName));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await s_image.CopyToAsync(stream);
            }

            user.s_image = s_image.FileName;
            _context.tbl_sign.Add(user);
            await _context.SaveChangesAsync();

            TempData["signup"] = "Sign Up Successfully!";
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string txtuser, string txtpass)
        {
            var login = _context.tbl_sign.FirstOrDefault(u => u.s_user == txtuser && u.s_pass == txtpass);

            if (login != null)
            {
                if (login.s_status == 1)
                {
                    HttpContext.Session.SetString("myuser", login.s_id.ToString());
                    TempData["loginsuccess"] = "Login Successfully!";
                    return RedirectToAction("Index");
                }
                TempData["deactive"] = "Your account has been deactivated.";
            }
            else
            {
                TempData["loginfailed"] = "Username or password is incorrect!";
            }
            return View();
        }

        public IActionResult Product()
        {
            var products = _context.tbl_product.ToList();
            return View(products);
        }

        public IActionResult Appointment(int id)
        {
            var sessionUserId = HttpContext.Session.GetString("myuser");
            if (sessionUserId == null)
            {
                TempData["loginfirst"] = "Please login first!";
                return RedirectToAction("Login");
            }

            var product = _context.tbl_product.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Appointment(Buy buy)
        {
            _context.tbl_buy.Add(buy);
            _context.SaveChanges();
            TempData["Book"] = "Your appointment was booked successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Profile()
        {
            var sessionUserId = HttpContext.Session.GetString("myuser");
            if (sessionUserId == null)
            {
                TempData["loginfirst"] = "Please login first!";
                return RedirectToAction("Login");
            }

            int userId = int.Parse(sessionUserId);
            var userData = _context.tbl_sign.Where(u => u.s_id == userId).ToList();
            return View(userData);
        }

        public IActionResult ProfileUpdate(int id)
        {
            var user = _context.tbl_sign.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult ProfileUpdate(Sign user)
        {
            _context.tbl_sign.Update(user);
            _context.SaveChanges();
            TempData["Password"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("myuser");
            return RedirectToAction("Login");
        }
    }
}
