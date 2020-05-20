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
        public DbSet<ProjectOwnerDetails> ProjectOwnerDetails { get; set; }
        public DbSet<ProjectOwnerDetailList> ProjectOwnerDetailList { get; set; }
        public DbSet<ProjectOwnerForDoc> ProjectOwnerForDoc { get; set; }
        
        public DbSet<SaveSocialLink> SaveSocialLink { get; set; }
        public DbSet<ProjectOwnerDetailSurveyWise> ProjectOwnerDetailSurveyWise { get; set; }
        
        public DbSet<ProjectDetailAfterSanction> ProjectDetailAfterSanction { get; set; }
        public DbSet<ProjectAuthorityOwnerList> ProjectAuthorityOwnerList { get; set; }
        public DbSet<SaveMobile> SaveMobile { get; set; }
        public DbSet<SaveEmail> SaveEmail { get; set; }
        public DbSet<SaveCertification> SaveCertification { get; set; }
        public DbSet<SaveCertificationPerson> SaveCertificationPerson { get; set; }
        public DbSet<SaveInternalTeam> SaveInternalTeam { get; set; }
        public DbSet<SaveExternalTeam> SaveExternalTeam { get; set; }
        public DbSet<PersonInfo> PersonInfo { get; set; }
        public DbSet<BussinessCategoryList> BussinessCategoryList { get; set; }
        public DbSet<BussinessCategory> BussinessCategory { get; set; }
        public DbSet<BussinessSubCategoryList> BussinessSubCategoryList { get; set; }
        public DbSet<BussinessSubCategory> BussinessSubCategory { get; set; }
        public DbSet<TeamDesignation> TeamDesignation { get; set; }
        public DbSet<TeamDesignationList> TeamDesignationList { get; set; }
        public DbSet<TeamSubDesignation> TeamSubDesignation { get; set; }
        public DbSet<TeamSubDesignationList> TeamSubDesignationList { get; set; }
        public DbSet<TeamSubSubDesignation> TeamSubSubDesignation { get; set; }
        public DbSet<TeamSubSubDesignationList> TeamSubSubDesignationList { get; set; }
        public DbSet<SaveCompanyList> SaveCompanyList { get; set; } 
        public DbSet<nProjectDetail> nProjectDetail { get; set; }
        public DbSet<SaveProjectInternalTeam> SaveProjectInternalTeam { get; set; }
        public DbSet<SaveProjectExternalTeam> SaveProjectExternalTeam { get; set; }
        public DbSet<SaveProjectOfficeSideTeam> SaveProjectOfficeSideTeam { get; set; }
        public DbSet<ProformaSetting> ProformaSetting { get; set; }

        public DbSet<AuthoritySignatory> AuthoritySignatory { get; set; }
        public DbSet<AuthoritySignatoryDetail> AuthoritySignatoryDetail { get; set; }
        public DbSet<CertificationList> CertificationList { get; set; }
        public DbSet<Certification> Certification { get; set; }
        public DbSet<nSaveSurvayDetails> nSaveSurvayDetails { get; set; }
        public DbSet<ProjectDeveloper> ProjectDeveloper { get; set; }

        public DbSet<RelationList> RelationList { get; set; }
        public DbSet<Relation> Relation { get; set; }

        public DbSet<UnitsList> UnitsList { get; set; }
        public DbSet<Units> Units { get; set; }

        public DbSet<FormationTypeList> FormationTypeList { get; set; }
        public DbSet<FormationType> FormationType { get; set; }

        public DbSet<CertificationTypeList> CertificationTypeList { get; set; }
        public DbSet<CertificationType> CertificationType { get; set; }

        public DbSet<WorkdepartmentList> WorkdepartmentList { get; set; }
        public DbSet<Workdepartment> Workdepartment { get; set; }

        public DbSet<ProjectStatusList> ProjectStatusList { get; set; }
        public DbSet<ProjectStatuses> ProjectStatuses { get; set; }

        public DbSet<ProjectTypeList> ProjectTypeList { get; set; }
        public DbSet<ProjectTypes> ProjectTypes { get; set; }
    }
}