using System;
using HMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MCtick_new.CustomClass;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        HMS_DB2Entities db = new HMS_DB2Entities();
        public ActionResult UserLogin(decimal? id)
        {
            User_Login model = new User_Login();
            return View(model);
        }
        [HttpPost]
        public ActionResult UserLogin(User_Login model)
        
        
     {
            try
            {
               
                var data = db.Tbl_Master_User.Where(x => x.UserName == model.username).FirstOrDefault();
                if (data != null)
                {
                    if (data.Password == model.password)
                    {
                                Session["UserName"] = data.UserName;
                                return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        ViewBag.msg = "Your Password is Incorrect";
                    }
                }
                else
                {
                    ViewBag.msg = "Your UserName is Incorrect";
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();  
            }
            
        }
        public ActionResult Home()
        { 
            return View();
        }
    }
}