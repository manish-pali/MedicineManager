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
                var ip = HttpContext.Request.UserHostAddress.ToString();
                Session["ip"] = ip;
                var data = db.Tbl_Master_User.Where(x => x.UserName == model.username).FirstOrDefault();
                if (data != null)
                {
                    if (data.Password == model.password)
                    {
                        if (Session["captcha"].ToString().ToLower() == model.cap.ToLower())
                        {
                            if (model.username == "Admin" && model.password == "1234")
                            {
                                Session["Session"] = "2023-24";
                                Session["UserName"] = data.UserName;
                                Session["Uid"] = data.Id;
                                CustomClasses.UserLog(false, "AdminLogin", "Login|PageLoad", "Successful", Convert.ToString(model.username));
                                return RedirectToAction("Dashboard", "Admin");
                            }
                            else
                            {
                                if (data.Ip == "Global")
                                {
                                    Session["Session"] = "2023-24";
                                    Session["UserName"] = data.UserName;
                                    Session["Uid"] = data.Id;
                                    CustomClasses.UserLog(false, "AdminLogin", "Login|PageLoad", "Successful", Convert.ToString(model.username));
                                    return RedirectToAction("Dashboard", "Admin");
                                }
                                else
                                {
                                    ViewBag.msg = "Your Ip is Incorrect";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.msg = "Your Capture is Incorrect";
                        }
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