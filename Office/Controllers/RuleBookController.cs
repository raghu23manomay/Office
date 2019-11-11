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
    public class RuleBookController : Controller
    {
        // GET: RuleBook
        public ActionResult Index()
        {
            return View();
        }
       
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

        public ActionResult RuleBook()
        {
            OfficeDbContext _db = new OfficeDbContext();
            ViewData["AuthorityList"] = binddropdown("AuthorityList", 0);

            return View();
        }
        public ActionResult GetRuleBook()
            {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<RuleBookData> result = _db.RuleBookData.SqlQuery(@"exec UspGetRuleBookHead ")
 
                .ToList<RuleBookData>();
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("Rules", result)
                    : View("Rules", result);
        }
        
        public ActionResult SubRules(string SubmenuID )
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<RuleBookData> result = _db.RuleBookData.SqlQuery(@"exec UspGetRuleBook @SubmenuID ",
                new SqlParameter("@SubmenuID", SubmenuID))
                .ToList<RuleBookData>();
            if(result.Count()==0)
            {
                return Json("fail");  
            }
         else
            {
                return Request.IsAjaxRequest()
                      ? (ActionResult)PartialView("SubRules", result)
                      : View("SubRules", result);
            }
        } 
       
        public ActionResult DescriptionRule(string rule_no="1" )
        {
            try
            {


                OfficeDbContext _db = new OfficeDbContext();

                var result = _db.RuleDescription.SqlQuery(@"exec UspShowRuleBook @rule_no",
                new SqlParameter("@rule_no", rule_no)
                 ).ToList<RuleDescription>();
                var data = result.FirstOrDefault();
                return Request.IsAjaxRequest()
                        ? (ActionResult)PartialView("DescriptionRule", data)
                        : View("DescriptionRule", data);
            }
            catch (Exception ex)
            {
                var mgs = ex.Message;
                return View();

            }
            finally
            {

            }
        }
    }
}