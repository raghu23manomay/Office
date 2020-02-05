using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace office.Models
{
    public class AdminFlow
    {
    }

    public class SaveProject
    {
        [Key]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime EnquiryDate     { get; set; }
        public string ProjectShortName { get; set; }
        public int StatusId        { get; set; }
        public int ProjectTypeId   { get; set; }
        public string CustomerFileNo  { get; set; }
        public string PhysicalPath    { get; set; }
        public string Road   { get; set; }
        public string Goan  { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        //public string Duration { get; set; }
        //public decimal Cost { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate  { get; set; }
        public bool IsActive       { get; set; }
        public int CreatedBy       { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectType { get; set; }        
        public int? TotalRows { get; set; }        

    }

    public class UpdateProject
    {
       [Key]
       public int? ProjectID { get; set; } 
       public string Name { get; set; }
       public DateTime? EnquiryDate { get; set; } 
       public string ShortName    { get; set; } 
       public int? StatusId { get; set; }
       public int? ProjectTypeId { get; set; } 
       public string CustomerFileNo { get; set; } 
       public string PhysicalPath   { get; set; } 
       public string Road           { get; set; } 
       public string Goan           { get; set; } 
       public string Taluka         { get; set; } 
       public string District       { get; set; } 
       public string Duration       { get; set; } 
       public decimal Cost           { get; set; } 
       public DateTime StartDate      { get; set; } 
       public DateTime EndDate        { get; set; } 
       public bool IsActive       { get; set; } 
       public int? CreatedBy      { get; set; } 

    }

        public class SaveDeveloper
    {
        [Key]
        public int DeveloperId { get; set; }
        public int OwnershipId { get; set; }

    }
   
    public class SaveDeveloperContact
    {
        [Key]
        public int DeveloperContactPersonId { get; set; }
    }
    
    public class SaveCoordinator
    {
        [Key]
        public int CoordinatorId { get; set; }
    }
   
    public class SaveAssistant
    {
        [Key]
        public int AssistantId { get; set; }
    }
    
    public class SaveOfficeContact
    {
        [Key]
        public int OfficeContactPersonId { get; set; }
    }
   
    public class SaveSurvayDetails
    {
        [Key]
       public int SurvayNo { get; set; }
       public int HissaNo  { get; set; }
       public decimal Area { get; set; }
       public int UnitID { get; set; }
    }

   
    public class SaveGatDetails
    {
       [Key]
       public int GatNo     { get; set; }
       public int HissaNo   { get; set; }
       public decimal Area  { get; set; }
       public int UnitID { get; set; }
    }
    
    public class SaveCTSDetails
    {
      [Key]
      public int  CTSNo     { get; set; }
      public int  HissaNo   { get; set; }
      public decimal  Area  { get; set; }
      public int UnitID { get; set; }
    }
   
    public class SavePlotDetails
    {
       [Key]
       public int PlotNo    { get; set; }
       public decimal Area  { get; set; }
       public int UnitID { get; set; }
    }
   
    public class SaveFinalPlotDetails
    {
       [Key]
       public int FinalPlotNo { get; set; }
       public decimal Area    { get; set; }
       public int UnitID { get; set; }
    }
   
    public class SaveConsultant
    {
        [Key]
        public int ConsultantId     { get; set; }
        public int ConsultantTypeId { get; set; }
    }
    
    public class SaveContractor
    {
       [Key]
       public int ConstractorId     { get; set; }
       public int ContractorTypeId { get; set; }
    }
    
    public class SaveField
    {
        [Key]
        public string Field { get; set; }
    }
    
    public class SaveProjectParameter
    {
        [Key]
        public string Parameter { get; set; }
    }

    public class DeveloperDetails
    {
        [Key]        
        public int DeveloperDetailId { get; set; }
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string OwnershipType { get; set; }
        public int DeveloperId   { get; set; }
        public int OwnershipId { get; set; }
    }


    public class DeveloperContactDetails
    {
        [Key]
        public int DeveloperContactDetailId { get; set; }
        public int ProjectID { get; set; }
        public int DeveloperContactPersonId { get; set; }
        public string FullName { get; set; }         

    }

    public class CoordinatorDetails
    {
        [Key]
        public int CoordinatorDetailId { get; set; }
        public int ProjectID { get; set; }
        public int CoordinatorId { get; set; }
        public string FullName { get; set; }

    }

    public class AssistantDetails
    {
        [Key]
        public int AssistantDetailId { get; set; }
        public int ProjectID { get; set; }
        public int AssistantId { get; set; }
        public string FullName { get; set; }
    }
    
    public class OfficePersonDetails
    {
        [Key]
        public int OfficeContactDetailId { get; set; }
        public int ProjectID { get; set; }
        public int OfficeContactPersonId { get; set; }
        public string FullName { get; set; }

    }


    public class SurvayDetails
    {
        
    [Key]
    public int SurvayDetailId { get; set; }
    public int ProjectID { get; set; }
    public int SurvayNo      { get; set; }
    public int HissaNo       { get; set; }
    public decimal Area          { get; set; }
    public int UnitID        { get; set; }
    public string Unit { get; set; }

    }

    public class GatDetails
    {
        [Key]
        public int GatDetailId { get; set; }
        public int ProjectID { get; set; }
        public int GatNo { get; set; }
        public int HissaNo { get; set; }
        public decimal Area { get; set; }
        public int UnitID { get; set; }
        public string Unit { get; set; }
    }

    public class CTSDetails
    {
        [Key]
        public int CTSDetailId { get; set; }
        public int ProjectID { get; set; }
        public int CTSNo { get; set; }
        public int HissaNo { get; set; }
        public decimal Area { get; set; }
        public int UnitID { get; set; }
        public string Unit { get; set; }

    }

    public class PlotDetails
    {
        [Key]
        public int PlotDetailId { get; set; }
        public int ProjectID { get; set; }
        public int PlotNo { get; set; }
        public decimal Area { get; set; }
        public int UnitID { get; set; }
        public string Unit { get; set; }

    }


    public class FinalPlotDetails
    {
        [Key]
        public int FinalPlotDetailId { get; set; }
        public int ProjectID { get; set; }
        public int FinalPlotNo { get; set; }
        public decimal Area { get; set; }
        public int UnitID { get; set; }
        public string Unit { get; set; }


    }

    public class ConsultantDetails
    {
        [Key]
        public int ConsultantDetailId { get; set; }
        public int ProjectID          { get; set; }
        public int ConsultantId       { get; set; }
        public int ConsultantTypeID   { get; set; }
        public string Name            { get; set; }
        public string ConsultantType { get; set; }

    }

    public class ContractorDetails
    {
       [Key]        
       public int ContractorDetailId { get; set; }
       public int ProjectID          { get; set; }
       public int ConstractorId      { get; set; }
       public int ContractorTypeId   { get; set; }
       public string Name               { get; set; }
       public string ContractorType { get; set; }

    }

    public class FieldDetails
    {
        [Key]

        public int FieldDetailId { get; set; }
        public int ProjectID     { get; set; }
        public string Field { get; set; }

    }
  
  
    public class ParameterDetails
    {
        [Key]

        public int ParameterDetailId { get; set; }
        public int ProjectID         { get; set; }
        public string Parameter { get; set; }

    }


}

















