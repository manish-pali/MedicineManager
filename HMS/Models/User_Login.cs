using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class User_Login
    {
        public decimal id { get; set; }
        [Required(ErrorMessage = "Please Enter UserName")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string password { get; set; }
        public decimal? uid { get; set; }
        public string Session { get; set; }
        public string Date { get; set; }
        public string ip { get; set; }
        public string cap { get; set; }

    }
}