
using office.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.Controllers
{
    public class HomeController : Controller
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

        #region Project


        public ActionResult ProjectList(int? page, String Name = null)
        {
            StaticPagedList<SaveProject> itemsAsIPagedList;
            itemsAsIPagedList = ProjectGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ProjectList", itemsAsIPagedList)
                    : View("ProjectList", itemsAsIPagedList);
        }

        public ActionResult LoadProjectList(int? page, String Name = null)
        {
            StaticPagedList<SaveProject> itemsAsIPagedList;
            itemsAsIPagedList = ProjectGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ProjectGrid", itemsAsIPagedList)
                    : View("ProjectGrid", itemsAsIPagedList);
        }
        public StaticPagedList<SaveProject> ProjectGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            SaveProject clist = new SaveProject();

            IEnumerable<SaveProject> result = _db.DFSaveProject.SqlQuery(@"exec GetProject
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<SaveProject>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<SaveProject>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        public ActionResult AddProject()
        {
            ViewData["ConactPersonList"] = binddropdown("ConactPersonList", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);
            ViewData["OwnershipTypeList"] = binddropdown("OwnershipTypeList", 0);
            ViewData["DeveloperList"] = binddropdown("DeveloperList", 0);
            ViewData["CompanyNameList"] = binddropdown("CompanyNameList", 0); 
            ViewData["ConsultantList"] = binddropdown("ConsultantList", 0);
            ViewData["ContracterList"] = binddropdown("ContracterList", 0);
            ViewData["ProjectStatusList"] = binddropdown("ProjectStatusList", 0);
            ViewData["ProjectTypeList"] = binddropdown("ProjectTypeList", 0);
            ViewData["UnitList"] = binddropdown("UnitList", 0);
            return View();
        }

        public ActionResult EditProject(int? id)
        {
            OfficeDbContext _db = new OfficeDbContext();

            var result = _db.DFUpdateProject.SqlQuery(@"exec GetProjectDetailsForUpdate
               @ProjectId",
               new SqlParameter("@ProjectId", id)
               ).ToList<UpdateProject>();
            UpdateProject data = new UpdateProject();
            data = result.FirstOrDefault();
            ViewData["ConactPersonList"] = binddropdown("ConactPersonList", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);
            ViewData["OwnershipTypeList"] = binddropdown("OwnershipTypeList", 0);
            ViewData["DeveloperList"] = binddropdown("DeveloperList", 0);
            ViewData["CompanyNameList"] = binddropdown("CompanyNameList", 0);
            ViewData["ConsultantList"] = binddropdown("ConsultantList", 0);
            ViewData["ContracterList"] = binddropdown("ContracterList", 0);
            ViewData["ProjectStatusList"] = binddropdown("ProjectStatusList", 0);
            ViewData["ProjectTypeList"] = binddropdown("ProjectTypeList", 0);
            ViewData["UnitList"] = binddropdown("UnitList", 0);
            string startDate = data.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string endDate = data.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            return View("EditProject", data);
        }



        [HttpPost]
        public ActionResult DeleteProjectInformation(int? Id, string Label = "")
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec DeleteProjectMultipalInformation 
                 @Label,@Id",
                new SqlParameter("@Label", Label),
                new SqlParameter("@Id", Id));
                return Json("Delete Sucessfull");
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }


        // Getting Developer Details For Update
        public ActionResult GetDeveloperDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();
            try
            {
                IEnumerable<DeveloperDetails> result = _db.DFDeveloperDetails.SqlQuery(@"exec GetProjectDeveloperDetails
               @ProjectId",
                   new SqlParameter("@ProjectId", ProjectID)
                   ).ToList<DeveloperDetails>();
                return View("_DeveloperDetailsForProject", result);
            }
            catch (Exception ex)
            {
                return View("_DeveloperDetailsForProject");
            }
            

        }

        // Getting Developer conact Details For Update
        public ActionResult GetDeveloperContactDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<DeveloperContactDetails> result = _db.DFDeveloperContactDetails.SqlQuery(@"exec GetDeveloperContactDetailsForProject
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<DeveloperContactDetails>();
            return View("_DeveloperContactDetailsForProject", result);

        }

        // Getting Coordinator Details For Update
        public ActionResult GetCoordinatorDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<CoordinatorDetails> result = _db.DFCoordinatorDetails.SqlQuery(@"exec GetProjectCoordinatorDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<CoordinatorDetails>();
            return View("_CoordinatorDetailsForProject", result);

        }

        // Getting Assistant Details For Update
        public ActionResult GetAssistantDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<AssistantDetails> result = _db.DFAssistantDetails.SqlQuery(@"exec GetProjectAssistantDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<AssistantDetails>();
            return View("_AssistantDetailsForProject", result);

        }

        // Getting Office Contact Details For Update
        public ActionResult GetOfficeContactDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<OfficePersonDetails> result = _db.DFOfficePersonDetails.SqlQuery(@"exec GetProjectOfficeContactDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<OfficePersonDetails>();
            return View("_OfficeContactDetailsForProject", result);

        }

        // Getting Survay details For Update
        public ActionResult GetSurvayDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<SurvayDetails> result = _db.DFSurvayDetails.SqlQuery(@"exec GetProjectSurvayDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<SurvayDetails>();
            return View("_SurvayDetailsForProject", result);

        }

        // Getting Gat details For Update
        public ActionResult GetGatDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<GatDetails> result = _db.DFGatDetails.SqlQuery(@"exec GetProjectGatDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<GatDetails>();
            return View("_GatDetailsForProject", result);

        }

        // Getting CTS details For Update
        public ActionResult GetCTSDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<CTSDetails> result = _db.DFCTSDetails.SqlQuery(@"exec GetProjectCTSDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<CTSDetails>();
            return View("_CTSDetailsForProject", result);

        }

        // Getting Plot details For Update
        public ActionResult GetPlotDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<PlotDetails> result = _db.DFPlotDetails.SqlQuery(@"exec GetProjectPlotDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<PlotDetails>();
            return View("_PlotDetailsForProject", result);

        }

        // Getting Final plot details For Update
        public ActionResult GetFinalPlotDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<FinalPlotDetails> result = _db.DFFinalPlotDetails.SqlQuery(@"exec GetProjectFinalPlotDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<FinalPlotDetails>();
            return View("_FinalPlotDetailsForProject", result);

        }

        // Getting Get Consultant details For Update
        public ActionResult GetConsultantDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<ConsultantDetails> result = _db.DFConsultantDetails.SqlQuery(@"exec GetProjectConsultantDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<ConsultantDetails>();
            return View("_ConsultantDetailsForProject", result);

        }

        // Getting Get Contractor details For Update
        public ActionResult GetContractorDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<ContractorDetails> result = _db.DFContractorDetails.SqlQuery(@"exec GetProjectContractorDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<ContractorDetails>();
            return View("_ContractorDetailsForProject", result);

        }

        // Getting Get Field details For Update
        public ActionResult GetFieldDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<FieldDetails> result = _db.DFFieldDetails.SqlQuery(@"exec GetProjectFieldDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)
               ).ToList<FieldDetails>();
            return View("_FieldDetailsForProject", result);

        }


        // Getting Get Parameter details For Update
        public ActionResult GetParameterDetailsForProject(int? ProjectID)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<ParameterDetails> result = _db.DFParameterDetails.SqlQuery(@"exec GetProjectParameterDetails
               @ProjectId",
               new SqlParameter("@ProjectId", ProjectID)).ToList<ParameterDetails>();
            return View("_ParameterDetailsForProject", result);

        }

        //[HttpPost]
        //public ActionResult SaveProject(SaveProject sp, List<SaveDeveloper> SaveDeveloper, List<SaveDeveloperContact> SaveDeveloperContact, List<SaveCoordinator> SaveCoordinator, List<SaveAssistant> SaveAssistant, List<SaveOfficeContact> SaveOfficeContact, List<SaveSurvayDetails> SaveSurvayDetails, List<SaveGatDetails> SaveGatDetails, List<SaveCTSDetails> SaveCTSDetails, List<SavePlotDetails> SavePlotDetails, List<SaveFinalPlotDetails> SaveFinalPlotDetails, List<SaveConsultant> SaveConsultant, List<SaveContractor> SaveContractor, List<SaveField> SaveField, List<SaveProjectParameter> SaveProjectParameter)
        //{
        //    try
        //    {
        //        DataTable dtDeveloper = new DataTable();
        //        DataTable dtDeveloperContact = new DataTable();
        //        DataTable dtCoordinator = new DataTable();
        //        DataTable dtAssistant = new DataTable();
        //        DataTable dtOfficeContact = new DataTable();
        //        DataTable dtSurvayDetails = new DataTable();
        //        DataTable dtGatDetails = new DataTable();
        //        DataTable dtCTSDetails = new DataTable();
        //        DataTable dtPlotDetails = new DataTable();
        //        DataTable dtFinalPlotDetails = new DataTable();
        //        DataTable dtConsultant = new DataTable();
        //        DataTable dtContractor = new DataTable();
        //        DataTable dtField = new DataTable();
        //        DataTable dtProjectParameter = new DataTable();

        //        dtDeveloper.Columns.Add("DeveloperId", typeof(int));
        //        dtDeveloper.Columns.Add("OwnershipId", typeof(int));

        //        dtDeveloperContact.Columns.Add("DeveloperContactPersonId", typeof(int));

        //        dtCoordinator.Columns.Add("CoordinatorId", typeof(int));
        //        dtAssistant.Columns.Add("AssistantId", typeof(int));

        //        dtOfficeContact.Columns.Add("OfficeContactPersonId", typeof(int));

        //        dtSurvayDetails.Columns.Add("SurvayNo", typeof(int));
        //        dtSurvayDetails.Columns.Add("HissaNo", typeof(int));
        //        dtSurvayDetails.Columns.Add("Area", typeof(decimal));
        //        dtSurvayDetails.Columns.Add("UnitID", typeof(int));

        //        dtGatDetails.Columns.Add("GatNo", typeof(int));
        //        dtGatDetails.Columns.Add("HissaNo", typeof(int));
        //        dtGatDetails.Columns.Add("Area", typeof(decimal));
        //        dtGatDetails.Columns.Add("UnitID", typeof(int));

        //        dtCTSDetails.Columns.Add("CTSNo", typeof(int));
        //        dtCTSDetails.Columns.Add("HissaNo", typeof(int));
        //        dtCTSDetails.Columns.Add("Area", typeof(decimal));
        //        dtCTSDetails.Columns.Add("UnitID", typeof(int));

        //        dtPlotDetails.Columns.Add("PlotNo", typeof(int));
        //        dtPlotDetails.Columns.Add("Area", typeof(decimal));
        //        dtPlotDetails.Columns.Add("UnitID", typeof(int));

        //        dtFinalPlotDetails.Columns.Add("FinalPlotNo", typeof(int));
        //        dtFinalPlotDetails.Columns.Add("Area", typeof(decimal));
        //        dtFinalPlotDetails.Columns.Add("UnitID", typeof(int));

        //        dtConsultant.Columns.Add("ConsultantId", typeof(int));
        //        dtConsultant.Columns.Add("ConsultantTypeId", typeof(int));

        //        dtContractor.Columns.Add("ConstractorId", typeof(int));
        //        dtContractor.Columns.Add("ContractorTypeId", typeof(int));

        //        dtField.Columns.Add("Field", typeof(string));

        //        dtProjectParameter.Columns.Add("Parameter", typeof(string));

        //        // Adding Developers In DT
        //        if (SaveDeveloper != null)
        //        {
        //            if (SaveDeveloper.Count > 0)
        //            {
        //                foreach (var item in SaveDeveloper)
        //                {
        //                    DataRow dr_Developer = dtDeveloper.NewRow();
        //                    dr_Developer["DeveloperId"] = item.DeveloperId;
        //                    dr_Developer["OwnershipId"] = item.OwnershipId;
        //                    dtDeveloper.Rows.Add(dr_Developer);
        //                }
        //            }
        //        }


        //        // Adding Developers In DT
        //        if (SaveDeveloperContact != null)
        //        {
        //            if (SaveDeveloperContact.Count > 0)
        //            {
        //                foreach (var item in SaveDeveloperContact)
        //                {
        //                    DataRow dr_DeveloperContactDeveloper = dtDeveloperContact.NewRow();
        //                    dr_DeveloperContactDeveloper["DeveloperContactPersonId"] = item.DeveloperContactPersonId;
        //                    dtDeveloperContact.Rows.Add(dr_DeveloperContactDeveloper);
        //                }
        //            }
        //        }



        //        // Adding Coordinator In DT
        //        if (SaveCoordinator != null)
        //        {
        //            if (SaveCoordinator.Count > 0)
        //            {
        //                foreach (var item in SaveCoordinator)
        //                {
        //                    DataRow dr_Coordinator = dtCoordinator.NewRow();
        //                    dr_Coordinator["CoordinatorId"] = item.CoordinatorId;
        //                    dtCoordinator.Rows.Add(dr_Coordinator);
        //                }
        //            }
        //        }

        //        // Adding Assistant In DT
        //        if (SaveAssistant != null)
        //        {
        //            if (SaveAssistant.Count > 0)
        //            {
        //                foreach (var item in SaveAssistant)
        //                {
        //                    DataRow dr_Assistant = dtAssistant.NewRow();
        //                    dr_Assistant["AssistantId"] = item.AssistantId;
        //                    dtAssistant.Rows.Add(dr_Assistant);
        //                }
        //            }
        //        }

        //        // Adding Office Contact In DT
        //        if (SaveOfficeContact != null)
        //        {
        //            if (SaveOfficeContact.Count > 0)
        //            {
        //                foreach (var item in SaveOfficeContact)
        //                {
        //                    DataRow dr_OfficeContactAssistant = dtOfficeContact.NewRow();
        //                    dr_OfficeContactAssistant["OfficeContactPersonId"] = item.OfficeContactPersonId;
        //                    dtOfficeContact.Rows.Add(dr_OfficeContactAssistant);
        //                }
        //            }
        //        }

        //        // Adding survay Details In DT
        //        if (SaveSurvayDetails != null)
        //        {
        //            if (SaveSurvayDetails.Count > 0)
        //            {
        //                foreach (var item in SaveSurvayDetails)
        //                {
        //                    DataRow dr_SurvayDetails = dtSurvayDetails.NewRow();
        //                    dr_SurvayDetails["SurvayNo"] = item.SurvayNo;
        //                    dr_SurvayDetails["HissaNo"] = item.HissaNo;
        //                    dr_SurvayDetails["Area"] = item.Area;
        //                    dr_SurvayDetails["UnitID"] = item.UnitID;
        //                    dtSurvayDetails.Rows.Add(dr_SurvayDetails);
        //                }
        //            }
        //        }

        //        // Adding Gat Details In DT
        //        if (SaveGatDetails != null)
        //        {
        //            if (SaveGatDetails.Count > 0)
        //            {
        //                foreach (var item in SaveGatDetails)
        //                {
        //                    DataRow dr_GatDetails = dtGatDetails.NewRow();
        //                    dr_GatDetails["GatNo"] = item.GatNo;
        //                    dr_GatDetails["HissaNo"] = item.HissaNo;
        //                    dr_GatDetails["Area"] = item.Area;
        //                    dr_GatDetails["UnitID"] = item.UnitID;
        //                    dtGatDetails.Rows.Add(dr_GatDetails);
        //                }
        //            }
        //        }

        //        // Adding CTS Details In DT
        //        if (SaveCTSDetails != null)
        //        {
        //            if (SaveCTSDetails.Count > 0)
        //            {
        //                foreach (var item in SaveCTSDetails)
        //                {
        //                    DataRow dr_CTSDetails = dtCTSDetails.NewRow();
        //                    dr_CTSDetails["CTSNo"] = item.CTSNo;
        //                    dr_CTSDetails["HissaNo"] = item.HissaNo;
        //                    dr_CTSDetails["Area"] = item.Area;
        //                    dr_CTSDetails["UnitID"] = item.UnitID;
        //                    dtCTSDetails.Rows.Add(dr_CTSDetails);
        //                }
        //            }
        //        }

        //        // Adding Plot Details In DT
        //        if (SavePlotDetails != null)
        //        {
        //            if (SavePlotDetails.Count > 0)
        //            {
        //                foreach (var item in SavePlotDetails)
        //                {
        //                    DataRow dr_PlotDetails = dtPlotDetails.NewRow();
        //                    dr_PlotDetails["PlotNo"] = item.PlotNo;
        //                    dr_PlotDetails["Area"] = item.Area;
        //                    dr_PlotDetails["UnitID"] = item.UnitID;
        //                    dtPlotDetails.Rows.Add(dr_PlotDetails);
        //                }
        //            }
        //        }


        //        // Adding Final Plot Details In DT
        //        if (SaveFinalPlotDetails != null)
        //        {
        //            if (SaveFinalPlotDetails.Count > 0)
        //            {
        //                foreach (var item in SaveFinalPlotDetails)
        //                {
        //                    DataRow dr_FinalPlotDetails = dtFinalPlotDetails.NewRow();
        //                    dr_FinalPlotDetails["FinalPlotNo"] = item.FinalPlotNo;
        //                    dr_FinalPlotDetails["Area"] = item.Area;
        //                    dr_FinalPlotDetails["UnitID"] = item.UnitID;
        //                    dtFinalPlotDetails.Rows.Add(dr_FinalPlotDetails);
        //                }
        //            }
        //        }

        //        // Adding Consultant Details In DT
        //        if (SaveConsultant != null)
        //        {
        //            if (SaveConsultant.Count > 0)
        //            {
        //                foreach (var item in SaveConsultant)
        //                {
        //                    DataRow dr_Consultants = dtConsultant.NewRow();
        //                    dr_Consultants["ConsultantId"] = item.ConsultantId;
        //                    dr_Consultants["ConsultantTypeId"] = item.ConsultantTypeId;
        //                    dtConsultant.Rows.Add(dr_Consultants);
        //                }
        //            }
        //        }

        //        // Adding Contractor Details In DT
        //        if (SaveContractor != null)
        //        {
        //            if (SaveContractor.Count > 0)
        //            {
        //                foreach (var item in SaveContractor)
        //                {
        //                    DataRow dr_Contractor = dtContractor.NewRow();
        //                    dr_Contractor["ConstractorId"] = item.ConstractorId;
        //                    dr_Contractor["ContractorTypeId"] = item.ContractorTypeId;
        //                    dtContractor.Rows.Add(dr_Contractor);
        //                }
        //            }
        //        }

        //        // Adding Field Details In DT
        //        if (SaveField != null)
        //        {
        //            if (SaveField.Count > 0)
        //            {
        //                foreach (var item in SaveField)
        //                {
        //                    DataRow dr_Field = dtField.NewRow();
        //                    dr_Field["Field"] = item.Field;
        //                    dtField.Rows.Add(dr_Field);
        //                }
        //            }
        //        }

        //        // Adding Parameter Details In DT
        //        if (SaveProjectParameter != null)
        //        {
        //            if (SaveProjectParameter.Count > 0)
        //            {
        //                foreach (var item in SaveProjectParameter)
        //                {
        //                    DataRow dr_ProjectParameter = dtProjectParameter.NewRow();
        //                    dr_ProjectParameter["Parameter"] = item.Parameter;
        //                    dtProjectParameter.Rows.Add(dr_ProjectParameter);
        //                }
        //            }
        //        }


        //        SqlParameter tvpParamDeveloper = new SqlParameter();
        //        tvpParamDeveloper.ParameterName = "@ProjectDeveloper";
        //        tvpParamDeveloper.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamDeveloper.Value = dtDeveloper;
        //        tvpParamDeveloper.TypeName = "UT_ProjectDeveloper";

        //        SqlParameter tvpParamDeveloperContact = new SqlParameter();
        //        tvpParamDeveloperContact.ParameterName = "@ProjectDeveloperContact";
        //        tvpParamDeveloperContact.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamDeveloperContact.Value = dtDeveloperContact;
        //        tvpParamDeveloperContact.TypeName = "UT_ProjectDeveloperContact";

        //        SqlParameter tvpParamCoordinator = new SqlParameter();
        //        tvpParamCoordinator.ParameterName = "@ProjectCoordinator";
        //        tvpParamCoordinator.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamCoordinator.Value = dtCoordinator;
        //        tvpParamCoordinator.TypeName = "UT_ProjectCoordinator";

        //        SqlParameter tvpParamAssistant = new SqlParameter();
        //        tvpParamAssistant.ParameterName = "@ProjectAssistant";
        //        tvpParamAssistant.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamAssistant.Value = dtAssistant;
        //        tvpParamAssistant.TypeName = "UT_ProjectAssistant";

        //        SqlParameter tvpParamOfficeContact = new SqlParameter();
        //        tvpParamOfficeContact.ParameterName = "@ProjectOfficeContact";
        //        tvpParamOfficeContact.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamOfficeContact.Value = dtOfficeContact;
        //        tvpParamOfficeContact.TypeName = "UT_ProjectOfficeContact";

        //        SqlParameter tvpParamSurvayDetails = new SqlParameter();
        //        tvpParamSurvayDetails.ParameterName = "@ProjectSurvay";
        //        tvpParamSurvayDetails.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamSurvayDetails.Value = dtSurvayDetails;
        //        tvpParamSurvayDetails.TypeName = "UT_ProjectSurvay";

        //        SqlParameter tvpParamGatDetails = new SqlParameter();
        //        tvpParamGatDetails.ParameterName = "@ProjectGat";
        //        tvpParamGatDetails.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamGatDetails.Value = dtGatDetails;
        //        tvpParamGatDetails.TypeName = "UT_ProjectGat";

        //        SqlParameter tvpParamCTSDetails = new SqlParameter();
        //        tvpParamCTSDetails.ParameterName = "@ProjectCTS";
        //        tvpParamCTSDetails.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamCTSDetails.Value = dtCTSDetails;
        //        tvpParamCTSDetails.TypeName = "UT_ProjectCTS";

        //        SqlParameter tvpParamPlotDetails = new SqlParameter();
        //        tvpParamPlotDetails.ParameterName = "@ProjectPlot";
        //        tvpParamPlotDetails.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamPlotDetails.Value = dtPlotDetails;
        //        tvpParamPlotDetails.TypeName = "UT_ProjectPlot";

        //        SqlParameter tvpParamFinalPlotDetails = new SqlParameter();
        //        tvpParamFinalPlotDetails.ParameterName = "@ProjectFinalPlot";
        //        tvpParamFinalPlotDetails.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamFinalPlotDetails.Value = dtFinalPlotDetails;
        //        tvpParamFinalPlotDetails.TypeName = "UT_ProjectFinalPlot";

        //        SqlParameter tvpParamConsultant = new SqlParameter();
        //        tvpParamConsultant.ParameterName = "@ProjectConsultant";
        //        tvpParamConsultant.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamConsultant.Value = dtConsultant;
        //        tvpParamConsultant.TypeName = "UT_ProjectConsultant";

        //        SqlParameter tvpParamContractor = new SqlParameter();
        //        tvpParamContractor.ParameterName = "@ProjectContractor";
        //        tvpParamContractor.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamContractor.Value = dtContractor;
        //        tvpParamContractor.TypeName = "UT_ProjectContractor";

        //        SqlParameter tvpParamField = new SqlParameter();
        //        tvpParamField.ParameterName = "@ProjectField";
        //        tvpParamField.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamField.Value = dtField;
        //        tvpParamField.TypeName = "UT_ProjectField";

        //        SqlParameter tvpParamProjectParameter = new SqlParameter();
        //        tvpParamProjectParameter.ParameterName = "@ProjectParameter";
        //        tvpParamProjectParameter.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParamProjectParameter.Value = dtProjectParameter;
        //        tvpParamProjectParameter.TypeName = "UT_ProjectParameter";


        //        OfficeDbContext _db = new OfficeDbContext();
        //        Boolean Active = true;
        //        if (sp.IsActive == false)
        //        {
        //            Active = false;
        //        }
        //        var result = _db.Database.ExecuteSqlCommand(@"exec USP_SaveProject 
        //       @ProjectID,@Name,@EnquiryDate,@ShortName,@StatusId,@ProjectTypeId,@CustomerFileNo,@PhysicalPath,@Road,@Goan,@Taluka,@District,@Duration,@Cost,@StartDate,@EndDate,@IsActive,@CreatedBy,
        //        @ProjectDeveloper
        //       ,@ProjectDeveloperContact
        //       ,@ProjectCoordinator
        //       ,@ProjectAssistant
        //       ,@ProjectOfficeContact
        //       ,@ProjectSurvay
        //       ,@ProjectGat
        //       ,@ProjectCTS
        //       ,@ProjectPlot
        //       ,@ProjectFinalPlot
        //       ,@ProjectConsultant
        //       ,@ProjectContractor
        //       ,@ProjectField
        //       ,@ProjectParameter",
        //        new SqlParameter("@ProjectID", sp.ProjectID),
        //        new SqlParameter("@Name", sp.Name),
        //        new SqlParameter("@EnquiryDate", sp.EnquiryDate),
        //        new SqlParameter("@ShortName", sp.ShortName),
        //        new SqlParameter("@StatusId", sp.StatusId),
        //        new SqlParameter("@ProjectTypeId", sp.ProjectTypeId),
        //        new SqlParameter("@CustomerFileNo", sp.CustomerFileNo),
        //        new SqlParameter("@PhysicalPath", sp.PhysicalPath),
        //        new SqlParameter("@Road", sp.Road),
        //        new SqlParameter("@Goan", sp.Goan),
        //        new SqlParameter("@Taluka", sp.Taluka),
        //        new SqlParameter("@District", sp.District),
        //        new SqlParameter("@Duration", sp.Duration),
        //        new SqlParameter("@Cost", sp.Cost),
        //        new SqlParameter("@StartDate", sp.StartDate),
        //        new SqlParameter("@EndDate", sp.EndDate),
        //        new SqlParameter("@IsActive", Active),
        //        new SqlParameter("@CreatedBy", 1)
        //        , tvpParamDeveloper
        //        , tvpParamDeveloperContact
        //        , tvpParamCoordinator
        //        , tvpParamAssistant
        //        , tvpParamOfficeContact
        //        , tvpParamSurvayDetails
        //        , tvpParamGatDetails
        //        , tvpParamCTSDetails
        //        , tvpParamPlotDetails
        //        , tvpParamFinalPlotDetails
        //        , tvpParamConsultant
        //        , tvpParamContractor
        //        , tvpParamField
        //        , tvpParamProjectParameter
        //    );

        //        return Json("Success");

        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return Json(message);

        //    }
        //}
        #endregion


    }
}

















