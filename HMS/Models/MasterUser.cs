using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class MasterUser
    {
        public decimal Id { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public string Status { get; set; }
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