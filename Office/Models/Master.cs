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
        public int? PersonID { get; set; }
        public string ShortName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? DesignationId { get; set; }

        public string DesignationName { get; set; }
        public int? TotalRows { get; set; }
        public bool IsActive { get; set; }

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
        public Nullable<System.DateTime> BirthDate { get; set; } 
        public int CertificationID { get; set; }
        public string ResidentialAddress1 { get; set; }
        public string ResidentialAddress2 { get; set; }
        public int? ResidentialStateID { get; set; }
        public string ResidentialDistrict { get; set; }
        public int? ResidentialCityID { get; set; }
        public string ResidentialZipCode { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
         
        public IEnumerable<SaveCompanyMobile> SaveCompanyMobile { get; set; }
    }

    public class MemberList
    {
        [Key]
        public int MemberID { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public int TitleID { get; set; }
        public string Name { get; set; }
        public int MemberType { get; set; }
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
        public int MemberID { get; set; }
        public string PersonName { get; set; }
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }

    }


    public class ContactPersonListByMember
    {
        [Key]
        public int MembeConactPersonDetailId { get; set; }
        public int MemberID { get; set; }
        public int ContactPersonId { get; set; }
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
        public int PersonalInfoID { get; set; }
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
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public int CategoryID1 { get; set; }
        public int SubCategoryID1 { get; set; }
        public int CertificationID { get; set; }
        public int CompanyOwnershipTypeID { get; set; }
        public int Isclient { get; set; }
        public Nullable<DateTime> RequestDate { get; set; }
        public Nullable<DateTime> InceptionDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime>   CreatedDate { get; set; }
    }
    public class ProjectDetailsLeftSide
    {
        public int ProjectID { get; set; }
        public IEnumerable<SaveProjectInternalTeam> SaveProjectInternalTeam { get; set; }
        public IEnumerable<SaveProjectExternalTeam> SaveProjectExternalTeam { get; set; }
        public IEnumerable<SaveProjectOfficeSideTeam> SaveProjectOfficeSideTeam { get; set; }
        public IEnumerable<AuthoritySignatory> AuthoritySignatory { get; set; }
        public IEnumerable<AuthoritySignatoryDetail> AuthoritySignatoryDetail { get; set; }
    }
    public class CompanyDetailsLeftSide 
    {
        public int CompanyID { get; set; }
        public IEnumerable<CompanyAddress> CompanyAddress { get; set; }
        public IEnumerable<SaveCompanyMobile> SaveCompanyMobile { get; set; }
        public IEnumerable<SaveCertification> SaveCertification { get; set; }
        public IEnumerable<SaveInternalTeam> SaveInternalTeam { get; set; }
        public IEnumerable<SaveExternalTeam> SaveExternalTeam { get; set; }
        public IEnumerable<SaveCompanyMobile> SaveCompanyMobile2 { get; set; }
        public IEnumerable<SaveCompanyMobile> SaveCompanyMobile3 { get; set; }
    }
    public class CompanyDetailsforPerson
    {
        public int PersonID { get; set; }
        public IEnumerable<SaveCompanyList> SaveCompanyList { get; set; }
        public IEnumerable<SaveCertificationPerson> SaveCertificationPerson { get; set; }
        public IEnumerable<SaveCompanyMobile> SaveCompanyMobile { get; set; }
        public IEnumerable<SaveSocialLink> SaveSocialLink { get; set; }
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
        public Nullable<DateTime>   RequestDate { get; set; }
        public Nullable<DateTime>   InceptionDate { get; set; }
        public int CreatedBy { get; set; }
        public string   CreatedDate { get; set; }
        public int? TotalRows { get; set; } 
    }

    public class SaveCompanyMobile
    {
        [Key]
        public int? CompanyPhoneID { get; set; }
        public string Value { get; set; } 
        public int Type { get; set; }
        public int WorkDepartmentID { get; set; }
        public string Extension { get; set; }
        public int AddressID { get; set; }

    }

    public class SaveSocialLink
    {
        [Key]
        public string SocialLinkText { get; set; }
        public int? SocialLinkId { get; set; }
    }
    public class SavePersonMobile
    {
        [Key]
        public string Value { get; set; }
        public int? PersonPhoneID { get; set; }
        public int Type { get; set; }
        public int WorkDepartmentID { get; set; }
        public string Extension { get; set; }
        public int AddressID { get; set; }

    }
    public class SaveCompanyEmail
    {
        [Key]
        public string Value { get; set; }
        public int? CompanyPhoneID { get; set; }
        public int Type { get; set; }
        public int WorkDepartmentID { get; set; }
        public int AddressID { get; set; }
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
    public class SaveProjectInternalTeam
    {
        [Key]
        public int internalpersonid { get; set; }
        public int parentcompanyid { get; set; }
        public int internalTeamid { get; set; }
        public int designationid1 { get; set; }
        public int subdesignationid1 { get; set; }
        public int subpartdesignationid1 { get; set; }
        public string InternalPersonName { get; set; }
        public string DesignationidText { get; set; }
        public string SubDesignationText { get; set; }
        public string SubPartDesignationText { get; set; }
    }
    public class SaveProjectExternalTeam
    {
        [Key]
        public int Externalpersonid { get; set; }
        public int parentcompanyid { get; set; }
        public int ExternalTeamid { get; set; }
        public int designationid1 { get; set; }
        public int subdesignationid1 { get; set; }
        public int subpartdesignationid1 { get; set; }
        public string InternalPersonName { get; set; }
        public string DesignationidText { get; set; }
        public string SubDesignationText { get; set; }
        public string SubPartDesignationText { get; set; }
    }
    public class SaveProjectOfficeSideTeam
    {
        [Key]
        public int personid { get; set; }
        public int Teamid { get; set; }
        public int designationid1 { get; set; }
        public int subdesignationid1 { get; set; }
        public int subpartdesignationid1 { get; set; }
        public string InternalPersonName { get; set; }
        public string DesignationidText { get; set; }
        public string SubDesignationText { get; set; }
        public string SubPartDesignationText { get; set; }
    }
    public class SaveCompanyList
    {
        [Key]
        public int? Listid { get; set; }
        public int CompanyID { get; set; }
        
        public int designationid1 { get; set; }
        public int subdesignationid1 { get; set; }
        public int subpartdesignationid1 { get; set; }
        public string CompanyName { get; set; }
        public string DesignationidText { get; set; }
        public string SubDesignationText { get; set; }
        public string SubPartDesignationText { get; set; }
    }
    public class SaveExternalTeam
    {
        [Key]
      
        public int ExternalTeamid { get; set; }
        public int ExternalPersonId { get; set; }
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
        public int CompanyCertificationsDetailID { get; set; }
        public int CertificationID { get; set; }
        public String CertificationText { get; set; }
        public String Value { get; set; }
    }
    public class SaveCertificationPerson
    {
        [Key]
        public int PersonCertificationsDetailID { get; set; }
        public int CertificationID { get; set; }
        public String CertificationText { get; set; }
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
    public class BussinessCategoryList
    {
        [Key]
        public int? BusinessCategoryID { get; set; }
        public string BusinessCategoryName { get; set; }
        public string IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalRows { get; set; }

    }

    public class BussinessCategory
    {
        [Key]
        public int? BusinessCategoryID { get; set; }
        public string BusinessCategoryName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }

    }
    public class BussinessSubCategoryList
    {
        [Key]

        public int? BusinessSubCategoryID { get; set; }
        public int? BusinessCategoryID { get; set; }
        public string BusinessCategoryName { get; set; }
        public string BusinessSubCategoryName { get; set; }
        public string IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalRows { get; set; }

    }

    public class BussinessSubCategory
    {
        [Key]
        public int? BusinessSubCategoryID { get; set; }
        public int? BusinessCategoryID { get; set; }
        public string BusinessSubCategoryName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }

    }
    public class TeamDesignation
    {
        [Key]
        public int? TeamDesignationID { get; set; }
        public string TeamDesignationName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }

    }
    public class TeamDesignationList
    {
        [Key]
        public int? TeamDesignationID { get; set; }
        public string TeamDesignationName { get; set; }
        public string IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalRows { get; set; }

    }
    public class TeamSubDesignation
    {
        [Key]
        public int? TeamSubDesignationID { get; set; }
        public int? TeamDesignationID { get; set; }
        public string TeamSubDesignationName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }

    }
    public class TeamSubDesignationList
    {
        [Key]
        public int? TeamSubDesignationID { get; set; }
        public int? TeamDesignationID { get; set; }
        public string TeamDesignationName { get; set; }
        public string TeamSubDesignationName { get; set; }
        public string IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalRows { get; set; }

    }
    public class TeamSubSubDesignation
    {
        [Key]
        public int? TeamSubSubDesignationID { get; set; }
        public int? TeamSubDesignationID { get; set; }
        public int? TeamDesignationID { get; set; }
        public string TeamSubSubDesignationName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
    public class TeamSubSubDesignationList
    {
        [Key]
        public int? TeamSubDesignationID { get; set; }
        public int? TeamSubSubDesignationID { get; set; }
        public int? TeamDesignationID { get; set; }
        public string TeamDesignationName { get; set; }
        public string TeamSubDesignationName { get; set; }
        public string TeamSubSubDesignationName { get; set; }
        public string IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalRows { get; set; }
    }
    public class nProjectList
    {
        [Key]
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public DateTime EnquiryDate { get; set; }
        public string ShortName { get; set; }
        public int StatusId { get; set; }
        public int ProjectTypeId { get; set; }
        public string CustomerFileNo { get; set; }
        public string PhysicalPath { get; set; }
        public string Road { get; set; }
        public string Goan { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectType { get; set; }
        public int? TotalRows { get; set; }

    }
    public class nProjectDetail
    {
        [Key]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime EnquiryDate { get; set; }
        public string ProjectShortName { get; set; }
        public int StatusId { get; set; }
        public int ProjectTypeId { get; set; }
        public string CustomerFileNo { get; set; }
        public string PhysicalPath { get; set; }
        public int StateID { get; set; } 
        public int TalukaID { get; set; }
        public string District { get; set; }
        public string Road { get; set; }
        public string Goan { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public decimal Cost { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; } 
        public string Developers { get; set; }
        public int DeveloperTypeID { get; set; }
    }

    public class nSaveSurvayDetails
    {
        [Key]
        public int SurvayNo { get; set; }
        public int SurvayDetailId { get; set; }
        public int SurvayTypeID { get; set; }
        public int HissaNo { get; set; }
        public decimal Area { get; set; }
        public int UnitID { get; set; }
    }
    public class SaveProformaSetting
    {
        [Key]

        public string SelectedField { get; set; }
        public string UnSelectedField { get; set; }

    }
    public class ProformaSetting
    {
        [Key]
        public int FieldID { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string FieldName { get; set; }
        public string TableName { get; set; }
        public bool isApplicable { get; set; }
    }

    public class AuthoritySignatory
    {
        [Key]
        public int signatorId { get; set; }
        public int ProjectID { get; set; }
        public int OwnerID { get; set; }
        public decimal AreaUnderDevelopment { get; set; }
        public int AreaUnitID { get; set; }
        public string AgreementNo { get; set; }
        public DateTime AgreementDate { get; set; }
        public string SubRegistarOffice { get; set; }
        public int DocumentID { get; set; } 
        public string selectedVal1 { get; set; }
        public string selectedVal2 { get; set; }
        public string selectedVal3 { get; set; }
        public string selectedVal4 { get; set; }
        public string selectedVal5 { get; }
        public int either1 { get; set; }
        public int either2 { get; set; }
        public int either3 { get; set; }
        public int either4 { get; set; }
        public int either5 { get; set; }
    }
    public class AuthoritySignatoryDetail
    {
        [Key] 
        public int SignatoryDetailID { get; set; }
        public int SignatoryID { get; set; }
        public int ProjectID { get; set; }
        public int OwnerID { get; set; } 
        public int PersonId { get; set; }
        public int GroupId { get; set; }
        public string PName { get; set; } 
    }
    public class CompanyFields 
    {

        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public int CategoryID1 { get; set; }
        public int SubCategoryID1 { get; set; }
        public int CertificationID { get; set; }
        public int CompanyOwnershipTypeID { get; set; }
        public int Isclient { get; set; }
        public Nullable<DateTime> RequestDate { get; set; }
        public Nullable<DateTime> InceptionDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<ProformaSetting> ProformaSetting { get; set; }
    }
    public class ProjectOwnerSurveyNo
    { 
        [Key]
        public int SurvayDetailId { get; set; }
        public int ProjectID { get; set; }
        public string  S_SurveyNo  { get; set; }
        public int  S_OwnerID         { get; set; }
        public string  S_HissaNo         { get; set; }
        public string  S_PlotNo          { get; set; }
        public string  S_OldNo           { get; set; }
        public string  G_SurveyNo    { get; set; }
        public string  G_HissaNo         { get; set; }
        public string  G_PlotNo          { get; set; }
        public string  G_OldNo           { get; set; }        
        public string  CTS_SurveyNo  { get; set; }
        public string  CTS_HissaNo       { get; set; }
        public string CTS_PlotNo        { get; set; }
        public string CTS_OldNo         { get; set; }
        public string FP_SurveyNo  { get; set; }
        public int PrimarySurvey { get; set; }
    }
    public class ProjectOwnerDetails
    {
        [Key]
        public int OwnerID { get; set; }
        public String OwnerName { get; set; }
        
        public int PropertCardTypeId { get; set; }
        public int SurvayDetailId { get; set; }  
        public int isUndevidedShare { get; set; }
        public Decimal Area { get; set; }
        public int AreaUnitID { get; set; }
        public String UnitName { get; set; }
        
    }
    public class ProjectOwnerDetailList
    {
        [Key]
        public int OwnerID { get; set; }
        public String OwnerName { get; set; } 
        public int PropertCardTypeId { get; set; }
        public int SurvayDetailId { get; set; }
        public int isUndevidedShare { get; set; }
        public decimal Area { get; set; }
        public int AreaUnitID { get; set; }
        public String UnitName { get; set; }
        public String SurvayNo { get; set; } 
        public String SHissaNo { get; set; }
        public String GatNo { get; set; } 
        public String GatHissaNo { get; set; } 
        public String CTSNo { get; set; }
        public String CTSHissaNo { get; set; } 
        public String FInalPlotNo { get; set; } 
    }
    public class ProjectAuthorityOwnerList
    {
        [Key]
        public int OwnerID { get; set; }
        public String OwnerName { get; set; }
        public int PropertCardTypeId { get; set; }
        public int SurvayDetailId { get; set; }
        public int isUndevidedShare { get; set; }
        public decimal Area { get; set; }
        public int AreaUnitID { get; set; }
        public String UnitName { get; set; }
        public String SurvayNo { get; set; }
        public String SHissaNo { get; set; }
        public String GatNo { get; set; }
        public String GatHissaNo { get; set; }
        public String CTSNo { get; set; }
        public String CTSHissaNo { get; set; }
        public String FInalPlotNo { get; set; }
        public String Remark { get; set; } 
    }
    public class ProjectOwnerDetailSurveyWise 
    {
        [Key]
        public int SurvayDetailId { get; set; }
        public String SurvayNo { get; set; }
        public String OwnerName { get; set; } 
        public Decimal Area { get; set; }  
        public String UnitName { get; set; } 
        public String SHissaNo { get; set; }
        public String GatNo { get; set; }
        public String GatHissaNo { get; set; }
        public String CTSNo { get; set; }
        public String CTSHissaNo { get; set; }
        public String FInalPlotNo { get; set; }
    }
    public class Certification
    {
        [Key]
        public int CertificationID { get; set; }
        public string CertificationName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
    public class CertificationList
    {
        [Key]
        public int CertificationID { get; set; }
        public string CertificationName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? TotalRows { get; set; }

    }

    public class ProjectOwnerDetailSurveyWiseAll
    {

        public int ProjectID { get; set; }
        public IEnumerable<ProjectOwnerDetailList> ProjectOwnerDetailList { get; set; }
        public IEnumerable<ProjectOwnerDetailSurveyWise> ProjectOwnerDetailSurveyWise { get; set; }
        
    }
}



