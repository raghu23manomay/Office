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
        public ActionResult AddCompanyInfo()
        {
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

            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AddCompanyInfo")
                    : View("AddCompanyInfo");
        }
        public ActionResult AddPersonInfo()
        {
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            ViewData["ConactPersonList"] = binddropdown("ConactPersonList", 0);
            ViewData["PersonList"] = binddropdown("PersonList", 0);
            ViewData["ConsultantTypeList"] = binddropdown("ConsultantTypeList", 0);
            ViewData["ContractorTypeList"] = binddropdown("ContractorTypeList", 0);

            ViewData["OwnershipTypeList"] = binddropdown("OwnershipTypeList", 0);
            ViewData["BusinessCategoryList"] = binddropdown("BusinessCategoryList", 0);
            ViewData["BusinessSubCategoryList"] = binddropdown("BusinessSubCategoryList", 0);
            ViewData["CertificationList"] = binddropdown("CertificationList", 0);
            ViewData["CompanyList"] = binddropdown("CompanyList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AddPersonInfo")
                    : View("AddPersonInfo");
        }
        [HttpPost]
        public ActionResult SaveCompany(CompanyDetails Company,  List<SaveCompanyMobile> SaveMobile,List<CompanyAddress> SaveAddress , List<SaveInternalTeam> SaveInternalTeam, List<SaveExternalTeam> SaveExternalTeam)
        {
            try
            {
                DataTable dtMobile = new DataTable();   
                DataTable dtEmail = new DataTable();
                DataTable dtAddress = new DataTable();
                DataTable dtInternalTeam = new DataTable();
                DataTable dtExternalTeam = new DataTable();
                dtMobile.Columns.Add("CompanyPhoneID", typeof(string));
                dtMobile.Columns.Add("PhoneType", typeof(int));
                dtMobile.Columns.Add("PhoneNumber", typeof(string)); 
                dtMobile.Columns.Add("WorkDepartmentID", typeof(int));
                dtMobile.Columns.Add("Extension", typeof(string));
                
               
    
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

                // Adding Contact Person In DT
                if (SaveMobile != null)
                {
                    if (SaveMobile.Count > 0)
                    {
                        foreach (var item in SaveMobile)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            dr_Mobile["PhoneNumber"] = item.Value;
                            dr_Mobile["PhoneType"] = item.Type;
                            dr_Mobile["WorkDepartmentID"] = item.WorkDepartmentID;
                            dr_Mobile["Extension"] = item.Extension;
                            dtMobile.Rows.Add(dr_Mobile);
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
                            dr_SaveAddress["AddressID"] = 0;
                            dr_SaveAddress["AddressType"] = 1;
                            dr_SaveAddress["Address1"] = item.OfcAddress1;
                            dr_SaveAddress["Address2"] = item.OfcAddress2;
                            dr_SaveAddress["StateID"] = item.StateID2;
                            dr_SaveAddress["District"] = item.OfcDistrict;
                            dr_SaveAddress["CityID"] = item.CityID2;
                            dr_SaveAddress["ZipCode"] = item.OfcZip;
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
                ",
                new SqlParameter("@CompanyID", -1),
                new SqlParameter("@CompanyName", Company.CompanyName), 
                new SqlParameter("@ShortName", Company.ShortName),
                new SqlParameter("@BusinessCategoryID", Company.BusinessCategoryID),
                new SqlParameter("@BusinessSubCategoryID", Company.BusinessSubCategoryID),
                new SqlParameter("@CertificationID", Company.CertificationID),
                new SqlParameter("@CompanyOwnershipTypeID", Company.CompanyOwnershipTypeID),
                new SqlParameter("@Isclient", Company.Isclient),
                new SqlParameter("@RequestDate", Company.RequestDate),
                new SqlParameter("@InceptionDate", Company.InceptionDate),
                new SqlParameter("@CreatedBy", 1)  
                , tvpParamMobile 
                , tvpParamAddress 
                , tvpParamInternalTeam
                ,tvpParamExternalTeam
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
        public ActionResult SavePersonInfo(PersonInfo Mem, List<SaveMobilePerson> SaveMobilePerson, List<SaveAdditionalFiled> SaveAdditionalFiled, List<SaveParameter> SaveParameter)
        {
            try
            { 
                 
                DataTable dtMobile = new DataTable();
                
                DataTable dtAdditionalFiled = new DataTable();
                DataTable dtParameter = new DataTable();
                DataTable dtMemberPerson = new DataTable();
                dtMobile.Columns.Add("id", typeof(int));
                dtMobile.Columns.Add("Type", typeof(string));
                dtMobile.Columns.Add("Value", typeof(string));
                dtMobile.Columns.Add("DepartmentID", typeof(string));
                dtAdditionalFiled.Columns.Add("AdditionlField", typeof(string));
                dtParameter.Columns.Add("Parameter", typeof(string));
                dtMemberPerson.Columns.Add("PersonMemberID", typeof(int));
                dtMemberPerson.Columns.Add("DesignationId", typeof(int));
                 
                // Adding Contact Person In DT
                if (SaveMobilePerson != null)
                {
                    if (SaveMobilePerson.Count > 0)
                    {
                        foreach (var item in SaveMobilePerson)
                        {
                            DataRow dr_Mobile = dtMobile.NewRow();
                            dr_Mobile["id"] = 0;
                            dr_Mobile["Type"] = item.Type;
                            dr_Mobile["Value"] = item.Value;
                            dr_Mobile["DepartmentID"] = item.DepartmentID;
                            dtMobile.Rows.Add(dr_Mobile);
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

                SqlParameter tvpParamMobile = new SqlParameter();
                tvpParamMobile.ParameterName = "@MemberMobileParam";
                tvpParamMobile.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParamMobile.Value = dtMobile;
                tvpParamMobile.TypeName = "UTT_PersonMobile";
                  
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
                ,@MemberMobileParam  
                ,@MemberAdditionalFeildParam
                ,@MemberParameterParam
              ",
                new SqlParameter("@PersonId", Mem.PersonID == null ? 0 : Mem.PersonID), 
                new SqlParameter("@TitleID", Mem.TitleID),
                new SqlParameter("@fName", Mem.fName == null ? (object)DBNull.Value : Mem.fName),
                new SqlParameter("@mName", Mem.fName == null ? (object)DBNull.Value : Mem.mName),
                new SqlParameter("@lName", Mem.fName == null ? (object)DBNull.Value : Mem.lName),
                new SqlParameter("@ShortName", Mem.ShortName),
                new SqlParameter("@Website", Mem.Website),  
                new SqlParameter("@Address1", Mem.ResidentialAddress1 == null ? (object)DBNull.Value : Mem.ResidentialAddress1),
                new SqlParameter("@Address2", Mem.ResidentialAddress2 == null ? (object)DBNull.Value : Mem.ResidentialAddress2),
                new SqlParameter("@StateID", Mem.ResidentialStateID == null ? (object)DBNull.Value : Mem.ResidentialStateID),
                new SqlParameter("@District", Mem.ResidentialDistrict == null ? (object)DBNull.Value : Mem.ResidentialDistrict),
                new SqlParameter("@CityID", Mem.ResidentialCityID == null ? (object)DBNull.Value : Mem.ResidentialCityID),
                new SqlParameter("@ZipCode", Mem.ResidentialZipCode == null ? (object)DBNull.Value : Mem.ResidentialZipCode),
                new SqlParameter("@BirthDate", Mem.BirthDate), 
                new SqlParameter("@CreatedBy", 1)
                , tvpParamMobile 
                , tvpParamAdditionalFeild
                , tvpParamParameter
              
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

        public ActionResult FliterStateWiseCity(int StateID = 0)
        {
           
            ViewData["CityList"] = binddropdown("StateWiseCityList",0,StateID);            
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("_StateWsieCityFilter")
                    : View("_StateWsieCityFilter");
        }
        public ActionResult CategoryWiseSubCategory(int CategoryID = 0, int val = 0)
        {
            if (val == 0)
            {
                ViewBag.value = 1;
                ;
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

        public ActionResult BindTeamSubDesignation(int DesignationID = 0,int val=0)
        {
            if(val==0)
            {
                ViewBag.value = 1;
;            }
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

        public ActionResult AddPerson(int? id)
        {
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AddPerson")
                    : View("AddPerson");
        }

        // Getting Person Details For Update
        public ActionResult EditPerson(int? Id)
        {
            OfficeDbContext _db = new OfficeDbContext();

            var result = _db.DFPerson.SqlQuery(@"exec GetPersonDetailsForupdate
               @Id",
               new SqlParameter("@Id", Id)
               ).ToList<Person>();

            Person data = new Person();
            data = result.FirstOrDefault();            
            string Dob = data.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);            
            ViewBag.Dob = Dob;
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            return View("EditPerson", data);

        }

        public ActionResult GetPersonMobileList(int? Id)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<PersonMobileList> result = _db.DFPersonMobileList.SqlQuery(@"exec GetContactPersonMobileList
               @Id",
               new SqlParameter("@Id", Id)
               ).ToList<PersonMobileList>();

            return View("_GetContactPersonMobileList", result);

        }

        public ActionResult GetPersonEmailList(int? Id)
        {
            OfficeDbContext _db = new OfficeDbContext();

            IEnumerable<PersonEmailList> result = _db.DFPersonEmailList.SqlQuery(@"exec GetContactPersonEmailList
               @Id",
               new SqlParameter("@Id", Id)
               ).ToList<PersonEmailList>();

            return View("_GetContactPersonEmailList", result);

        }


        [HttpPost]
        public ActionResult DeleteContactPersonMobile(int? Id)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec DeleteContactPersonMobile 
                 @Id",
                new SqlParameter("@Id", Id));

                return Json("Mobile Deleted");
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }

        [HttpPost]
        public ActionResult DeleteContactPersonEmail(int? Id)
        {
            try
            {
                OfficeDbContext _db = new OfficeDbContext();
                var result = _db.Database.ExecuteSqlCommand(@"exec DeleteContactPersonEmail 
                 @Id",
                new SqlParameter("@Id", Id));

                return Json("Email Deleted");
            }
            catch (Exception ex)
            {

                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(message);

            }
        }

        [HttpPost]
        public ActionResult SavePerson(Person Cp, List<SaveMobile> SaveMobile,List<SaveEmail> SaveEmail)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                dt.Columns.Add("Mobile", typeof(string));
                dt1.Columns.Add("Email", typeof(string));

                if (SaveMobile != null)
                {
                    if (SaveMobile.Count > 0)
                    {
                    foreach (var item in SaveMobile)
                    {
                           DataRow dr = dt.NewRow();
                           dr["Mobile"] = item.Mobile;                            
                           dt.Rows.Add(dr);
                    }
                    }
                }
                if (SaveEmail != null)
                {
                    if (SaveEmail.Count > 0)
                    {                        
                        foreach (var item in SaveEmail)
                        {
                            DataRow dr1 = dt1.NewRow();
                            dr1["Email"] = item.Email;
                            dt1.Rows.Add(dr1);
                        }
                    }
                }

                SqlParameter tvpParam = new SqlParameter();
                tvpParam.ParameterName = "@MobileParameters";
                tvpParam.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParam.Value = dt;
                tvpParam.TypeName = "UT_PersonMobile";

                SqlParameter tvpParam1 = new SqlParameter();
                tvpParam1.ParameterName = "@EmailParameters";
                tvpParam1.SqlDbType = System.Data.SqlDbType.Structured;
                tvpParam1.Value = dt1;
                tvpParam1.TypeName = "UT_PersonEmail";


                OfficeDbContext _db = new OfficeDbContext();
                Boolean Active = true;
                if (Cp.IsActive.ToString() == "false")
                {
                    Active = false;
                }
                var result = _db.Database.ExecuteSqlCommand(@"exec USP_SavePerson 
                @PersonalInfoID
               ,@Prefix    
               ,@FirstName 
               ,@MiddleName
               ,@LastName  
               ,@BirthDate 
               ,@Address1   
               ,@Address2   
               ,@DesignationId
               ,@Note  
               ,@CreatedBy    
               ,@IsActive
               ,@MobileParameters
               ,@EmailParameters",
                new SqlParameter("@PersonalInfoID", Cp.PersonalInfoID),
                new SqlParameter("@Prefix",Cp.Prefix),
                new SqlParameter("@FirstName", Cp.FirstName),
                new SqlParameter("@MiddleName", Cp.MiddleName == null ? (object)DBNull.Value : Cp.MiddleName),
                new SqlParameter("@LastName", Cp.LastName),
                new SqlParameter("@BirthDate", Cp.BirthDate == null ? (object)DBNull.Value : Cp.BirthDate),
                new SqlParameter("@Address1", Cp.Address1 == null ? (object)DBNull.Value : Cp.Address1),
                new SqlParameter("@Address2", Cp.Address2 == null ? (object)DBNull.Value : Cp.Address2),
                new SqlParameter("@DesignationId", Cp.DesignationId == null ? (object)DBNull.Value : Cp.DesignationId),
                new SqlParameter("@Note", Cp.Note == null ? (object)DBNull.Value : Cp.Note),
                new SqlParameter("@CreatedBy",1),
                new SqlParameter("@IsActive", Active),
                tvpParam == null ? (object)DBNull.Value : tvpParam,
                tvpParam1 == null ? (object)DBNull.Value : tvpParam1
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
    }
}