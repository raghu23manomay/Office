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

        public ActionResult GenerateDocument(int TemplateId = 0)
        {
            DocTemplateList data = new DocTemplateList();
               
            return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("GenerateDocument", data)
              : View("GenerateDocument", data);
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
        public ActionResult GetProjectData(int? id,int DTTemplateID=0)
        {
            ProjectsData data = new ProjectsData();
            try
            {
                if (DTTemplateID == 0)
                {
                    OfficeDbContext _db = new OfficeDbContext();
                    var result = _db.ProjectsData.SqlQuery(@"exec GetProjectDetailsForTemplate
               @ProjectId",
                       new SqlParameter("@ProjectId", id)
                       ).ToList<ProjectsData>();

                    data = result.FirstOrDefault();

                    IEnumerable<DeveloperData> result2 = _db.DeveloperData.SqlQuery(@"exec GetProjectDetailsForTemplate
                 @ProjectId,@Tno",
                        new SqlParameter("@ProjectId", id),
                         new SqlParameter("@Tno", 2)
                          ).ToList<DeveloperData>();

                    data.DeveloperDatalist = result2;

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
                else
                {
                    OfficeDbContext _db = new OfficeDbContext();
                    var result = _db.ProjectsData.SqlQuery(@"exec GetProjectDetailsForTemplate
               @ProjectId,@Tno,@DTTemplateID",
                       new SqlParameter("@ProjectId", id),
                       new SqlParameter("@Tno", 1),
                       new SqlParameter("@DTTemplateID", DTTemplateID)
                       ).ToList<ProjectsData>();

                    data = result.FirstOrDefault();

                    IEnumerable<DeveloperData> result2 = _db.DeveloperData.SqlQuery(@"exec GetProjectDetailsForTemplate
                @ProjectId,@Tno,@DTTemplateID",
                       new SqlParameter("@ProjectId", id),
                       new SqlParameter("@Tno", 2),
                       new SqlParameter("@DTTemplateID", DTTemplateID)
                       ).ToList<DeveloperData>();

                    data.DeveloperDatalist = result2;

                    IEnumerable<CoordinatorDetailsData> result3 = _db.CoordinatorDetailsData.SqlQuery(@"exec GetProjectDetailsForTemplate
                 @ProjectId,@Tno,@DTTemplateID",
                       new SqlParameter("@ProjectId", id),
                       new SqlParameter("@Tno", 3),
                       new SqlParameter("@DTTemplateID", DTTemplateID)
                      ).ToList<CoordinatorDetailsData>();


                    data.CoordinatorDetailsData = result3;


                    IEnumerable<AssistantDetailsData> result4 = _db.AssistantDetailsData.SqlQuery(@"exec GetProjectDetailsForTemplate
                 @ProjectId,@Tno,@DTTemplateID",
                       new SqlParameter("@ProjectId", id),
                       new SqlParameter("@Tno", 4),
                       new SqlParameter("@DTTemplateID", DTTemplateID)
                          ).ToList<AssistantDetailsData>();
                    data.AssistantDetailsData = result4;
                }
            }
            catch (Exception e) { }
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
            catch (Exception e) { }
            return View("ReplacePlaceholderByDataTemplate", data);
        }
        
        [HttpPost]
         public ActionResult GenerateDataTemplate(ProjectsTemplateData sp )
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();


                var result = _db.Database.ExecuteSqlCommand(@"exec USP_GenerateDataTemplate
                @DTTemplateID,@DataTemplateName,@ProjectID,@DeveloperID,@CoordinatorID",
                new SqlParameter("@DTTemplateID", sp.DTTemplateID),
                new SqlParameter("@DataTemplateName", sp.DataTemplateName),
                new SqlParameter("@ProjectID", sp.ProjectID),
                new SqlParameter("@DeveloperID", sp.DeveloperID),
                new SqlParameter("@CoordinatorID", sp.CoordinatorID)


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
            
            var result = _db.temlatesList.SqlQuery(@"exec GetTemplate 
                @TemplateID",
               new SqlParameter("@TemplateID", id)).ToList<temlatesInfo>();

            data = result.FirstOrDefault();

            return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("ComposeHtml", data)
                     : View("ComposeHtml", data);
        }

        public ActionResult DepartmentType(int id = 1)
        {
            OfficeDbContext _db = new OfficeDbContext();
            DepartmentType data = new DepartmentType();
            IEnumerable<DepartmentType> result = _db.DepartmentTypes.SqlQuery(@"exec getDepartment").ToList<DepartmentType>();
           
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("DepartmentType", result)
                    : View("DepartmentType", result);
        }

        #region test
        public ActionResult Test( )
        {
           
            return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("Test")
                     : View("Test");
        }
        [HttpPost]
        public ActionResult Test1(int? page, String Name = "")
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
            return      Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataTemplateList( int ProjectID=0)
        {
            DataTemplateList data = new DataTemplateList();
             
            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<DataTemplateList> result = _db.DataTemplateLists.SqlQuery(@"exec usp_GetDatTemplateList @ProjectID",
                 new SqlParameter("@ProjectID", ProjectID)
               ).ToList<DataTemplateList>();
            return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("DataTemplateList", result)
              : View("DataTemplateList", result);
        }
        #endregion
    }
}