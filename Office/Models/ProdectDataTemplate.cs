using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace office.Models
{
    public class ProdectDataTemplate
    {
    }
    public class ProjectsData
    {
        [Key]
        public int? ProjectID { get; set; }
        public int? TemplateID { get; set; }
        public string Name { get; set; }
        public int?  DTTemplateID   { get; set; }
        public string DataTemplateName { get; set; }
        public string ShortName { get; set; } 
        public string Road { get; set; }
        public string Goan { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public string Duration { get; set; }
       
        public  IEnumerable<DeveloperData> DeveloperDatalist { get; set; } 
        public IEnumerable<CoordinatorDetailsData> CoordinatorDetailsData { get; set; }
        public IEnumerable<AssistantDetailsData> AssistantDetailsData { get; set; }
        public IEnumerable<DeveloperSideContactPerson> DeveloperSideContactPersons { get; set; }
        public IEnumerable<OfficeSideContactPerson> OfficeSideContactPersons { get; set; }
        public IEnumerable<CustomPlaceholders> CustomPlaceholders { get; set; }

    }
     
    public class ProjectsDataWithValue
    {
        [Key]
        public int? DTTemplateID { get; set; }
        public int? TemplateID { get; set; }
        public int? ProjectID { get; set; } 
        public string TemplateName { get; set; }
        public string DataTemplateName { get; set; }
        public string TemplateDescription { get; set; }
        public string FilePath { get; set; }
    }
    public class DeveloperData
    {
        [Key]
     public int? DeveloperDetailId { get; set; } 
     public int ProjectID                { get; set; }
	 public string Name                     { get; set; }
	 public string OwnershipType            { get; set; }
     public int DeveloperId              { get; set; }
     public int OwnershipId             { get; set; }
	 public int MemberID                 { get; set; }
     public int MemberType               { get; set; }
     public int TypeId                   { get; set; }
     public int TitleID                  { get; set; }
     public string ShortName                { get; set; }
     public string CompanyName              { get; set; }
     public int DesignationId            { get; set; }
     public string Website                  { get; set; }
     public string ResidentialAddress1      { get; set; }
     public string ResidentialAddress2      { get; set; }
     public int ResidentialStateID       { get; set; }
     public string ResidentialDistrict      { get; set; }
     public int ResidentialCityID        { get; set; }
     public string ResidentialZipCode       { get; set; }
     public string OfficeAddress1           { get; set; }
     public string OfficeAddress2           { get; set; }
     public int StateID                  { get; set; }
     public string District                 { get; set; }
     public int CityID                   { get; set; }
     public string ZipCode                  { get; set; }
     public DateTime BirthDate                { get; set; }
     public string ShippingAddress          { get; set; }
     public string PowerofAttorny         { get; set; }
      public bool isDeveloperApplied { get; set; }
       
    }
    public class CoordinatorDetailsData
    {
        [Key]
        public int CoordinatorDetailId { get; set; }
        public int ProjectID { get; set; }
        public int CoordinatorId { get; set; }
        public string FullName { get; set; }
        public bool isCoordinatorApplied { get; set; }
    }

    public class AssistantDetailsData
    {
        [Key]
        public int AssistantDetailId { get; set; }
        public int ProjectID { get; set; }
        public int AssistantId { get; set; }
        public string FullName { get; set; } 
        public bool isAssistantApplied { get; set; }
    }
    public class ProjectsTemplateData
    {
        [Key]
        public int? ProjectID { get; set; }
        public int? DeveloperID { get; set; } 
        public int? DeveloperDetailId { get; set; }
        public int? CoordinatorID { get; set; }
        public int? ConsultantId { get; set; }
        public int? contractorID { get; set; }
        public int? AssistanceID { get; set; }
        public int? DTTemplateID { get; set; }
        public String DataTemplateName { get; set; }
        public int? TemplateID { get; set; }
        
    }
    public class SaveCustomField 
    {
        [Key]
        public int FieldID { get; set; }
        public String Value { get; set; }

    }
    public class DeveloperSideContactPerson
    {
        [Key] 
        public int DeveloperContactPersonId { get; set; }
        public string DeveloperContactPersonName { get; set; }
        public bool isContactApplied { get; set; }
    }
    public class OfficeSideContactPerson
    {
        [Key]
        public int OfficeContactPersonId { get; set; }
        public string OfficeContactPersonName { get; set; }
        public bool isContactApplied { get; set; }
    }
    public class EmptyTemplateData 
    {
        [Key]
         
        public int placeholder { get; set; }
        public int value { get; set; }
        
        
    }
}