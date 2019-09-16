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
        public string Name { get; set; }
        public DateTime? EnquiryDate { get; set; }
        public string ShortName { get; set; }
        public int? StatusId { get; set; }
        public int? ProjectTypeId { get; set; }
        public string CustomerFileNo { get; set; }
        public string PhysicalPath { get; set; }
        public string Road { get; set; }
        public string Goan { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public string Duration { get; set; }
        public decimal Cost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public  IEnumerable<DeveloperData> DeveloperDatalist { get; set; } 
        public IEnumerable<CoordinatorDetailsData> CoordinatorDetailsData { get; set; }
        public IEnumerable<AssistantDetailsData> AssistantDetailsData { get; set; }
    }
    public class ProjectsDataWithValue
    {
        [Key]
        public int? TemplateID { get; set; }
        public int? ProjectID { get; set; } 
        public string TemplateName { get; set; } 
        public string TemplateDescription { get; set; }
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
}