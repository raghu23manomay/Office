﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calibration.Models;
using office.Models;
using PagedList;
using Spire.Doc;
using Spire.Doc.Documents; 
//using Xceed.Words.NET;

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
        public ActionResult GetCustomPlaceholder(int TemplateID=0)
        {
            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<CustomPlaceholders> result = _db.CustomPlaceholders.SqlQuery(@"exec usp_GetCustomPlaceholder @TemplateID", new SqlParameter("@TemplateID",  TemplateID)).ToList<CustomPlaceholders>();
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CustomPlaceholder", result)
                    : View("CustomPlaceholder", result);
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
    public ActionResult LoadDocTemplateGrid(int? page, int DepartmentID = 0)
    {
        StaticPagedList<DocTemplateList> itemsAsIPagedList;
        itemsAsIPagedList = TemplateGridList(page, DepartmentID);

        return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("TemplateGrid", itemsAsIPagedList)
                : View("TemplateGrid", itemsAsIPagedList);
    }

        //public ActionResult LoadGeneratedDocumentGrid(int? page, int DepartmentID = 0)
        //{
        //    StaticPagedList<GeeratedDocumentList> itemsAsIPagedList;
        //    itemsAsIPagedList = GeneratedDocumentGridList(page, DepartmentID);

        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("TemplateGrid", itemsAsIPagedList)
        //            : View("TemplateGrid", itemsAsIPagedList);
        //}
        public ActionResult DocTemplateList( int ProjectID=0)
    {
            ViewData["ProjectID"] = ProjectID;
        return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("DocTemplateList")
                : View("DocTemplateList");
    }
    public ActionResult GeneratedDocumentList(int? page, int DepartmentID = 0)
    {
        return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("GeneratedDocumentList")
                : View("GeneratedDocumentList"); 
    }
        public StaticPagedList<DocTemplateList> TemplateGridList(int? page, int DepartmentID = 0)
    {
        OfficeDbContext _db = new OfficeDbContext();
        var pageIndex = (page ?? 1);
        const int pageSize =10;
        int totalCount = 10;
            DocTemplateList clist = new  DocTemplateList();

        IEnumerable<DocTemplateList> result = _db.DocTemplateLists.SqlQuery(@"exec getDocumentTemplate
                   @pPageIndex, @pPageSize,@DepartmentID",
           new SqlParameter("@pPageIndex", pageIndex),
           new SqlParameter("@pPageSize", pageSize),
           new SqlParameter("@DepartmentID",  DepartmentID)
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
        #region DocumentGeneratedList
        public ActionResult LoadDocumentGrid(int? page, int DepartmentID = 0)
        {
            StaticPagedList<GeeratedDocumentList> itemsAsIPagedList;
            itemsAsIPagedList = DocumentGridList(page, DepartmentID);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("DocumentGrid", itemsAsIPagedList)
                    : View("DocumentGrid", itemsAsIPagedList);
        }

        public ActionResult DocumentList(int? page, int DepartmentID = 0)
        {
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("DocumentList")
                    : View("DocumentList");
        }
        public StaticPagedList<GeeratedDocumentList> DocumentGridList(int? page, int DepartmentID = 0)
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            GeeratedDocumentList clist = new GeeratedDocumentList();

            IEnumerable<GeeratedDocumentList> result = _db.GeeratedDocumentLists.SqlQuery(@"exec getGeneratedDocument
                   @pPageIndex, @pPageSize,@DepartmentID",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@DepartmentID", DepartmentID)
               ).ToList<GeeratedDocumentList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<GeeratedDocumentList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        #endregion
        public ActionResult GenerateDocument(int id = 0)  
        {
            DocTemplateList data = new DocTemplateList();
            data.TemplateID = id;
            ViewData["TemplateID"] = id;
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
        public ActionResult GetProjectData(int? TemplateID, int DTTemplateID=0,int ProjectID=1)
        {


            ProjectsData data = new ProjectsData();
            try
            {
                if (DTTemplateID == 0)
                {
                    OfficeDbContext _db = new OfficeDbContext();
                    var result = _db.ProjectsData.SqlQuery(@"exec GetProjectDetailsForTemplate
               @ProjectId",
                       new SqlParameter("@ProjectId", ProjectID)
                       ).ToList<ProjectsData>();

                    data = result.FirstOrDefault();
                  
                    IEnumerable<DeveloperData> result2 = _db.DeveloperData.SqlQuery(@"exec GetProjectDetailsForTemplate
                 @ProjectId,@Tno",
                        new SqlParameter("@ProjectId", ProjectID),
                         new SqlParameter("@Tno", 2)
                          ).ToList<DeveloperData>();

                    data.DeveloperDatalist = result2;

                //IEnumerable<SaveProjectInternalTeam> result4 = _db.SaveProjectInternalTeam.SqlQuery(@"exec uspGetProjectInternalTeam
                //@ProjectID",
                //new SqlParameter("@ProjectID", ProjectID)
                //).ToList<SaveProjectInternalTeam>();
                //data.SaveProjectInternalTeam = result4;

                //IEnumerable<SaveProjectExternalTeam> result5 = _db.SaveProjectExternalTeam.SqlQuery(@"exec uspGetProjectExternalTeam
                //@ProjectID",
                //  new SqlParameter("@ProjectID", ProjectID)
                //  ).ToList<SaveProjectExternalTeam>();
                //    data.SaveProjectExternalTeam = result5;

                //IEnumerable<SaveProjectOfficeSideTeam> result6 = _db.SaveProjectOfficeSideTeam.SqlQuery(@"exec uspGetProjectOfficeSideTeam
                //@ProjectID",
                //new SqlParameter("@ProjectID", ProjectID)
                //).ToList<SaveProjectOfficeSideTeam>();
                //    data.SaveProjectOfficeSideTeam = result6;

                //IEnumerable<AuthoritySignatory> result7 = _db.AuthoritySignatory.SqlQuery(@"exec GetProjectSignatory
                //@ProjectID",
                //new SqlParameter("@ProjectID", ProjectID)
                //).ToList<AuthoritySignatory>();

                //    IEnumerable<AuthoritySignatoryDetail> result8 = _db.AuthoritySignatoryDetail.SqlQuery(@"exec GetProjectSignatoryDetail
                //@ProjectID",
                //new SqlParameter("@ProjectID", ProjectID)
                //).ToList<AuthoritySignatoryDetail>();

           
                  
                    IEnumerable<CustomPlaceholders> resultCustomPlaceholder = _db.CustomPlaceholders.SqlQuery(@"exec usp_GetCustomPlaceholder @TemplateID,@DtTemplateID",
                    new SqlParameter("@TemplateID", TemplateID),
                     new SqlParameter("@DTTemplateID", DTTemplateID)).ToList<CustomPlaceholders>();
                    data.CustomPlaceholders = resultCustomPlaceholder;
                }
                else
                {
                    OfficeDbContext _db = new OfficeDbContext();
                    var result = _db.ProjectsData.SqlQuery(@"exec GetProjectDetailsForTemplate
                       @ProjectId,@Tno,@DTTemplateID",
                       new SqlParameter("@ProjectId", ProjectID),
                       new SqlParameter("@Tno", 1),
                       new SqlParameter("@DTTemplateID", DTTemplateID)
                       ).ToList<ProjectsData>();

                    data = result.FirstOrDefault();

                    IEnumerable<DeveloperData> result2 = _db.DeveloperData.SqlQuery(@"exec GetProjectDetailsForTemplate
                @ProjectId,@Tno,@DTTemplateID",
                       new SqlParameter("@ProjectId", ProjectID),
                       new SqlParameter("@Tno", 2),
                       new SqlParameter("@DTTemplateID", DTTemplateID)
                       ).ToList<DeveloperData>();

                    data.DeveloperDatalist = result2; 

                    
            IEnumerable<SaveProjectInternalTeam> result3 = _db.SaveProjectInternalTeam.SqlQuery(@"exec uspGetProjectInternalTeam
                @ProjectID,@DTTemplateID",
           new SqlParameter("@ProjectID", ProjectID),
           new SqlParameter("@DTTemplateID", DTTemplateID)
           ).ToList<SaveProjectInternalTeam>();

                    data.SaveProjectInternalTeam = result3;

                    IEnumerable<CustomPlaceholders> resultCustomPlaceholder = _db.CustomPlaceholders.SqlQuery(@"exec usp_GetCustomPlaceholder @TemplateID,@DtTemplateID",
                       new SqlParameter("@TemplateID", TemplateID),
                        new SqlParameter("@DTTemplateID", DTTemplateID)).ToList<CustomPlaceholders>();
                    data.CustomPlaceholders = resultCustomPlaceholder;
                }
                data.TemplateID = TemplateID;
            }
            catch (Exception e) { }
            return View("ProjectData", data);
        }
        public ActionResult EmptyTemplateData(int TemplateID = 1, int DtTemplateID = 1, int EmptyField = 0)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                IEnumerable<EmptyTemplateData> result = _db.EmptyTemplateData.SqlQuery(@"exec usp_ReplacePlaceholder
                       @TemplateID,@dtTemplateID,@withcolor,@EmptyField",
                   new SqlParameter("@TemplateID", TemplateID),
                   new SqlParameter("@DtTemplateID", DtTemplateID),
                   new SqlParameter("@withcolor", 1),
                   new SqlParameter("@EmptyField", 1)
                   ).ToList<EmptyTemplateData>();
                var a = result.Count();
                return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("EmptyTemplateData", result)
                   : View("EmptyTemplateData", result);
            }
            catch (Exception e) { }
           
            return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("EmptyTemplateData")
                     : View("EmptyTemplateData");

        }
        public ActionResult ReplacePlaceholderByDataTemplate(int TemplateID=1,int DtTemplateID=1,int EmptyField=0)
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

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=file1.docx");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-word";
                    Response.Output.Write(data.TemplateDescription);
                    string folderPath = "~/Document/" + TemplateID;
                    string filePath = "~/Document/" + TemplateID + "/file-" + TemplateID + "-" + DtTemplateID + ".docx";
                    string fileName = Server.MapPath(filePath);

                    bool exists = System.IO.Directory.Exists(Server.MapPath(folderPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(folderPath));
                    Document doc = new Document();
                    doc.AddSection();

                    Paragraph para = doc.Sections[0].AddParagraph();

                    para.AppendHTML(data.TemplateDescription);

                    doc.SaveToFile(fileName, FileFormat.Docx2013);


                    var result2 = _db.Database.ExecuteSqlCommand(@"exec uspSaveDocument 
                   @TemplateID, @FilePath",
                      new SqlParameter("@TemplateID", TemplateID),
                      new SqlParameter("@FilePath", filePath)
                   );

                    data.FilePath = filePath;
                 
                //Process.Start("WINWORD.EXE", fileName);
                //Response.TransmitFile(fileName);
                //Response.Flush();
                //Response.End();

            }
            catch (Exception e) { }
            return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("ReplacePlaceholderByDataTemplate", data)
                     : View("ReplacePlaceholderByDataTemplate", data);
          
        }

        public ActionResult CompareDataTemplates(int TemplateID = 1, String DTTemplateIDList = "",int  ProjectID=0)
        { 
               
          OfficeDbContext _db = new OfficeDbContext();
            try
            {
                IEnumerable<ProjectsDataWithValue> result = _db.ProjectsDataWithValue.SqlQuery(@"exec uspCompareTemplate
                 @TemplateID,@DtTemplateIDList",
                   new SqlParameter("@TemplateID", TemplateID),
                   new SqlParameter("@DtTemplateIDList", DTTemplateIDList)
                      ).ToList<ProjectsDataWithValue>();
                return View("CompareDataTemplates", result);
            }
            catch (Exception e) { }
            
            return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("CompareDataTemplates")
                     : View("CompareDataTemplates");
             
        }
        [HttpPost]
         public ActionResult GenerateDataTemplate(ProjectsTemplateData sp, List<SaveCustomField> SaveCustomField)
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                DataTable dtCustomField = new DataTable();
                dtCustomField.Columns.Add("FieldID", typeof(int));
                dtCustomField.Columns.Add("Value", typeof(string));

                // Adding CustomField In DT
                if (SaveCustomField != null)
                {
                    if (SaveCustomField.Count > 0)
                    {
                        foreach (var item in SaveCustomField)
                        {
                            DataRow dr_CustomField = dtCustomField.NewRow();
                            dr_CustomField["FieldID"] = item.FieldID;
                            dr_CustomField["Value"] = item.Value;
                            dtCustomField.Rows.Add(dr_CustomField);
                        }
                    }
                }
                SqlParameter tvpParamCustomField = new SqlParameter();
                tvpParamCustomField.ParameterName = "@CustomField";
                tvpParamCustomField.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamCustomField.Value = dtCustomField;
                tvpParamCustomField.TypeName = "UT_CustomField";

                var result = _db.Database.ExecuteSqlCommand(@"exec USP_GenerateDataTemplate @TemplateID,
                @DTTemplateID,@DataTemplateName,@ProjectID,@DeveloperID,@InternalTeamId,@ExternalTeamId,@OfficeTeamId,@CInternalTeamID,@CExternalTeamID, @CAddressID,@CustomField",
                new SqlParameter("@TemplateID", sp.TemplateID),
                new SqlParameter("@DTTemplateID", sp.DTTemplateID==null?0:sp.DTTemplateID),
                new SqlParameter("@DataTemplateName", sp.DataTemplateName),
                new SqlParameter("@ProjectID", sp.ProjectID),
                new SqlParameter("@DeveloperID", sp.DeveloperID),
                new SqlParameter("@InternalTeamId", sp.InternalTeamId), 
                new SqlParameter("@ExternalTeamId", sp.ExternalTeamId),
                new SqlParameter("@OfficeTeamId", sp.OfficeTeamId),

                 new SqlParameter("@CInternalTeamID", sp.CInternalTeamID),
                new SqlParameter("@CExternalTeamID", sp.CExternalTeamID),
                new SqlParameter("@CAddressID", sp.CAddressID)

                , tvpParamCustomField
                

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
        public ActionResult DepartmentType1(int id = 1)
        {
            OfficeDbContext _db = new OfficeDbContext();
            DepartmentType data = new DepartmentType();
            IEnumerable<DepartmentType> result = _db.DepartmentTypes.SqlQuery(@"exec getDepartment").ToList<DepartmentType>();

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("DepartmentType1", result)
                    : View("DepartmentType1", result);
        }
        //public ActionResult GetDevelopersData(int? id, int val = 0)
        //{
        //    OfficeDbContext _db = new OfficeDbContext();
        //    DepartmentType data = new DepartmentType();
        //    IEnumerable<DeveloperSideContactPerson> result = _db.DeveloperSideContactPersons.SqlQuery(@"exec GetProjectAllDetails
        //      @ProjectId,@val",
        //       new SqlParameter("@ProjectId", id),
        //       new SqlParameter("@val", val)
        //       ).ToList<DeveloperSideContactPerson>();

        //    return Request.IsAjaxRequest()
        //           ? (ActionResult)PartialView("DevelopersData", result)
        //           : View("DevelopersData", result);
        //} 
        [ValidateInput(false)]
        public ActionResult Export(HtmlTemplate HtmlText)
        {
             
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Grid.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
            Response.Output.Write(HtmlText.Description);
            Response.Flush();
            Response.End();
            return Request.IsAjaxRequest()
                 ? (ActionResult)PartialView()
                 : View();
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
        [HttpPost]
        public ActionResult SaveCustomField(int FieldID =0,String CustomFieldName = "" )
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
               
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveCustomField @Name",
                  new SqlParameter("@Name", CustomFieldName)
                 
            );

                return Json("Success");

            }
            catch (Exception ex) 
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }
        public ActionResult DownloadFile(string path)
        {
            Response.ContentType = "application/docx";
            Response.AddHeader("Content-Disposition", "inline;  filename=MyFile.docx");
            //Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.docx");
            Response.TransmitFile(Server.MapPath(path));
            Response.End();
            return    Request.IsAjaxRequest()
              ? (ActionResult)PartialView("DownloadFile")
              : View("DownloadFile");
        }
            
    }
}