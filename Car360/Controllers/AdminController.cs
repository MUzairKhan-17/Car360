using Car360.Contexts;
using Car360.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using System.Runtime.Intrinsics.X86;

namespace Car360.Controllers
{
    public class AdminController : Controller
    {
        private MyContext a_context;

        private IWebHostEnvironment env;

        public  AdminController(MyContext context,IWebHostEnvironment _env) 
        { 
            a_context = context;
            env = _env;
        }

        public IActionResult Index()
        {
            var sess = HttpContext.Session.GetString("myadmin");

            if (sess != null)
            {
                var fetching = a_context.tbl_sign.ToList();
                return View(fetching);
            }
            else 
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult Index(string Search)
        {
            List<Sign> Fetching = new List<Sign>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_sign.ToList();
            }
            else
            {
                Fetching = a_context.tbl_sign.FromSqlInterpolated($"SELECT * FROM tbl_sign WHERE s_id LIKE {Search} or s_name LIKE {Search} or s_user LIKE {Search} or s_mail LIKE {Search} or s_phone LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0) 
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult UserActiveDeactive()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_sign.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult UserActiveDeactive(string Search)
        {
            List<Sign> Fetching = new List<Sign>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_sign.ToList();
            }
            else
            {
                Fetching = a_context.tbl_sign.FromSqlInterpolated($"SELECT * FROM tbl_sign WHERE s_id LIKE {Search} or s_name LIKE {Search} or s_user LIKE {Search} or s_mail LIKE {Search} or s_phone LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult UserDeactive(int id) 
        {
            var deactive = a_context.tbl_sign.FirstOrDefault(row => row.s_id == id);

            if (deactive != null)
            {
                deactive.s_status = 0;
                a_context.SaveChanges();
                return RedirectToAction("UserActiveDeactive");
            }
            else 
            {
                return RedirectToAction("UserActiveDeactive");
            }
        }
        public IActionResult UserActive(int id)
        {
            var deactive = a_context.tbl_sign.FirstOrDefault(row => row.s_id == id);
            if (deactive != null)
            {
                deactive.s_status = 1;
                a_context.SaveChanges();
                return RedirectToAction("UserActiveDeactive");
            }
            else
            {
                return RedirectToAction("UserActiveDeactive");
            }
        }
        public IActionResult UserDelete()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_sign.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult UserDelete(string Search)
        {
            List<Sign> Fetching = new List<Sign>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_sign.ToList();
            }
            else
            {
                Fetching = a_context.tbl_sign.FromSqlInterpolated($"SELECT * FROM tbl_sign WHERE s_id LIKE {Search} or s_name LIKE {Search} or s_user LIKE {Search} or s_mail LIKE {Search} or s_phone LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult UserDeleteQuery(int id) 
        {
            var d_id = a_context.tbl_sign.Find(id);
            a_context.tbl_sign.Remove(d_id);
            a_context.SaveChanges();
            return RedirectToAction("UserDelete");
        }
        public IActionResult AdminSignup()
        {
            var check = HttpContext.Session.GetString("myadmin");
            if (check != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpPost]
        public IActionResult AdminSignup(IFormFile a_image,string a_mail,Admin add)
        {
            string fileExtension = Path.GetExtension(a_image.FileName);

            var mail = a_context.tbl_admin.FirstOrDefault(row => row.a_mail == a_mail);

            if (mail != null)
            {
                ViewBag.mail = "*Gmail Already Taken";
            }
            else if (fileExtension == ".jpg" || fileExtension == ".png")
            {
                string imgmail = add.a_mail;

                string filename = Path.GetFileName(a_image.FileName);

                string dirpath = Path.Combine(env.WebRootPath, "Admin-image", imgmail);
                Directory.CreateDirectory(dirpath);

                string filepath = Path.Combine(dirpath, filename);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                a_image.CopyTo(fs);

                add.a_image = filename;

                a_context.tbl_admin.Add(add);
                a_context.SaveChanges();
                TempData["Adminsign"] = "Admin Sign Up Seccessfully!";
                return RedirectToAction("AdminFetch");
            }
            else
            {
                ViewBag.image = "*Only jpg Or png Image";
            }
            return View();
        }
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(string txtmail,string txtpass)
        {
            var log = a_context.tbl_admin.FirstOrDefault(row => row.a_mail == txtmail && row.a_pass == txtpass);

            if (log != null && log.a_status == 1)
            {
                HttpContext.Session.SetString("myadmin", log.a_id.ToString());
                TempData["login"] = "Login Successfully!";
                return RedirectToAction("Index");
            }
            else if (log != null && log.a_status == 0) 
            {
                TempData["Deactive"] = "Your Account Has Been Deactive!";
                return View();
            }
            else
            {
                TempData["wrong"] = "Gmail Or Password Is Incorrect!";
                return View();
            }
            
        }
        public IActionResult AdminFetch()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_admin.ToList();
                return View(fetching);
            }
            else 
            { 
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult AdminFetch(string Search)
        {
            List<Admin> Fetching = new List<Admin>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_admin.ToList();
            }
            else
            {
                Fetching = a_context.tbl_admin.FromSqlInterpolated($"SELECT * FROM tbl_admin WHERE a_id LIKE {Search} or a_name LIKE {Search} or a_mail LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult AdminDelete()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_admin.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult AdminDelete(string Search)
        {
            List<Admin> Fetching = new List<Admin>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_admin.ToList();
            }
            else
            {
                Fetching = a_context.tbl_admin.FromSqlInterpolated($"SELECT * FROM tbl_admin WHERE a_id LIKE {Search} or a_name LIKE {Search} or a_mail LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult AdminDeleteQuery(int id)
        {
            var d_id = a_context.tbl_admin.Find(id);
            a_context.tbl_admin.Remove(d_id);
            a_context.SaveChanges();
            return RedirectToAction("AdminDelete");
        }
        public IActionResult AdminActiveDeactive()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_admin.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult AdminActiveDeactive(string Search)
        {
            List<Admin> Fetching = new List<Admin>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_admin.ToList();
            }
            else
            {
                Fetching = a_context.tbl_admin.FromSqlInterpolated($"SELECT * FROM tbl_admin WHERE a_id LIKE {Search} or a_name LIKE {Search} or a_mail LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult DeactiveAdmin(int id)
        {
            var d_id = a_context.tbl_admin.FirstOrDefault(row => row.a_id == id);

            if (d_id != null)
            {
                d_id.a_status = 0;
                a_context.SaveChanges();
                return RedirectToAction("AdminActiveDeactive");
            }
            else 
            {
                return RedirectToAction("AdminActiveDeactive");
            }
        }
        public IActionResult ActiveAdmin(int id)
        {
            var d_id = a_context.tbl_admin.FirstOrDefault(row => row.a_id == id);

            if (d_id != null)
            {
                d_id.a_status = 1;
                a_context.SaveChanges();
                return RedirectToAction("AdminActiveDeactive");
            }
            else
            {
                return RedirectToAction("AdminActiveDeactive");
            }
        }
        public IActionResult AdminProfile()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var ses = HttpContext.Session.GetString("myadmin");

                var fetch = a_context.tbl_admin.Where(row => row.a_id == int.Parse(ses)).ToList();
                return View(fetch);
            }
            else 
            {
                return RedirectToAction("AdminLogin");
            }
            
        }
        public IActionResult AdminUpdate(int id)
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetch = a_context.tbl_admin.Find(id);
                return View(fetch);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpPost]
        public IActionResult AdminUpdate(Admin mod)
        {
            a_context.tbl_admin.Update(mod);
            a_context.SaveChanges();
            return RedirectToAction("AdminProfile");
        }
        public IActionResult AdminLogout()
        {
            HttpContext.Session.Remove("myadmin");
            return RedirectToAction("AdminLogin");
        }
        public IActionResult ProductInsert()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpPost]
        public IActionResult ProductInsert(IFormFile p_image,Product pro)
        {
            string fileExtension = Path.GetExtension(p_image.FileName);

            if (fileExtension == ".jpg" || fileExtension == ".png")
            {
                string filename = Path.GetFileName(p_image.FileName);

                string dirpath = Path.Combine(env.WebRootPath, "Product-image", filename);
                Directory.CreateDirectory(dirpath);

                string filepath = Path.Combine(dirpath, filename);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                p_image.CopyTo(fs);

                pro.p_image = filename;

                a_context.tbl_product.Add(pro);
                a_context.SaveChanges();
                TempData["add"] = "Product Added Successfully!";
                return RedirectToAction("ProductFetch");
            }
            else
            {
                ViewBag.image = "*Only jpg Or png Image";
                return View();
            }
        }
        public IActionResult ProductFetch()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_product.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult ProductFetch(string Search)
        {
            List<Product> Fetching = new List<Product>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_product.ToList();
            }
            else
            {
                Fetching = a_context.tbl_product.FromSqlInterpolated($"SELECT * FROM tbl_product WHERE p_id LIKE {Search} or p_company LIKE {Search} or p_name LIKE {Search} or p_price LIKE {Search} or p_model LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult ProductDelete()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_product.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult ProductDelete(string Search)
        {
            List<Product> Fetching = new List<Product>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_product.ToList();
            }
            else
            {
                Fetching = a_context.tbl_product.FromSqlInterpolated($"SELECT * FROM tbl_product WHERE p_id LIKE {Search} or p_company LIKE {Search} or p_name LIKE {Search} or p_price LIKE {Search} or p_model LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult ProductDeleteQuery(int id)
        {
            var d_id = a_context.tbl_product.Find(id);
            a_context.tbl_product.Remove(d_id);
            a_context.SaveChanges();
            return RedirectToAction("ProductDelete");
        }
        public IActionResult ProductUpdate()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_product.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult ProductUpdate(string Search)
        {
            List<Product> Fetching = new List<Product>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_product.ToList();
            }
            else
            {
                Fetching = a_context.tbl_product.FromSqlInterpolated($"SELECT * FROM tbl_product WHERE p_id LIKE {Search} or p_company LIKE {Search} or p_name LIKE {Search} or p_price LIKE {Search} or p_model LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult ProductUpdateQuery(int id)
        {
            var fetching = a_context.tbl_product.Find(id);
            return View(fetching);
        }
        [HttpPost]
        public IActionResult ProductUpdateQuery(Product pro)
        {
            a_context.tbl_product.Update(pro);
            a_context.SaveChanges();
            TempData["update"] = "Record Updated Successfully!";
            return RedirectToAction("ProductUpdate");
        }
        public IActionResult ProductActiveDeactive()
        {
            var check = HttpContext.Session.GetString("myadmin");

            if (check != null)
            {
                var fetching = a_context.tbl_product.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult ProductActiveDeactive(string Search)
        {
            List<Product> Fetching = new List<Product>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_product.ToList();
            }
            else
            {
                Fetching = a_context.tbl_product.FromSqlInterpolated($"SELECT * FROM tbl_product WHERE p_id LIKE {Search} or p_company LIKE {Search} or p_name LIKE {Search} or p_price LIKE {Search} or p_model LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult ProductActive(int id)
        {
            var active = a_context.tbl_product.FirstOrDefault(row => row.p_id == id);
            if (active != null)
            {
                active.p_status = 1;
                a_context.SaveChanges();
                return RedirectToAction("ProductActiveDeactive");
            }
            else
            {
                return RedirectToAction("ProductActiveDeactive");
            }
        }
        public IActionResult ProductDeactive(int id)
        {
            var active = a_context.tbl_product.FirstOrDefault(row => row.p_id == id);
            if (active != null)
            {
                active.p_status = 0;
                a_context.SaveChanges();
                return RedirectToAction("ProductActiveDeactive");
            }
            else
            {
                return RedirectToAction("ProductActiveDeactive");
            }
        }
        public IActionResult Appointment()
        {
            var check = HttpContext.Session.GetString("myadmin");
            if (check != null)
            {
                var fetching = a_context.tbl_buy.ToList();
                return View(fetching);
            }
            else 
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult Appoinment(string Search)
        {
            List<Buy> Fetching = new List<Buy>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_buy.ToList();
            }
            else
            {
                Fetching = a_context.tbl_buy.FromSqlInterpolated($"SELECT * FROM tbl_buy WHERE b_id LIKE {Search} or b_company LIKE {Search} or b_name LIKE {Search} or b_price LIKE {Search} or b_model LIKE {Search} or b_date LIKE {Search} or User_ID LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult DeleteAppointment()
        {
            var check = HttpContext.Session.GetString("myadmin");
            if (check != null)
            {
                var fetching = a_context.tbl_buy.ToList();
                return View(fetching);
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public IActionResult DeleteAppoinment(string Search)
        {
            List<Buy> Fetching = new List<Buy>();

            if (string.IsNullOrEmpty(Search))
            {
                Fetching = a_context.tbl_buy.ToList();
            }
            else
            {
                Fetching = a_context.tbl_buy.FromSqlInterpolated($"SELECT * FROM tbl_buy WHERE b_id LIKE {Search} or b_company LIKE {Search} or b_name LIKE {Search} or b_price LIKE {Search} or b_model LIKE {Search} or b_date LIKE {Search} or User_ID LIKE {Search}").ToList();
            }
            if (Fetching.Count == 0)
            {
                ViewBag.notfound = "User Not Found!";
            }

            return View(Fetching);
        }
        public IActionResult DeleteAppointmentQuery(int id)
        {
            var d_id = a_context.tbl_buy.Find(id);
            a_context.tbl_buy.Remove(d_id);
            a_context.SaveChanges();
            return RedirectToAction("DeleteAppointment");
        }
    }
}
