using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Calibration.Models;

namespace office.Models
{
    public class OfficeDbContext : DbContext
    { 
        static OfficeDbContext()
        {
            Database.SetInitializer<OfficeDbContext>(null);
        }
        public OfficeDbContext()
            : base("Name=OfficeDbContext")
        {
        } 
        public DbSet<UserPermission> UserPermission { get; set; }

      
        public DbSet<DesignationList> DFList { get; set; }
        public DbSet<Designation> DesigList { get; set; }
        public DbSet<Role> RoleLists { get; set; }
        public DbSet<RoleList> DFRoleLists { get; set; }
        public DbSet<Authority> AuthorityLists { get; set; }
        public DbSet<AuthorityList> DFAuthorityLists { get; set; }
        public DbSet<Module> ModuleLists { get; set; }
        public DbSet<ModuleList> DFModuleLists { get; set; }
        public DbSet<Subscription> SubscriptionLists { get; set; }
        public DbSet<SubscriptionList> DFSubscriptionLists { get; set; }
        public DbSet<Customer> CustomerLists { get; set; }
        public DbSet<CustomerList> DFCustomerLists { get; set; }
        public DbSet<City> CItyLists { get; set; }
        public DbSet<CityList> DFCityLists { get; set; }

        public DbSet<Employee> EmployeeLists { get; set; }
        public DbSet<EmployeeList> DFEmployeeLists { get; set; }
        public DbSet<PersonList> DFPersonList { get; set; }
        public DbSet<MemberList> DFMemberList { get; set; }
        public DbSet<MemberPersonList> DFMemberPersonList { get; set; }
        public DbSet<ContactPersonListByMember> DFContactPersonListByMember { get; set; }
        public DbSet<Member> DFMember { get; set; }
        public DbSet<MemberContactDetails> DFMemberContactDetails { get; set; }
        public DbSet<Person> DFPerson { get; set; }
        public DbSet<PersonMobileList> DFPersonMobileList { get; set; }
        public DbSet<PersonEmailList> DFPersonEmailList { get; set; }
        public DbSet<DepartmentList> DFDepartmentList { get; set; }
        public DbSet<Department> DepartmentLists { get; set; }
     
    }
    
}