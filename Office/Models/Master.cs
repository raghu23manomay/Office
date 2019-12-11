using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace office.Models
{


   
    public class DesignationList
    {
        [Key]
        public int DesignationID { get; set; }
        IEnumerable<DesignationList> DesignationIDlist { get; set; }
        public String DesignationName { get; set; }
        public String IsActive { get; set; }
        public int? TotalRows { get; set; }

    }
    public class Designation
    {
        [Key]
        public int DesignationID { get; set; }
        public String DesignationName { get; set; }
        public Boolean IsActive { get; set; }
    }

    public class RoleList
    {
        [Key]
        public int RoleID { get; set; }
        IEnumerable<RoleList> RoleIDlist { get; set; }
        public String RoleName { get; set; }
        public String IsActive { get; set; }
        public int? TotalRows { get; set; }

    }
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public String RoleName { get; set; }
        public Boolean IsActive { get; set; }
    }
    public class CityList
    {
        [Key]
        public int CityID { get; set; }
        IEnumerable<CityList> CityIDlist { get; set; }
        public int StateID { get; set; }
        public String CityName { get; set; }
        public String IsActive { get; set; }
        public int? TotalRows { get; set; }

    }
    public class City
    {
        [Key]
        public int CityID { get; set; }
        public int StateID { get; set; }
        public String CityName { get; set; }
        public Boolean IsActive { get; set; }
    }

    public class AuthorityList
    {
        [Key]
        public int AuthorityID { get; set; }
        IEnumerable<AuthorityList> AuthorityIDList { get; set; }
        public String AuthorityName { get; set; }
        public String IsActive { get; set; }
        public int? TotalRows { get; set; }

    }
    public class Authority
    {
        [Key]
        public int AuthorityID { get; set; }
        public String AuthorityName { get; set; }
        public Boolean IsActive { get; set; }
    }
    public class ModuleList
    {
        [Key]
        public int ModuleID { get; set; }
        IEnumerable<ModuleList> ModuleIDlist { get; set; }
        public String ModuleName { get; set; }
        public String IsActive { get; set; }
        public int? TotalRows { get; set; }

    }
    public class Module
    {
        [Key]
        public int ModuleID { get; set; }
        public String ModuleName { get; set; }
        public Boolean IsActive { get; set; }
    }

    public class CustomerList
    {
        [Key]

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int CityID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public String IsActive { get; set; }
        public String CityName { get; set; }
        IEnumerable<CustomerList> CustomerIDlist { get; set; }
        public int? TotalRows { get; set; }
    }
    public class Customer
    {

        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int CityID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Boolean IsActive { get; set; }

    }
    public class SubscriptionList
    {
        [Key]
        public int SubscriptionID { get; set; }
        IEnumerable<SubscriptionList> SubscriptionIDlist { get; set; }
        public String PlanName { get; set; }
        public int DurationInDays { get; set; }
        public decimal Cost { get; set; }
        public String IsActive { get; set; }
        public int? TotalRows { get; set; }

    }
    public class Subscription
    {
        [Key]
        public int SubscriptionID { get; set; }
        public String PlanName { get; set; }
        public int DurationInDays { get; set; }
        public decimal Cost { get; set; }
        public Boolean IsActive { get; set; }
    }
    public class Employee
    {

        [Key]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int CityID { get; set; }

        public string Mobile { get; set; }
        public string Email { get; set; }
        public Boolean IsActive { get; set; }

    }
    public class EmployeeList
    {
        [Key]
        public int EmployeeID { get; set; }
        IEnumerable<EmployeeList> EmployeeIDlist { get; set; }
        public String EmployeeName { get; set; }
        public String IsActive { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? TotalRows { get; set; }

    }

    public class PersonList
    {
        [Key]
        public int? PersonalInfoID { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string DesignationName { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? TotalRows { get; set; }

    }

    public class Person
    {
        [Key]
        public int? PersonalInfoID { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? DesignationId { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }

    }

    public class SaveMobile
    {
        [Key]
        public string Mobile { get; set; }
    }
    public class SaveMobilePerson
    {
        [Key] 
        public string Value { get; set; }
        public int Type { get; set; }
        public int DepartmentID { get; set; }
    }
    public class SaveEmail
    {
        [Key]
        public string Email { get; set; }
    }

    public class SaveConactPerson
    {
        [Key]
        public int? ContactPersonId { get; set; }
    }

    public class SavePhone
    {
        [Key]
        public string Phone { get; set; }
    }

    public class SaveOfficeNo
    {
        [Key]
        public string OfficeNo { get; set; }
    }

    public class SaveAdditionalFiled
    {
        [Key]
        public string AdditionlField { get; set; }
    }

    public class SaveParameter
    {
        [Key]
        public string Parameter { get; set; }
    }

    public class SaveMemberPerson
    {
        [Key]
        public int PersonMemberID { get; set; }
        public int DesignationId { get; set; }
    }


    public class Member
    {
        [Key]        
        public int? MemberID { get; set; }
        public int MemberType { get; set; }
        public int? TypeId { get; set; }        
        public int TitleID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CompanyName { get; set; }
        public int? DesignationId { get; set; }
        public string Website { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public int? StateID { get; set; }
        public string District { get; set; }
        public int? CityID { get; set; }
        public string ZipCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string ShippingAddress { get; set; }
        public string PowerofAttorny { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }        
        public string ResidentialAddress1 { get; set; }
        public string ResidentialAddress2 { get; set; }
        public int? ResidentialStateID { get; set; }
        public string ResidentialDistrict { get; set; }
        public int? ResidentialCityID { get; set; }
        public string ResidentialZipCode { get; set; }
       
    }

    public class PersonInfo
    {
        [Key]
        public int? PersonID { get; set; }   
        public int TitleID { get; set; }
        public string fName { get; set; }
        public string mName { get; set; } 
        public string lName { get; set; }
        public string ShortName { get; set; } 
        public string Website { get; set; }
        public string SocialMedia { get; set; }
        public DateTime BirthDate { get; set; } 
        public int CertificationID { get; set; } 
        public string ResidentialAddress1 { get; set; }
        public string ResidentialAddress2 { get; set; }
        public int? ResidentialStateID { get; set; }
        public string ResidentialDistrict { get; set; }
        public int? ResidentialCityID { get; set; }
        public string ResidentialZipCode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MemberList
    {
       [Key]
       public int MemberID       { get; set; }
       public string CompanyName    { get; set; }
       public string ShortName      { get; set; }
       public int TitleID        { get; set; }
       public string Name           { get; set; }
       public int MemberType     { get; set; }
       public string MemberTypeName { get; set; }
       public string PowerofAttorny { get; set; }
       public int MemberPersonCount { get; set; }
       public int ContactPersonCount { get; set; }
       public string CreatedDate { get; set; }
       public string CreatedBy { get; set; }

        public int? TotalRows { get; set; }
        
    }


    public class MemberPersonList
    {
        [Key]
        public int MemberPersonDetailId { get; set; }
        public int MemberID             { get; set; }
        public string PersonName { get; set; }
        public int DesignationID   { get; set; }
        public string DesignationName { get; set; }

    }


    public class ContactPersonListByMember
    {
       [Key]
       public int MembeConactPersonDetailId { get; set; }
       public int MemberID                  { get; set; }
       public int ContactPersonId           { get; set; }
       public string FullName { get; set; }

    }

    public class MemberContactDetails
    {
        [Key]
        public string Contact { get; set; }
        public int Id { get; set; }
        public string ContactType { get; set; }
    }

    public class PersonMobileList
    {
        [Key]
        public int PMobileDetailId { get; set; }
        public int PersonalInfoID  { get; set; }
        public string Mobile { get; set; }
    }

    public class PersonEmailList
    {
        [Key]
        public int PEmailDetailId { get; set; }
        public int PersonalInfoID { get; set; }
        public string Email { get; set; }
    }


    public class DepartmentList
    {
        [Key]
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalRows { get; set; }       

    }

    public class Department
    {
        [Key]
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }        

    }
    public class ConsultantTypeMaster
    {
        [Key]
        public int? ConsultantTypeID { get; set; }
        public string ConsultantType { get; set; }
        public bool IsActive { get; set; }
    }
    public class ConsultantTypeList
    {
        [Key]
        public int? ConsultantTypeID { get; set; }
        public string ConsultantType { get; set; }
        public string IsActive { get; set; } 
        public int? TotalRows { get; set; }

    }

    public class CompanyDetails
    {
        [Key] 
       public int CompanyID                 { get; set; }
       public string CompanyName            { get; set; }
       public string ShortName              { get; set; }
       public int CategoryID1               { get; set; }
       public int SubCategoryID1            { get; set; }
       public int CertificationID           { get; set; }
       public int CompanyOwnershipTypeID    { get; set; }
       public int Isclient                  { get; set; }
       public DateTime RequestDate          { get; set; }
       public DateTime InceptionDate        { get; set; }
       public int CreatedBy                 { get; set; }
       public DateTime CreatedDate          { get; set; }
      
        
    }
    public class CompanyDetailsLeftSide
    {
        public int CompanyID { get; set; }
        public IEnumerable<CompanyAddress> CompanyAddress { get; set; }
        public IEnumerable<SaveCompanyMobile> SaveCompanyMobile { get; set; }
        public IEnumerable<SaveCertification> SaveCertification { get; set; }
        public IEnumerable<SaveInternalTeam> SaveInternalTeam { get; set; } 
        public IEnumerable<SaveExternalTeam> SaveExternalTeam { get; set; }
    }
    public class CompanyDetailsList
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public int BusinessCategoryID { get; set; }
        public int BusinessSubCategoryID { get; set; }
        public string BusinessCategoryName { get; set; } 
        public string BusinessSubCategoryName { get; set; }
        public int CertificationID { get; set; }
        public int CompanyOwnershipTypeID { get; set; }
        public int Isclient { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime InceptionDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalRows { get; set; }

    }
  
    public class SaveCompanyMobile
    {
        [Key]
          
           public string Value          { get; set; }
           public int Type                { get; set; } 
           public int WorkDepartmentID   { get; set; } 
           public string Extension       { get; set; }
           public int AddressID { get; set; }
        public int? CompanyPhoneID { get; set; }
    }
    public class SaveCompanyEmail
    {
        [Key]
        public string Email { get; set; } 
        public int WorkDepartmentID { get; set; }
        public string Extension { get; set; }

    }
    public class CompanyAddress
    {
        [Key]
        public int AddressID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? StateID { get; set; }
        public string District { get; set; }
        public int? CityID { get; set; }
        public string ZipCode { get; set; }
        public string Website { get; set; }
    }
    public class SaveInternalTeam
    {
        [Key]
        public int internalpersonid { get; set; }
        public int internalTeamid { get; set; } 
        public int designationid1 { get; set; }
        public int subdesignationid1 { get; set; }
        public int subpartdesignationid1 { get; set; }
        public string InternalPersonName { get; set; }
        public string DesignationidText { get; set; }
        public string SubDesignationText { get; set; }
        public string SubPartDesignationText { get; set; }
    }
    public class SaveExternalTeam
    {
        [Key]
        public int ExternalPersonId { get; set; }
        public int ExternalTeamid { get; set; }
        public int RelationId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int ExternalCompanyId { get; set; }
        public int DesignationId { get; set; } 
        public int SubDesignationId { get; set; }
        public int SubpartDesignationId { get; set; }
        public string RelationName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ExternalPersonName { get; set; }
        public string DesignationidText { get; set; }
        public string SubDesignationText { get; set; }
        public string SubPartDesignationText { get; set; }
    }
    public class SaveCertification
    {
        [Key]
        public int CertificationID { get; set; } 
        public String CertificationText { get; set; }
        public int CompanyCertificationsDetailID { get; set; } 
        public String Value { get; set; }
        
    }
    public class CompanyAddressMobile
    {
        [Key]
        public int AddressID { get; set; }
        public string OfcAddress1 { get; set; }
        public string OfcAddress2 { get; set; }
        public int? StateID2 { get; set; }
        public string OfcDistrict { get; set; }
        public int? CityID2 { get; set; }
        public string OfcZip { get; set; }
        public string Website { get; set; }
        public IEnumerable<SaveCompanyMobile> SaveCompanyMobile { get; set; }
         
        }

}











