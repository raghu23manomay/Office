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
}