using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class MasterSupplier
    {
        public decimal Id { get; set; }
        [Required(ErrorMessage = "Please Enter Supplier Name")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Contact")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "Please Enter GSTIN")]
        public string GSTIN { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string EMail { get; set; }
        [Required(ErrorMessage = "Please Select State")]
        public decimal? StateId { get; set; }
        public string StateCode { get; set; }

        public decimal? Uid { get; set; }
        public string Session { get; set; }
        public string Date { get; set; }
        public string CDate { get; set; }
        public string Time { get; set; }
        public string Ip { get; set; }
        public decimal? DFlag { get; set; }
        public decimal? UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateTime { get; set; }
    }
}