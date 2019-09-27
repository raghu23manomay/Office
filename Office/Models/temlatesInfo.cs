using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.Models
{
   
    
    public class temlatesInfo
    {
        [Key]
        public int TemplateID { get; set; }   
        public int CityID { get; set; }
        public int AuthorityID { get; set; }
        public int DepartmentID { get; set; }
        public int TemplateTypeID { get; set; }
        public string TemplateName { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
    public class PerformaUser 
    {
        [Key]
        public int EmployeeID { get; set; } 
        public String EmployeeName { get; set; } 
    }
    public class PerformaPlaceholders
    {
        [Key]
        
        public String PlaceHolderName { get; set; }
        
    }
    
        public class DepartmentType
    {
        [Key]
        public int DepartmentId { get; set; }
        public String DepartmentName { get; set; }

    }
    public class DocTemplateList
    {
        [Key]
        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        [AllowHtml]
        [Display(Name = "Message")]
        public string Description { get; set; }
        public int TemplateTypeid { get; set; }
        public int? TotalRows { get; set; }
    }
    public class DocCreationTemplate
    {
        [Key]
        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        [AllowHtml]
        [Display(Name = "Message")]
        public string Description { get; set; }
        public int TemplateTypeid { get; set; }
        public int? TotalRows { get; set; }
    }
    public class DocCreationFilters
    {
        [Key]
        public int CityId { get; set; }
        public int AuthorityID { get; set; }
        public int TemplateTypeID { get; set; }
        List<DocCreationTemplate> result = new List<DocCreationTemplate>();
    }
    public class DataTemplateList
    {
        [Key]
        public int DTTemplateID { get; set; }
        public int ProjectID { get; set; }
        public int DeveloperId { get; set; }
        public int ConsultantId { get; set; }
        public int CoordinatorID { get; set; }
        public String DataTemplateName { get; set; }
    }

    public class PlaceholderHtml
    {
        [Key]
        public int PlaceholdeID { get; set; }
        public string FieldName { get; set; } 
         
    }
    public class DeveloperContactPerson
    {
        [Key]
        public int DeveloperContactPersonId { get; set; }
        public String DeveloperContactPersonName  { get; set; }

    }
    public class HtmlTemplate
    {
        [Key]
        public int TemplateID { get; set; } 
        [AllowHtml]
        public string Description { get; set; }
       
    }
}


