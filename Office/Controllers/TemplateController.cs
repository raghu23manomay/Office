using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calibration.Models;
using office.Models;
using PagedList;

namespace office.Controllers
{
    public class TemplateController : Controller
    {

        public List<SelectListItem> binddropdown(string action, int val = 0, int StateID = 0)
        {
            OfficeDbContext _db = new OfficeDbContext();

            var res = _db.Database.SqlQuery<SelectListItem>("exec BindDropDown @action , @val, @StateID",
                    new SqlParameter("@action", action),
                    new SqlParameter("@val", val),
                    new SqlParameter("@StateID", StateID))
                   .ToList()
                   .AsEnumerable()
                   .Select(r => new SelectListItem
                   {
                       Text = r.Text.ToString(),
                       Value = r.Value.ToString(),
                       Selected = r.Value.Equals(Convert.ToString(val))
                   }).ToList();

            return res;
        }

        // GET: Template
        public ActionResult Compose(int id = 0)
        {

            OfficeDbContext _db = new OfficeDbContext();
            temlatesInfo data = new temlatesInfo();
            var result = _db.temlatesList.SqlQuery(@"exec GetTemplate 
                @TemplateID",
               new SqlParameter("@TemplateID", id)).ToList<temlatesInfo>();

            data = result.FirstOrDefault();

            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["AuthorityList"] = binddropdown("AuthorityList", 0);
            ViewData["DepartmentList"] = binddropdown("DepartmentList", 0); 
            ViewData["TemplateTypeList"] = binddropdown("TemplateTypeList", 0);
            return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("Compose", data)
                     : View("Compose", data);
        }
       
        public ActionResult GetPlaceholder()
        {
            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<PerformaPlaceholders> result = _db.PerformaPlaceholders.SqlQuery(@"exec getAdminPlaceholder").ToList<PerformaPlaceholders>();
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("Placeholder", result)
                    : View("Placeholder", result);
        }
 
        public ActionResult SaveTemplate(temlatesInfo t  )
        {
            OfficeDbContext _db = new OfficeDbContext();   
            
            var result = _db.Database.ExecuteSqlCommand(@"exec SaveTemplate 
               @TemplateID, @TemplateName,@Description,@CityID,@AuthorityID,@DepartmentID",
            new SqlParameter("@TemplateID", t.TemplateID),
            new SqlParameter("@TemplateName", t.TemplateName),
            new SqlParameter("@Description", t.Description),
           new SqlParameter("@CityID", t.CityID),
           new SqlParameter("@AuthorityID", t.AuthorityID),
            new SqlParameter("@DepartmentID", t.DepartmentID)

        );

            return Json("Success"); 
        }
   

    #region TemplateMaster
    public ActionResult LoadDocTemplateyGrid(int? page, String Name = null)
    {
        StaticPagedList<DocTemplateList> itemsAsIPagedList;
        itemsAsIPagedList = TemplateGridList(page, Name);

        return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("DocTemplateList", itemsAsIPagedList)
                : View("DocTemplateList", itemsAsIPagedList);
    }

    public ActionResult DocTemplateList(int? page, String Name = null)
    {
        StaticPagedList<DocTemplateList> itemsAsIPagedList;
        itemsAsIPagedList = TemplateGridList(page, Name);

        return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("DocTemplateList", itemsAsIPagedList)
                : View("DocTemplateList", itemsAsIPagedList);
    }
    public StaticPagedList<DocTemplateList> TemplateGridList(int? page, String Name = "")
    {
        OfficeDbContext _db = new OfficeDbContext();
        var pageIndex = (page ?? 1);
        const int pageSize = 10;
        int totalCount = 10;
            DocTemplateList clist = new  DocTemplateList();

        IEnumerable<DocTemplateList> result = _db.DocTemplateLists.SqlQuery(@"exec getDocumentTemplate
                   @pPageIndex, @pPageSize,@Name",
           new SqlParameter("@pPageIndex", pageIndex),
           new SqlParameter("@pPageSize", pageSize),
           new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
           ).ToList<DocTemplateList>();

        totalCount = 0;
        if (result.Count() > 0)
        {
            totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
        }
        var itemsAsIPagedList = new StaticPagedList<DocTemplateList>(result, pageIndex, pageSize, totalCount);
        return itemsAsIPagedList;

    }
   #endregion

