using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using office.Models;
using PagedList;

namespace office.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Compose(int id =1)
        {
            OfficeDbContext _db = new OfficeDbContext();
            temlatesInfo data = new temlatesInfo();
            var result = _db.temlatesList.SqlQuery(@"exec GetTemplate 
                @TemplateID",
               new SqlParameter("@TemplateID", id)).ToList<temlatesInfo>();

            data = result.FirstOrDefault(); 
            
          
            return Request.IsAjaxRequest()
                     ? (ActionResult)PartialView("Compose", data)
                     : View("Compose", data);
        }
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
               @TemplateID, @TemplateName,@Description",
            new SqlParameter("@TemplateID", t.TemplateID),
            new SqlParameter("@TemplateName", t.TemplateName),
            new SqlParameter("@Description", t.Description)

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
            DocTemplateList result = new DocTemplateList();
            data.
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
    }
}