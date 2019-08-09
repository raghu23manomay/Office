using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Calibration.Models
{
    public class Login
    {
        [Key]
        public int? User_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class mail
    {
        [Key]
        public int? User_ID { get; set; }
        public string emailFrom { get; set; }
        public string emailto { get; set; }
        public string Description { get; set; }
        public string subject { get; set; }
    }


    public class mailTemplate
    {
        [Key]
        public int TempId       { get; set; }
        public string Temptitle    { get; set; }
        public string Emailsubject { get; set; }
        public string EmailBody    { get; set; }
    }


}