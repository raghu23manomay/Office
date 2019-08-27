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
    public  Boolean IsActive { get; set; }
     
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
    public class  EmployeeList
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
       public int? PersonalInfoID   { get; set; }
       public string FullName { get; set; }      
       public string BirthDate        { get; set; }
       public string Address1         { get; set; }
       public string Address2         { get; set; }
       public string DesignationName { get; set; }
       public string Note             { get; set; }
       public string Email            { get; set; }
       public string Mobile           { get; set; }
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
        public string DesignationId { get; set; }
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
     
       public string MemberType        { get; set; }
       public int TitleID           { get; set; }
       public string Name              { get; set; }
       public string ShortName         { get; set; }
       public string CompanyName       { get; set; }
       public int DesignationId     { get; set; }
       public string Website           { get; set; }
       public string OfficeAddress1    { get; set; }
       public string OfficeAddress2    { get; set; }
       public int StateID           { get; set; }
       public string District        { get; set; }
       public int CityID            { get; set; }
       public string ZipCode           { get; set; }
       public DateTime BirthDate         { get; set; }
       public string ShippingAddress   { get; set; }
       public string PowerofAttorny    { get; set; }
       public int CreatedBy         { get; set; }
       public string ResidentialAddress1  { get; set; }
       public string ResidentialAddress2  { get; set; }
       public int? ResidentialStateID   { get; set; }
       public string ResidentialDistrict  { get; set; }
       public int? ResidentialCityID    { get; set; }
       public string ResidentialZipCode { get; set; }

    }
    
}












