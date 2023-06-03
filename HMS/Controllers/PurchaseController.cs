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
    public class PurchaseController : Controller
    {
        HMS_DB2Entities db = new HMS_DB2Entities();
        ObjectParameter msg = new ObjectParameter("msg", typeof(string));

        // GET: Purchase
        #region Purchase
        public ActionResult Purchase(decimal? id)
        {
            Purchase model = new Purchase();
            model.uid = Convert.ToInt32(Session["AdminID"]);
            model.Session = Convert.ToString(Session["Year"]);
            var maxid = db.Tbl_Purchase.FirstOrDefault();
            if (maxid == null)
            {
                model.VN = 1;
                model.InvoiceNo = 1;
            }
            else
            {
                model.VN = Convert.ToInt32(db.Tbl_Purchase.Max(x => x.Id) + 1);
                model.InvoiceNo = Convert.ToInt32(db.Tbl_Purchase.Max(x => x.Id) + 1);
            }
            model.calcu = Convert.ToString(db.Tbl_MasterFirm.Max(x => x.StateCode));
            model.KeyID = Guid.NewGuid().ToString("N");
            model.qty = 0;
            model.MRP = 0;
            model.Rate = 0;
            model.Amount = 0;
            model.CGST = 6;
            model.SGST = 6;
            model.IGST = 6;
            model.Discper = 0;
            model.NetAmount = 0;
            model.InvoiceDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.Date = DateTime.Now.ToString("yyyy-MM-dd");
            model.Expiry = "00-0000";
            if (id != null && id > 0)
            {

                var data = db.Sp_get_Purchase().Where(x => x.Id == id && x.DFlag != 1).FirstOrDefault();
                if (data != null)
                {
                    model.id = data.Id;
                    model.VN = Convert.ToInt32(data.VN);
                    DateTime date = DateTime.ParseExact(data.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    model.Date = date.ToString("yyyy-MM-dd");
                    model.InvoiceNo = Convert.ToInt32(data.InvoiceNo);
                    DateTime invoicedate = DateTime.ParseExact(data.InvoiceDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    model.InvoiceDate = invoicedate.ToString("yyyy-MM-dd");
                    model.TotalAmount = data.TotalAmount;
                    model.Narration = data.Narration;
                    model.SupplierId = data.Supplierid;
                    model.Address = data.Address;
                    model.gstn = data.GSTIN;
                    model.Statecode = data.StateCode;
                    model.Contact = data.Contact;
                    db.Sp_Update_PurchaseDetails(model.id, model.KeyID, msg);
                }
            }
            return View(model);
        }

        [HttpPost]

        public ActionResult Purchase(Purchase model)
        {
            ObjectParameter Id = new ObjectParameter("id", typeof(decimal));
            ObjectParameter Id1 = new ObjectParameter("id", typeof(decimal));

            decimal? id = 0;

            if (model.id > 0)
            {
                id = model.id;
                db.Sp_Update_Purchase(model.id, model.VN, Convert.ToDateTime(model.Date).ToString("dd-MM-yyyy"), model.InvoiceNo, Convert.ToDateTime(model.InvoiceDate).ToString("dd-MM-yyyy"), model.SupplierId, model.TotalAmount, model.Narration, model.uid, msg);

                
                var TempData = db.Tbl_Temp_Purchase_Details.Where(x => x.KeyId == model.KeyID).ToList();
                foreach (var item in TempData)
                {
                    if (item.Edit > 0)
                    {
                        if (item.DeleteId == 0)
                        {
                            db.Sp_Update_EditPurchaseDetails(item.Edit, item.ItemId, item.Unit, item.Pack, item.Batch, item.Expiry, item.Qty, item.TotalQty, item.MRP, item.Disc, item.Rate, item.Amount, item.CGST, item.SGST, item.IGST, item.NetAmount, msg);
                        }
                        else
                        {
                            db.Sp_Delete_PurchaseDetails(item.Edit, msg);
                        }
                    }
                    else
                    {
                        if (item.DeleteId == 0)
                        {
                            db.Sp_Update_AllPurchaseDetails(model.id, item.Id, msg);
                        }
                    }
                }
            }
            else
            {

                db.Sp_Insert_Purchase(Convert.ToDateTime(model.Date).ToString("dd-MM-yyyy"), model.InvoiceNo, Convert.ToDateTime(model.InvoiceDate).ToString("dd-MM-yyyy"), model.SupplierId, model.TotalAmount, model.Narration, model.uid, model.Session, msg, Id);
                var tid = Convert.ToDecimal(Id.Value);

                db.Sp_Insert_PurchaseDetails(tid, Convert.ToDateTime(model.Date).ToString("dd-MM-yyyy"), model.KeyID, model.uid, model.Session, msg);
                id = tid;
            }

            TempData["Message"] = "Successfull!!";
            return RedirectToAction("Purchase", new { id = 0 });
        }

        public ActionResult DeletePurchase(decimal? id)
        {
            if (id > 0)
            {
                db.Sp_Delete_Purchase(id, msg);
                TempData["msg"] = msg.Value;

            }
            return RedirectToAction("SearchPurchase", new { id = 0 });
        }

        public JsonResult GetCode(decimal? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Sp_get_Master_Item().Where(x => x.Id == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSupplier(decimal? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Sp_Get_Master_Supplier().Where(x => x.Id == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEditItem(decimal? id)
        {
            Purchase model = new Purchase();
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Sp_get_Temp_Purchase_Detail().Where(x => x.Id == id).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseDetailsList(string KeyID)
        {
            return PartialView(db.sp_get_Temp_PurchaseDetail(KeyID).ToList());
        }

        public JsonResult DeleteTransactionDetails(decimal? id)
        {
            if (id > 0)
            {
                db.Sp_Delete_TempPurchaseDetails(id, msg);

            }
            var jsonResult = Json(id, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult SaveItem(Purchase model)
        {
            decimal id = 0;
            if (model.qty != 0 && model.MRP != 0)
            {
                if (model.IGST1 > 0)
                {
                    db.Sp_Update_TempPurchaseDetails(model.IGST1, model.KeyID, model.ItemId1, model.Unit, model.Pack, model.Batch, model.Expiry, model.qty, model.Totalqty, model.MRP, model.Discper, model.Rate, model.Amount, model.CGST, model.SGST, model.IGST, model.NetAmount, msg);
                }
                else
                {
                    db.Sp_Insert_TempPurchaseDetails(model.KeyID, model.ItemId1, model.Unit, model.Pack, model.Batch, model.Expiry, model.qty, model.Totalqty, model.MRP, model.Discper, model.Rate, model.Amount, model.CGST, model.SGST, model.IGST, model.NetAmount, msg);

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

        public ActionResult SearchPurchase(decimal? id)
        {
            Purchase model = new Purchase();
            var maxid = db.Tbl_Purchase.FirstOrDefault();
            if (maxid == null)
            {
                model.VN = 1;
                model.InvoiceNo = 1;
            }
            else
            {
                model.VN = Convert.ToInt32(db.Tbl_Purchase.Max(x => x.Id) + 1);
                model.InvoiceNo = Convert.ToInt32(db.Tbl_Purchase.Max(x => x.Id) + 1);
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

            model.InvoiceDate = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");
            model.Date = DateTime.Now.ToString("yyyy-MM-dd");
            model.Expiry = "00-0000";
            if (id != null && id > 0)
            {

                var data = db.Tbl_Purchase.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    model.id = data.Id;
                    model.VN = Convert.ToInt32(data.VN);
                    model.Date = data.Date;
                    model.InvoiceNo = Convert.ToInt32(data.InvoiceNo);
                    model.InvoiceDate = data.InvoiceDate;
                    model.TotalAmount = data.TotalAmount;
                    model.Narration = data.Narration;
                    model.SupplierId = data.Supplierid;
                    model.uid = data.uid;

                }

            }
            return View(model);
        }

        public ActionResult SearchPurchaseList()
        {
            return PartialView(db.Sp_get_Purchase().Where(x => x.DFlag != 1).OrderByDescending(x => x.Id).ToList());
        }

        #endregion
    }
}