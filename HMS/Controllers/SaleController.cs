using HMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class SaleController : Controller
    {

        HMS_DB2Entities db = new HMS_DB2Entities();
        ObjectParameter msg = new ObjectParameter("msg", typeof(string));

        #region Sale
        public ActionResult Sale(decimal? id)
        {
            Sale model = new Sale();
            model.uid = Convert.ToInt32(Session["AdminID"]);
            model.Session = Convert.ToString(Session["Year"]);
            var maxid = db.Tbl_Sale.FirstOrDefault();
            if (maxid == null)
            {
                model.VN = 1;

            }
            else
            {
                model.VN = Convert.ToInt32(db.Tbl_Sale.Max(x => x.Id) + 1);
            }
            model.calcu = Convert.ToString(db.Tbl_MasterFirm.Max(x => x.StateCode));
            model.KeyID = Guid.NewGuid().ToString("N");
            model.qty = 0;
            model.MRP = 0;
            model.Rate = 0;
            model.Amount = 0;
            model.CGST = 0;
            model.SGST = 0;
            model.IGST = 0;
            model.Discper = 0;
            model.NetAmount = 0;
            model.Doctor = 1;
            model.Date = DateTime.Now.ToString("yyyy-MM-dd");
            model.Expiry = "00-0000";
            if (id != null && id > 0)
            {

                var data = db.Sp_get_Sale(id).Where(x => x.Dflag != 1).FirstOrDefault();
                if (data != null)
                {
                    model.id = data.Id;
                    model.VN = Convert.ToInt32(data.VN);
                    DateTime date = DateTime.ParseExact(data.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    model.Date = date.ToString("yyyy-MM-dd");
                    model.TotalAmount = data.TotalAmount;
                    model.Narration = data.Narration;
                    model.CustomerId = data.CustomerId;
                    model.Doctor = data.Doctor;
                    model.Address = data.Address;
                    model.gstn = data.GSTIN;
                    model.Statecode = data.StateCode;
                    model.Contact = data.Contact;
                    db.Sp_Update_SaleDetails(model.id, model.KeyID);
                }
            }
            return View(model);
        }

        [HttpPost]

        public ActionResult Sale(Sale model)
        {
            ObjectParameter Id = new ObjectParameter("id", typeof(decimal));
            ObjectParameter Id1 = new ObjectParameter("id", typeof(decimal));

            decimal? id = 0;

            if (model.id > 0)
            {
                id = model.id;
                db.Sp_Update_Sale(model.id, model.VN, Convert.ToDateTime(model.Date).ToString("dd-MM-yyyy"), model.CustomerId, model.Doctor, model.TotalAmount, model.Narration, model.uid, msg);
              
                var TempData = db.Tbl_Temp_Sale_Details.Where(x => x.KeyId == model.KeyID).ToList();
                foreach (var item in TempData)
                {
                    if (item.Edit > 0)
                    {
                        if (item.DeleteId == 0)
                        {
                            db.Sp_Update_EditSaleDetails(item.Edit, item.ItemId, item.Unit, item.Pack, item.Batch, item.Expiry, item.Qty, item.TotalQty, item.MRP, item.Disc, item.Rate, item.Amount, item.CGST, item.SGST, item.IGST, item.NetAmount, msg);
                        }
                        else
                        {
                            db.Sp_Delete_SaleDetails(item.Edit, msg);
                        }
                    }
                    else
                    {
                        if (item.DeleteId == 0)
                        {
                            db.Sp_Update_AllSaleDetails(model.id, item.Id, msg);
                        }
                    }
                }
            }
            else
            {

                db.Sp_Insert_Sale(Convert.ToDateTime(model.Date).ToString("dd-MM-yyyy"), model.CustomerId, model.Doctor, model.TotalAmount, model.Narration, model.uid, model.Session, msg, Id);
                var tid = Convert.ToDecimal(Id.Value);

                db.Sp_Insert_SaleDetails(tid, Convert.ToDateTime(model.Date).ToString("dd-MM-yyyy"), model.KeyID, model.uid, model.Session, msg);
                id = tid;
            }
            if (model.IsGenerateSlip)
            {

                return RedirectToAction("SaleSlip", "Admin", new { id = id });
            }
            TempData["Message"] = "Successfull!!";
            return RedirectToAction("Sale", new { id = 0 });
        }

        public JsonResult GetCode(decimal? id)
        {

            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Sp_get_Master_Item().Where(x => x.Id == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBatchItem(decimal? id)
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            return Json(db.Tbl_Sale_Detail.Where(x => x.ItemId == id).GroupBy(x => x.Batch).Select(x => x.FirstOrDefault()).Select(x => new SelectListItem() { Text = x.Batch, Value = x.Batch }).ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetBatch(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Tbl_Sale_Detail.Where(x => x.Batch == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomer(decimal? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Sp_Get_Master_Customer().Where(x => x.Id == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEditItem(decimal? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Sp_get_Temp_Sale_Detail().Where(x => x.Id == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleDetailsList(string KeyID)
        {
            return PartialView(db.sp_get_Temp_SaleDetail(KeyID).ToList());
        }

        public JsonResult DeleteSaleDetails(decimal? id)
        {
            if (id > 0)
            {
                db.Sp_Delete_TempSaleDetails(id, msg);

            }
            var jsonResult = Json(id, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]

        public JsonResult SaveItem(Sale model)
        {
            decimal id = 0;

            if (model.qty != 0 && model.MRP != 0)
            {
                if (model.IGST1 > 0)
                {
                    db.Sp_Update_TempSaleDetails(model.IGST1, model.KeyID, model.ItemId1, model.Unit, model.Pack, model.Batch, model.Expiry, model.qty, model.Totalqty, model.MRP, model.Discper, model.Rate, model.Amount, model.CGST, model.SGST, model.IGST, model.NetAmount, msg);
                }
                else
                {
                    db.Sp_Insert_TempSaleDetails(model.KeyID, model.ItemId1, model.Unit, model.Pack, model.Batch, model.Expiry, model.qty, model.Totalqty, model.MRP, model.Discper, model.Rate, model.Amount, model.CGST, model.SGST, model.IGST, model.NetAmount, msg);

                }
                TempData["Message"] = "Successfully!!";
                var jsonResult = Json(id, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                TempData["Message"] = "Qty is not zero.";
                var jsonResult = Json(id, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }


        }


        public ActionResult SearchSale(string Date)
        {
            Sale model = new Sale();
            if (Date != null)
            {
                model.Date = Date;
            }
            else
            {
                model.Category = "ALL";
            }
            return View(model);
        }

        public ActionResult SearchSaleList(string Date)
        {
            if (Date != null)
            {
                return PartialView(db.Sp_get_Sale1().Where(x => x.Dflag != 1 && x.VN == Convert.ToInt32(Date)).OrderByDescending(x => x.Id).ToList());
            }
            else
            {
                return PartialView(db.Sp_get_Sale1().Where(x => x.Dflag != 1).OrderByDescending(x => x.Id).ToList());
            }

        }

        #endregion
    }
}