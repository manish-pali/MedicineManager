using HMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class MasterController : Controller
    {
        HMS_DB2Entities db = new HMS_DB2Entities();
        ObjectParameter msg = new ObjectParameter("msg", typeof(string));

        #region MasterCategory
        public ActionResult Category(decimal? id)
        {

            MasterCategory model = new MasterCategory();
            if (id != null && id > 0)
            {
                var data = db.Tbl_Master_Category.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Category = data.Category;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Category(MasterCategory model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_Category(model.Id, model.Category, uid);
                    ViewBag.msg = "Record update Sucessfully";
                }
                else
                {
                    db.Sp_Insert_Master_Category(model.Category, uid, Session1, Ip);
                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("Category", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult CategoryList()
        {
            return PartialView(db.Tbl_Master_Category.Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult DeleteCategory(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_Category(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("Category", new { id = 0 });
        }
        #endregion

        #region MasterItemType
        public ActionResult ItemType(decimal? id)
        {

            MasterItemType model = new MasterItemType();
            if (id != null && id > 0)
            {
                var data = db.Tbl_Master_ItemType.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.ItemType = data.ItemType;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ItemType(MasterItemType model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_ItemType(model.Id, model.ItemType, uid);
                    ViewBag.msg = "Record update Sucessfully";
                }
                else
                {
                    db.Sp_Insert_Master_Itemtype(model.ItemType, uid, Session1, Ip);
                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("ItemType", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult ItemTypeList()
        {
            return PartialView(db.Tbl_Master_ItemType.Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult DeleteItemType(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_ItemType(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("ItemType", new { id = 0 });
        }
        #endregion

        #region MasterUnit
        public ActionResult Unit(decimal? id)
        {

            MasterUnit model = new MasterUnit();
            if (id != null && id > 0)
            {
                var data = db.Tbl_Master_Unit.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.Unit = data.Unit;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Unit(MasterUnit model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_Unit(model.Id, model.Unit, uid);
                    ViewBag.msg = "Record update Sucessfully";
                }
                else
                {
                    db.Sp_Insert_Master_Unit(model.Unit, uid, Session1, Ip);
                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("Unit", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult UnitList()
        {
            return PartialView(db.Tbl_Master_Unit.Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult DeleteUnit(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_Unit(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("Unit", new { id = 0 });
        }
        #endregion

        #region MasterDoctor
        public ActionResult Doctor(decimal? id)
        {

            MasterDoctor model = new MasterDoctor();
            if (id != null && id > 0)
            {
                var data = db.Tbl_Master_Doctor.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.DoctorName = data.DoctorName;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Doctor(MasterDoctor model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_Doctor(model.Id, model.DoctorName, uid);
                    ViewBag.msg = "Record update Sucessfully";
                }
                else
                {
                    db.Sp_Insert_Master_Doctor(model.DoctorName, uid, Session1, Ip);
                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("Doctor", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult DoctorList()
        {
            return PartialView(db.Tbl_Master_Doctor.Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult DeleteDoctor(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_Doctor(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("Doctor", new { id = 0 });
        }
        #endregion

        #region MasterCompany
        public ActionResult Company(decimal? id)
        {

            MasterCompany model = new MasterCompany();
            if (id != null && id > 0)
            {
                var data = db.Tbl_Master_Company.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.CompanyName = data.CompanyName;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Company(MasterCompany model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_Company(model.Id, model.CompanyName, uid);
                    ViewBag.msg = "Record update Sucessfully";
                }
                else
                {
                    db.Sp_Insert_Master_Company(model.CompanyName, uid, Session1, Ip);
                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("Company", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult CompanyList()
        {
            return PartialView(db.Tbl_Master_Company.Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult DeleteCompany(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_Company(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("Company", new { id = 0 });
        }
        #endregion

        #region MasterState
        public ActionResult State(decimal? id)
        {

            MasterState model = new MasterState();
            if (id != null && id > 0)
            {
                var data = db.Tbl_Master_State.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.State = data.State;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult State(MasterState model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_State(model.Id, model.State, model.StateCode, uid);

                }
                else
                {
                    db.Sp_Insert_Master_State(model.State, model.StateCode, uid, Session1, Ip);
                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("State", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult StateList()
        {
            return PartialView(db.Tbl_Master_State.Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult DeleteState(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_State(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("State", new { id = 0 });
        }
        #endregion

        #region MasterCustomer
        public ActionResult Customer(decimal? id)
        {

            MasterCustomer model = new MasterCustomer();
            if (id != null && id > 0)
            {
                var data = db.Sp_Get_Master_Customer().Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.CustomerName = data.CustomerName;
                    model.Address = data.Address;
                    model.Contact = data.Contact;
                    model.EMail = data.EMail;
                    model.GSTIN =  data.GSTIN;  
                    model.StateId = data.StateId;
                    model.StateCode = data.StateCode;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Customer(MasterCustomer model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_Customer(model.Id, model.CustomerName, model.Address, model.Contact, model.EMail, model.GSTIN, model.StateId, model.UpdateBy); 
                    ViewBag.msg = "Record update Sucessfully";
                }
                else
                {
                    db.Sp_Insert_Master_Customer(model.CustomerName, model.Address, model.Contact, model.GSTIN, model.EMail, model.StateId, uid, Session1, Ip);

                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("Customer", new { id = 0 });
            }
            return View(model);
        }


        public JsonResult GetCode1(decimal? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Tbl_Master_State.Where(x => x.Id == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CustomerList()
        {
            return PartialView(db.Sp_Get_Master_Customer().Where(x => x.DFlag == 0).ToList());
        } 

        public ActionResult DeleteCustomer(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_Customer(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("Customer", new { id = 0 });
        }
        #endregion

        #region MasterSupplier
        public ActionResult Supplier(decimal? id)
        {

            MasterSupplier model = new MasterSupplier();
            if (id != null && id > 0)
            {
                var data = db.Tbl_Master_Supplier.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.SupplierName = data.SupplierName;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Supplier(MasterSupplier model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_Supplier(model.Id, model.SupplierName, model.Address, model.Contact, model.EMail, model.GSTIN, model.StateId, model.UpdateBy); ;
                    ViewBag.msg = "Record update Sucessfully";
                }
                else
                {
                    db.Sp_Insert_Master_Supplier(model.SupplierName, model.Address, model.Contact, model.GSTIN, model.EMail, model.StateId, uid, Session1, Ip);

                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("Supplier", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult SupplierList()
        {
            return PartialView(db.Sp_Get_Master_Supplier().Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult DeleteSupplier(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_Supplier(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("Supplier", new { id = 0 });
        }
        #endregion

        #region MasterUser
        public ActionResult User(decimal? id)
        {

            MasterUser model = new MasterUser();
            if (id != null && id > 0)
            {
                var data = db.Tbl_Master_User.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.UserName = data.UserName;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult User(MasterUser model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_User(model.Id, model.UserName, model.Password, model.Status, model.UpdateBy);
                    ViewBag.msg = "Record update Sucessfully";
                }
                else
                {
                    db.Sp_Insert_Master_User(model.UserName, model.Password, model.Status ,uid, Session1, Ip);
                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("User", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult UserList()
        {
            return PartialView(db.Tbl_Master_User.Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult DeleteUser(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_User(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("User", new { id = 0 });
        }
        #endregion

        #region MasterItem
        public ActionResult Item(decimal? id)
        {

            MasterItem model = new MasterItem();
            if (id != null && id > 0)
            {
                var data = db.Sp_get_Master_Item().Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.Id = data.Id;
                    model.CompanyId= data.CompanyId;
                    model.CategoryId= data.CategoryId;
                    model.ItemName = data.ItemName;
                    model.UnitId = data.UnitId;
                    model.Pack = data.Pack;
                    model.MRP = data.MRP;
                    model.Disc = data.Disc;
                    model.Rate = data.Rate;
                    model.Reorder = data.Reorder;
                    model.Tax = data.Tax;
                    model.OpeningQty = data.OpeningQty;
                    model.Batch = data.Batch;
                    model.Expiry = data.Expiry;
                    model.PCSRate = data.PCSRate;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Item(MasterItem model)
        {
            var uid = Convert.ToDecimal(Session["Uid"]);
            var Session1 = Convert.ToString(Session["Session"]);
            var Ip = Convert.ToString(Session["ip"]);
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    db.Sp_Update_Master_Item(model.Id, model.CompanyId, model.CategoryId,model.ItemName,model.UnitId, model.Pack, model.MRP,model.Disc, model.Rate,model.Reorder,model.Tax,model.OpeningQty,model.Batch,model.Expiry,model.PCSRate, model.UpdateBy);
                    ViewBag.msg = "Record update Sucessfully";

                }
                else
                {
                    db.Sp_Insert_Master_Item(model.CompanyId, model.CategoryId, model.ItemName, model.UnitId, model.Pack, model.MRP, model.Disc, model.Rate, model.Reorder, model.Tax, model.OpeningQty, model.Batch, model.Expiry, model.PCSRate, uid, Session1, Ip);

                    ViewBag.msg = "Record Save Sucessfully";
                }
                return RedirectToAction("Item", new { id = 0 });
            }
            return View(model);
        }

        public ActionResult ItemList()
        {
            return PartialView(db.Sp_get_Master_Item().Where(x => x.DFlag == 0).ToList());
        }

        public ActionResult ItemDelete(decimal? id)
        {
            var uid = Convert.ToDecimal(Session["uid"]);
            if (id > 0)
            {
                db.Sp_Delete_Master_Item(id, uid, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("Item", new { id = 0 });
        }
        #endregion
    }
}