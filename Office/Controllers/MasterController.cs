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

namespace Calibration.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        //Designation
        #region designation
        public ActionResult LoadDesignationGrid(int? page, String Name = null)
        {
            StaticPagedList<DesignationList> itemsAsIPagedList;
            itemsAsIPagedList = GridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("DesignationGrid", itemsAsIPagedList)
                    : View("DesignationGrid", itemsAsIPagedList);
        }

        public ActionResult DesignationList(int? page, String Name = null)
        {
            StaticPagedList<DesignationList> itemsAsIPagedList;
            itemsAsIPagedList = GridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("DesignationList", itemsAsIPagedList)
                    : View("DesignationList", itemsAsIPagedList);
        }
        public StaticPagedList<DesignationList> GridList(int? page, String Name = "")
        { 
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            DesignationList clist = new DesignationList();

            IEnumerable<DesignationList> result = _db.DFList.SqlQuery(@"exec GetDesignationList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<DesignationList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<DesignationList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        public ActionResult AddDesignation(int id =0)
        {
            Designation data = new Designation();
            OfficeDbContext _db = new OfficeDbContext();
            if (id>0)
            { 
               
                var result = _db.DesigList.SqlQuery(@"exec GetDesignationDetails 
                @DesignationID",
                 new SqlParameter("@DesignationID" , id)).ToList<Designation>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.DesignationID = 0;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddDesignation", data)
                  : View("AddDesignation", data);
        }
        [HttpPost]
        public ActionResult SaveDesignation(int DesignationID=0,String DesignationName="", String IsActive="")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveDesignation 
               @DesignationID, @DesignationName,@CreatedBy,@IsActive",
                new SqlParameter("@DesignationID", DesignationID),
                new SqlParameter("@DesignationName", DesignationName),
                new SqlParameter("@CreatedBy", 1)  , 
                new SqlParameter("@IsActive", Active)
            ); 
                
                return Json("Success"); 

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
                 
            }
        }
        #endregion

       //RoleMaster
        #region RoleMaster
        public ActionResult LoadRoleGrid(int? page, String Name = null)
        {
            StaticPagedList<RoleList> itemsAsIPagedList;
            itemsAsIPagedList = RoleGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("RoleGrid", itemsAsIPagedList)
                    : View("RoleGrid", itemsAsIPagedList);
        }

        public ActionResult RoleList(int? page, String Name = null)
        {
            StaticPagedList<RoleList> itemsAsIPagedList;
            itemsAsIPagedList = RoleGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("RoleList", itemsAsIPagedList)
                    : View("RoleList", itemsAsIPagedList);
        }
        public StaticPagedList<RoleList> RoleGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            RoleList clist = new RoleList();

            IEnumerable<RoleList> result = _db.DFRoleLists.SqlQuery(@"exec GetRoleList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<RoleList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<RoleList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        public ActionResult AddRole(int id = 0)
        {
            Role data = new Role();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {

                var result = _db.RoleLists.SqlQuery(@"exec GetRoleDetails 
                @RoleID",
                 new SqlParameter("@RoleID", id)).ToList<Role>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.RoleID = 0;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddRole", data)
                  : View("AddRole", data);
        }
        [HttpPost]
        public ActionResult SaveRole(int RoleID = 0, String RoleName = "", String IsActive = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveRole 
               @RoleID, @RoleName,@CreatedBy,@IsActive",
                new SqlParameter("@RoleID", RoleID),
                new SqlParameter("@RoleName",RoleName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
        }
        #endregion


       //ModuleMaster
        #region ModuleMaster
        public ActionResult LoadModuleGrid(int? page, String Name = null)
        {
            StaticPagedList<ModuleList> itemsAsIPagedList;
            itemsAsIPagedList = ModuleGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ModuleGrid", itemsAsIPagedList)
                    : View("ModuleGrid", itemsAsIPagedList);
        }

        public ActionResult ModuleList(int? page, String Name = null)
        {
            StaticPagedList<ModuleList> itemsAsIPagedList;
            itemsAsIPagedList = ModuleGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ModuleList", itemsAsIPagedList)
                    : View("ModuleList", itemsAsIPagedList);
        }
        public StaticPagedList<ModuleList> ModuleGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            ModuleList clist = new ModuleList();

            IEnumerable<ModuleList> result = _db.DFModuleLists.SqlQuery(@"exec GetModuleList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<ModuleList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<ModuleList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        public ActionResult AddModule(int id = 0)
        {
            Module data = new Module();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {

                var result = _db.ModuleLists.SqlQuery(@"exec GetModuleDetails 
                @ModuleID",
                 new SqlParameter("@ModuleID", id)).ToList<Module>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.ModuleID = 0;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddModule", data)
                  : View("AddModule", data);
        }
        [HttpPost]
        public ActionResult SaveModule(int ModuleID = 0, String ModuleName = "", String IsActive = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveModule 
               @ModuleID, @ModuleName,@CreatedBy,@IsActive",
                new SqlParameter("@ModuleID", ModuleID),
                new SqlParameter("@ModuleName", ModuleName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
        }
        #endregion

       //AuthorityMaster
        #region AuthorityMaster
        public ActionResult LoadAuthorityGrid(int? page, String Name = null)
        {
            StaticPagedList<AuthorityList> itemsAsIPagedList;
            itemsAsIPagedList = AuthorityGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AuthorityGrid", itemsAsIPagedList)
                    : View("AuthorityGrid", itemsAsIPagedList);
        }

        public ActionResult AuthorityList(int? page, String Name = null)
        {
            StaticPagedList<AuthorityList> itemsAsIPagedList;
            itemsAsIPagedList = AuthorityGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AuthorityList", itemsAsIPagedList)
                    : View("AuthorityList", itemsAsIPagedList);
        }
        public StaticPagedList<AuthorityList> AuthorityGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            AuthorityList clist = new AuthorityList();

            IEnumerable<AuthorityList> result = _db.DFAuthorityLists.SqlQuery(@"exec GetAuthorityList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<AuthorityList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<AuthorityList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        public ActionResult AddAuthority(int id = 0)
        {
            Authority data = new Authority();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {

                var result = _db.AuthorityLists.SqlQuery(@"exec GetAuthorityDetails 
                @AuthorityID",
                 new SqlParameter("@AuthorityID", id)).ToList<Authority>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.AuthorityID = 0;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddAuthority", data)
                  : View("AddAuthority", data);
        }
        [HttpPost]
        public ActionResult SaveAuthority(int AuthorityID = 0, String AuthorityName = "", String IsActive = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveAuthority 
               @AuthorityID, @AuthorityName,@CreatedBy,@IsActive",
                new SqlParameter("@AuthorityID", AuthorityID),
                new SqlParameter("@AuthorityName", AuthorityName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
        }
        #endregion

       //CityMaster
        #region CityMaster
        public ActionResult LoadCityGrid(int? page, String Name = null)
        {
            StaticPagedList<CityList> itemsAsIPagedList;
            itemsAsIPagedList = CityGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CityGrid", itemsAsIPagedList)
                    : View("CityGrid", itemsAsIPagedList);
        }

        public ActionResult CityList(int? page, String Name = null)
        {
            StaticPagedList<CityList> itemsAsIPagedList;
            itemsAsIPagedList = CityGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CityList", itemsAsIPagedList)
                    : View("CityList", itemsAsIPagedList);
        }
        public StaticPagedList<CityList> CityGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            CityList clist = new CityList();

            IEnumerable<CityList> result = _db.DFCityLists.SqlQuery(@"exec GetCityList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<CityList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<CityList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        public ActionResult AddCity(int id = 0)
        {
            City data = new City();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {

                var result = _db.CItyLists.SqlQuery(@"exec GetCityDetails 
                @CityID",
                 new SqlParameter("@CityID", id)).ToList<City>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.CityID = 0;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddCity", data)
                  : View("AddCity", data);
        }
        [HttpPost]
        public ActionResult SaveCity(int CityID = 0, String CityName = "", String IsActive = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveCity 
               @CityID, @CityName,@CreatedBy,@IsActive",
                new SqlParameter("@CityID", CityID),
                new SqlParameter("@CityName", CityName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
        }
        #endregion


        //SubscriptionMaster
        #region SubscriptionMaster
        public ActionResult LoadSubscriptionGrid(int? page, String Name = null)
        {
            StaticPagedList<SubscriptionList> itemsAsIPagedList;
            itemsAsIPagedList = SubscriptionGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("SubscriptionGrid", itemsAsIPagedList)
                    : View("SubscriptionGrid", itemsAsIPagedList);
        }

        public ActionResult SubscriptionList(int? page, String Name = null)
        {
            StaticPagedList<SubscriptionList> itemsAsIPagedList;
            itemsAsIPagedList = SubscriptionGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("SubscriptionList", itemsAsIPagedList)
                    : View("SubscriptionList", itemsAsIPagedList);
        }
        public StaticPagedList<SubscriptionList> SubscriptionGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            SubscriptionList clist = new SubscriptionList();

            IEnumerable<SubscriptionList> result = _db.DFSubscriptionLists.SqlQuery(@"exec GetSubscriptionList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<SubscriptionList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<SubscriptionList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        public ActionResult AddSubscription(int id = 0)
        {
            Subscription data = new Subscription();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {

                var result = _db.SubscriptionLists.SqlQuery(@"exec GetSubscriptionDetails 
                @SubscriptionID",
                 new SqlParameter("@SubscriptionID", id)).ToList<Subscription>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.SubscriptionID = 0;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddSubscription", data)
                  : View("AddSubscription", data);
        }
        [HttpPost]
        public ActionResult SaveSubscription(int SubscriptionID = 0, String PlanName = "",int DurationInDays=0,Decimal Cost=0,String IsActive = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveSubscription 
               @SubscriptionID, @PlanName,@DurationInDays,@Cost,@CreatedBy,@IsActive",
                new SqlParameter("@SubscriptionID", SubscriptionID),
                new SqlParameter("@PlanName", PlanName),
                new SqlParameter("@DurationInDays", DurationInDays),
                new SqlParameter("@Cost", Cost),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
        }
        #endregion


        //CustomerMaster
        #region CustomerMaster
        public ActionResult LoadCustomerGrid(int? page, String Name = null)
        {
            StaticPagedList<CustomerList> itemsAsIPagedList;
            itemsAsIPagedList = CustomerGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CustomerGrid", itemsAsIPagedList)
                    : View("CustomerGrid", itemsAsIPagedList);
        }

        public ActionResult CustomerList(int? page, String Name = null)
        {
            StaticPagedList<CustomerList> itemsAsIPagedList;
            itemsAsIPagedList = CustomerGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CustomerList", itemsAsIPagedList)
                    : View("CustomerList", itemsAsIPagedList);
        }
        public StaticPagedList<CustomerList> CustomerGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            CustomerList clist = new CustomerList();

            IEnumerable<CustomerList> result = _db.DFCustomerLists.SqlQuery(@"exec GetCustomerList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<CustomerList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<CustomerList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        public ActionResult AddCustomer(int id = 0)
        {
            Customer data = new Customer();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {

                var result = _db.CustomerLists.SqlQuery(@"exec GetCustomerDetails 
                @CustomerID",
                 new SqlParameter("@CustomerID", id)).ToList<Customer>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.CustomerID = 0;
            }
            ViewData["CityList"] = binddropdown("CityList", 0);
            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddCustomer", data)
                  : View("AddCustomer", data);
        }
        [HttpPost]
        public ActionResult SaveCustomer(int CustomerID = 0, String CustomerName =null,int CityID=0,String Mobile=null, String Email=null, String IsActive = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveCustomer 
               @CustomerID, @CustomerName,@CityID,@Mobile,@Email,@CreatedBy,@IsActive",
                new SqlParameter("@CustomerID", CustomerID),
                new SqlParameter("@CustomerName", CustomerName),
                new SqlParameter("@CityID", CityID),
                new SqlParameter("@Mobile", Mobile),
                new SqlParameter("@Email", Email),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
        }
        #endregion

        //EmployeeMaster
        #region EmployeeMaster
        public ActionResult LoadEmployeeGrid(int? page, String Name = null)
        {
            StaticPagedList<EmployeeList> itemsAsIPagedList;
            itemsAsIPagedList = EmployeeGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("EmployeeGrid", itemsAsIPagedList)
                    : View("EmployeeGrid", itemsAsIPagedList);
        }

        public ActionResult EmployeeList(int? page, String Name = null)
        {
            StaticPagedList<EmployeeList> itemsAsIPagedList;
            itemsAsIPagedList = EmployeeGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("EmployeeList", itemsAsIPagedList)
                    : View("EmployeeList", itemsAsIPagedList);
        }
        public StaticPagedList<EmployeeList> EmployeeGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            EmployeeList clist = new EmployeeList();

            IEnumerable<EmployeeList> result = _db.DFEmployeeLists.SqlQuery(@"exec GetEmployeeList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<EmployeeList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<EmployeeList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }
        public ActionResult AddEmployee(int id = 0)
        {
            ViewData["CityList"] = binddropdown("CityList", 0);
            Employee data = new Employee();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {

                var result = _db.EmployeeLists.SqlQuery(@"exec GetEmployeeDetails 
                @EmployeeID",
                 new SqlParameter("@EmployeeID", id)).ToList<Employee>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.EmployeeID = 0;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddEmployee", data)
                  : View("AddEmployee", data);
        }
        [HttpPost]
        public ActionResult SaveEmployee(int EmployeeID = 0, String EmployeeName = null, int CityID = 0, String Mobile = null, String Email = null, String IsActive = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveEmployee 
               @EmployeeID, @EmployeeName,@CityID,@Mobile,@Email,@CreatedBy,@IsActive",
                new SqlParameter("@EmployeeID", EmployeeID),
                new SqlParameter("@EmployeeName", EmployeeName),
                new SqlParameter("@CityID", CityID),
                new SqlParameter("@Mobile", Mobile),
                new SqlParameter("@Email", Email),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
        }

        #endregion

        public List<SelectListItem> binddropdown(string action, int val = 0 ,int StateID = 0)
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


        #region MemberMaster


        public ActionResult MemberList(int? page, String Name = null,int MemberTypeId = 0)
        {
            StaticPagedList<MemberList> itemsAsIPagedList;
            itemsAsIPagedList = MemberGrid(page, Name, MemberTypeId);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("MemberList", itemsAsIPagedList)
                    : View("MemberList", itemsAsIPagedList);
        }
        public StaticPagedList<MemberList> MemberGrid(int? page, String Name = "",int MemberTypeId = 0)
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            MemberList clist = new MemberList();

            IEnumerable<MemberList> result = _db.DFMemberList.SqlQuery(@"exec GetMember
                   @pPageIndex, @pPageSize,@CompanyName,@Membertype",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@CompanyName", Name == null ? (object)DBNull.Value : Name),
               new SqlParameter("@Membertype", MemberTypeId)
               ).ToList<MemberList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<MemberList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        public ActionResult LoadMemberGrid(int? page, String Name = null,int MemberTypeId = 0)
        {
            StaticPagedList<MemberList> itemsAsIPagedList;
            itemsAsIPagedList = MemberGrid(page, Name, MemberTypeId);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_MemberGrid", itemsAsIPagedList)
                    : View("_MemberGrid", itemsAsIPagedList);
        }



        public ActionResult GetPersonListByMemberType(int? MemberId)
        {
            OfficeDbContext _db = new OfficeDbContext();            

            IEnumerable<MemberPersonList> result = _db.DFMemberPersonList.SqlQuery(@"exec GetMemberPersons
               @MemberId",
               new SqlParameter("@MemberId", MemberId)              
               ).ToList<MemberPersonList>();

            return View("_GetMemberPersonList", result);

        }


        public ActionResult GetPersonListForUpdate(int? MemberId)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<MemberPersonList> result = _db.DFMemberPersonList.SqlQuery(@"exec GetMemberPersons
               @MemberId",
               new SqlParameter("@MemberId", MemberId)
               ).ToList<MemberPersonList>();

            return View("_MemberListForUpdate", result);

        }

        public ActionResult GetConactPersonByMemberType(int? MemberId)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<ContactPersonListByMember> result = _db.DFContactPersonListByMember.SqlQuery(@"exec GetContactPersonsByMember
               @MemberId",
               new SqlParameter("@MemberId", MemberId)
               ).ToList<ContactPersonListByMember>();

            return View("_GetMemberContactPerson", result);

        }


        [HttpPost]
        public ActionResult DeleteContactFromMember(int? Id, string ContactType = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec DeleteContactFromMember 
                 @Id,@ContactType ",
                new SqlParameter("@Id", Id),
                new SqlParameter("@ContactType", ContactType));

                return Json("Contact Deleted");
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }


        [HttpPost]
        public ActionResult DeleteContactPersonFromMember(int? Id)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec DeleteContactPersonFromMember 
                 @Id",
                new SqlParameter("@Id", Id));               

                return Json("Contact Person Deleted");
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }

        [HttpPost]
        public ActionResult DeleteMemberPersonFromMember(int? Id)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec DeleteMemberPersonFromMember 
                 @Id",
                new SqlParameter("@Id", Id));

                return Json("Member Person Deleted");
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }

        public ActionResult AddMember()
        {
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            ViewData["ConactPersonList"] = binddropdown("ConactPersonList", 0);
            ViewData["PersonList"] = binddropdown("PersonList", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AddMember")
                    : View("AddMember");
        }
        public ActionResult GetCompanyInfoLeftSide(int CompanyId = 0)
        {
            CompanyDetailsLeftSide data = new CompanyDetailsLeftSide();
            OfficeDbContext _db = new OfficeDbContext();
             

                IEnumerable<CompanyAddress> result1 = _db.CompanyAddress.SqlQuery(@"exec uspGetCompanyAddress
                @CompanyID",
                new SqlParameter("@CompanyID", CompanyId)
                ).ToList<CompanyAddress>();
                data.CompanyAddress = result1;

                IEnumerable<SaveCompanyMobile> result2 = _db.SaveCompanyMobile.SqlQuery(@"exec uspGetCompanyPhoneNo
                @CompanyID",
              new SqlParameter("@CompanyID", CompanyId)
              ).ToList<SaveCompanyMobile>();
                data.SaveCompanyMobile = result2;

            IEnumerable<SaveCompanyMobile> resultPhone = _db.SaveCompanyMobile.SqlQuery(@"exec uspGetTeamCompanyPhoneNo
                @CompanyID",
              new SqlParameter("@CompanyID", CompanyId)
              ).ToList<SaveCompanyMobile>();
            data.SaveCompanyMobile2 = resultPhone;

            IEnumerable<SaveCompanyMobile> resultSupportTeamPhone = _db.SaveCompanyMobile.SqlQuery(@"exec uspGetSupportTeamCompanyPhoneNo
                @CompanyID",
           new SqlParameter("@CompanyID", CompanyId)
           ).ToList<SaveCompanyMobile>();
            data.SaveCompanyMobile3 = resultSupportTeamPhone;

            IEnumerable<SaveCertification> result3 = _db.SaveCertification.SqlQuery(@"exec uspGetCompanyCertification
                @CompanyID",
             new SqlParameter("@CompanyID", CompanyId)
             ).ToList<SaveCertification>();
            data.SaveCertification = result3;

            IEnumerable<SaveInternalTeam> result4 = _db.SaveInternalTeam.SqlQuery(@"exec uspGetInternalTeam
                @CompanyID",
           new SqlParameter("@CompanyID", CompanyId)
           ).ToList<SaveInternalTeam>();
            data.SaveInternalTeam = result4;

            IEnumerable<SaveExternalTeam> result5 = _db.SaveExternalTeam.SqlQuery(@"exec uspGetExternalTeam
                @CompanyID",
          new SqlParameter("@CompanyID", CompanyId) 
          ).ToList<SaveExternalTeam>();
            data.SaveExternalTeam = result5;
            data.CompanyID = CompanyId;

                return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("PartialCompanyAddress", data)
                   : View("PartialCompanyAddress", data);
              
        }
        public ActionResult GetProjectInfoLeftSide(int ProjectID = 0)
        {
            OfficeDbContext _db = new OfficeDbContext();
            ProjectDetailsLeftSide data = new ProjectDetailsLeftSide();
                
            IEnumerable<SaveProjectInternalTeam> result  = _db.SaveProjectInternalTeam.SqlQuery(@"exec uspGetProjectInternalTeam
                @ProjectID",
           new SqlParameter("@ProjectID", ProjectID)
           ).ToList<SaveProjectInternalTeam>();
            data.SaveProjectInternalTeam = result ;

            IEnumerable<SaveProjectExternalTeam> result2 = _db.SaveProjectExternalTeam.SqlQuery(@"exec uspGetProjectExternalTeam
                @ProjectID",
          new SqlParameter("@ProjectID", ProjectID)
          ).ToList<SaveProjectExternalTeam>();

            IEnumerable<SaveProjectOfficeSideTeam> result3 = _db.SaveProjectOfficeSideTeam.SqlQuery(@"exec uspGetProjectOfficeSideTeam
                @ProjectID",
        new SqlParameter("@ProjectID", ProjectID)
        ).ToList<SaveProjectOfficeSideTeam>();

            IEnumerable<AuthoritySignatory> result4 = _db.AuthoritySignatory.SqlQuery(@"exec GetProjectSignatory
                @ProjectID",
        new SqlParameter("@ProjectID", ProjectID)
        ).ToList<AuthoritySignatory>();

            IEnumerable<AuthoritySignatoryDetail> result5 = _db.AuthoritySignatoryDetail.SqlQuery(@"exec GetProjectSignatoryDetail
                @ProjectID",
        new SqlParameter("@ProjectID", ProjectID)
        ).ToList<AuthoritySignatoryDetail>();
            
            data.SaveProjectOfficeSideTeam = result3;
            data.SaveProjectExternalTeam = result2;
            data.ProjectID = ProjectID;
            data.AuthoritySignatory = result4;
            data.AuthoritySignatoryDetail = result5;

            var result8 = _db.nProjectDetail.SqlQuery(@"exec uspGetnProjectDetails
                   @ProjectID",
                  new SqlParameter("@ProjectID", ProjectID)
                  ).ToList<nProjectDetail>();
            data.nProjectDetail = result8.FirstOrDefault();

            var result10 = _db.nSaveSurvayDetails.SqlQuery(@"exec uspGetnProjectSurveyDetails
                   @ProjectID",
                 new SqlParameter("@ProjectID", ProjectID)
                 ).ToList<nSaveSurvayDetails>();
            data.nSaveSurvayDetails = result10;

            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("ProjectInfoLeftSide",data)
               : View("ProjectInfoLeftSide",data);
        }
        public ActionResult GetProjectInfoAuthority(int ProjectID = 0 ,int OwnerID=0)
        {
            AuthoritySignatory s = new AuthoritySignatory();
            OfficeDbContext _db = new OfficeDbContext();
            ViewData["InternalTeamUnderProject"] = binddropdown("InternalTeamUnderProject", ProjectID);
            ViewData["MDocumentList"] = binddropdown("MDocumentList", 0);
            ViewData["UnitList"] = binddropdown("UnitList", 0);
            s.signatorId = 0;
            AuthoritySignatory s1 = _db.AuthoritySignatory.SqlQuery(@"exec GetProjectSignatory
                @ProjectID,@signatorId",
        new SqlParameter("@ProjectID", ProjectID),
        new SqlParameter("@signatorId", OwnerID)
        ).FirstOrDefault();

           if(s1!=null)
            {
                s = s1;
            }
            
            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("ProjectInfoAuthority", s)
               : View("ProjectInfoAuthority", s);
        }
        public ActionResult GetLeftSideForPerson(int PersonID = 0)
        {
            CompanyDetailsforPerson data = new CompanyDetailsforPerson();
            OfficeDbContext _db = new OfficeDbContext();
            try
            {
                IEnumerable<SaveCompanyList> result = _db.SaveCompanyList.SqlQuery(@"exec uspGetCompanyListForPerson
                @PersonID",
              new SqlParameter("@PersonID", PersonID)
              ).ToList<SaveCompanyList>();
                data.SaveCompanyList = result;
                data.PersonID = PersonID;

                IEnumerable<SaveCertificationPerson> result1 = _db.SaveCertificationPerson.SqlQuery(@"exec uspGetPersonCertification
                @PersonID",
                   new SqlParameter("@PersonID", PersonID)
                   ).ToList<SaveCertificationPerson>();
                data.SaveCertificationPerson = result1;

                IEnumerable<SaveCompanyMobile> resultPhone = _db.SaveCompanyMobile.SqlQuery(@"exec uspGetTeamPersonCompanyPhoneNo
                @PersonID",
             new SqlParameter("@PersonID", PersonID)
             ).ToList<SaveCompanyMobile>();
                data.SaveCompanyMobile = resultPhone; 

                IEnumerable<SaveSocialLink> resultSocialLink = _db.SaveSocialLink.SqlQuery(@"exec uspGetPersonSocialLink
                @PersonID",
             new SqlParameter("@PersonID", PersonID)
             ).ToList<SaveSocialLink>();
                data.SaveSocialLink = resultSocialLink;
            }
            catch (Exception ee) { }
            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("PersonLeftSide", data)
               : View("PersonLeftSide", data);

        }
        public ActionResult AddCompanyInfo(int id = 0, int i = 0)
        {
            CompanyDetails data = new CompanyDetails();
            OfficeDbContext _db = new OfficeDbContext();
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            ViewData["PersonList"] = binddropdown("PersonList", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);
            ViewData["OwnershipTypeList"] = binddropdown("OwnershipTypeList", 0);
            ViewData["BusinessCategoryList"] = binddropdown("BusinessCategoryList", 0);
            ViewData["BusinessSubCategoryList"] = binddropdown("BusinessSubCategoryList", 0);
            ViewData["CertificationList"] = binddropdown("CertificationList", 0);
            ViewData["CompanyRelationList"] = binddropdown("CompanyRelationList", 0);
            ViewData["CompanyList"] = binddropdown("CompanyList", 0);

            ViewData["TeamDesignationList"] = binddropdown("TeamDesignationList", 0);
            ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", 0);
            ViewData["TeamSubPartDesignationList"] = binddropdown("TeamSubPartDesignationList", 0);
            if (Request.IsAjaxRequest())
            {
                ViewBag.layout = "0";
            }
            else
            {
                ViewBag.layout = "1";
            }
            if (i > 0)
            {
                ViewData["redirect"] = 1;
            }
            else
            {
                ViewData["redirect"] = 0;
            }

           if (id > 0)
            {
                var result = _db.CompanyDetails.SqlQuery(@"exec uspGetCompanyDetails
                   @CompanyID",
                new SqlParameter("@CompanyID", id)
                ).ToList<CompanyDetails>();
                data = result.FirstOrDefault();
 

                return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("AddCompanyInfo", data)
                   : View("AddCompanyInfo", data);
            }

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AddCompanyInfo",data)
                    : View("AddCompanyInfo",data);
        }
        public ActionResult AddPersonInfo(int id=0, int i = 0)
        {
            PersonInfo data = new PersonInfo();
            OfficeDbContext _db = new OfficeDbContext();
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            //ViewData["ConactPersonList"] = binddropdown("ConactPersonList", 0);
            ViewData["PersonList"] = binddropdown("PersonList", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);

            ViewData["OwnershipTypeList"] = binddropdown("OwnershipTypeList", 0);
            ViewData["BusinessCategoryList"] = binddropdown("BusinessCategoryList", 0);
            ViewData["BusinessSubCategoryList"] = binddropdown("BusinessSubCategoryList", 0);
            ViewData["CertificationList"] = binddropdown("CertificationList", 0);
            ViewData["CompanyList"] = binddropdown("CompanyList", 0);
            ViewData["TeamDesignationList"] = binddropdown("TeamDesignationList", 0);
            ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", 0);
            ViewData["TeamSubPartDesignationList"] = binddropdown("TeamSubPartDesignationList", 0);
            if (i > 0)
            {
                ViewData["redirect"] = 1;
            }
            else
            {
                ViewData["redirect"] = 0;
            }
            if (id > 0)
            {
                var result = _db.PersonInfo.SqlQuery(@"exec [GetPersonDetailsForupdate]
                   @PersonID",
                new SqlParameter("@PersonID", id)
                ).ToList<PersonInfo>();
                data = result.FirstOrDefault();

                IEnumerable<SaveCompanyMobile> result2 = _db.SaveCompanyMobile.SqlQuery(@"exec uspGetPersonPhoneNo
                @PersonID",
           new SqlParameter("@PersonID", id)
           ).ToList<SaveCompanyMobile>();
               
                data.SaveCompanyMobile = result2;
            }

            return Request.IsAjaxRequest()
                 ? (ActionResult)PartialView("AddPersonInfo", data)
                 : View("AddPersonInfo", data);
        }
        [HttpPost]
        public ActionResult SaveCompanyOfficeAddress(int CompanyId,CompanyAddress Address, List<SaveCompanyMobile>   SaveMobileNo)
        {
            try
            {
                DataTable dtMobile = new DataTable();

                dtMobile.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobile.Columns.Add("PhoneType", typeof(int));
                dtMobile.Columns.Add("PhoneNumber", typeof(string));
                dtMobile.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobile.Columns.Add("Extension", typeof(string));
                dtMobile.Columns.Add("AdressID", typeof(int));
                // Adding Contact Person In DT
                if (SaveMobileNo != null)
                {
                    if (SaveMobileNo.Count > 0)
                    {
                        foreach (var item in SaveMobileNo)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;
                            dr_Mobile["Extension"] = item.Extension;
                            dr_Mobile["CompanyPhoneID"] = item.CompanyPhoneID == null ? 0 : item.CompanyPhoneID;
                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobile.Rows.Add(dr_Mobile);
                        }
                    }
                }


                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@CompanyMobileParam";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtMobile;
                tvpParamMobile.TypeName = "UTT_CompanyMobile";


                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec uspUpdateCompanyAddress
                 @CompanyId  
                ,@AddressID   
                ,@Address1  
                ,@Address2  
                ,@StateID  
                ,@District  
                ,@CityID  
                ,@ZipCode  
                ,@Website  
                ,@CompanyMobileParam
                ",
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@AddressID", Address.AddressID), 
                new SqlParameter("@Address1", Address.Address1 == null ? (object)DBNull.Value : Address.Address1),
                new SqlParameter("@Address2", Address.Address2 == null ? (object)DBNull.Value : Address.Address2),
                new SqlParameter("@StateID", Address.StateID == null ? (object)DBNull.Value : Address.StateID),
                new SqlParameter("@District", Address.District == null ? (object)DBNull.Value : Address.District),
                new SqlParameter("@CityID", Address.CityID == null ? (object)DBNull.Value : Address.CityID),
                new SqlParameter("@ZipCode", Address.ZipCode == null ? (object)DBNull.Value : Address.ZipCode),
                new SqlParameter("@Website", Address.Website == null ? (object)DBNull.Value : Address.Website)
                , tvpParamMobile

                );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }

        [HttpPost]
        public ActionResult DeleteCompanyOfficeAddress(int CompanyId, int AddressID)
        {
            try
            {
               

                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec uspdeleteCompanyAddress
                 @CompanyId  
                ,@AddressID  
                ",
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@AddressID", AddressID)  
                );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }

        [HttpPost]
        public ActionResult DeleteCompanyInternalTeam(int CompanyId, int InternalTeamId)
        {
            try
            { 
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec uspdeleteCompanyInternalTeam
                 @CompanyId  
                ,@InternalTeamId  
                ",
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@InternalTeamId", InternalTeamId)
                ); 
                return Json("Success"); 
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult DeleteCompanyExternalTeam(int CompanyId, int ExternalTeamId)
        {
            try
            { 
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec uspdeleteCompanyExternalTeam
                 @CompanyId  
                ,@ExternalTeamId  
                ",
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@ExternalTeamId", ExternalTeamId)
                ); 
                return Json("Success"); 
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult DeletePersonInternalTeam(int PersonId, int InternalTeamId)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec uspdeletePersonInternalTeam
                 @PersonId  
                ,@InternalTeamId  
                ",
                new SqlParameter("@PersonId", PersonId),
                new SqlParameter("@InternalTeamId", InternalTeamId)
                );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult SaveCompanyCertification(int CompanyId, SaveCertification Cert)
        {
            try
            { 
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdateCompanyCertification
                 @CompanyId  
                ,@CertificationID   
                ,@CompanyCertificationsDetailID  
                ,@Value  
                ",
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@CertificationID", Cert.CertificationID),
                new SqlParameter("@CompanyCertificationsDetailID", Cert.CompanyCertificationsDetailID),
                new SqlParameter("@Value", Cert.Value)
                );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult SavePersonCertification(int PersonId, SaveCertificationPerson Cert)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdatePersonCertification
                 @PersonId  
                ,@CertificationID   
                ,@PersonCertificationsDetailID  
                ,@Value  
                ",
                new SqlParameter("@PersonId", PersonId),
                new SqlParameter("@CertificationID", Cert.CertificationID),
                new SqlParameter("@PersonCertificationsDetailID", Cert.PersonCertificationsDetailID),
                new SqlParameter("@Value", Cert.Value)
                );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }

        [HttpPost]
        public ActionResult SaveSocialLink(int PersonID, int SocialLinkID, string SocialLinkText)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdateSocialLink
                 @PersonId  
                ,@SocialLinkId   
                ,@SocialLinkText   
                ",
                new SqlParameter("@PersonId", PersonID),
                new SqlParameter("@SocialLinkId", SocialLinkID),
                new SqlParameter("@SocialLinkText", SocialLinkText)  
                );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult SaveInternalTeam(int CompanyId, SaveInternalTeam InternalTeam, List<SaveCompanyMobile> SaveInternalTeamMobile) 
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();

                DataTable dtMobile = new DataTable();

                dtMobile.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobile.Columns.Add("PhoneType", typeof(int));
                dtMobile.Columns.Add("PhoneNumber", typeof(string));
                dtMobile.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobile.Columns.Add("Extension", typeof(string));
                dtMobile.Columns.Add("AdressID", typeof(int));
                // Adding Contact Person In DT
                if (SaveInternalTeamMobile != null)
                {
                    if (SaveInternalTeamMobile.Count > 0)
                    {
                        foreach (var item in SaveInternalTeamMobile)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;
                            dr_Mobile["Extension"] = item.Extension;
                            dr_Mobile["CompanyPhoneID"] = item.CompanyPhoneID == null ? 0 : item.CompanyPhoneID;
                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobile.Rows.Add(dr_Mobile);
                        }
                    }
                } 

                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@CompanyMobileParam";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtMobile;
                tvpParamMobile.TypeName = "UTT_CompanyMobile";

                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdateInternalTeam
                 @CompanyId  
                ,@internalTeamid   
                ,@internalpersonid  
                ,@designationid1
                ,@subdesignationid1 
                ,@subpartdesignationid1
                ,@CompanyMobileParam
                ",
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@internalTeamid", InternalTeam.internalTeamid),
                new SqlParameter("@internalpersonid", InternalTeam.internalpersonid),
                new SqlParameter("@designationid1", InternalTeam.designationid1),
                new SqlParameter("@subdesignationid1", InternalTeam.subdesignationid1),
                new SqlParameter("@subpartdesignationid1", InternalTeam.subpartdesignationid1),
               tvpParamMobile);

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }

        [HttpPost]
        public ActionResult SaveProjectInternalTeam(int ProjectID, SaveProjectInternalTeam InternalTeam)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext(); 
                DataTable dtMobile = new DataTable();
                  
                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdateProjectInternalTeam
                 @ProjectID  
                , @ParentCompanyID
                ,@internalTeamid   
                ,@internalpersonid  
                ,@designationid1
                ,@subdesignationid1 
                ,@subpartdesignationid1
                ",
                new SqlParameter("@ProjectID", ProjectID),
                 new SqlParameter("@ParentCompanyID", InternalTeam.parentcompanyid),
                new SqlParameter("@internalTeamid", InternalTeam.internalTeamid),
                new SqlParameter("@internalpersonid", InternalTeam.internalpersonid),
                new SqlParameter("@designationid1", InternalTeam.designationid1),
                new SqlParameter("@subdesignationid1", InternalTeam.subdesignationid1),
                new SqlParameter("@subpartdesignationid1", InternalTeam.subpartdesignationid1)
              );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult SaveProjectExternalTeam(int ProjectID, SaveProjectInternalTeam InternalTeam)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                DataTable dtMobile = new DataTable();

                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdateProjectExternalTeam
                 @ProjectID  
                , @ParentCompanyID
                ,@internalTeamid   
                ,@internalpersonid  
                ,@designationid1
                ,@subdesignationid1 
                ,@subpartdesignationid1
                ",
                new SqlParameter("@ProjectID", ProjectID),
                 new SqlParameter("@ParentCompanyID", InternalTeam.parentcompanyid),
                new SqlParameter("@internalTeamid", InternalTeam.internalTeamid),
                new SqlParameter("@internalpersonid", InternalTeam.internalpersonid),
                new SqlParameter("@designationid1", InternalTeam.designationid1),
                new SqlParameter("@subdesignationid1", InternalTeam.subdesignationid1),
                new SqlParameter("@subpartdesignationid1", InternalTeam.subpartdesignationid1)
              );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult SaveProjectOfficeTeam(int ProjectID, SaveProjectOfficeSideTeam OfficeSideTeam)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                DataTable dtMobile = new DataTable();

                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdateProjectofficeTeam
                 @ProjectID   
                ,@Teamid   
                ,@PersonId  
                ,@designationid1
                ,@subdesignationid1 
                ,@subpartdesignationid1
                ",
                new SqlParameter("@ProjectID", ProjectID), 
                new SqlParameter("@Teamid", OfficeSideTeam.Teamid),
                new SqlParameter("@PersonId", OfficeSideTeam.personid),
                new SqlParameter("@designationid1", OfficeSideTeam.designationid1),
                new SqlParameter("@subdesignationid1", OfficeSideTeam.subdesignationid1),
                new SqlParameter("@subpartdesignationid1", OfficeSideTeam.subpartdesignationid1)
              );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult SavePersonCompanyTeam(int PersonId, int CompanyId, int personCompanyid, int DesignationId1, int SubDesignationID1, int SubPartDesignationID1, List<SaveCompanyMobile> SaveInternalTeamMobile)
        {
          
            try
            {
                OfficeDbContext _db = new OfficeDbContext(); 
                DataTable dtMobile = new DataTable();
                DataTable dtCompanyList = new DataTable();
                dtMobile.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobile.Columns.Add("PhoneType", typeof(int));
                dtMobile.Columns.Add("PhoneNumber", typeof(string));
                dtMobile.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobile.Columns.Add("Extension", typeof(string));
                dtMobile.Columns.Add("AdressID", typeof(int));
                
                dtCompanyList.Columns.Add("Listid", typeof(int));
                dtCompanyList.Columns.Add("CompanyID", typeof(int));
                dtCompanyList.Columns.Add("designationid1", typeof(int));
                dtCompanyList.Columns.Add("subdesignationid1", typeof(int));
                dtCompanyList.Columns.Add("subpartdesignationid1", typeof(int));
                

                // Adding Contact Person In DT
                if (SaveInternalTeamMobile != null)
                {
                    if (SaveInternalTeamMobile.Count > 0)
                    {
                        foreach (var item in SaveInternalTeamMobile)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;
                            dr_Mobile["Extension"] = item.Extension;
                            dr_Mobile["CompanyPhoneID"] = item.CompanyPhoneID == null ? 0 : item.CompanyPhoneID;
                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobile.Rows.Add(dr_Mobile);
                        }
                    }
                }

                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@CompanyMobileParam";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtMobile;
                tvpParamMobile.TypeName = "UTT_CompanyMobile";

                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdatePersonCompany
                 @PersonID  
                 ,@CompanyId  
                ,@personCompanyid    
                ,@designationid1
                ,@subdesignationid1 
                ,@subpartdesignationid1
                ,@CompanyMobileParam
                ",
                new SqlParameter("@PersonID", PersonId),
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@personCompanyid", personCompanyid),
                new SqlParameter("@designationid1", DesignationId1),
                new SqlParameter("@subdesignationid1", SubDesignationID1),
                new SqlParameter("@subpartdesignationid1", SubPartDesignationID1),
               tvpParamMobile);

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }

        [HttpPost]
        public ActionResult SaveCompanySupportTeam(int CompanyId, SaveExternalTeam ExternalTeam , List<SaveCompanyMobile> SaveSupportTeamMobile)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                DataTable dtMobile = new DataTable();

                dtMobile.Columns.Add("CompanyPhoneID", typeof(string)); 
                dtMobile.Columns.Add("PhoneType", typeof(int));
                dtMobile.Columns.Add("PhoneNumber", typeof(string));
                dtMobile.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobile.Columns.Add("Extension", typeof(string));
                dtMobile.Columns.Add("AdressID", typeof(int));
                // Adding Contact Person In DT
                if (SaveSupportTeamMobile != null)
                {
                    if (SaveSupportTeamMobile.Count > 0)
                    {
                        foreach (var item in SaveSupportTeamMobile)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;
                            dr_Mobile["Extension"] = item.Extension;
                            dr_Mobile["CompanyPhoneID"] = item.CompanyPhoneID == null ? 0 : item.CompanyPhoneID;
                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobile.Rows.Add(dr_Mobile);
                        }
                    }
                }

                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@CompanyMobileParam";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtMobile;
                tvpParamMobile.TypeName = "UTT_CompanyMobile";

                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertUpdateExternalTeam
                 @CompanyId  
                ,@CategoryID   
                ,@SubCategoryID  
                ,@PersonID
                ,@Designationid 
                ,@SubdesignationId
                ,@subpartDesignationId
                ,@ExternalCompanyID 
                ,@ExternalTeamID
                ,@RelationID 
                ,@CompanyMobileParam
                ",
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@CategoryID", ExternalTeam.CategoryId),
                new SqlParameter("@SubCategoryID", ExternalTeam.SubCategoryId),
                new SqlParameter("@PersonID", ExternalTeam.ExternalPersonId),
                new SqlParameter("@Designationid", ExternalTeam.DesignationId),
                new SqlParameter("@SubdesignationId", ExternalTeam.SubDesignationId),
                new SqlParameter("@subpartDesignationId", ExternalTeam.SubpartDesignationId),
                new SqlParameter("@ExternalCompanyID", ExternalTeam.ExternalCompanyId),
                new SqlParameter("@ExternalTeamID", ExternalTeam.ExternalTeamid),
                new SqlParameter("@RelationID", ExternalTeam.RelationId) ,
                tvpParamMobile
                ); 
                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult SaveCompany(CompanyDetails Company,  List<SaveCompanyMobile> SaveMobile, List<SaveCompanyEmail> SaveEmail, List<CompanyAddress> SaveAddress , List<SaveInternalTeam> SaveInternalTeam, List<SaveCompanyMobile> SaveInternalTeamMobile, List<SaveExternalTeam> SaveExternalTeam , List<SaveCertification> SaveCertification ,  List<SaveCompanyMobile> SaveSupportTeamMobile)
        {
            try
            {
                DataTable dtMobile = new DataTable();
                DataTable dtMobileTeam = new DataTable();
                DataTable dtEmail = new DataTable();
                DataTable dtMobileSupportTeam = new DataTable();
                DataTable dtAddress = new DataTable();
                DataTable dtInternalTeam = new DataTable();
                DataTable dtExternalTeam = new DataTable(); 
                DataTable dtCertificaton = new DataTable();
                dtMobile.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobile.Columns.Add("PhoneType", typeof(int));
                dtMobile.Columns.Add("PhoneNumber", typeof(string)); 
                dtMobile.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobile.Columns.Add("Extension", typeof(string));
                dtMobile.Columns.Add("AdressID", typeof(int));

                dtMobileTeam.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobileTeam.Columns.Add("PhoneType", typeof(int));
                dtMobileTeam.Columns.Add("PhoneNumber", typeof(string));
                dtMobileTeam.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobileTeam.Columns.Add("Extension", typeof(string));
                dtMobileTeam.Columns.Add("AdressID", typeof(int));

                dtMobileSupportTeam.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobileSupportTeam.Columns.Add("PhoneType", typeof(int));
                dtMobileSupportTeam.Columns.Add("PhoneNumber", typeof(string));
                dtMobileSupportTeam.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobileSupportTeam.Columns.Add("Extension", typeof(string));
                dtMobileSupportTeam.Columns.Add("AdressID", typeof(int));


                dtAddress.Columns.Add("AddressID", typeof(int));
                dtAddress.Columns.Add("AddressType", typeof(string));
                dtAddress.Columns.Add("Address1", typeof(string));
                dtAddress.Columns.Add("Address2", typeof(string));
                dtAddress.Columns.Add("StateID", typeof(int));
                dtAddress.Columns.Add("District", typeof(string));
                dtAddress.Columns.Add("CityID", typeof(int));
                dtAddress.Columns.Add("ZipCode", typeof(string));
                dtAddress.Columns.Add("Website", typeof(string));

                dtInternalTeam.Columns.Add("internalpersonid", typeof(int));
                dtInternalTeam.Columns.Add("internalTeamid", typeof(int));
                dtInternalTeam.Columns.Add("designationid1", typeof(int));
                dtInternalTeam.Columns.Add("subdesignationid1", typeof(int));
                dtInternalTeam.Columns.Add("subpartdesignationid1", typeof(int));
                 
                dtExternalTeam.Columns.Add("ExternalTeamid", typeof(int));
                dtExternalTeam.Columns.Add("RelationId", typeof(int));
                dtExternalTeam.Columns.Add("CategoryId", typeof(int));
                dtExternalTeam.Columns.Add("SubCategoryId", typeof(int));
                dtExternalTeam.Columns.Add("ExternalCompanyId", typeof(int));
                dtExternalTeam.Columns.Add("ExternalPersonId", typeof(int));
                dtExternalTeam.Columns.Add("designationid", typeof(int));
                dtExternalTeam.Columns.Add("subdesignationid", typeof(int));
                dtExternalTeam.Columns.Add("subpartdesignationid", typeof(int)); 

                dtCertificaton.Columns.Add("CompanyCertificationsDetailID", typeof(int));
                dtCertificaton.Columns.Add("CertificationID", typeof(int));
                dtCertificaton.Columns.Add("Value", typeof(string));

                if (SaveCertification != null)
                {
                    if (SaveCertification.Count > 0)
                    {
                        foreach (var item in SaveCertification)
                        {
                            DataRow dr_Certification = dtCertificaton.NewRow();
                            dr_Certification["CompanyCertificationsDetailID"] = item.CompanyCertificationsDetailID;
                            dr_Certification["CertificationID"] = item.CertificationID;
                            dr_Certification["Value"] = item.Value; 
                            dtCertificaton.Rows.Add(dr_Certification);
                        }
                    }
                } 
                // Adding Contact Person In DT
                if (SaveMobile != null)
                {
                    if (SaveMobile.Count > 0)
                    {
                        foreach (var item in SaveMobile)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            if(item.Type==3)
                            {
                                dr_Mobile["Extension"] = "";
                            }
                            else
                            {
                                dr_Mobile["Extension"] = item.Extension;
                            }

                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;
                            
                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobile.Rows.Add(dr_Mobile);
                        }
                    }
                }

                if (SaveInternalTeamMobile != null)
                {
                    if (SaveInternalTeamMobile.Count > 0)
                    {
                        foreach (var item in SaveInternalTeamMobile)
                        {
                            DataRow dr_Mobile = dtMobileTeam.NewRow();
                            if (item.Type == 3)
                            {
                                dr_Mobile["Extension"] = "";
                            }
                            else
                            {
                                dr_Mobile["Extension"] = item.Extension;
                            }

                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;

                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobileTeam.Rows.Add(dr_Mobile);
                        }
                    }
                }

                if (SaveSupportTeamMobile != null)
                {
                    if (SaveSupportTeamMobile.Count > 0)
                    {
                        foreach (var item in SaveSupportTeamMobile)
                        {
                            DataRow dr_Mobile = dtMobileSupportTeam.NewRow();
                            if (item.Type == 3)
                            {
                                dr_Mobile["Extension"] = "";
                            }
                            else
                            {
                                dr_Mobile["Extension"] = item.Extension;
                            }

                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;

                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobileSupportTeam.Rows.Add(dr_Mobile);
                        }
                    }
                }
                if (SaveAddress != null)
                {
                    if (SaveAddress.Count > 0)
                    {
                        foreach (var item in SaveAddress)
                        {
                            DataRow dr_SaveAddress = dtAddress.NewRow();
                            dr_SaveAddress["AddressID"] = item.AddressID;
                            dr_SaveAddress["AddressType"] = 1;
                            dr_SaveAddress["Address1"] = item.Address1;
                            dr_SaveAddress["Address2"] = item.Address2 == null ? (object)DBNull.Value : item.Address2;
                            dr_SaveAddress["StateID"] = item.StateID == null ? (object)DBNull.Value : item.StateID;
                            dr_SaveAddress["District"] = item.District == null ? (object)DBNull.Value : item.District;
                            dr_SaveAddress["CityID"] = item.CityID == null ? (object)DBNull.Value : item.CityID;
                            dr_SaveAddress["ZipCode"] = item.ZipCode == null ? (object)DBNull.Value : item.ZipCode;
                            dr_SaveAddress["Website"] = "";

                            dtAddress.Rows.Add(dr_SaveAddress);
                        }
                    }
                } 
                if (SaveInternalTeam != null)
                {
                    if (SaveInternalTeam.Count > 0)
                    {
                        foreach (var item in SaveInternalTeam)
                        {
                            DataRow dr_InternalTeam = dtInternalTeam.NewRow();
                            dr_InternalTeam["internalpersonid"] = item.internalpersonid;
                            dr_InternalTeam["internalTeamid"] = item.internalTeamid;
                            dr_InternalTeam["designationid1"] = item.designationid1;
                            dr_InternalTeam["subdesignationid1"] = item.subdesignationid1;
                            dr_InternalTeam["subpartdesignationid1"] = item.subpartdesignationid1;
                            dtInternalTeam.Rows.Add(dr_InternalTeam);
                        }
                    }
                }
                if (SaveExternalTeam != null)
                {
                    if (SaveExternalTeam.Count > 0)
                    {
                        foreach (var item in SaveExternalTeam)
                        {
                            DataRow dr_ExternalTeam = dtExternalTeam.NewRow();
                            dr_ExternalTeam["ExternalPersonId"] = item.ExternalPersonId;
                            dr_ExternalTeam["ExternalTeamid"] = item.ExternalTeamid;
                            dr_ExternalTeam["RelationId"] = item.RelationId;
                            dr_ExternalTeam["CategoryId"] = item.CategoryId;
                            dr_ExternalTeam["SubCategoryId"] = item.SubCategoryId;
                            dr_ExternalTeam["ExternalCompanyId"] = item.ExternalCompanyId;
                            dr_ExternalTeam["ExternalPersonId"] = item.ExternalPersonId;
                            dr_ExternalTeam["DesignationId"] = item.DesignationId;
                            dr_ExternalTeam["SubDesignationId"] = item.SubDesignationId;
                            dr_ExternalTeam["SubpartDesignationId"] = item.SubpartDesignationId;
                            dtExternalTeam.Rows.Add(dr_ExternalTeam);
                        }
                    }
                }
                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@CompanyMobileParam";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtMobile;
                tvpParamMobile.TypeName = "UTT_CompanyMobile";

                SqlParameter tvpParamMobileTeam = new SqlParameter();
                tvpParamMobileTeam.ParameterName = "@CompanyMobileParamTeam";
                tvpParamMobileTeam.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobileTeam.Value = dtMobileTeam;
                tvpParamMobileTeam.TypeName = "UTT_CompanyMobile";

                SqlParameter tvpParamMobileSupportTeam = new SqlParameter();
                tvpParamMobileSupportTeam.ParameterName = "@CompanyMobileParamSupportTeam";
                tvpParamMobileSupportTeam.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobileSupportTeam.Value = dtMobileSupportTeam;
                tvpParamMobileSupportTeam.TypeName = "UTT_CompanyMobile";

                SqlParameter tvpParamAddress = new SqlParameter();
                tvpParamAddress.ParameterName = "@CompanyAddressParam";
                tvpParamAddress.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamAddress.Value = dtAddress;
                tvpParamAddress.TypeName = "UTT_CompanyAddress";

                SqlParameter tvpParamInternalTeam= new SqlParameter();
                tvpParamInternalTeam.ParameterName = "@InternalTeamParam";
                tvpParamInternalTeam.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamInternalTeam.Value = dtInternalTeam;
                tvpParamInternalTeam.TypeName = "UTT_InternalTeam";

                SqlParameter tvpParamExternalTeam = new SqlParameter();
                tvpParamExternalTeam.ParameterName = "@ExternalTeamParam";
                tvpParamExternalTeam.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamExternalTeam.Value = dtExternalTeam;
                tvpParamExternalTeam.TypeName = "UTT_ExternalTeam";

                SqlParameter tvpParamCertification = new SqlParameter();
                tvpParamCertification.ParameterName = "@CompanyCertificationParam";
                tvpParamCertification.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamCertification.Value = dtCertificaton;
                tvpParamCertification.TypeName = "UTT_CompanyCertification"; 

                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_SaveCompany
                 @CompanyID  
                ,@CompanyName  
                ,@ShortName 
                ,@BusinessCategoryID  
                ,@BusinessSubCategoryID  
                ,@CertificationID  
                ,@CompanyOwnershipTypeID  
                ,@Isclient  
                ,@RequestDate  
                ,@InceptionDate  
                ,@CreatedBy  
                ,@CompanyMobileParam
                ,@CompanyAddressParam
                ,@InternalTeamParam
                ,@ExternalTeamParam
                ,@CompanyCertificationParam
                ,@CompanyMobileParamTeam 
                ,@CompanyMobileParamSupportTeam 
                ",
                new SqlParameter("@CompanyID", Company.CompanyID),
                new SqlParameter("@CompanyName", Company.CompanyName), 
                new SqlParameter("@ShortName", Company.ShortName),
                new SqlParameter("@BusinessCategoryID", Company.CategoryID1),
                new SqlParameter("@BusinessSubCategoryID", Company.SubCategoryID1),
                new SqlParameter("@CertificationID", Company.CertificationID),
                new SqlParameter("@CompanyOwnershipTypeID", Company.CompanyOwnershipTypeID),
                new SqlParameter("@Isclient", Company.Isclient),
                new SqlParameter("@RequestDate", Company.RequestDate == null ? (object)DBNull.Value : Company.RequestDate),
                new SqlParameter("@InceptionDate", Company.InceptionDate == null ? (object)DBNull.Value : Company.InceptionDate),
                new SqlParameter("@CreatedBy", 1)  
                , tvpParamMobile 
                , tvpParamAddress 
                , tvpParamInternalTeam
                ,tvpParamExternalTeam
                ,tvpParamCertification
                , tvpParamMobileTeam
                 , tvpParamMobileSupportTeam
                );
                return Json("Success");
            }
            catch (Exception ex)
            { 
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message); 
            }
        }
        
        [HttpPost]
        public ActionResult SavePersonInfo(PersonInfo Mem, List<SaveCompanyMobile> SaveMobile, List<SaveAdditionalFiled> SaveAdditionalFiled, List<SaveParameter> SaveParameter, List<SaveCompanyList> SaveCompanyList, List<SaveCertificationPerson> SaveCertification , List<SaveCompanyMobile> SaveTeamMobile, List<SaveSocialLink> SaveSocialLink)
        {
            try
            { 
                 
                DataTable dtMobile = new DataTable();
                DataTable dtMobileTeam = new DataTable();
                DataTable dtAdditionalFiled = new DataTable();
                DataTable dtParameter = new DataTable();
                DataTable dtMemberPerson = new DataTable();
                DataTable dtCompanyList = new DataTable();
                DataTable dtCertificaton = new DataTable();

                DataTable dtSociallink = new DataTable();
                dtMobile.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobile.Columns.Add("PhoneType", typeof(int));
                dtMobile.Columns.Add("PhoneNumber", typeof(string));
                dtMobile.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobile.Columns.Add("Extension", typeof(string));
                dtMobile.Columns.Add("AdressID", typeof(int));

                dtMobileTeam.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobileTeam.Columns.Add("PhoneType", typeof(int));
                dtMobileTeam.Columns.Add("PhoneNumber", typeof(string));
                dtMobileTeam.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobileTeam.Columns.Add("Extension", typeof(string));
                dtMobileTeam.Columns.Add("AdressID", typeof(int));

                dtAdditionalFiled.Columns.Add("AdditionlField", typeof(string));
                dtParameter.Columns.Add("Parameter", typeof(string));
                dtMemberPerson.Columns.Add("PersonMemberID", typeof(int));
                dtMemberPerson.Columns.Add("DesignationId", typeof(int));


                dtCompanyList.Columns.Add("Listid", typeof(int));
                dtCompanyList.Columns.Add("CompanyID", typeof(int));
                dtCompanyList.Columns.Add("designationid1", typeof(int));
                dtCompanyList.Columns.Add("subdesignationid1", typeof(int));
                dtCompanyList.Columns.Add("subpartdesignationid1", typeof(int));

                dtCertificaton.Columns.Add("PersonCertificationsDetailID", typeof(int));
                dtCertificaton.Columns.Add("CertificationID", typeof(int));
                dtCertificaton.Columns.Add("Value", typeof(string));
                 
                dtSociallink.Columns.Add("SociallLinkId", typeof(int));
                dtSociallink.Columns.Add("SocialLinkText", typeof(string));
                
                if (SaveMobile != null)
                {
                    if (SaveMobile.Count > 0)
                    {
                        foreach (var item in SaveMobile)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            if (item.Type == 3)
                            {
                                dr_Mobile["Extension"] = "";
                            }
                            else
                            {
                                dr_Mobile["Extension"] = item.Extension;
                            }
                            if(item.CompanyPhoneID==null)
                            {
                                dr_Mobile["CompanyPhoneID"] = 0;
                            }
                            else
                            {
                                dr_Mobile["CompanyPhoneID"] = item.CompanyPhoneID;
                            }
                            
                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;

                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobile.Rows.Add(dr_Mobile);
                        }
                    }
                }
                if (SaveTeamMobile != null)
                {
                    if (SaveTeamMobile.Count > 0)
                    {
                        foreach (var item in SaveTeamMobile)
                        {
                            DataRow dr_Mobile = dtMobileTeam.NewRow();
                            if (item.Type == 3)
                            {
                                dr_Mobile["Extension"] = "";
                            }
                            else
                            {
                                dr_Mobile["Extension"] = item.Extension;
                            }

                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;

                            dr_Mobile["AdressID"] = item.AddressID;
                            dtMobileTeam.Rows.Add(dr_Mobile);
                        }
                    }
                }

                // Adding Additional Field In DT
                if (SaveAdditionalFiled != null)
                {
                    if (SaveAdditionalFiled.Count > 0)
                    {
                        foreach (var item in SaveAdditionalFiled)
                        {
                            DataRow dr_AdditionField = dtAdditionalFiled.NewRow();
                            dr_AdditionField["AdditionlField"] = item.AdditionlField;
                            dtAdditionalFiled.Rows.Add(dr_AdditionField);
                        }
                    }
                }

                if (SaveCompanyList != null)
                {
                    if (SaveCompanyList.Count > 0)
                    {
                        foreach (var item in SaveCompanyList)
                        {
                            DataRow dr_CompanyList = dtCompanyList.NewRow();
                            dr_CompanyList["CompanyID"] = item.CompanyID;
                            dr_CompanyList["Listid"] = item.Listid;
                            dr_CompanyList["designationid1"] = item.designationid1;
                            dr_CompanyList["subdesignationid1"] = item.subdesignationid1;
                            dr_CompanyList["subpartdesignationid1"] = item.subpartdesignationid1;
                            dtCompanyList.Rows.Add(dr_CompanyList);
                        }
                    }
                }

                if (SaveCertification != null)
                {
                    if (SaveCertification.Count > 0)
                    {
                        foreach (var item in SaveCertification)
                        {
                            DataRow dr_Certification = dtCertificaton.NewRow();
                            dr_Certification["PersonCertificationsDetailID"] = item.PersonCertificationsDetailID;
                            dr_Certification["CertificationID"] = item.CertificationID;
                            dr_Certification["Value"] = item.Value;
                            dtCertificaton.Rows.Add(dr_Certification);
                        }
                    }
                }
                if (SaveSocialLink != null)
                {
                    if (SaveSocialLink.Count > 0)
                    {
                        foreach (var item in SaveSocialLink)
                        {
                            DataRow dr_SociallLink = dtSociallink.NewRow();
                            if (item.SocialLinkId == null)
                            { dr_SociallLink["SociallLinkId"] = 0; }
                            else
                            {
                                dr_SociallLink["SociallLinkId"] = item.SocialLinkId;
                            }
                            dr_SociallLink["SocialLinkText"] = item.SocialLinkText; 
                            dtSociallink.Rows.Add(dr_SociallLink);
                        }
                    }
                }
                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@PersonMobileParam";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtMobile;
                tvpParamMobile.TypeName = "UTT_CompanyMobile";

                SqlParameter tvpParamMobileTeam = new SqlParameter();
                tvpParamMobileTeam.ParameterName = "@PersonMobileParamTeam";
                tvpParamMobileTeam.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobileTeam.Value = dtMobileTeam;
                tvpParamMobileTeam.TypeName = "UTT_CompanyMobile"; 

                SqlParameter tvpParamAdditionalFeild = new SqlParameter();
                tvpParamAdditionalFeild.ParameterName = "@MemberAdditionalFeildParam ";
                tvpParamAdditionalFeild.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamAdditionalFeild.Value = dtAdditionalFiled;
                tvpParamAdditionalFeild.TypeName = "UT_MemberAdditionField";

                SqlParameter tvpParamParameter = new SqlParameter();
                tvpParamParameter.ParameterName = "@MemberParameterParam";
                tvpParamParameter.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamParameter.Value = dtParameter;
                tvpParamParameter.TypeName = "UT_MemberParameter";
                
                SqlParameter tvpParamCompanyList = new SqlParameter();
                tvpParamCompanyList.ParameterName = "@MemberParameterCompanyList";
                tvpParamCompanyList.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamCompanyList.Value = dtCompanyList;
                tvpParamCompanyList.TypeName = "UTT_CompanyList";

                SqlParameter tvpParamCertification = new SqlParameter();
                tvpParamCertification.ParameterName = "@CompanyCertificationParam";
                tvpParamCertification.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamCertification.Value = dtCertificaton;
                tvpParamCertification.TypeName = "UTT_CompanyCertification";

                SqlParameter tvpParamSocialLink = new SqlParameter();
                tvpParamSocialLink.ParameterName = "@SocialLinkParam";
                tvpParamSocialLink.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamSocialLink.Value = dtSociallink; 
                tvpParamSocialLink.TypeName = "UTT_PersonSocialLink";

                //DateTime? dBirthDate = Mem.BirthDate.Value;
                //if(dBirthDate.Value.Year==1)
                //{
                //    Mem.BirthDate = null;
                //}
                //DateTime? DCreatedDate = Mem.CreatedDate.Value;
                //if (DCreatedDate.Value.Year == 1)
                //{
                //    Mem.CreatedDate = null;
                //}

                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_SavePersonInfo
                 @PersonID
                ,@TitleID
                ,@fName  
                ,@mName
                ,@lName
                ,@ShortName 
                ,@Website
                ,@Address1
                ,@Address2
                ,@StateID
                ,@District
                ,@CityID
                ,@ZipCode
                ,@BirthDate
                ,@CreatedBy
                ,@CreatedDate
                ,@PersonMobileParam  
                ,@MemberAdditionalFeildParam
                ,@MemberParameterParam
                ,@MemberParameterCompanyList
                ,@CompanyCertificationParam
                ,@PersonMobileParamTeam  
                ,@SocialLinkParam  

              ",
                new SqlParameter("@PersonId", Mem.PersonID == null ? 0 : Mem.PersonID), 
                new SqlParameter("@TitleID", Mem.TitleID),
                new SqlParameter("@fName", Mem.fName == null ? (object)DBNull.Value : Mem.fName),
                new SqlParameter("@mName", Mem.mName == null ? (object)DBNull.Value : Mem.mName),
                new SqlParameter("@lName", Mem.lName == null ? (object)DBNull.Value : Mem.lName),
                new SqlParameter("@ShortName", Mem.ShortName == null ? (object)DBNull.Value : Mem.ShortName),
                new SqlParameter("@Website", Mem.Website == null ? (object)DBNull.Value : Mem.Website),  
                new SqlParameter("@Address1", Mem.ResidentialAddress1 == null ? (object)DBNull.Value : Mem.ResidentialAddress1),
                new SqlParameter("@Address2", Mem.ResidentialAddress2 == null ? (object)DBNull.Value : Mem.ResidentialAddress2),
                new SqlParameter("@StateID", Mem.ResidentialStateID == null ? (object)DBNull.Value : Mem.ResidentialStateID),
                new SqlParameter("@District", Mem.ResidentialDistrict == null ? (object)DBNull.Value : Mem.ResidentialDistrict),
                new SqlParameter("@CityID", Mem.ResidentialCityID == null ? (object)DBNull.Value : Mem.ResidentialCityID),
                new SqlParameter("@ZipCode", Mem.ResidentialZipCode == null ? (object)DBNull.Value : Mem.ResidentialZipCode),
                new SqlParameter("@BirthDate", Mem.BirthDate == null ? (object)DBNull.Value : Mem.BirthDate), 
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@CreatedDate", Mem.CreatedDate == null ? (object)DBNull.Value : Mem.CreatedDate)
                , tvpParamMobile
                , tvpParamAdditionalFeild
                , tvpParamParameter
               , tvpParamCompanyList
               ,tvpParamCertification
               , tvpParamMobileTeam
               ,tvpParamSocialLink
                );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }

        [HttpPost]
        public ActionResult SaveMember(Member Mem, List<SaveConactPerson> SaveConactPerson, List<SaveMobile> SaveMobile, List<SavePhone> SavePhone, List<SaveOfficeNo> SaveOfficeNo, List<SaveEmail> SaveEmail, List<SaveAdditionalFiled> SaveAdditionalFiled, List<SaveParameter> SaveParameter, List<SaveMemberPerson> SaveMemberPerson)
        {
            try
            {
                   
                DataTable dtContactPerson = new DataTable();
                DataTable dtMobile = new DataTable();
                DataTable dtPhone = new DataTable();
                DataTable dtOffice = new DataTable();
                DataTable dtEmail = new DataTable();
                DataTable dtAdditionalFiled = new DataTable();
                DataTable dtParameter = new DataTable();
                DataTable dtMemberPerson = new DataTable();

                dtContactPerson.Columns.Add("ContactPersonId", typeof(int));
                dtMobile.Columns.Add("Mobile", typeof(string));
                dtPhone.Columns.Add("Phone", typeof(string));
                dtOffice.Columns.Add("OfficeNo", typeof(string));
                dtEmail.Columns.Add("Email", typeof(string));
                dtAdditionalFiled.Columns.Add("AdditionlField", typeof(string));
                dtParameter.Columns.Add("Parameter", typeof(string));
                dtMemberPerson.Columns.Add("PersonMemberID", typeof(int));
                dtMemberPerson.Columns.Add("DesignationId", typeof(int));

                // Adding Contact Person In DT
                if (SaveConactPerson != null)
                {
                    if (SaveConactPerson.Count > 0)
                    {
                        foreach (var item in SaveConactPerson)
                        {
                            DataRow dr_contactPerson = dtContactPerson.NewRow();
                            dr_contactPerson["ContactPersonId"] = item.ContactPersonId;
                            dtContactPerson.Rows.Add(dr_contactPerson);
                        }
                    }
                }

                // Adding Contact Person In DT
                if (SaveMobile != null)
                {
                    if (SaveMobile.Count > 0)
                    {
                        foreach (var item in SaveMobile)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            dr_Mobile["Mobile"] = item.Mobile;
                            dtMobile.Rows.Add(dr_Mobile);
                        }
                    }
                }

                // Adding Phone In DT
                if (SavePhone != null)
                {
                    if (SavePhone.Count > 0)
                    {
                        foreach (var item in SavePhone)
                        {
                            DataRow dr_Phone = dtPhone.NewRow();
                            dr_Phone["Phone"] = item.Phone;
                            dtPhone.Rows.Add(dr_Phone);
                        }
                    }
                }

                // Adding OfficeNo In DT
                if (SaveOfficeNo != null)
                {
                    if (SaveOfficeNo.Count > 0)
                    {
                        foreach (var item in SaveOfficeNo)
                        {
                            DataRow dr_OfficeNo = dtOffice.NewRow();
                            dr_OfficeNo["OfficeNo"] = item.OfficeNo;
                            dtOffice.Rows.Add(dr_OfficeNo);
                        }
                    }
                }


                // Adding Email In DT
                if (SaveEmail != null)
                {
                    if (SaveEmail.Count > 0)
                    {
                        foreach (var item in SaveEmail)
                        {
                            DataRow dr_SaveEmail = dtEmail.NewRow();
                            dr_SaveEmail["Email"] = item.Email;
                            dtEmail.Rows.Add(dr_SaveEmail);
                        }
                    }
                }

                // Adding Additional Field In DT
                if (SaveAdditionalFiled != null)
                {
                    if (SaveAdditionalFiled.Count > 0)
                    {
                        foreach (var item in SaveAdditionalFiled)
                        {
                            DataRow dr_AdditionField = dtAdditionalFiled.NewRow();
                            dr_AdditionField["AdditionlField"] = item.AdditionlField;
                            dtAdditionalFiled.Rows.Add(dr_AdditionField);
                        }
                    }
                }

                // Adding Member In DT
                if (SaveMemberPerson != null)
                {
                    if (SaveMemberPerson.Count > 0)
                    {
                        foreach (var item in SaveMemberPerson)
                        {
                            DataRow dr_MemberPerson = dtMemberPerson.NewRow();
                            dr_MemberPerson["PersonMemberID"] = item.PersonMemberID;
                            dr_MemberPerson["DesignationId"] = item.DesignationId;
                            dtMemberPerson.Rows.Add(dr_MemberPerson);
                        }
                    }
                }

                
                SqlParameter tvpParamContactPerson = new SqlParameter();
                tvpParamContactPerson.ParameterName = "@MemberContactPersonParam";
                tvpParamContactPerson.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamContactPerson.Value = dtContactPerson;
                tvpParamContactPerson.TypeName = "UT_MemberContactPerson";


                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@MemberMobileParam";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtMobile;
                tvpParamMobile.TypeName = "UT_MemberMobile";

                SqlParameter tvpParamPhone = new SqlParameter();
                tvpParamPhone.ParameterName = "@MemberPhoneParam ";
                tvpParamPhone.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamPhone.Value = dtPhone;
                tvpParamPhone.TypeName = "UT_MemberPhone";

                SqlParameter tvpParamOfffice = new SqlParameter();
                tvpParamOfffice.ParameterName = "@MemberOfficeNoParam  ";
                tvpParamOfffice.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamOfffice.Value = dtOffice;
                tvpParamOfffice.TypeName = "UT_MemberOfficeNo";

                SqlParameter tvpParamEmail = new SqlParameter();
                tvpParamEmail.ParameterName = "@MemberEmailParam";
                tvpParamEmail.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamEmail.Value = dtEmail;
                tvpParamEmail.TypeName = "UT_MemberEmail";

                SqlParameter tvpParamAdditionalFeild = new SqlParameter();
                tvpParamAdditionalFeild.ParameterName = "@MemberAdditionalFeildParam ";
                tvpParamAdditionalFeild.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamAdditionalFeild.Value = dtAdditionalFiled;
                tvpParamAdditionalFeild.TypeName = "UT_MemberAdditionField";

                SqlParameter tvpParamParameter = new SqlParameter();
                tvpParamParameter.ParameterName = "@MemberParameterParam";
                tvpParamParameter.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamParameter.Value = dtParameter;
                tvpParamParameter.TypeName = "UT_MemberParameter";
                
                    
                SqlParameter tvpParamMemberPerson = new SqlParameter();
                tvpParamMemberPerson.ParameterName = "@MemberPersonParam";
                tvpParamMemberPerson.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMemberPerson.Value = dtMemberPerson;
                tvpParamMemberPerson.TypeName = "UT_MemberPerson";


                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_SaveMember
                 @MemberId
                ,@MemberType
                ,@TypeId
                ,@TitleID
                ,@Name
                ,@ShortName
                ,@CompanyName
                ,@DesignationId
                ,@Website
                ,@ResidentialAddress1	
                ,@ResidentialAddress2	
                ,@ResidentialStateID	
                ,@ResidentialDistrict	
                ,@ResidentialCityID	  
                ,@ResidentialZipCode	
                ,@OfficeAddress1
                ,@OfficeAddress2
                ,@StateID
                ,@District
                ,@CityID
                ,@ZipCode
                ,@BirthDate
                ,@ShippingAddress
                ,@PowerofAttorny
                ,@CreatedBy
                ,@MemberContactPersonParam
                ,@MemberMobileParam                  
                ,@MemberPhoneParam 
                ,@MemberOfficeNoParam 
                ,@MemberEmailParam 
                ,@MemberAdditionalFeildParam
                ,@MemberParameterParam
                ,@MemberPersonParam",
                new SqlParameter("@MemberId", Mem.MemberID),
                new SqlParameter("@MemberType",Mem.MemberType),
                new SqlParameter("@TypeId", Mem.TypeId == null ? (object)DBNull.Value : Mem.TypeId),
                new SqlParameter("@TitleID",Mem.TitleID),
                new SqlParameter("@Name",Mem.Name == null ? (object)DBNull.Value : Mem.Name),
                new SqlParameter("@ShortName",Mem.ShortName),
                new SqlParameter("@CompanyName",Mem.CompanyName),
                new SqlParameter("@DesignationId",Mem.DesignationId == null ? (object)DBNull.Value : Mem.DesignationId),
                new SqlParameter("@Website",Mem.Website),
                new SqlParameter("@ResidentialAddress1",Mem.ResidentialAddress1 == null ? (object)DBNull.Value : Mem.ResidentialAddress1),
                new SqlParameter("@ResidentialAddress2",Mem.ResidentialAddress2 == null ? (object)DBNull.Value : Mem.ResidentialAddress2),
                new SqlParameter("@ResidentialStateID",Mem.ResidentialStateID == null ? (object)DBNull.Value : Mem.ResidentialStateID),
                new SqlParameter("@ResidentialDistrict",Mem.ResidentialDistrict == null ? (object)DBNull.Value : Mem.ResidentialDistrict),
                new SqlParameter("@ResidentialCityID", Mem.ResidentialCityID == null ? (object)DBNull.Value : Mem.ResidentialCityID),
                new SqlParameter("@ResidentialZipCode",  Mem.ResidentialZipCode == null ? (object)DBNull.Value : Mem.ResidentialZipCode),
                new SqlParameter("@OfficeAddress1",Mem.OfficeAddress1 == null ? (object)DBNull.Value : Mem.OfficeAddress1),
                new SqlParameter("@OfficeAddress2",Mem.OfficeAddress2 == null ? (object)DBNull.Value : Mem.OfficeAddress2),
                new SqlParameter("@StateID",Mem.StateID == null ? (object)DBNull.Value : Mem.StateID),
                new SqlParameter("@District",Mem.District == null ? (object)DBNull.Value : Mem.District),
                new SqlParameter("@CityID",Mem.CityID == null ? (object)DBNull.Value : Mem.CityID),
                new SqlParameter("@ZipCode",Mem.ZipCode == null ? (object)DBNull.Value : Mem.ZipCode),
                new SqlParameter("@BirthDate",Mem.BirthDate),
                new SqlParameter("@ShippingAddress",Mem.ShippingAddress),
                new SqlParameter("@PowerofAttorny",Mem.PowerofAttorny),
                new SqlParameter("@CreatedBy",1)
                ,tvpParamContactPerson
                ,tvpParamMobile
                ,tvpParamPhone
                ,tvpParamOfffice
                ,tvpParamEmail
                ,tvpParamAdditionalFeild
                ,tvpParamParameter
                ,tvpParamMemberPerson
                );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }
        [HttpGet]
        public JsonResult GetCompanyName( )
        {
            var lstitem = binddropdown("CompanyList", 0).Select(i => new { i.Value, i.Text }).ToList().FirstOrDefault();
            return Json(lstitem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CategoryWiseSubCategory1(int CategoryID = 0, int val = 0)
        {
            var lstitem = binddropdown("BusinessSubCategoryList", CategoryID).Select(i => new { i.Value, i.Text }).ToList().FirstOrDefault();
            return Json(lstitem, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetInternalTeamUnderCompany(int CompanyID = 0)
        { 
            ViewData["InternalTeamUnderCompany"] = binddropdown("InternalTeamUnderCompany", CompanyID);            
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("InternalTeamUnderCompany")
                    : View("InternalTeamUnderCompany");
        }
        public ActionResult GetInternalTeamUnderCompanyForSignatory(int CompanyID = 0, int val = 0)
        {
            ViewBag.value = val; 

            ViewData["InternalTeamUnderCompany"] = binddropdown("InternalTeamUnderCompany", CompanyID);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("InternalTeamUnderCompanyForSignatory")
                    : View("InternalTeamUnderCompanyForSignatory");
        } 
        public ActionResult GetExternalTeamUnderCompany(int CompanyID = 0)
        {
            ViewData["ExternalTeamUnderCompany"] = binddropdown("ExternalTeamUnderCompany", CompanyID);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ExternalTeamUnderCompany")
                    : View("ExternalTeamUnderCompany");
        }
        public ActionResult FliterStateWiseCity(int StateID = 0)
        {
            ViewData["CityList"] = binddropdown("StateWiseCityList", 0, StateID);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_StateWsieCityFilter")
                    : View("_StateWsieCityFilter");
        }
        public ActionResult CategoryWiseSubCategory(int CategoryID = 0, int val = 0)
        {
            if (val == 0)
            {
                ViewBag.value = 1;
                 
            }
            else
            {
                ViewBag.value = 2;
            }

            ViewData["BusinessSubCategoryList"] = binddropdown("BusinessSubCategoryList",  CategoryID);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_SubCategory")
                    : View("_SubCategory");
        }
        public ActionResult GetSurveyNoByTypeID(int ProjectID=0 ,int SurveyNoTypeID = 0) 
        {
            ViewData["SurveyNoListByTypeId"] = binddropdown("SurveyNoListByTypeId", ProjectID,SurveyNoTypeID);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("SurveyNoByTypeId")
                    : View("SurveyNoByTypeId");
        }
        public ActionResult BindTeamSubDesignation(int DesignationID = 0,int val=0)
        {
            if (val == 0)
            {
                ViewBag.value = 1; 
            }
            if (val == 3)
            {
                ViewBag.value = 3;
            }
            else
            {
                ViewBag.value = 2;
            }
            ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", DesignationID);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("SubDesignationList")
                    : View("SubDesignationList");
        }
        public ActionResult BindTeamSubPartDesignation(int SubDesignationID = 0, int val = 0)
        {
            if (val == 0)
            {
                ViewBag.value = 1; 
            }
            if (val == 3)
            {
                ViewBag.value = 3;
            }
            else
            {
                ViewBag.value = 2;
            }
            ViewData["TeamSubPartDesignationList"] = binddropdown("TeamSubPartDesignationList", SubDesignationID);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("SubPartDesignationList")
                    : View("SubDesignationList");
        }
        // Getting Member Details For Update
        public ActionResult GetMemberDetailsForUpdate(int? MemberId)
        {
            OfficeDbContext _db = new OfficeDbContext();

            var result = _db.DFMember.SqlQuery(@"exec GetMemberDetailsForUpdate
               @MemberId",
               new SqlParameter("@MemberId", MemberId)
               ).ToList<Member>();
            Member data = new Member();
            data = result.FirstOrDefault();
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            ViewData["ConactPersonList"] = binddropdown("ConactPersonList", 0);
            ViewData["PersonList"] = binddropdown("PersonList", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);
            string Dob = data.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string Createddate = data.CreatedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            ViewBag.Dob = Dob;
            ViewBag.Createddate = Createddate;
            return View("EditMember", data);

        }


        // Getting Member conact Details For Update
        public ActionResult GetMemberConactDetails(int? MemberId)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<MemberContactDetails> result = _db.DFMemberContactDetails.SqlQuery(@"exec GetMemberWiseContactDetailsForUpdate
               @MemberId",
               new SqlParameter("@MemberId", MemberId)
               ).ToList<MemberContactDetails>();          
            return View("_MemberWiseContactDetailsList", result);

        }

        

        #endregion

        #region Person
        public ActionResult PersonList(int? page, String Name = null)
        {
            StaticPagedList<PersonList> itemsAsIPagedList;
            itemsAsIPagedList = PersonGrid(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("PersonList", itemsAsIPagedList)
                    : View("PersonList", itemsAsIPagedList);
        }
        public StaticPagedList<PersonList> PersonGrid(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            PersonList clist = new PersonList();

            IEnumerable<PersonList> result = _db.DFPersonList.SqlQuery(@"exec GetPerson
                   @pPageIndex, @pPageSize,@FirstName",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@FirstName", Name == null ? (object)DBNull.Value : Name)
               ).ToList<PersonList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<PersonList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        public ActionResult LoadPersonGrid(int? page, String Name = null)
        {
            StaticPagedList<PersonList> itemsAsIPagedList;
            itemsAsIPagedList = PersonGrid(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_PersonGrid", itemsAsIPagedList)
                    : View("_PersonGrid", itemsAsIPagedList);
        }

        //public ActionResult AddPerson(int? id)
        //{
        //    ViewData["DesignationList"] = binddropdown("DesignationList", 0);
        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("AddPerson")
        //            : View("AddPerson");
        //}

        // Getting Person Details For Update
        //public ActionResult EditPerson(int? Id)
        //{
        //    OfficeDbContext _db = new OfficeDbContext();

        //    var result = _db.DFPerson.SqlQuery(@"exec GetPersonDetailsForupdate
        //       @Id",
        //       new SqlParameter("@Id", Id)
        //       ).ToList<Person>();

        //    Person data = new Person();
        //    data = result.FirstOrDefault();            
        //    string Dob = data.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);            
        //    ViewBag.Dob = Dob;
        //    ViewData["DesignationList"] = binddropdown("DesignationList", 0);
        //    return View("EditPerson", data);

        //}

        //public ActionResult GetPersonMobileList(int? Id)
        //{
        //    OfficeDbContext _db = new OfficeDbContext();

        //    IEnumerable<PersonMobileList> result = _db.DFPersonMobileList.SqlQuery(@"exec GetContactPersonMobileList
        //       @Id",
        //       new SqlParameter("@Id", Id)
        //       ).ToList<PersonMobileList>();

        //    return View("_GetContactPersonMobileList", result);

        //}

        //public ActionResult GetPersonEmailList(int? Id)
        //{
        //    OfficeDbContext _db = new OfficeDbContext();

        //    IEnumerable<PersonEmailList> result = _db.DFPersonEmailList.SqlQuery(@"exec GetContactPersonEmailList
        //       @Id",
        //       new SqlParameter("@Id", Id)
        //       ).ToList<PersonEmailList>();

        //    return View("_GetContactPersonEmailList", result);

        //}


        //[HttpPost]
        //public ActionResult DeleteContactPersonMobile(int? Id)
        //{
        //    try
        //    {
        //        OfficeDbContext _db = new OfficeDbContext();
        //        var result = _db.Database.ExecuteSqlCommand(@"exec DeleteContactPersonMobile 
        //         @Id",
        //        new SqlParameter("@Id", Id));

        //        return Json("Mobile Deleted");
        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return Json(message);

        //    }
        //}

        //[HttpPost]
        //public ActionResult DeleteContactPersonEmail(int? Id)
        //{
        //    try
        //    {
        //        OfficeDbContext _db = new OfficeDbContext();
        //        var result = _db.Database.ExecuteSqlCommand(@"exec DeleteContactPersonEmail 
        //         @Id",
        //        new SqlParameter("@Id", Id));

        //        return Json("Email Deleted");
        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return Json(message);

        //    }
        //}

        //[HttpPost]
        //public ActionResult SavePerson(Person Cp, List<SaveMobile> SaveMobile,List<SaveEmail> SaveEmail)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        DataTable dt1 = new DataTable();
        //        dt.Columns.Add("Mobile", typeof(string));
        //        dt1.Columns.Add("Email", typeof(string));

        //        if (SaveMobile != null)
        //        {
        //            if (SaveMobile.Count > 0)
        //            {
        //            foreach (var item in SaveMobile)
        //            {
        //                   DataRow dr = dt.NewRow();
        //                   dr["Mobile"] = item.Mobile;                            
        //                   dt.Rows.Add(dr);
        //            }
        //            }
        //        }
        //        if (SaveEmail != null)
        //        {
        //            if (SaveEmail.Count > 0)
        //            {                        
        //                foreach (var item in SaveEmail)
        //                {
        //                    DataRow dr1 = dt1.NewRow();
        //                    dr1["Email"] = item.Email;
        //                    dt1.Rows.Add(dr1);
        //                }
        //            }
        //        }

        //        SqlParameter tvpParam = new SqlParameter();
        //        tvpParam.ParameterName = "@MobileParameters";
        //        tvpParam.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParam.Value = dt;
        //        tvpParam.TypeName = "UT_PersonMobile";

        //        SqlParameter tvpParam1 = new SqlParameter();
        //        tvpParam1.ParameterName = "@EmailParameters";
        //        tvpParam1.SqlDbType = System.Data.SqlDbType.Structured;
        //        tvpParam1.Value = dt1;
        //        tvpParam1.TypeName = "UT_PersonEmail";


        //        OfficeDbContext _db = new OfficeDbContext();
        //        Boolean Active = true;
        //        if (Cp.IsActive.ToString() == "false")
        //        {
        //            Active = false;
        //        }
        //        var result = _db.Database.ExecuteSqlCommand(@"exec USP_SavePerson 
        //        @PersonalInfoID
        //       ,@Prefix    
        //       ,@FirstName 
        //       ,@MiddleName
        //       ,@LastName  
        //       ,@BirthDate 
        //       ,@Address1   
        //       ,@Address2   
        //       ,@DesignationId
        //       ,@Note  
        //       ,@CreatedBy    
        //       ,@IsActive
        //       ,@MobileParameters
        //       ,@EmailParameters",
        //        new SqlParameter("@PersonalInfoID", Cp.PersonalInfoID),
        //        new SqlParameter("@Prefix",Cp.Prefix),
        //        new SqlParameter("@FirstName", Cp.FirstName),
        //        new SqlParameter("@MiddleName", Cp.MiddleName == null ? (object)DBNull.Value : Cp.MiddleName),
        //        new SqlParameter("@LastName", Cp.LastName),
        //        new SqlParameter("@BirthDate", Cp.BirthDate == null ? (object)DBNull.Value : Cp.BirthDate),
        //        new SqlParameter("@Address1", Cp.Address1 == null ? (object)DBNull.Value : Cp.Address1),
        //        new SqlParameter("@Address2", Cp.Address2 == null ? (object)DBNull.Value : Cp.Address2),
        //        new SqlParameter("@DesignationId", Cp.DesignationId == null ? (object)DBNull.Value : Cp.DesignationId),
        //        new SqlParameter("@Note", Cp.Note == null ? (object)DBNull.Value : Cp.Note),
        //        new SqlParameter("@CreatedBy",1),
        //        new SqlParameter("@IsActive", Active),
        //        tvpParam == null ? (object)DBNull.Value : tvpParam,
        //        tvpParam1 == null ? (object)DBNull.Value : tvpParam1
        //        );

        //        return Json("Success");

        //    }
        //    catch (Exception ex)
        //    {

        //        string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
        //        return Json(message);

        //    }
        //}

        #endregion


        #region DeleteMaster

        [HttpPost]
        public ActionResult DeleteMaster(int? Id,string MasterName = "")
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec DeleteMaster 
                 @MasterName,@Id",
                new SqlParameter("@MasterName", MasterName),
                new SqlParameter("@Id", Id));
                string messegetext = MasterName + " Deleted";
                return Json(messegetext);
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }

        #endregion

        #region
        public ActionResult LoadDepartmentGrid(int? page, String Name = null)
        {
            StaticPagedList<DepartmentList> itemsAsIPagedList;
            itemsAsIPagedList = DepartmentGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_DepartmentGrid", itemsAsIPagedList)
                    : View("_DepartmentGrid", itemsAsIPagedList);
        }

        public ActionResult DepartmentList(int? page, String Name = null)
        {
            StaticPagedList<DepartmentList> itemsAsIPagedList;
            itemsAsIPagedList = DepartmentGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("DepartmentList", itemsAsIPagedList)
                    : View("DepartmentList", itemsAsIPagedList);
        }
        public StaticPagedList<DepartmentList> DepartmentGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            DepartmentList clist = new DepartmentList();

            IEnumerable<DepartmentList> result = _db.DFDepartmentList.SqlQuery(@"exec GetDepartmentList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<DepartmentList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<DepartmentList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        public ActionResult AddDepartment(int? id)
        {
            Department data = new Department();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {

                var result = _db.DepartmentLists.SqlQuery(@"exec GetDepartmentDetails 
                @DepartmentId",
                 new SqlParameter("@DepartmentId", id)).ToList<Department>();

                data = result.FirstOrDefault();
            }
            else
            {
                data.DepartmentId = 0;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddDepartment", data)
                  : View("AddDepartment", data);
        }

        [HttpPost]
        public ActionResult SaveDepartment(int DepartmentId = 0, String DepartmentName = "", String IsActive = "")
        {
            try
            {

                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveDepartment 
               @DepartmentId, @DepartmentName,@IsActive,@CreatedBy",
                new SqlParameter("@DepartmentId", DepartmentId),
                new SqlParameter("@DepartmentName", DepartmentName),
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


        #region BussinessCategoryMaster
        public ActionResult LoadBussinessCategoryGrid(int? page, String Name = null)
        {
            StaticPagedList<BussinessCategoryList> itemsAsIPagedList;
            itemsAsIPagedList = BussinessCategoryGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("BussinessCategoryGrid", itemsAsIPagedList)
                    : View("BussinessCategoryGrid", itemsAsIPagedList);
        }

        public ActionResult BussinessCategoryList(int? page, String Name = null)
        {
            StaticPagedList<BussinessCategoryList> itemsAsIPagedList;
            itemsAsIPagedList = BussinessCategoryGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("BussinessCategoryList", itemsAsIPagedList)
                    : View("BussinessCategoryList", itemsAsIPagedList);
        }
        public StaticPagedList<BussinessCategoryList> BussinessCategoryGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            BussinessCategoryList clist = new BussinessCategoryList();

            IEnumerable<BussinessCategoryList> result = _db.BussinessCategoryList.SqlQuery(@"exec GetBusinessCategoryList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<BussinessCategoryList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<BussinessCategoryList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList; 
        }
        public ActionResult AddBussinessCategory(int id = 0)
        {
            BussinessCategory data = new BussinessCategory();
            OfficeDbContext _db = new OfficeDbContext();
               
            if (id > 0)
            { 
                var result = _db.BussinessCategory.SqlQuery(@"exec GetBussinessCategoryDetails 
                @BussinessCategoryID",
                 new SqlParameter("@BussinessCategoryID", id)).ToList<BussinessCategory>();
                data = result.FirstOrDefault(); 
            }
            else
            {
                data.BusinessCategoryID = 0;
                data.IsActive = true;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddBussinessCategory", data)
                  : View("AddBussinessCategory", data);
        }
        [HttpPost]
        public ActionResult SaveBussinessCategory(int BussinessCategoryID = 0, String BussinessCategoryName = "", String IsActive = "")
        {
            try
            { 
                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveBusinessCategory 
               @BusinessCategoryID, @BusinessCategoryName,@CreatedBy,@IsActive",
                new SqlParameter("@BusinessCategoryID", BussinessCategoryID),
                new SqlParameter("@BusinessCategoryName", BussinessCategoryName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            ); 
                return Json("Success"); 
            }
            catch (Exception ex)
            { 
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message); 
            }
        }
        #endregion
        public ActionResult LoadCompanyGrid(int? page, String Name = null)
        {
            StaticPagedList<CompanyDetailsList> itemsAsIPagedList;
            itemsAsIPagedList = CompanyGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CompanyGrid", itemsAsIPagedList)
                    : View("CompanyGrid", itemsAsIPagedList);
        }

        public ActionResult CompanyList(int? page, String Name = null)
        {
            StaticPagedList<CompanyDetailsList> itemsAsIPagedList;
            itemsAsIPagedList = CompanyGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CompanyList", itemsAsIPagedList)
                    : View("CompanyList", itemsAsIPagedList);
        }
               
        public StaticPagedList<CompanyDetailsList> CompanyGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            SaveProject clist = new SaveProject();

            IEnumerable<CompanyDetailsList> result = _db.CompanyDetailsList.SqlQuery(@"exec GetCompany
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<CompanyDetailsList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<CompanyDetailsList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;

        }

        #region Bussiness SubCategoryMaster
        public ActionResult LoadBussinessSubCategoryGrid(int? page, String Name = null)
        {
            StaticPagedList<BussinessSubCategoryList> itemsAsIPagedList;
            itemsAsIPagedList = BussinessSubCategoryGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("BussinessSubCategoryGrid", itemsAsIPagedList)
                    : View("BussinessSubCategoryGrid", itemsAsIPagedList);
        }

        public ActionResult BussinessSubCategoryList(int? page, String Name = null)
        {
            StaticPagedList<BussinessSubCategoryList> itemsAsIPagedList;
            itemsAsIPagedList = BussinessSubCategoryGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("BussinessSubCategoryList", itemsAsIPagedList)
                    : View("BussinessSubCategoryList", itemsAsIPagedList);
        }
        public StaticPagedList<BussinessSubCategoryList> BussinessSubCategoryGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            BussinessSubCategoryList clist = new BussinessSubCategoryList();

            IEnumerable<BussinessSubCategoryList> result = _db.BussinessSubCategoryList.SqlQuery(@"exec GetBusinessSubCategoryList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<BussinessSubCategoryList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<BussinessSubCategoryList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;
        }
        public ActionResult AddBussinessSubCategory(int id = 0)
        {
            BussinessSubCategory data = new BussinessSubCategory();
            OfficeDbContext _db = new OfficeDbContext();
            ViewData["BusinessCategoryList"] = binddropdown("BusinessCategoryList", 0);
            if (id > 0)
            {
                var result = _db.BussinessSubCategory.SqlQuery(@"exec GetBussinessSubCategoryDetails 
                @BussinessSubCategoryID",
                 new SqlParameter("@BussinessSubCategoryID", id)).ToList<BussinessSubCategory>();
                data = result.FirstOrDefault();
            }
            else
            {
                data.BusinessCategoryID = 0;
                data.IsActive = true;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddBussinessSubCategory", data)
                  : View("AddBussinessSubCategory", data);
        }
        [HttpPost]
        public ActionResult SaveBussinessSubCategory(int BussinessCategoryID  = 0, int BussinessSubCategoryID = 0, String BussinessSubCategoryName = "", String IsActive = "")
        { 
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveBusinessSubCategory 
               @BusinessCategoryID,@BussinessSubCategoryID, @BusinessSubCategoryName,@CreatedBy,@IsActive",
                new SqlParameter("@BusinessCategoryID", BussinessCategoryID),
                new SqlParameter("@BussinessSubCategoryID", BussinessSubCategoryID),
                new SqlParameter("@BusinessSubCategoryName", BussinessSubCategoryName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
            }
        }
        #endregion

        #region TeamDesignationMaster
        public ActionResult LoadTeamDesignationGrid(int? page, String Name = null)
        {
            StaticPagedList<TeamDesignationList> itemsAsIPagedList;
            itemsAsIPagedList = TeamDesignationGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("TeamDesignationGrid", itemsAsIPagedList)
                    : View("TeamDesignationGrid", itemsAsIPagedList);
        }

        public ActionResult TeamDesignationList(int? page, String Name = null)
        {
            StaticPagedList<TeamDesignationList> itemsAsIPagedList;
            itemsAsIPagedList = TeamDesignationGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("TeamDesignationList", itemsAsIPagedList)
                    : View("TeamDesignationList", itemsAsIPagedList);
        }
        public StaticPagedList<TeamDesignationList> TeamDesignationGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            TeamDesignationList clist = new TeamDesignationList();

            IEnumerable<TeamDesignationList> result = _db.TeamDesignationList.SqlQuery(@"exec GetTeamDesignationList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<TeamDesignationList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<TeamDesignationList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;
        }
        public ActionResult AddTeamDesignation(int id = 0)
        {
            TeamDesignation data = new TeamDesignation();
            OfficeDbContext _db = new OfficeDbContext(); 
            if (id > 0)
            {
                var result = _db.TeamDesignation.SqlQuery(@"exec GetTeamDesignationDetails 
                @TeamDesignationID",
                 new SqlParameter("@TeamDesignationID", id)).ToList<TeamDesignation>();
                data = result.FirstOrDefault();
            }
            else
            {
                data.TeamDesignationID = 0;
            } 
            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddTeamDesignation", data)
                  : View("AddTeamDesignation", data);
        }
        [HttpPost]
        public ActionResult SaveTeamDesignation(int TeamDesignationID = 0, String TeamDesignationName = "", String IsActive = "")
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveTeamDesignation 
               @TeamDesignationID, @TeamDesignationName,@CreatedBy,@IsActive",
                new SqlParameter("@TeamDesignationID", TeamDesignationID),
                new SqlParameter("@TeamDesignationName", TeamDesignationName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
            }
        }
        #endregion

        #region TeamSubDesignationMaster
        public ActionResult LoadTeamSubDesignationGrid(int? page, String Name = null)
        {
            StaticPagedList<TeamSubDesignationList> itemsAsIPagedList;
            itemsAsIPagedList = TeamSubDesignationGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("TeamSubDesignationGrid", itemsAsIPagedList)
                    : View("TeamSubDesignationGrid", itemsAsIPagedList);
        }

        //public ActionResult TeamSubDesignationList(int? page, String Name = null)
        //{
        //    StaticPagedList<TeamSubDesignationList> itemsAsIPagedList;
        //    itemsAsIPagedList = TeamSubDesignationGridList(page, Name);

        //    return Request.IsAjaxRequest()
        //            ? (ActionResult)PartialView("TeamSubDesignationList", itemsAsIPagedList)
        //            : View("TeamSubDesignationList", itemsAsIPagedList);
        //}
        public StaticPagedList<TeamSubDesignationList> TeamSubDesignationGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            TeamSubDesignationList clist = new TeamSubDesignationList();

            IEnumerable<TeamSubDesignationList> result = _db.TeamSubDesignationList.SqlQuery(@"exec GetTeamSubDesignationList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<TeamSubDesignationList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<TeamSubDesignationList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;
        }
        public ActionResult AddTeamSubDesignation(int id = 0)
        {
            ViewData["TeamDesignationList"] = binddropdown("TeamDesignationList", 0);
            TeamSubDesignation data = new TeamSubDesignation();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {
                var result = _db.TeamSubDesignation.SqlQuery(@"exec GetTeamSubDesignationDetails 
                @TeamSubDesignationID",
                 new SqlParameter("@TeamSubDesignationID", id)).ToList<TeamSubDesignation>();
                data = result.FirstOrDefault();
            }
            else
            {
                data.TeamSubDesignationID = 0;
            }
            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddTeamSubDesignation", data)
                  : View("AddTeamSubDesignation", data);
        }
        [HttpPost]
        public ActionResult SaveTeamSubDesignation(int TeamDesignationID,int TeamSubDesignationID = 0, String TeamSubDesignationName = "", String IsActive = "")
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveTeamSubDesignation 
               @TeamDesignationID,@TeamSubDesignationID, @TeamSubDesignationName,@CreatedBy,@IsActive",
               new SqlParameter("@TeamDesignationID", TeamDesignationID),
               new SqlParameter("@TeamSubDesignationID", TeamSubDesignationID),
                new SqlParameter("@TeamSubDesignationName", TeamSubDesignationName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
            }
        }
        #endregion

        #region TeamSubSubDesignationMaster
        public ActionResult LoadTeamSubSubDesignationGrid(int? page, String Name = null)
        {
            StaticPagedList<TeamSubSubDesignationList> itemsAsIPagedList;
            itemsAsIPagedList = TeamSubSubDesignationGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("TeamSubSubDesignationGrid", itemsAsIPagedList)
                    : View("TeamSubSubDesignationGrid", itemsAsIPagedList);
        } 
        public StaticPagedList<TeamSubSubDesignationList> TeamSubSubDesignationGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            TeamSubSubDesignationList clist = new TeamSubSubDesignationList();

            IEnumerable<TeamSubSubDesignationList> result = _db.TeamSubSubDesignationList.SqlQuery(@"exec GetTeamSubSubDesignationList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<TeamSubSubDesignationList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<TeamSubSubDesignationList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;
        }
        public ActionResult AddTeamSubSubDesignation(int id = 0)
        {
            ViewData["TeamDesignationList"] = binddropdown("TeamDesignationList", 0);
            ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", 0);
            TeamSubSubDesignation data = new TeamSubSubDesignation();
            OfficeDbContext _db = new OfficeDbContext();
            if (id > 0)
            {
                try
                {
                    var result = _db.TeamSubSubDesignation.SqlQuery(@"exec GetTeamSubSubDesignationDetails 
                @TeamSubSubDesignationID",
                     new SqlParameter("@TeamSubSubDesignationID", id)).ToList<TeamSubSubDesignation>();
                    data = result.FirstOrDefault();
                    ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", Convert.ToInt32(data.TeamDesignationID.ToString()));
                }
                catch(Exception ee) { }
            }
            else
            {
                data.TeamSubSubDesignationID = 0;
            }
            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddTeamSubSubDesignation", data)
                  : View("AddTeamSubSubDesignation", data);
        }
        [HttpPost]
        public ActionResult SaveTeamSubSubDesignation(int TeamDesignationID, int TeamSubDesignationID, int TeamSubSubDesignationID = 0, String TeamSubSubDesignationName = "", String IsActive = "")
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveTeamSubSubDesignation 
               @TeamDesignationID,@TeamSubDesignationID,@TeamSubSubDesignationID, @TeamSubSubDesignationName,@CreatedBy,@IsActive",
               new SqlParameter("@TeamDesignationID", TeamDesignationID),
                new SqlParameter("@TeamSubDesignationID", TeamSubDesignationID),
                new SqlParameter("@TeamSubSubDesignationID", TeamSubSubDesignationID),
                new SqlParameter("@TeamSubSubDesignationName", TeamSubSubDesignationName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
            }
        }
        #endregion
        #region project new 
        public ActionResult ProjectNew(int id = 0)
        {
            nProjectDetail data = new nProjectDetail();
            OfficeDbContext _db = new OfficeDbContext();
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            ViewData["ExternalTeamUnderCompany"] = binddropdown("ExternalTeamUnderCompany", 0);
            ViewData["InternalTeamUnderCompany"] = binddropdown("InternalTeamUnderCompany", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);
            ViewData["OwnershipTypeList"] = binddropdown("OwnershipTypeList", 0);
            ViewData["BusinessCategoryList"] = binddropdown("BusinessCategoryList", 0);
            ViewData["BusinessSubCategoryList"] = binddropdown("BusinessSubCategoryList", 0);
            ViewData["CertificationList"] = binddropdown("CertificationList", 0);
            ViewData["CompanyRelationList"] = binddropdown("CompanyRelationList", 0);
            ViewData["CompanyList"] = binddropdown("CompanyList", 0); 
            ViewData["PersonList"] = binddropdown("PersonList", 0);

            ViewData["TeamDesignationList"] = binddropdown("TeamDesignationList", 0);
            ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", 0);
            ViewData["TeamSubPartDesignationList"] = binddropdown("TeamSubPartDesignationList", 0);
            ViewData["ProjectTypeList"] = binddropdown("ProjectTypeList", 0);
            ViewData["ProjectStatusList"] = binddropdown("ProjectStatusList", 0);
            ViewData["UnitList"] = binddropdown("UnitList", 0);
            ViewData["SurveyNoTypeList"] = binddropdown("SurveyNoTypeList", 0);
            ViewData["InternalTeamUnderProject"] = binddropdown("InternalTeamUnderProject", id);
            ViewData["MDocumentList"] = binddropdown("MDocumentList", 0);
            ViewData["Either1"] = binddropdown("Either1", 0);
            
                ViewData["SurveyNoList"] = binddropdown("SurveyNoList", id);
            ViewData["SurveyNoProperyCardList"] = binddropdown("SurveyNoProperyCardList", id);

            ViewData["CompanyListByProjectID"] = binddropdown("CompanyListByProjectID", id);
            if (Request.IsAjaxRequest())
            {
                ViewBag.layout = "0";
            }
            else
            {
                ViewBag.layout = "1";
            }
            if (id > 0)
            {
                try
                {
                    var result = _db.nProjectDetail.SqlQuery(@"exec uspGetnProjectDetails
                   @ProjectID",
                    new SqlParameter("@ProjectID", id)
                    ).ToList<nProjectDetail>();
                    data = result.FirstOrDefault();
                }catch(Exception ss)
                { }

                return Request.IsAjaxRequest()
                   ? (ActionResult)PartialView("ProjectNew", data)
                   : View("ProjectNew", data);
            }

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AddCompanyInfo", data)
                    : View("ProjectNew", data);
        }
        public ActionResult SaveProjectNew(nProjectDetail proj ) //, List<nSaveSurvayDetails> SaveSurvayDetails , List<SaveProjectInternalTeam> SaveProjectInternalTeam,List<SaveProjectExternalTeam> SaveProjectExternalTeam)
        {
            try
            { 
                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (proj.IsActive == false)
                {
                    Active = false;
                }


                //DataTable dtInternalTeam = new DataTable();
                //DataTable dtExternalTeam = new DataTable();
                //DataTable dtSurvayDetails = new DataTable(); 
                //dtSurvayDetails.Columns.Add("SurvayDetailId", typeof(int));
                //dtSurvayDetails.Columns.Add("SurvayTypeID", typeof(int));
                //dtSurvayDetails.Columns.Add("SurvayNo", typeof(string));
                //dtSurvayDetails.Columns.Add("HissaNo", typeof(string));
                //dtSurvayDetails.Columns.Add("Area", typeof(decimal));
                //dtSurvayDetails.Columns.Add("UnitID", typeof(int));

                
                //dtInternalTeam.Columns.Add("parentcompanyid", typeof(int));
                //dtInternalTeam.Columns.Add("internalpersonid", typeof(int));
                //dtInternalTeam.Columns.Add("internalTeamid", typeof(int));
                //dtInternalTeam.Columns.Add("designationid1", typeof(int));
                //dtInternalTeam.Columns.Add("subdesignationid1", typeof(int));
                //dtInternalTeam.Columns.Add("subpartdesignationid1", typeof(int));



                //dtExternalTeam.Columns.Add("parentcompanyid", typeof(int));
                //dtExternalTeam.Columns.Add("internalpersonid", typeof(int));
                //dtExternalTeam.Columns.Add("internalTeamid", typeof(int));
                //dtExternalTeam.Columns.Add("designationid1", typeof(int));
                //dtExternalTeam.Columns.Add("subdesignationid1", typeof(int));
                //dtExternalTeam.Columns.Add("subpartdesignationid1", typeof(int));

                //if (SaveProjectInternalTeam != null)
                //{
                //    if (SaveProjectInternalTeam.Count > 0)
                //    {
                //        foreach (var item in SaveProjectInternalTeam)
                //        {
                //            DataRow dr_InternalTeam = dtInternalTeam.NewRow();
                //            dr_InternalTeam["parentcompanyid"] = item.parentcompanyid;
                //            dr_InternalTeam["internalpersonid"] = item.internalpersonid;
                //            dr_InternalTeam["internalTeamid"] = item.internalTeamid;
                //            dr_InternalTeam["designationid1"] = item.designationid1;
                //            dr_InternalTeam["subdesignationid1"] = item.subdesignationid1;
                //            dr_InternalTeam["subpartdesignationid1"] = item.subpartdesignationid1;
                //            dtInternalTeam.Rows.Add(dr_InternalTeam);
                //        }
                //    }
                //}

                //if (SaveProjectExternalTeam != null)
                //{
                //    if (SaveProjectExternalTeam.Count > 0)
                //    {
                //        foreach (var item in SaveProjectExternalTeam)
                //        {
                //            DataRow dr_externalTeam = dtExternalTeam.NewRow();
                //            dr_externalTeam["parentcompanyid"] = item.parentcompanyid;
                //            dr_externalTeam["internalpersonid"] = item.Externalpersonid;
                //            dr_externalTeam["internalTeamid"] = item.ExternalTeamid;
                //            dr_externalTeam["designationid1"] = item.designationid1;
                //            dr_externalTeam["subdesignationid1"] = item.subdesignationid1;
                //            dr_externalTeam["subpartdesignationid1"] = item.subpartdesignationid1;
                //            dtExternalTeam.Rows.Add(dr_externalTeam);
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
                //            if(item.SurvayDetailId>0)
                //            {
                //                dr_SurvayDetails["SurvayDetailId"] = item.SurvayDetailId;
                //            }
                //            else
                //            {
                //                dr_SurvayDetails["SurvayDetailId"] = 0;
                //            }
                            
                //            dr_SurvayDetails["SurvayTypeID"] = item.SurvayTypeID;
                //            dr_SurvayDetails["SurvayNo"] = item.SurvayNo;
                //            dr_SurvayDetails["HissaNo"] = item.HissaNo;
                //            dr_SurvayDetails["Area"] = item.Area;
                //            dr_SurvayDetails["UnitID"] = item.UnitID;
                //            dtSurvayDetails.Rows.Add(dr_SurvayDetails);
                //        }
                //    }
                //}
                //SqlParameter tvpParamSurvayDetails = new SqlParameter();
                //tvpParamSurvayDetails.ParameterName = "@ProjectSurvay";
                //tvpParamSurvayDetails.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamSurvayDetails.Value = dtSurvayDetails;
                //tvpParamSurvayDetails.TypeName = "UTT_nProjectSurvay";

                //SqlParameter tvpParamInternalTeam = new SqlParameter();
                //tvpParamInternalTeam.ParameterName = "@ProjectInternalTeam";
                //tvpParamInternalTeam.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamInternalTeam.Value = dtInternalTeam;
                //tvpParamInternalTeam.TypeName = "UTT_ProjectInternalTeam";

                //SqlParameter tvpParamExternalTeam = new SqlParameter();
                //tvpParamExternalTeam.ParameterName = "@ProjectExternalTeam";
                //tvpParamExternalTeam.SqlDbType = System.Data.SqlDbType.Structured;
                //tvpParamExternalTeam.Value = dtExternalTeam;
                //tvpParamExternalTeam.TypeName = "UTT_ProjectInternalTeam";

                var outParam = new SqlParameter();
                outParam.ParameterName = "pOut";
                outParam.SqlDbType = System.Data.SqlDbType.Int;//DataType Of OutPut Parameter
                outParam.Direction = System.Data.ParameterDirection.Output;
                outParam.Value = 0;

                var result = _db.Database.ExecuteSqlCommand(@"exec USP_SaveProjectNew 
                @ProjectID,@ProjectName,@EnquiryDate,@ProjectShortName,@StatusId,@ProjectTypeId,@CustomerFileNo,@PhysicalPath,@StateID,@District,@TalukaID,
                 @Goan,@Road,@IsActive,@CreatedBy,@Cost,@StartDate,@EndDate,@Developers,@DeveloperTypeID
                 ,@pOut out",
                new SqlParameter("@ProjectID", proj.ProjectID),
                new SqlParameter("@ProjectName", proj.ProjectName),
                new SqlParameter("@EnquiryDate", proj.EnquiryDate),
                new SqlParameter("@ProjectShortName", proj.ProjectShortName),
                new SqlParameter("@StatusId", proj.StatusId),
                new SqlParameter("@ProjectTypeId", proj.ProjectTypeId),
                new SqlParameter("@CustomerFileNo", proj.CustomerFileNo == null ? (object)DBNull.Value : proj.CustomerFileNo),
                new SqlParameter("@PhysicalPath", proj.PhysicalPath == null ? (object)DBNull.Value : proj.PhysicalPath),
                new SqlParameter("@StateID", proj.StateID  ), 
                new SqlParameter("@District", proj.District == null ? (object)DBNull.Value : proj.District),
                new SqlParameter("@TalukaID", proj.TalukaID),
                new SqlParameter("@Goan", proj.Goan == null ? (object)DBNull.Value : proj.Goan),
                new SqlParameter("@Road", proj.Road == null ? (object)DBNull.Value : proj.Road), 
                new SqlParameter("@IsActive", Active),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@Cost", proj.Cost),
                new SqlParameter("@StartDate", proj.StartDate == null ? (object)DBNull.Value : proj.StartDate),
                new SqlParameter("@EndDate", proj.EndDate == null ? (object)DBNull.Value : proj.EndDate),
                new SqlParameter("@Developers", proj.Developers),
                new SqlParameter("@DeveloperTypeID", proj.DeveloperTypeID)
                
                //tvpParamSurvayDetails
                //, tvpParamInternalTeam
                //,tvpParamExternalTeam
                , outParam
            );
                int outval = 0;
                if (outParam.Value != DBNull.Value)
                {
                    outval = Convert.ToInt32(outParam.Value);
                }
                if (outval != 0)
                {
                    return Json("Success" + outval);  
                }
                else
                {
                    return Json("error" );
                }
                

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }

        #endregion
        public ActionResult BindCompanyNames(int val = 0) 
        {
            if (val == 0)
            {
                ViewBag.value = 1; 
            }
            else
            {
                ViewBag.value = 2;
            } 
            ViewData["CompanyList"] = binddropdown("CompanyList", 0);
             
          
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("BindCompanyNames")
                    : View("BindCompanyNames");
        }

        public ActionResult BindPersonNames(int val = 0)
        {
            if (val == 0)
            {
                ViewBag.value = 1;
            }
            else
            {
                ViewBag.value = 2;
            } 
            ViewData["PersonList"] = binddropdown("PersonList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("BindPersonNames")
                    : View("BindPersonNames");
        }

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
        public ActionResult ProformaSetting(int GroupId = 0)
        {
          
            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<ProformaSetting> result3 = _db.ProformaSetting.SqlQuery(@"exec GetProformaSetting
                @GroupId", new SqlParameter("@GroupId", GroupId)
         ).ToList<ProformaSetting>();
            
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ProformaSetting", result3)
                    : View("ProformaSetting", result3);
        }
        
        [HttpPost]
        public ActionResult SaveProformaSetting(SaveProformaSetting SaveProformaSetting)
        {
            try
            { 
                OfficeDbContext _db = new OfficeDbContext();
               if(SaveProformaSetting.UnSelectedField==null)
                {
                    SaveProformaSetting.UnSelectedField = "";
                }
                if (SaveProformaSetting.SelectedField == null)
                {
                    SaveProformaSetting.SelectedField = "";
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveProformaSetting 
               @SelectedField, @UnSelectedField",
                new SqlParameter("@SelectedField", SaveProformaSetting.SelectedField),
                new SqlParameter("@UnSelectedField", SaveProformaSetting.UnSelectedField)
                 
            );

                return Json("Success");

            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            }
        }
        [HttpPost] 
        public ActionResult SaveProjectOwnerSurveyNo(ProjectOwnerSurveyNo ProjectOwnerSurveyNo)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext(); 
                DataTable dtMobile = new DataTable(); 
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_SaveProjectProposalSurvayDetails
                 @SurvayDetailId
                ,@ProjectID     
                ,@SurvayNo   	
                ,@SHissaNo  	
                ,@SPlotNo    	
                ,@OldSurvayNo   
                ,@GatNo    		
                ,@GatHissaNo    
                ,@GatPlotNo   	
                ,@OldGatNo    	
                ,@CTSNo     	
                ,@CTSHissaNo  	
                ,@CTSPlotNo   	
                ,@OldCTSNo    	
                ,@FInalPlotNo
                ,@PrimarySurvey",
                new SqlParameter("@SurvayDetailId", ProjectOwnerSurveyNo.SurvayDetailId),
                new SqlParameter("@ProjectID", ProjectOwnerSurveyNo.ProjectID), 
                new SqlParameter("@SurvayNo", ProjectOwnerSurveyNo.S_SurveyNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.S_SurveyNo),
                new SqlParameter("@SHissaNo", ProjectOwnerSurveyNo.S_HissaNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.S_SurveyNo),
                new SqlParameter("@SPlotNo", ProjectOwnerSurveyNo.S_PlotNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.S_SurveyNo),
                new SqlParameter("@OldSurvayNo", ProjectOwnerSurveyNo.S_OldNo== null ? (object)DBNull.Value : ProjectOwnerSurveyNo.S_OldNo),
                new SqlParameter("@GatNo", ProjectOwnerSurveyNo.G_SurveyNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.G_SurveyNo),
                new SqlParameter("@GatHissaNo", ProjectOwnerSurveyNo.G_HissaNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.G_HissaNo),
                new SqlParameter("@GatPlotNo", ProjectOwnerSurveyNo.G_PlotNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.G_PlotNo),
                new SqlParameter("@OldGatNo", ProjectOwnerSurveyNo.G_OldNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.G_OldNo),
                new SqlParameter("@CTSNo", ProjectOwnerSurveyNo.CTS_SurveyNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.CTS_SurveyNo),
                new SqlParameter("@CTSHissaNo", ProjectOwnerSurveyNo.CTS_HissaNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.CTS_HissaNo),
                new SqlParameter("@CTSPlotNo", ProjectOwnerSurveyNo.CTS_PlotNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.CTS_PlotNo),
                new SqlParameter("@OldCTSNo", ProjectOwnerSurveyNo.CTS_OldNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.CTS_OldNo),
                new SqlParameter("@FinalPlotNo", ProjectOwnerSurveyNo.FP_SurveyNo == null ? (object)DBNull.Value : ProjectOwnerSurveyNo.FP_SurveyNo),
                new SqlParameter("@PrimarySurvey", ProjectOwnerSurveyNo.PrimarySurvey )
                ); 
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            } 
        }
        [HttpPost]
        public ActionResult SaveProjectOwnerDetails(int PropertCardTypeId , int SurvayDetailId, int ProjectID, int IsUndevidedShares, List<ProjectOwnerDetails> ProjectOwnerDetails)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext(); 

                DataTable dtOwner = new DataTable();
                dtOwner.Columns.Add("OwnerID", typeof(int));
                dtOwner.Columns.Add("OwnerName", typeof(string));
                
                dtOwner.Columns.Add("isUndevidedShare", typeof(int));
                dtOwner.Columns.Add("Area", typeof(Decimal));
                dtOwner.Columns.Add("AreaUnitID", typeof(int));
                if (ProjectOwnerDetails != null)
                {
                    if (ProjectOwnerDetails.Count > 0)
                    {
                        foreach (var item in ProjectOwnerDetails)
                        {
                            DataRow dr_Owner = dtOwner.NewRow();
                            dr_Owner["OwnerID"] = item.OwnerID;
                            dr_Owner["OwnerName"] = item.OwnerName;
                            dr_Owner["isUndevidedShare"] = IsUndevidedShares;
                            dr_Owner["Area"] = item.Area;
                            dr_Owner["AreaUnitID"] = item.AreaUnitID; 
                            dtOwner.Rows.Add(dr_Owner);
                        }
                    }
                }
                SqlParameter tvpParamOwner = new SqlParameter();
                tvpParamOwner.ParameterName = "@nProjectOwners";
                tvpParamOwner.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamOwner.Value = dtOwner;
                tvpParamOwner.TypeName = "UTT_nProjectOwners";
                var result = _db.Database.ExecuteSqlCommand(@"exec uspInsertOwner
                 @SurveyTypeId
                ,@SurvayDetailId     
                ,@ProjectID

                ,@nProjectOwners",
                new SqlParameter("@SurveyTypeId", PropertCardTypeId),
                new SqlParameter("@SurvayDetailId", SurvayDetailId),
                new SqlParameter("@ProjectID", ProjectID),
                tvpParamOwner 
                );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        [HttpPost]
        public ActionResult SaveAuthoritySignature(  AuthoritySignatory AuthoritySignatory)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();

                DataTable dtMobile = new DataTable();

                var result = _db.Database.ExecuteSqlCommand(@"exec SaveProjectSignatory
                 @ProjectID  
                ,@OwnerID   
                ,@AreaUnderDevelopment  
                ,@AreaUnitID
                ,@AgreemetNo 
                ,@AgreementDate
                ,@SubRegistarOffice
                ,@DocumentID 
                ,@selectedVal1
                ,@selectedVal2
                ,@selectedVal3
                ,@selectedVal4
                ,@selectedVal5
               ,@either1 ,@either2 ,@either3, @either4,@either5 ,@SignatorID
                ",
                new SqlParameter("@ProjectID", AuthoritySignatory.ProjectID),
                new SqlParameter("@OwnerID", AuthoritySignatory.OwnerID),
                new SqlParameter("@AreaUnderDevelopment", AuthoritySignatory.AreaUnderDevelopment),
                new SqlParameter("@AreaUnitID", AuthoritySignatory.AreaUnitID),
                new SqlParameter("@AgreemetNo", AuthoritySignatory.AgreementNo),
                new SqlParameter("@AgreementDate", AuthoritySignatory.AgreementDate),
                new SqlParameter("@SubRegistarOffice", AuthoritySignatory.SubRegistarOffice),
                new SqlParameter("@DocumentID", AuthoritySignatory.DocumentID) , 
                new SqlParameter("@selectedVal1", AuthoritySignatory.selectedVal1 == null ? "" : AuthoritySignatory.selectedVal1),
                new SqlParameter("@selectedVal2", AuthoritySignatory.selectedVal2 ==null? "": AuthoritySignatory.selectedVal2),
                new SqlParameter("@selectedVal3", AuthoritySignatory.selectedVal3 == null ? "" : AuthoritySignatory.selectedVal3),
                new SqlParameter("@selectedVal4", AuthoritySignatory.selectedVal4 == null ? "" : AuthoritySignatory.selectedVal4),
                new SqlParameter("@selectedVal5", AuthoritySignatory.selectedVal5 == null ? "" : AuthoritySignatory.selectedVal5),
                new SqlParameter("@either1", AuthoritySignatory.either1),
                new SqlParameter("@either2", AuthoritySignatory.either2),
                new SqlParameter("@either3", AuthoritySignatory.either3),
                new SqlParameter("@either4", AuthoritySignatory.either4),
                new SqlParameter("@either5", AuthoritySignatory.either5) ,
                new SqlParameter("@SignatorID", AuthoritySignatory.signatorId)
                );

                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
        public ActionResult test(int id=0) 
        {
            CompanyFields data = new CompanyFields();
               CompanyDetails data1 = new CompanyDetails();
            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<ProformaSetting> result3 = _db.ProformaSetting.SqlQuery(@"exec GetProformaSetting
                @GroupId", new SqlParameter("@GroupId", 2)
        ).ToList<ProformaSetting>();
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            ViewData["PersonList"] = binddropdown("PersonList", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);
            ViewData["OwnershipTypeList"] = binddropdown("OwnershipTypeList", 0);
            ViewData["BusinessCategoryList"] = binddropdown("BusinessCategoryList", 0);
            ViewData["BusinessSubCategoryList"] = binddropdown("BusinessSubCategoryList", 0);
            ViewData["CertificationList"] = binddropdown("CertificationList", 0);
            ViewData["CompanyRelationList"] = binddropdown("CompanyRelationList", 0);
            ViewData["CompanyList"] = binddropdown("CompanyList", 0);

            ViewData["TeamDesignationList"] = binddropdown("TeamDesignationList", 0);
            ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", 0);
            ViewData["TeamSubPartDesignationList"] = binddropdown("TeamSubPartDesignationList", 0);
            data.ProformaSetting = result3;
            
            return View("Test", data);
        }
        public ActionResult ProjectSetting(int id = 0)
        {
            CompanyFields data = new CompanyFields();
             
            OfficeDbContext _db = new OfficeDbContext();
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            ViewData["ExternalTeamUnderCompany"] = binddropdown("ExternalTeamUnderCompany", 0);
            ViewData["InternalTeamUnderCompany"] = binddropdown("InternalTeamUnderCompany", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);
            ViewData["OwnershipTypeList"] = binddropdown("OwnershipTypeList", 0);
            ViewData["BusinessCategoryList"] = binddropdown("BusinessCategoryList", 0);
            ViewData["BusinessSubCategoryList"] = binddropdown("BusinessSubCategoryList", 0);
            ViewData["CertificationList"] = binddropdown("CertificationList", 0);
            ViewData["CompanyRelationList"] = binddropdown("CompanyRelationList", 0);
            ViewData["CompanyList"] = binddropdown("CompanyList", 0);
            ViewData["PersonList"] = binddropdown("PersonList", 0);

            ViewData["TeamDesignationList"] = binddropdown("TeamDesignationList", 0);
            ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", 0);
            ViewData["TeamSubPartDesignationList"] = binddropdown("TeamSubPartDesignationList", 0);
            ViewData["ProjectTypeList"] = binddropdown("ProjectTypeList", 0);
            ViewData["ProjectStatusList"] = binddropdown("ProjectStatusList", 0);
            ViewData["UnitList"] = binddropdown("UnitList", 0);
            ViewData["SurveyNoTypeList"] = binddropdown("SurveyNoTypeList", 0);
            ViewData["InternalTeamUnderProject"] = binddropdown("InternalTeamUnderProject", id);
            ViewData["MDocumentList"] = binddropdown("MDocumentList", 0);
            ViewData["Either1"] = binddropdown("Either1", 0);

            ViewData["SurveyNoList"] = binddropdown("SurveyNoList", id);
            ViewData["SurveyNoProperyCardList"] = binddropdown("SurveyNoProperyCardList", id);

            ViewData["CompanyListByProjectID"] = binddropdown("CompanyListByProjectID", id);
            IEnumerable<ProformaSetting> result3 = _db.ProformaSetting.SqlQuery(@"exec GetProformaSetting
                @GroupId", new SqlParameter("@GroupId", 2)
        ).ToList<ProformaSetting>();
            if (Request.IsAjaxRequest())
            {
                ViewBag.layout = "0";
            }
            else
            {
                ViewBag.layout = "1";
            }
            data.ProformaSetting = result3;

            return View("ProjectSetting", data);
             
        }

        #region CertificationMaster
        public ActionResult LoadCertificationGrid(int? page, String Name = null)
        {
            StaticPagedList<CertificationList> itemsAsIPagedList;
            itemsAsIPagedList = CertificationGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CertificationGrid", itemsAsIPagedList)
                    : View("CertificationGrid", itemsAsIPagedList);
        }

        public ActionResult CertificationList(int? page, String Name = null)
        {
            StaticPagedList<CertificationList> itemsAsIPagedList;
            itemsAsIPagedList = CertificationGridList(page, Name);

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("CertificationList", itemsAsIPagedList)
                    : View("CertificationList", itemsAsIPagedList);
        }
        public StaticPagedList<CertificationList> CertificationGridList(int? page, String Name = "")
        {
            OfficeDbContext _db = new OfficeDbContext();
            var pageIndex = (page ?? 1);
            const int pageSize = 10;
            int totalCount = 10;
            CertificationList clist = new CertificationList();

            IEnumerable<CertificationList> result = _db.CertificationList.SqlQuery(@"exec GetBusinessCategoryList
                   @pPageIndex, @pPageSize,@Name",
               new SqlParameter("@pPageIndex", pageIndex),
               new SqlParameter("@pPageSize", pageSize),
               new SqlParameter("@Name", Name == null ? (object)DBNull.Value : Name)
               ).ToList<CertificationList>();

            totalCount = 0;
            if (result.Count() > 0)
            {
                totalCount = Convert.ToInt32(result.FirstOrDefault().TotalRows);
            }
            var itemsAsIPagedList = new StaticPagedList<CertificationList>(result, pageIndex, pageSize, totalCount);
            return itemsAsIPagedList;
        }

        
        public ActionResult GetOwnersPropertyCardList(int ProjectID ,int SurvayDetailId=0)
        {
            Certification data = new Certification();
            OfficeDbContext _db = new OfficeDbContext(); 

            IEnumerable<ProjectOwnerDetailList> result2 = _db.ProjectOwnerDetailList.SqlQuery(@"exec GetProjectOwnersUnderProperty
                @projectID,@SurvayDetailId",
          new SqlParameter("@projectID", ProjectID),
           new SqlParameter("@SurvayDetailId", SurvayDetailId)
          
          ).ToList<ProjectOwnerDetailList>(); 

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("OwnersPropertyCardList", result2)
                  : View("OwnersPropertyCardList", result2);
        }
        public ActionResult GetOwnersPropertyCardSubList(int ProjectID, int SurvayDetailId = 0)
        {
            Certification data = new Certification();
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<ProjectOwnerDetailList> result2 = _db.ProjectOwnerDetailList.SqlQuery(@"exec GetProjectOwnersUnderProperty
                @projectID,@SurvayDetailId",
          new SqlParameter("@projectID", ProjectID),
           new SqlParameter("@SurvayDetailId", SurvayDetailId)

          ).ToList<ProjectOwnerDetailList>();

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("OwnersPropertyCardSubList", result2)
                  : View("OwnersPropertyCardList", result2);
        }
        public ActionResult GetOwnersAuthorityList(int ProjectID, int developerid)
        {
             
            OfficeDbContext _db = new OfficeDbContext();
            ViewData["ProjectSurveyNoList"] = binddropdown("ProjectSurveyNoList", ProjectID);
            ViewData["ProjectOwerList"] = binddropdown("ProjectOwerList", ProjectID);
            IEnumerable<ProjectAuthorityOwnerList> result2 = _db.ProjectAuthorityOwnerList.SqlQuery(@"exec GetProjectOwnersArea
                @projectID,@developerid",
          new SqlParameter("@projectID", ProjectID) ,
           new SqlParameter("@developerid", developerid)


          ).ToList<ProjectAuthorityOwnerList>();

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AuthorityListGrid", result2)
                  : View("AuthorityListGrid", result2);
        }
        public ActionResult GetOwnersPropertyCardListAll(int ProjectID, int SurvayDetailId = 0)
        {
             
            OfficeDbContext _db = new OfficeDbContext();
            ProjectOwnerDetailSurveyWiseAll Data = new ProjectOwnerDetailSurveyWiseAll();
            try
            {
                IEnumerable<ProjectOwnerDetailList> result2 = _db.ProjectOwnerDetailList.SqlQuery(@"exec GetProjectOwnersUnderProperty
                @projectID,@SurvayDetailId",
              new SqlParameter("@projectID", ProjectID),
              new SqlParameter("@SurvayDetailId", SurvayDetailId)
              ).ToList<ProjectOwnerDetailList>();

                IEnumerable<ProjectOwnerDetailSurveyWise> result3 = _db.ProjectOwnerDetailSurveyWise.SqlQuery(@"exec GetallProjectOwners
                @projectID ",
             new SqlParameter("@projectID", ProjectID)

             ).ToList<ProjectOwnerDetailSurveyWise>();



                Data.ProjectOwnerDetailList = result2;
                Data.ProjectOwnerDetailSurveyWise = result3;
            }
            catch(Exception s) { }
            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("OwnersPropertyCardListAll", Data)
                  : View("OwnersPropertyCardListAll", Data);
        }
        public ActionResult GetOwnersPropertyCardListByid(int ProjectID,int OwnerID)
        {
            Certification data = new Certification();
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<ProjectOwnerDetailList> result2 = _db.ProjectOwnerDetailList.SqlQuery(@"exec GetProjectOwnersByOwnerID
                @projectID,@OwnerID",
          new SqlParameter("@projectID", ProjectID),
          new SqlParameter("@OwnerID", OwnerID)
          ).ToList<ProjectOwnerDetailList>();

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("OwnersPropertyCardList", result2)
                  : View("OwnersPropertyCardList", result2);
        }
        public ActionResult DeleteProjectOwnerSurvey(int ProjectID,   int OwnerID = 0, int isdevideshares = 0)
        {
            try
            {
                Certification data = new Certification();
                OfficeDbContext _db = new OfficeDbContext();
                IEnumerable<ProjectOwnerDetailList> result2 = Enumerable.Empty<ProjectOwnerDetailList>();

                var result = _db.Database.ExecuteSqlCommand(@"exec DeleteProjectOwnerDetails 
                 @projectID,@OwnerID ",
                new SqlParameter("@projectID", ProjectID),
                new SqlParameter("@OwnerID", OwnerID));

                return Json("Success");

            }
            catch
            {
                return Json("Error");
            }

        }
        public ActionResult GetProjectOwnerSurvey(int ProjectID, int SurvayTypeID = 0, int OwnerSurveyNo = 0, int OwnerID=0 , int isdevideshares=0)
        {
            Certification data = new Certification();
            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<ProjectOwnerDetailList> result2 = Enumerable.Empty<ProjectOwnerDetailList>();
               

            ViewData["UnitList"] = binddropdown("UnitList", 0);
            if (OwnerID > 0)
            {
                try
                {

                    result2 = _db.ProjectOwnerDetailList.SqlQuery(@"exec GetProjectOwnersByOwnerID
                @projectID,@OwnerID",
                 new SqlParameter("@projectID", ProjectID),
                 new SqlParameter("@OwnerID", OwnerID)
                 ).ToList<ProjectOwnerDetailList>();

                    return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("ProjectOwnerSurvey", result2)
                  : View("ProjectOwnerSurvey", result2);
                }
                catch (Exception rre) { }
            }
            else
            {
                result2 = _db.ProjectOwnerDetailList.SqlQuery(@"exec GetProjectOwnersByOwnerID
                @projectID,@OwnerID",
                 new SqlParameter("@projectID", ProjectID),
                 new SqlParameter("@OwnerID", OwnerID)
                 ).ToList<ProjectOwnerDetailList>();

               
                return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("ProjectOwnerSurvey", result2)
              : View("ProjectOwnerSurvey", result2);

            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("ProjectOwnerSurvey")
                  : View("ProjectOwnerSurvey"); 

            
        }
        public ActionResult GetProjectOwnerSurvey2(int ProjectID, int SurvayTypeID = 0, int OwnerSurveyNo = 0, int OwnerID = 0, int isdevideshares = 0)
        {
            Certification data = new Certification();
            OfficeDbContext _db = new OfficeDbContext();
            IEnumerable<ProjectOwnerDetailList> result2 = Enumerable.Empty<ProjectOwnerDetailList>();


            ViewData["UnitList"] = binddropdown("UnitList", 0);
            if (OwnerID > 0)
            {

                result2 = _db.ProjectOwnerDetailList.SqlQuery(@"exec GetProjectOwnersByOwnerID
                @projectID,@OwnerID",
             new SqlParameter("@projectID", ProjectID),
             new SqlParameter("@OwnerID", OwnerID)
             ).ToList<ProjectOwnerDetailList>();

            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("ProjectOwnerSurvey2", result2)
                  : View("ProjectOwnerSurvey2", result2);


        }
        public ActionResult AddCertification(int id = 0)
        {
            Certification data = new Certification();
            OfficeDbContext _db = new OfficeDbContext();

            if (id > 0)
            {
                var result = _db.Certification.SqlQuery(@"exec GetCertificationDetails 
                @CertificationID",
                 new SqlParameter("@CertificationID", id)).ToList<Certification>();
                data = result.FirstOrDefault();
            }
            else
            {
                data.CertificationID = 0;
                data.IsActive = true;
            }

            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("AddCertification", data)
                  : View("AddCertification", data);
        }
        [HttpPost]
        public ActionResult SaveCertification(int CertificationID = 0, String CertificationName = "", String IsActive = "")
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (IsActive == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec SaveBusinessCategory 
               @BusinessCategoryID, @BusinessCategoryName,@CreatedBy,@IsActive",
                new SqlParameter("@BusinessCategoryID", CertificationID),
                new SqlParameter("@BusinessCategoryName", CertificationName),
                new SqlParameter("@CreatedBy", 1),
                new SqlParameter("@IsActive", Active)
            );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
            }
        }
        #endregion
        public ActionResult BindinternalTeamunderCompany(int DesignationID = 0, int val = 0)
        {
            if (val == 0)
            {
                ViewBag.value = 1;
            }
            if (val == 3)
            {
                ViewBag.value = 3;
            }
            else
            {
                ViewBag.value = 2;
            }
            ViewData["TeamSubDesignationList"] = binddropdown("TeamSubDesignationList", DesignationID);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("SubDesignationList")
                    : View("SubDesignationList");
        }

        public ActionResult GetAuthorityListGrid(int PersonID = 0) 
        {
            CompanyDetailsforPerson data = new CompanyDetailsforPerson();
            OfficeDbContext _db = new OfficeDbContext();
            try
            {
                IEnumerable<SaveCompanyList> result = _db.SaveCompanyList.SqlQuery(@"exec uspGetCompanyListForPerson
                @PersonID",
              new SqlParameter("@PersonID", PersonID)
              ).ToList<SaveCompanyList>();
                data.SaveCompanyList = result;
                data.PersonID = PersonID;

                 
 
            }
            catch (Exception ee) { }
            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("PersonLeftSide", data)
               : View("PersonLeftSide", data);

        }

        [HttpPost]
        public ActionResult SaveAuthorityTable(int ProjectID, List<ProjectAuthorityTable> ProjectAuthorityTable )
        {
            try
            {
                DataTable dtrow = new DataTable();

                dtrow.Columns.Add("SurvayDetailId", typeof(string));
                dtrow.Columns.Add("DeveloperID", typeof(int));
                dtrow.Columns.Add("OwnerID", typeof(string));
                dtrow.Columns.Add("OwnerArea", typeof(decimal));
                dtrow.Columns.Add("OwnerAreaUnitID", typeof(int));
                dtrow.Columns.Add("DocArea", typeof(decimal));
                dtrow.Columns.Add("DocAreaUnitID", typeof(int));
                dtrow.Columns.Add("Remark", typeof(string));
                dtrow.Columns.Add("isTotalArea", typeof(Boolean));

                // Adding Contact Person In DT
                if (ProjectAuthorityTable != null)
                {
                    if (ProjectAuthorityTable.Count > 0)
                    {
                        foreach (var item in ProjectAuthorityTable)
                        {
                            DataRow dr_row = dtrow.NewRow();
                            dr_row["SurvayDetailId"] = item.SurvayDetailId;
                            dr_row["DeveloperID"] = item.DeveloperID;
                            dr_row["OwnerID"] = item.OwnerID;
                            dr_row["OwnerArea"] = 0;
                            dr_row["OwnerAreaUnitID"] = 1;
                            dr_row["DocArea"] = item.DocArea;
                            dr_row["DocAreaUnitID"] = item.DocAreaUnitID;
                            dr_row["Remark"] = item.Remark;
                            dr_row["isTotalArea"] = item.isTotalArea;
                            dtrow.Rows.Add(dr_row);
                        }
                    }
                }


                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@ProjectAuthority";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtrow;
                tvpParamMobile.TypeName = "UTT_AuthorityChartTable";


                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_SaveAuthorityTable
                 
                @ProjectID  
                ,@ProjectAuthority
                ",
                new SqlParameter("@ProjectID", ProjectID)
                , tvpParamMobile

                );

                return Json("Success");

            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }

        }

        public ActionResult GetPropertyDetailsAfterSaction(int ProjectID, int SurvayDetailId)
        {
            
            OfficeDbContext _db = new OfficeDbContext();
            try
            {

                IEnumerable<ProjectDetailAfterSanction> result2 = _db.ProjectDetailAfterSanction.SqlQuery(@"exec GetPropertyDetailsAfterSanction
                @projectID,@SurvayDetailId",
              new SqlParameter("@projectID", ProjectID),
              new SqlParameter("@SurvayDetailId", SurvayDetailId)
              ).ToList<ProjectDetailAfterSanction>();
                ViewData["UnitList"] = binddropdown("UnitList", 0);
                ProjectDetailAfterSanction s = new ProjectDetailAfterSanction();
                s = result2.FirstOrDefault();
                return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("PropertyDetailsAfterSaction", s)
                  : View("PropertyDetailsAfterSaction", s);
            }

            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);

            } 
            return Request.IsAjaxRequest()
                  ? (ActionResult)PartialView("PropertyDetailsAfterSaction")
                  : View("PropertyDetailsAfterSaction");
        }

        [HttpPost]
        public ActionResult SaveSanctionDetails( ProjectDetailAfterSanction p)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                
                
                var result = _db.Database.ExecuteSqlCommand(@"exec InsertSanctionDetails
                 @SurvayDetailId 
               , @ProjectID   	
               , @SurvayNo    	
               , @SHissaNo    	
               , @SPlotNo     	
               , @GatNo     		
               , @GatHissaNo  	
               , @GatPlotNo   	
               , @CTSNo    	 	
               , @CTSHissaNo  	
               , @CTSPlotNo   	
               , @FInalPlotNo   	
               , @Nomenclature  	
               , @Area         	
               , @AreaUnitID   	
               , @isTobeHandover 
               , @isHandOver 	
               , @OwnershipName  
               , @HandOverDate   
               , @Documentnumber 
               , @RegistrarOffice 
                ",
                 
                 new SqlParameter("@SurvayDetailId"  , p.SurvayDetailId )
               , new SqlParameter("@ProjectID     "  , p.ProjectID      )
               , new SqlParameter("@SurvayNo      "  , p.SurvayNo == null ? (object)DBNull.Value :p.SurvayNo    )
               , new SqlParameter("@SHissaNo      "  , p.SHissaNo == null ? (object)DBNull.Value :p.SHissaNo      )
               , new SqlParameter("@SPlotNo       "  , p.SPlotNo == null ? (object)DBNull.Value :p.SPlotNo     )
               , new SqlParameter("@GatNo         "  , p.GatNo == null ? (object)DBNull.Value : p.GatNo)
               , new SqlParameter("@GatHissaNo    "  , p.GatHissaNo == null ? (object)DBNull.Value : p.GatHissaNo)
               , new SqlParameter("@GatPlotNo     "  , p.GatPlotNo == null ? (object)DBNull.Value : p.GatPlotNo)
               , new SqlParameter("@CTSNo         "  , p.CTSNo == null ? (object)DBNull.Value : p.CTSNo)
               , new SqlParameter("@CTSHissaNo    "  , p.CTSHissaNo == null ? (object)DBNull.Value : p.CTSHissaNo)
               , new SqlParameter("@CTSPlotNo     "  , p.CTSPlotNo == null ? (object)DBNull.Value :p.CTSPlotNo     )
               , new SqlParameter("@FInalPlotNo   "  , p.FInalPlotNo == null ? (object)DBNull.Value :p.FInalPlotNo   )
               , new SqlParameter("@Nomenclature  "  , p.Nomenclature == null ? (object)DBNull.Value :p.Nomenclature  )
               , new SqlParameter("@Area          "  , p.Area        )
               , new SqlParameter("@AreaUnitID    "  , p.AreaUnitID     )
               , new SqlParameter("@isTobeHandover"  , p.isTobeHandover )
               , new SqlParameter("@isHandOver    "  , p.isHandOver     )
               , new SqlParameter("@OwnershipName "  , p.OwnershipName  )
               , new SqlParameter("@HandOverDate  "  , p.HandOverDate == null ? (object)DBNull.Value :p.HandOverDate  )
               , new SqlParameter("@Documentnumber"  , p.Documentnumber == null ? (object)DBNull.Value :p.Documentnumber )
               , new SqlParameter("@RegistrarOffice" , p.RegistrarOffice == null ? (object)DBNull.Value :p.RegistrarOffice)
            );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return View("IndexForUser", message);
            }
        }


        public ActionResult GetCompanyInfoForCompare(int CompanyId = 0)
        {
            CompanyDetailsLeftSide data = new CompanyDetailsLeftSide();
            OfficeDbContext _db = new OfficeDbContext();


            IEnumerable<CompanyAddress> result1 = _db.CompanyAddress.SqlQuery(@"exec uspGetCompanyAddress
                @CompanyID",
            new SqlParameter("@CompanyID", CompanyId)
            ).ToList<CompanyAddress>();
            data.CompanyAddress = result1;

            IEnumerable<SaveCompanyMobile> result2 = _db.SaveCompanyMobile.SqlQuery(@"exec uspGetCompanyPhoneNo
                @CompanyID",
          new SqlParameter("@CompanyID", CompanyId)
          ).ToList<SaveCompanyMobile>();
            data.SaveCompanyMobile = result2;

            IEnumerable<SaveCompanyMobile> resultPhone = _db.SaveCompanyMobile.SqlQuery(@"exec uspGetTeamCompanyPhoneNo
                @CompanyID",
              new SqlParameter("@CompanyID", CompanyId)
              ).ToList<SaveCompanyMobile>();
            data.SaveCompanyMobile2 = resultPhone;

            IEnumerable<SaveCompanyMobile> resultSupportTeamPhone = _db.SaveCompanyMobile.SqlQuery(@"exec uspGetSupportTeamCompanyPhoneNo
                @CompanyID",
           new SqlParameter("@CompanyID", CompanyId)
           ).ToList<SaveCompanyMobile>();
            data.SaveCompanyMobile3 = resultSupportTeamPhone;

            IEnumerable<SaveCertification> result3 = _db.SaveCertification.SqlQuery(@"exec uspGetCompanyCertification
                @CompanyID",
             new SqlParameter("@CompanyID", CompanyId)
             ).ToList<SaveCertification>();
            data.SaveCertification = result3;

            IEnumerable<SaveInternalTeam> result4 = _db.SaveInternalTeam.SqlQuery(@"exec uspGetInternalTeam
                @CompanyID",
           new SqlParameter("@CompanyID", CompanyId)
           ).ToList<SaveInternalTeam>();
            data.SaveInternalTeam = result4;

            IEnumerable<SaveExternalTeam> result5 = _db.SaveExternalTeam.SqlQuery(@"exec uspGetExternalTeam
                @CompanyID",
          new SqlParameter("@CompanyID", CompanyId)
          ).ToList<SaveExternalTeam>();
            data.SaveExternalTeam = result5;
            data.CompanyID = CompanyId;

            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("PartialCompanyAddress", data)
               : View("PartialCompanyAddress", data);

        }
        public ActionResult GetProjectInnerInfo(int ProjectID = 0)
        {
            OfficeDbContext _db = new OfficeDbContext();
            ProjectDetailsLeftSide data = new ProjectDetailsLeftSide();

            IEnumerable<SaveProjectInternalTeam> result = _db.SaveProjectInternalTeam.SqlQuery(@"exec uspGetProjectInternalTeam
                @ProjectID",
           new SqlParameter("@ProjectID", ProjectID)
           ).ToList<SaveProjectInternalTeam>();
            data.SaveProjectInternalTeam = result;

            IEnumerable<SaveProjectExternalTeam> result2 = _db.SaveProjectExternalTeam.SqlQuery(@"exec uspGetProjectExternalTeam
                @ProjectID",
          new SqlParameter("@ProjectID", ProjectID)
          ).ToList<SaveProjectExternalTeam>();

            IEnumerable<SaveProjectOfficeSideTeam> result3 = _db.SaveProjectOfficeSideTeam.SqlQuery(@"exec uspGetProjectOfficeSideTeam
                @ProjectID",
        new SqlParameter("@ProjectID", ProjectID)
        ).ToList<SaveProjectOfficeSideTeam>();

            IEnumerable<AuthoritySignatory> result4 = _db.AuthoritySignatory.SqlQuery(@"exec GetProjectSignatory
                @ProjectID",
        new SqlParameter("@ProjectID", ProjectID)
        ).ToList<AuthoritySignatory>();

            IEnumerable<AuthoritySignatoryDetail> result5 = _db.AuthoritySignatoryDetail.SqlQuery(@"exec GetProjectSignatoryDetail
                @ProjectID",
        new SqlParameter("@ProjectID", ProjectID)
        ).ToList<AuthoritySignatoryDetail>();

            data.SaveProjectOfficeSideTeam = result3;
            data.SaveProjectExternalTeam = result2;
            data.ProjectID = ProjectID;
            data.AuthoritySignatory = result4;
            data.AuthoritySignatoryDetail = result5;

            var result8 = _db.nProjectDetail.SqlQuery(@"exec uspGetnProjectDetails
                   @ProjectID",
                  new SqlParameter("@ProjectID", ProjectID)
                  ).ToList<nProjectDetail>();
            data.nProjectDetail = result8.FirstOrDefault();

            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("ProjectInnerInfo", data)
               : View("ProjectInnerInfo", data);
        }

        public ActionResult ProjectDeveloper(int ProjectID )
        {
            OfficeDbContext _db = new OfficeDbContext();
             
            IEnumerable<ProjectDeveloper> result = _db.ProjectDeveloper.SqlQuery(@"exec GetProjectDeveloper   @ProjectID",
                  new SqlParameter("@ProjectID", ProjectID)).ToList<ProjectDeveloper>();

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("ProjectDeveloper", result)
                    : View("ProjectDeveloper", result);
        }
        
        [HttpPost]
        public ActionResult SaveProjectSurvay(int ProjectID=0 ,string SurvayNo ="", int SurvayDetailId = 0,int SurveyTypeID=1,string HissaNo ="", decimal Area=0,int UnitID =1)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                DataTable dtMobile = new DataTable();
                var result = _db.Database.ExecuteSqlCommand(@"exec saveSurvay
                 @ProjectID     
                ,@SurveyTypeID   	
                ,@SurvayNo  	
                ,@HissaNo    	
                ,@Area   
                ,@UnitID    		
                ,@SurvayDetailId    
                 ",
                
                new SqlParameter("@ProjectID", ProjectID),
                new SqlParameter("@SurveyTypeID", SurveyTypeID),
                new SqlParameter("@SurvayNo", SurvayNo == null ? (object)DBNull.Value :  SurvayNo),
                new SqlParameter("@HissaNo",   HissaNo == null ? (object)DBNull.Value :HissaNo),
                new SqlParameter("@Area",  Area ),
                new SqlParameter("@UnitID", UnitID),
                new SqlParameter("@SurvayDetailId",   SurvayDetailId)
                  
                );
                return Json("Success");
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);
            }
        }
    }
}