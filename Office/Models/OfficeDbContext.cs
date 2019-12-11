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
        public DbSet<RuleDescription> RuleDescription { get; set; }
        public DbSet<RuleBookData> RuleBookData { get; set; }
        
        public DbSet<DocTemplateList> DocTemplateLists { get; set; }
        public DbSet<GeeratedDocumentList> GeeratedDocumentLists { get; set; }

        public DbSet<CustomPlaceholders> CustomPlaceholders { get; set; }
        public DbSet<PerformaPlaceholders> PerformaPlaceholders { get; set; }
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
        public DbSet<DeveloperDetails> DFDeveloperDetails { get; set; }
        public DbSet<SaveProject> DFSaveProject { get; set; }
        public DbSet<UpdateProject> DFUpdateProject { get; set; }
        public DbSet<DeveloperContactDetails> DFDeveloperContactDetails { get; set; }
        public DbSet<CoordinatorDetails> DFCoordinatorDetails { get; set; }
        public DbSet<AssistantDetails> DFAssistantDetails { get; set; }
        public DbSet<OfficePersonDetails> DFOfficePersonDetails { get; set; }
        public DbSet<SurvayDetails> DFSurvayDetails { get; set; }
        public DbSet<GatDetails> DFGatDetails { get; set; }
        public DbSet<CTSDetails> DFCTSDetails { get; set; }
        public DbSet<PlotDetails> DFPlotDetails { get; set; }
        public DbSet<FinalPlotDetails> DFFinalPlotDetails { get; set; }
        public DbSet<ConsultantDetails> DFConsultantDetails { get; set; }
        public DbSet<ContractorDetails> DFContractorDetails { get; set; }
        public DbSet<FieldDetails> DFFieldDetails { get; set; }
        public DbSet<ParameterDetails> DFParameterDetails { get; set; }
      

        public DbSet<temlatesInfo> temlatesList { get; set; }
        public DbSet<ProjectsData> ProjectsData { get; set; }
        public DbSet<ProjectsDataWithValue> ProjectsDataWithValue { get; set; }
        public DbSet<DeveloperData> DeveloperData { get; set; }
        public DbSet<CoordinatorDetailsData> CoordinatorDetailsData { get; set; }
        public DbSet<AssistantDetailsData> AssistantDetailsData { get; set; }
        public DbSet<DataTemplateList> DataTemplateLists { get; set; }
        public DbSet<DepartmentType> DepartmentTypes { get; set; }
      
        public DbSet<DeveloperSideContactPerson> DeveloperSideContactPersons { get; set; }
        public DbSet<OfficeSideContactPerson> OfficeSideContactPersons { get; set; }
        public DbSet<EmptyTemplateData> EmptyTemplateData { get; set; }
        public DbSet<CompanyDetailsList> CompanyDetailsList { get; set; }
        public DbSet<CompanyDetails> CompanyDetails { get; set; }
        public DbSet<CompanyAddress> CompanyAddress { get; set; }
        public DbSet<SaveCompanyMobile> SaveCompanyMobile { get; set; }
        public DbSet<SaveMobile> SaveMobile { get; set; }
        public DbSet<SaveEmail> SaveEmail { get; set; }
        public DbSet<SaveCertification> SaveCertification { get; set; }
        public DbSet<SaveInternalTeam> SaveInternalTeam { get; set; }
        public DbSet<SaveExternalTeam> SaveExternalTeam { get; set; }
    }
     
}