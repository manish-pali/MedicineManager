using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class MasterItem
    {
        public decimal Id { get; set; }
        [Required(ErrorMessage = "Please Select Company Name")]
        public decimal? CompanyId { get; set; }
        [Required(ErrorMessage = "Please Select Category Name")]
        public decimal? CategoryId { get; set; }
        [Required(ErrorMessage = "Please Enter Item Name")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "Please Select Unit Name")]
        public decimal? UnitId { get; set; }
        [Required(ErrorMessage = "Please Enter Pack")]
        public decimal? Pack { get; set; }
        [Required(ErrorMessage = "Please Enter MRP")]
        public decimal? MRP { get; set; }
        [Required(ErrorMessage = "Please Enter Discount")]
        public decimal? Disc { get; set; }
        [Required(ErrorMessage = "Please Enter Rate")]
        public decimal? Rate { get; set; }
        [Required(ErrorMessage = "Please Enter Reorder")]
        public decimal? Reorder { get; set; }
        [Required(ErrorMessage = "Please Enter Tax")]
        public decimal? Tax { get; set; }
        [Required(ErrorMessage = "Please Enter OpeningQty")]
        public decimal? OpeningQty { get; set; }
        [Required(ErrorMessage = "Please Enter Batch")]
        public string Batch { get; set; }
        [Required(ErrorMessage = "Please Enter Expiry Date")]
        public string Expiry { get; set; }
        [Required(ErrorMessage = "Please Enter PCSRate")]

        public decimal? PCSRate { get; set; }
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