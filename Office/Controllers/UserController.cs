using office.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult LoadUserPermissions(string username = "")
        //{
        //    OfficeDbContext _db = new OfficeDbContext();
        //    var result = _db.UserPermission.SqlQuery(@"exec GetUSerList @username",
        //        new SqlParameter("@username", username)).ToList<UserPermission>();
        //    IEnumerable<UserPermission> data = result;
        //    return Request.IsAjaxRequest()
        //        ? (ActionResult)PartialView("_UserPermissionList", data)
        //        : View("_UserPermissionList", data);
        //}

        //public ActionResult UserPermissions(string username = "")
        //{
        //    OfficeDbContext _db = new OfficeDbContext();
        //    var result = _db.UserPermission.SqlQuery(@"exec GetUSerList @username",
        //        new SqlParameter("@username", username)).ToList<UserPermission>();
        //    IEnumerable<UserPermission> data = result;
        //    return Request.IsAjaxRequest()
        //        ? (ActionResult)PartialView("UserPermissions", data)
        //        : View("UserPermissions", data);
        //}

        //public ActionResult _UserPermissionGrid(int? userid)
        //{
        //    OfficeDbContext _db = new OfficeDbContext();
        //    var result = _db.AddUserPermission.SqlQuery(@"exec GetUserPermissionDetail @userid",
        //       new SqlParameter("@userid", userid)).ToList<AddUserPermission>();
        //    IEnumerable<AddUserPermission> data = result;
        //    return Request.IsAjaxRequest()
        //        ? (ActionResult)PartialView("_UserPermissionGrid", data)
        //        : View("_UserPermissionGrid", data);
        //}


        //[HttpPost]
        //public ActionResult SaveUserPermission(SaveUserAccess[] SaveUserAccess)
        //{

        //    try
        //    {
        //        var result = 0;
        //        OfficeDbContext _db = new OfficeDbContext();
        //        foreach (var item in SaveUserAccess)
        //        {

        //            SaveUserAccess O = new SaveUserAccess();

        //            O.Userid = item.Userid;
        //            O.Actionid = item.Actionid;
        //            O.PermissionValue = item.PermissionValue;

        //            result = _db.Database.ExecuteSqlCommand(@"exec USP_SaveUserPermission 
        //            @UserId,@Actionid,@PermissionValue",
        //            new SqlParameter("@UserId", O.Userid),
        //            new SqlParameter("@Actionid", O.Actionid),
        //            new SqlParameter("@PermissionValue", O.PermissionValue));

        //        }

        //        return Json("User Permission Saved");

        //    }

        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        return Json(message);
        //    }


        //}

        //public ActionResult AddNewUser()
        //{
        //    UserMasterList data = new UserMasterList();
        //    data.RoleID = 0;
        //    ViewData["RoleList"] = binddropdown("RoleName", 0);
        //    return Request.IsAjaxRequest()
        //           ? (ActionResult)PartialView("AddNewUser", data)
        //           : View("AddNewUser", data);
        //}
        //public List<SelectListItem> binddropdown(string action, int val = 0)
        //{
        //    OfficeDbContext _db = new OfficeDbContext();

        //    var res = _db.Database.SqlQuery<SelectListItem>("exec USP_BindDropDown @action , @val",
        //           new SqlParameter("@action", action),
        //            new SqlParameter("@val", val))
        //           .ToList()
        //           .AsEnumerable()
        //           .Select(r => new SelectListItem
        //           {
        //               Text = r.Text.ToString(),
        //               Value = r.Value.ToString(),
        //               Selected = r.Value.Equals(Convert.ToString(val))
        //           }).ToList();

        //    return res;
        //}


        ////================================== Insert user Code ===========================================

        //[HttpPost]
        //public ActionResult add_User(UserMasterList rs)
        //{
        //    try
        //    {

        //         OfficeDbContext _db = new OfficeDbContext();
        //        var result = _db.Database.ExecuteSqlCommand(@"exec USP_AddUser 
        //        @User_Name,@F_Name,@M_Name,@L_Name,@phone,@Mobile,@Password,@emailId,@RoleID,@Isactive,@CreatedBy,@CreatedDate",
        //        new SqlParameter("@User_Name", rs.User_Name),
        //        new SqlParameter("@F_Name", rs.F_Name),
        //        new SqlParameter("@M_Name", rs.M_Name == null ? (object)DBNull.Value : rs.M_Name),
        //        new SqlParameter("@L_Name", rs.L_Name),
        //        new SqlParameter("@phone", rs.phone == null ? (object)DBNull.Value : rs.phone),
        //        new SqlParameter("@Mobile", rs.Mobile),
        //        new SqlParameter("@Password", rs.Password),
        //        new SqlParameter("@emailId", rs.emailId),
        //        new SqlParameter("@RoleID", rs.RoleID),
        //        new SqlParameter("@Isactive", rs.Isactive),
        //        new SqlParameter("@CreatedBy", 1),
        //        new SqlParameter("@CreatedDate", DateTime.Now)
        //    );
        //        return Json("IndexForUser");
        //        //return Request.IsAjaxRequest()
        //        //      ? (ActionResult)PartialView("IndexForUser", rs)
        //        //      : View("IndexForUser", rs);

        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return View("IndexForUser", message);
        //        //return PartialView(rs);
        //    }
        //}



        //================================== Edit User Code ===========================================

        //public ActionResult _EditUser()
        //{
        //    UserDetails data = new UserDetails();
        //    data.RoleID = 0;
        //    ViewData["RoleList"] = binddropdown("RoleName", 0);
        //    return Request.IsAjaxRequest()
        //           ? (ActionResult)PartialView("_EditUser", data)
        //           : View("_EditUser", data);
        //}


        //================================== Update User Code ===========================================


        //[HttpPost]
        //public ActionResult UpdateUser(UserDetails rs)
        //{

        //    try
        //    {
        //         OfficeDbContext _db = new OfficeDbContext();
        //        var result = _db.Database.ExecuteSqlCommand(@"exec USP_UpdateUser 
        //        @User_Id ,	@User_Name,	@F_Name,@M_Name,@L_Name,@phone,@Mobile,@Password,@emailId,@RoleID,@Isactive",
        //        new SqlParameter("@User_Id", rs.User_id),
        //        new SqlParameter("@User_Name", rs.User_Name),
        //        new SqlParameter("@F_Name", rs.F_Name),
        //        new SqlParameter("@M_Name", rs.M_Name == null ? (object)DBNull.Value : rs.M_Name),
        //        new SqlParameter("@L_Name", rs.L_Name),
        //        new SqlParameter("@phone", rs.phone == null ? (object)DBNull.Value : rs.phone),
        //        new SqlParameter("@Mobile", rs.Mobile),
        //        new SqlParameter("@Password", rs.Password),
        //        new SqlParameter("@emailId", rs.emailId),
        //        new SqlParameter("@RoleID", rs.RoleID),
        //        new SqlParameter("@Isactive", rs.Isactive)
        //         );

        //        return Json("User Updated Sucessfully.");

        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return View("IndexForUser", message);

        //    }
        //    finally
        //    {

        //    }
        //}

        ////================================== Fetch User For Update Code ===========================================

        //public ActionResult FetchUser(int User_id)
        //{

        //    try
        //    {
        //        UserDetails data = new UserDetails();

        //         OfficeDbContext _db = new OfficeDbContext();
        //        var result = _db.UDList.SqlQuery(@"exec usp_UserDetails 
        //        @User_id",
        //         new SqlParameter("@User_id", User_id)).ToList<UserDetails>();

        //        data = result.FirstOrDefault();

        //        //data.RoleID = 0;
        //        ViewData["RoleList"] = binddropdown("RoleName", 0);
        //        return Request.IsAjaxRequest()
        //               ? (ActionResult)PartialView("_EditUser", data)
        //               : View("_EditUser", data);
        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return RedirectToAction("_EditUser");

        //    }
        //    finally
        //    {

        //    }
        //}
    }
}