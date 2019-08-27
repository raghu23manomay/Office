using office.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public ActionResult AddMember()
        {
            ViewData["DesignationList"] = binddropdown("DesignationList", 0);
            ViewData["CityList"] = binddropdown("CityList", 0);
            ViewData["StateList"] = binddropdown("StateList", 0);
            ViewData["ConactPersonList"] = binddropdown("ConactPersonList", 0);
            ViewData["PersonList"] = binddropdown("PersonList", 0);
            return Request.IsAjaxRequest()
                    ? (ActionResult)PartialView("AddMember")
                    : View("AddMember");
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
                 @MemberType
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
                new SqlParameter("@MemberType",Mem.MemberType),
                new SqlParameter("@TitleID",Mem.TitleID),
                new SqlParameter("@Name",Mem.Name == null ? (object)DBNull.Value : Mem.Name),
                new SqlParameter("@ShortName",Mem.ShortName),
                new SqlParameter("@CompanyName",Mem.CompanyName),
                new SqlParameter("@DesignationId",Mem.DesignationId),
                new SqlParameter("@Website",Mem.Website),
                 new SqlParameter("@ResidentialAddress1",Mem.ResidentialAddress1 == null ? (object)DBNull.Value : Mem.ResidentialAddress1),
                 new SqlParameter("@ResidentialAddress2",Mem.ResidentialAddress2 == null ? (object)DBNull.Value : Mem.ResidentialAddress2),
                 new SqlParameter("@ResidentialStateID",Mem.ResidentialStateID),
                 new SqlParameter("@ResidentialDistrict",Mem.ResidentialDistrict == null ? (object)DBNull.Value : Mem.ResidentialDistrict),
                 new SqlParameter("@ResidentialCityID", Mem.ResidentialCityID),
                 new SqlParameter("@ResidentialZipCode",  Mem.ResidentialZipCode == null ? (object)DBNull.Value : Mem.ResidentialZipCode),
                new SqlParameter("@OfficeAddress1",Mem.OfficeAddress1),
                new SqlParameter("@OfficeAddress2",Mem.OfficeAddress2),
                new SqlParameter("@StateID",Mem.StateID),
                new SqlParameter("@District",Mem.DesignationId),
                new SqlParameter("@CityID",Mem.CityID),
                new SqlParameter("@ZipCode",Mem.ZipCode),
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
                @Prefix    
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

    }
}