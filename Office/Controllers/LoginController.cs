using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calibration.Models;
using System.Data.SqlClient;
using office.Models;

namespace Calibration.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login L,string ReturnUrl)
        {
             OfficeDbContext _db = new OfficeDbContext();
           // var result = _db.LoginDetail.SqlQuery(@"exec usp_login 
           //     @User_Name,@Password",
           //     new SqlParameter("@User_Name", L.UserName == null ? (object)DBNull.Value : L.UserName),
           //     new SqlParameter("@Password", L.Password == null ? (object)DBNull.Value : L.Password)).ToList<Login>();
           //// LoginDetail data = new LoginDetail();
           // L = result.FirstOrDefault();
           // if (L == null)
           // {
           //     ViewBag.error = "Please enter valid user Name password";
           // }
           // else
           // {
           //     Session["UserName"] = L.UserName;
           //     Session["User_id"] =  L.User_ID;
           //     Session["RoleID"] =   L.Role;

           //     return RedirectToAction("Index", "Master");                
           // }

            return View();
        }
    }

}