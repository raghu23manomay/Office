using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.Models
{

    
    public class RuleBookData
        {
            [Key]
      public int id { get; set; }
      public int localauthority_id {get;set;}
      public int rule_type         {get;set;}
      public string rule_no           {get;set;}
      public string name              {get;set;}
      public string text              {get;set;}
      public string parent_id         {get;set;}
      public string rule_head_no      {get;set;}
      public string text_data         {get;set;}
      public string page_no           {get;set;}
      public string pic               {get;set;}


    }
    public class RuleDescription
    {
        [Key]
        public int id { get; set; }
        public int localauthority_id { get; set; }
        public int rule_type { get; set; }
        public string rule_no { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string parent_id { get; set; }
        public string rule_head_no { get; set; }
        public string text_data { get; set; }
        public string page_no { get; set; }
        public string pic { get; set; }


    }
    public class Submenu
    {
        [Key]

        public string text { get; set; }
        
      

    }
}