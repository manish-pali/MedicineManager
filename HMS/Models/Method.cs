using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Models
{
    public class Method
    {
        public static List<SelectListItem> GetMasterState()
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            model.AddRange(db.Tbl_Master_State.Where(x=>x.DFlag==0).ToList().Select(x => new SelectListItem() { Text = x.State, Value = x.Id.ToString() }));
            return model;
        }
        public static List<SelectListItem> GetMasterCompany()
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            model.AddRange(db.Tbl_Master_Company.Where(x => x.DFlag == 0).ToList().Select(x => new SelectListItem() { Text = x.CompanyName, Value = x.Id.ToString() }));
            return model;
        }
        public static List<SelectListItem> GetMasterCategory()
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            model.AddRange(db.Tbl_Master_Category.Where(x => x.DFlag == 0).ToList().Select(x => new SelectListItem() { Text = x.Category, Value = x.Id.ToString() }));
            return model;
        }
        public static List<SelectListItem> GetMasterUnit()
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            model.AddRange(db.Tbl_Master_Unit.Where(x => x.DFlag == 0).ToList().Select(x => new SelectListItem() { Text = x.Unit, Value = x.Id.ToString() }));
            return model;
        }
        public static List<SelectListItem> GetMasterSupplier()
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            model.AddRange(db.Tbl_Master_Supplier.ToList().Select(x => new SelectListItem() { Text = x.SupplierName, Value = x.Id.ToString() }));
            return model;
        }
        public static List<SelectListItem> GetMasterItem()
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            model.AddRange(db.Tbl_Master_Item.ToList().Select(x => new SelectListItem() { Text = x.ItemName, Value = x.Id.ToString() }));
            return model;
        }
        public static List<SelectListItem> GetMasterCustomer()
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            model.AddRange(db.Tbl_Master_Customer.ToList().Select(x => new SelectListItem() { Text = x.CustomerName, Value = x.Id.ToString() }));
            return model;
        }
        public static List<SelectListItem> GetDoctor()
        {
            HMS_DB2Entities db = new HMS_DB2Entities();
            List<SelectListItem> model = new List<SelectListItem>();
            model.AddRange(db.Tbl_Master_Doctor.ToList().Select(x => new SelectListItem() { Text = x.DoctorName, Value = x.Id.ToString() }));
            return model;
        }
    }
}