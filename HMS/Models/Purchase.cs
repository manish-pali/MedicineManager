using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class Purchase
    {
        public decimal ItemId { get; set; }
        public decimal id { get; set; }
        public int VN { get; set; }
        public string Date { get; set; }
        [Required(ErrorMessage = "Please Enter Invoice No")]
        public int InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        [Required(ErrorMessage = "Please Select Supplier")]
        public decimal? SupplierId { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string gstn { get; set; }
        public string Statecode { get; set; }
        public decimal? SGST1 { get; set; }
        public decimal? Tax { get; set; }
        public decimal? ItemId1 { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public string Pack { get; set; }
        public string Batch { get; set; }
        public string CustomerName { get; set; }
        public string Expiry { get; set; }
        public decimal? qty { get; set; }
        public decimal? Totalqty { get; set; }
        public decimal? MRP { get; set; }
        public decimal? Discper { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal? IGST1 { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        [Required(ErrorMessage = "Please Enter Remark")]
        public string Narration { get; set; }
        public string KeyID { get; set; }
        public decimal? uid { get; set; }
        public string Session { get; set; }
        public string calcu { get; set; }
        public bool IsGenerateSlip { get; set; }
    }
}