        public ActionResult GenerateDocument(int? page, String Name = "")
        {
            DocTemplateList data = new DocTemplateList();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
             

            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<DocTemplateList> result = _db.DocTemplateLists.SqlQuery(@"exec getDocumentTemplate
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<DocTemplateList>();
            return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("GenerateDocument", result)
              : View("GenerateDocument", result);
        }
        public ActionResult DocCreation(int? page, String Name = "")
        {
            DocCreationFilters data = new DocCreationFilters();
            DocTemplateList result1 = new DocTemplateList();
            
            var pageIndex = (page ?? 1);
            const int pageSize = 10;


            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<DocTemplateList> result = _db.DocTemplateLists.SqlQuery(@"exec getDocumentTemplate
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<DocTemplateList>();
            return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("DocCreation", result)
              : View("DocCreation", result);
        }

        #region Document Data Template
        public ActionResult GetProjectData(int? id)
        {
            OfficeDbContext _db = new OfficeDbContext();
            var result = _db.ProjectsData.SqlQuery(@"exec GetProjectDetailsForTemplate
               @ProjectId",
               new SqlParameter("@ProjectId", id)
               ).ToList<ProjectsData>();
            ProjectsData data = new ProjectsData();
            data = result.FirstOrDefault();

            IEnumerable<DeveloperData> result2 = _db.DeveloperData.SqlQuery(@"exec GetProjectDetailsForTemplate
                 @ProjectId,@Tno",
                new SqlParameter("@ProjectId", id),
                 new SqlParameter("@Tno", 2)
                  ).ToList<DeveloperData>(); 

            data.DeveloperDatalist = result2;
            try
            {
                IEnumerable<CoordinatorDetailsData> result3 = _db.CoordinatorDetailsData.SqlQuery(@"exec GetProjectDetailsForTemplate
                 @ProjectId,@Tno",
                new SqlParameter("@ProjectId", id),
                 new SqlParameter("@Tno", 3)
                  ).ToList<CoordinatorDetailsData>();


            data.CoordinatorDetailsData = result3;
           

                IEnumerable<AssistantDetailsData> result4 = _db.AssistantDetailsData.SqlQuery(@"exec GetProjectDetailsForTemplate
                 @ProjectId,@Tno",
                    new SqlParameter("@ProjectId", id),
                     new SqlParameter("@Tno", 4)
                      ).ToList<AssistantDetailsData>();
                data.AssistantDetailsData = result4;
            }
            catch(Exception w) { }
            return View("ProjectData", data);
        }
        public ActionResult ReplacePlaceholderByDataTemplate(int TemplateID=1,int DtTemplateID=1)
        {
            ProjectsDataWithValue data = new ProjectsDataWithValue();
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.ProjectsDataWithValue.SqlQuery(@"exec usp_ReplacePlaceholder
               @TemplateID,@dtTemplateID",
                   new SqlParameter("@TemplateID", TemplateID),
                   new SqlParameter("@DtTemplateID", DtTemplateID)
                   ).ToList<ProjectsDataWithValue>();
                
                data = result.FirstOrDefault();
            }
            catch(Exception e) {   }
            return View("ReplacePlaceholderByDataTemplate", data);
        }
        
        [HttpPost]
        //public ActionResult GenerateDocument(SaveProject sp, List<SaveDeveloper> SaveDeveloper, List<SaveDeveloperContact> SaveDeveloperContact, List<SaveCoordinator> SaveCoordinator, List<SaveAssistant> SaveAssistant, List<SaveOfficeContact> SaveOfficeContact, List<SaveSurvayDetails> SaveSurvayDetails, List<SaveGatDetails> SaveGatDetails, List<SaveCTSDetails> SaveCTSDetails, List<SavePlotDetails> SavePlotDetails, List<SaveFinalPlotDetails> SaveFinalPlotDetails, List<SaveConsultant> SaveConsultant, List<SaveContractor> SaveContractor, List<SaveField> SaveField, List<SaveProjectParameter> SaveProjectParameter)
              public ActionResult GenerateDataTemplate(ProjectsData sp )
        {
            try
            {
                #region commented code
                //DataTable dtDeveloper = new DataTable();
                //DataTable dtDeveloperContact = new DataTable();
                //DataTable dtCoordinator = new DataTable();
                //DataTable dtAssistant = new DataTable();
                //DataTable dtOfficeContact = new DataTable();
                //DataTable dtSurvayDetails = new DataTable();
                //DataTable dtGatDetails = new DataTable();
                //DataTable dtCTSDetails = new DataTable();
                //DataTable dtPlotDetails = new DataTable();
                //DataTable dtFinalPlotDetails = new DataTable();
                //DataTable dtConsultant = new DataTable();
                //DataTable dtContractor = new DataTable();
                //DataTable dtField = new DataTable();
                //DataTable dtProjectParameter = new DataTable();

                //dtDeveloper.Columns.Add("DeveloperId", typeof(int));
                //dtDeveloper.Columns.Add("OwnershipId", typeof(int));

                //dtDeveloperContact.Columns.Add("DeveloperContactPersonId", typeof(int));

                //dtCoordinator.Columns.Add("CoordinatorId", typeof(int));
                //dtAssistant.Columns.Add("AssistantId", typeof(int));

                //dtOfficeContact.Columns.Add("OfficeContactPersonId", typeof(int));

                //dtSurvayDetails.Columns.Add("SurvayNo", typeof(int));
                //dtSurvayDetails.Columns.Add("HissaNo", typeof(int));
                //dtSurvayDetails.Columns.Add("Area", typeof(decimal));
                //dtSurvayDetails.Columns.Add("UnitID", typeof(int));

                //dtGatDetails.Columns.Add("GatNo", typeof(int));
                //dtGatDetails.Columns.Add("HissaNo", typeof(int));
                //dtGatDetails.Columns.Add("Area", typeof(decimal));
                //dtGatDetails.Columns.Add("UnitID", typeof(int));

                //dtCTSDetails.Columns.Add("CTSNo", typeof(int));
                //dtCTSDetails.Columns.Add("HissaNo", typeof(int));
                //dtCTSDetails.Columns.Add("Area", typeof(decimal));
                //dtCTSDetails.Columns.Add("UnitID", typeof(int));

                //dtPlotDetails.Columns.Add("PlotNo", typeof(int));
                //dtPlotDetails.Columns.Add("Area", typeof(decimal));
                //dtPlotDetails.Columns.Add("UnitID", typeof(int));

                //dtFinalPlotDetails.Columns.Add("FinalPlotNo", typeof(int));
                //dtFinalPlotDetails.Columns.Add("Area", typeof(decimal));
                //dtFinalPlotDetails.Columns.Add("UnitID", typeof(int));

                //dtConsultant.Columns.Add("ConsultantId", typeof(int));
                //dtConsultant.Columns.Add("ConsultantTypeId", typeof(int));

                //dtContractor.Columns.Add("ConstractorId", typeof(int));
                //dtContractor.Columns.Add("ContractorTypeId", typeof(int));

                //dtField.Columns.Add("Field", typeof(string));

                //dtProjectParameter.Columns.Add("Parameter", typeof(string));

                //// Adding Developers In DT
                //if (SaveDeveloper != null)
                //{
                //    if (SaveDeveloper.Count > 0)
                //    {
                //        foreach (var item in SaveDeveloper)
                //        {
                //            DataRow dr_Developer = dtDeveloper.NewRow();
                //            dr_Developer["DeveloperId"] = item.DeveloperId;
                //            dr_Developer["OwnershipId"] = item.OwnershipId;
                //            dtDeveloper.Rows.Add(dr_Developer);
                //        }
                //    }
                //}


                //// Adding Developers In DT
                //if (SaveDeveloperContact != null)
                //{
                //    if (SaveDeveloperContact.Count > 0)
                //    {
                //        foreach (var item in SaveDeveloperContact)
                //        {
                //            DataRow dr_DeveloperContactDeveloper = dtDeveloperContact.NewRow();
                //            dr_DeveloperContactDeveloper["DeveloperContactPersonId"] = item.DeveloperContactPersonId;
                //            dtDeveloperContact.Rows.Add(dr_DeveloperContactDeveloper);
                //        }
                //    }
                //}



                //// Adding Coordinator In DT
                //if (SaveCoordinator != null)
                //{
                //    if (SaveCoordinator.Count > 0)
                //    {
                //        foreach (var item in SaveCoordinator)
                //        {
                //            DataRow dr_Coordinator = dtCoordinator.NewRow();
                //            dr_Coordinator["CoordinatorId"] = item.CoordinatorId;
                //            dtCoordinator.Rows.Add(dr_Coordinator);
                //        }
                //    }
                //}

                //// Adding Assistant In DT
                //if (SaveAssistant != null)
                //{
                //    if (SaveAssistant.Count > 0)
                //    {
                //        foreach (var item in SaveAssistant)
                //        {
                //            DataRow dr_Assistant = dtAssistant.NewRow();
                //            dr_Assistant["AssistantId"] = item.AssistantId;
                //            dtAssistant.Rows.Add(dr_Assistant);
                //        }
                //    }
                //}

                //// Adding Office Contact In DT
                //if (SaveOfficeContact != null)
                //{
                //    if (SaveOfficeContact.Count > 0)
                //    {
                //        foreach (var item in SaveOfficeContact)
                //        {
                //            DataRow dr_OfficeContactAssistant = dtOfficeContact.NewRow();
                //            dr_OfficeContactAssistant["OfficeContactPersonId"] = item.OfficeContactPersonId;
                //            dtOfficeContact.Rows.Add(dr_OfficeContactAssistant);
                //        }
                //    }
                //}

                //// Adding survay Details In DT
                //if (SaveSurvayDetails != null)
                //{
                //    if (SaveSurvayDetails.Count > 0)
                //    {
                //        foreach (var item in SaveSurvayDetails)
                //        {
                //            DataRow dr_SurvayDetails = dtSurvayDetails.NewRow();
                //            dr_SurvayDetails["SurvayNo"] = item.SurvayNo;
                //            dr_SurvayDetails["HissaNo"] = item.HissaNo;
                //            dr_SurvayDetails["Area"] = item.Area;
                //            dr_SurvayDetails["UnitID"] = item.UnitID;
                //            dtSurvayDetails.Rows.Add(dr_SurvayDetails);
                //        }
                //    }
                //}

                //// Adding Gat Details In DT
                //if (SaveGatDetails != null)
                //{
                //    if (SaveGatDetails.Count > 0)
                //    {
                //        foreach (var item in SaveGatDetails)
                //        {
                //            DataRow dr_GatDetails = dtGatDetails.NewRow();
                //            dr_GatDetails["GatNo"] = item.GatNo;
                //            dr_GatDetails["HissaNo"] = item.HissaNo;
                //            dr_GatDetails["Area"] = item.Area;
                //            dr_GatDetails["UnitID"] = item.UnitID;
                //            dtGatDetails.Rows.Add(dr_GatDetails);
                //        }
                //    }
                //}

                //// Adding CTS Details In DT
                //if (SaveCTSDetails != null)
                //{
                //    if (SaveCTSDetails.Count > 0)
                //    {
                //        foreach (var item in SaveCTSDetails)
                //        {
                //            DataRow dr_CTSDetails = dtCTSDetails.NewRow();
                //            dr_CTSDetails["CTSNo"] = item.CTSNo;
                //            dr_CTSDetails["HissaNo"] = item.HissaNo;
                //            dr_CTSDetails["Area"] = item.Area;
                //            dr_CTSDetails["UnitID"] = item.UnitID;
                //            dtCTSDetails.Rows.Add(dr_CTSDetails);
                //        }
                //    }
                //}

                //// Adding Plot Details In DT
                //if (SavePlotDetails != null)
                //{
                //    if (SavePlotDetails.Count > 0)
                //    {
                //        foreach (var item in SavePlotDetails)
                //        {
                //            DataRow dr_PlotDetails = dtPlotDetails.NewRow();
                //            dr_PlotDetails["PlotNo"] = item.PlotNo;
                //            dr_PlotDetails["Area"] = item.Area;
                //            dr_PlotDetails["UnitID"] = item.UnitID;
                //            dtPlotDetails.Rows.Add(dr_PlotDetails);
                //        }
                //    }
                //}


                //// Adding Final Plot Details In DT
                //if (SaveFinalPlotDetails != null)
                //{
                //    if (SaveFinalPlotDetails.Count > 0)
                //    {
                //        foreach (var item in SaveFinalPlotDetails)
                //        {
                //            DataRow dr_FinalPlotDetails = dtFinalPlotDetails.NewRow();
                //            dr_FinalPlotDetails["FinalPlotNo"] = item.FinalPlotNo;
                //            dr_FinalPlotDetails["Area"] = item.Area;
                //            dr_FinalPlotDetails["UnitID"] = item.UnitID;
                //            dtFinalPlotDetails.Rows.Add(dr_FinalPlotDetails);
                //        }
                //    }
                //}

                //// Adding Consultant Details In DT
                //if (SaveConsultant != null)
                //{
                //    if (SaveConsultant.Count > 0)
                //    {
                //        foreach (var item in SaveConsultant)
                //        {
                //            DataRow dr_Consultants = dtConsultant.NewRow();
                //            dr_Consultants["ConsultantId"] = item.ConsultantId;
                //            dr_Consultants["ConsultantTypeId"] = item.ConsultantTypeId;
                //            dtConsultant.Rows.Add(dr_Consultants);
                //        }
                //    }
                //}

                //// Adding Contractor Details In DT
                //if (SaveContractor != null)
                //{
                //    if (SaveContractor.Count > 0)
                //    {
                //        foreach (var item in SaveContractor)
                //        {
                //            DataRow dr_Contractor = dtContractor.NewRow();
                //            dr_Contractor["ConstractorId"] = item.ConstractorId;
                //            dr_Contractor["ContractorTypeId"] = item.ContractorTypeId;
                //            dtContractor.Rows.Add(dr_Contractor);
                //        }
                //    }
                //}

                //// Adding Field Details In DT
                //if (SaveField != null)
                //{
                //    if (SaveField.Count > 0)
                //    {
                //        foreach (var item in SaveField)
                //        {
                //            DataRow dr_Field = dtField.NewRow();
                //            dr_Field["Field"] = item.Field;
                //            dtField.Rows.Add(dr_Field);
                //        }
                //    }
                //}

                //// Adding Parameter Details In DT
                //if (SaveProjectParameter != null)
                //{
                //    if (SaveProjectParameter.Count > 0)
                //    {
                //        foreach (var item in SaveProjectParameter)
                //        {
                //            DataRow dr_ProjectParameter = dtProjectParameter.NewRow();
                //            dr_ProjectParameter["Parameter"] = item.Parameter;
                //            dtProjectParameter.Rows.Add(dr_ProjectParameter);
                //        }
                //    }
                //}


                //SqlParameter tvpParamDeveloper = new SqlParameter();
                //tvpParamDeveloper.ParameterName = "@ProjectDeveloper";
                //tvpParamDeveloper.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamDeveloper.Value = dtDeveloper;
                //tvpParamDeveloper.TypeName = "UT_ProjectDeveloper";

                //SqlParameter tvpParamDeveloperContact = new SqlParameter();
                //tvpParamDeveloperContact.ParameterName = "@ProjectDeveloperContact";
                //tvpParamDeveloperContact.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamDeveloperContact.Value = dtDeveloperContact;
                //tvpParamDeveloperContact.TypeName = "UT_ProjectDeveloperContact";

                //SqlParameter tvpParamCoordinator = new SqlParameter();
                //tvpParamCoordinator.ParameterName = "@ProjectCoordinator";
                //tvpParamCoordinator.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamCoordinator.Value = dtCoordinator;
                //tvpParamCoordinator.TypeName = "UT_ProjectCoordinator";

                //SqlParameter tvpParamAssistant = new SqlParameter();
                //tvpParamAssistant.ParameterName = "@ProjectAssistant";
                //tvpParamAssistant.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamAssistant.Value = dtAssistant;
                //tvpParamAssistant.TypeName = "UT_ProjectAssistant";

                //SqlParameter tvpParamOfficeContact = new SqlParameter();
                //tvpParamOfficeContact.ParameterName = "@ProjectOfficeContact";
                //tvpParamOfficeContact.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamOfficeContact.Value = dtOfficeContact;
                //tvpParamOfficeContact.TypeName = "UT_ProjectOfficeContact";

                //SqlParameter tvpParamSurvayDetails = new SqlParameter();
                //tvpParamSurvayDetails.ParameterName = "@ProjectSurvay";
                //tvpParamSurvayDetails.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamSurvayDetails.Value = dtSurvayDetails;
                //tvpParamSurvayDetails.TypeName = "UT_ProjectSurvay";

                //SqlParameter tvpParamGatDetails = new SqlParameter();
                //tvpParamGatDetails.ParameterName = "@ProjectGat";
                //tvpParamGatDetails.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamGatDetails.Value = dtGatDetails;
                //tvpParamGatDetails.TypeName = "UT_ProjectGat";

                //SqlParameter tvpParamCTSDetails = new SqlParameter();
                //tvpParamCTSDetails.ParameterName = "@ProjectCTS";
                //tvpParamCTSDetails.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamCTSDetails.Value = dtCTSDetails;
                //tvpParamCTSDetails.TypeName = "UT_ProjectCTS";

                //SqlParameter tvpParamPlotDetails = new SqlParameter();
                //tvpParamPlotDetails.ParameterName = "@ProjectPlot";
                //tvpParamPlotDetails.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamPlotDetails.Value = dtPlotDetails;
                //tvpParamPlotDetails.TypeName = "UT_ProjectPlot";

                //SqlParameter tvpParamFinalPlotDetails = new SqlParameter();
                //tvpParamFinalPlotDetails.ParameterName = "@ProjectFinalPlot";
                //tvpParamFinalPlotDetails.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamFinalPlotDetails.Value = dtFinalPlotDetails;
                //tvpParamFinalPlotDetails.TypeName = "UT_ProjectFinalPlot";

                //SqlParameter tvpParamConsultant = new SqlParameter();
                //tvpParamConsultant.ParameterName = "@ProjectConsultant";
                //tvpParamConsultant.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamConsultant.Value = dtConsultant;
                //tvpParamConsultant.TypeName = "UT_ProjectConsultant";

                //SqlParameter tvpParamContractor = new SqlParameter();
                //tvpParamContractor.ParameterName = "@ProjectContractor";
                //tvpParamContractor.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamContractor.Value = dtContractor;
                //tvpParamContractor.TypeName = "UT_ProjectContractor";

                //SqlParameter tvpParamField = new SqlParameter();
                //tvpParamField.ParameterName = "@ProjectField";
                //tvpParamField.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamField.Value = dtField;
                //tvpParamField.TypeName = "UT_ProjectField";

                //SqlParameter tvpParamProjectParameter = new SqlParameter();
                //tvpParamProjectParameter.ParameterName = "@ProjectParameter";
                //tvpParamProjectParameter.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamProjectParameter.Value = dtProjectParameter;
                //tvpParamProjectParameter.TypeName = "UT_ProjectParameter";

                #endregion
                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (sp.IsActive == false)
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_GenerateDataTemplate
               @ProjectID,@Name,@EnquiryDate,@ShortName,@StatusId,@ProjectTypeId,@CustomerFileNo,@PhysicalPath,@Road,@Goan,@Taluka,@District,@Duration,@Cost,@StartDate,@EndDate,@IsActive,@CreatedBy
                ",
                new SqlParameter("@ProjectID", sp.ProjectID),
                new SqlParameter("@Name", sp.Name),
                new SqlParameter("@EnquiryDate",DateTime.Now),
                new SqlParameter("@ShortName", sp.ShortName),
                new SqlParameter("@StatusId", sp.StatusId),
                new SqlParameter("@ProjectTypeId", sp.ProjectTypeId),
                new SqlParameter("@CustomerFileNo", sp.CustomerFileNo),
                new SqlParameter("@PhysicalPath", sp.PhysicalPath),
                new SqlParameter("@Road", sp.Road),
                new SqlParameter("@Goan", sp.Goan),
                new SqlParameter("@Taluka", sp.Taluka),
                new SqlParameter("@District", sp.District),
                new SqlParameter("@Duration", sp.Duration),
                new SqlParameter("@Cost", sp.Cost),
                new SqlParameter("@StartDate", DateTime.Now),
                new SqlParameter("@EndDate", DateTime.Now),
                new SqlParameter("@IsActive", Active),
                new SqlParameter("@CreatedBy", 1)
             
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }
        #endregion

        public ActionResult ComposeHtml(int id = 1)
        {
            OfficeDbContext _db = new OfficeDbContext();
            temlatesInfo data = new temlatesInfo();
            var result = _db.temlatesList.SqlQuery(@"exec usp_ReplacePlaceholder 
                @TemplateID",
               new SqlParameter("@TemplateID", id)).ToList<temlatesInfo>();
            data = result.FirstOrDefault();

            return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("ComposeHtml", data)
                     : View("ComposeHtml", data);
        }
    }
}