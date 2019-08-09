using office.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public List<SelectListItem> binddropdown(string action, int val = 0)
        {
            OfficeDbContext _db = new OfficeDbContext();

            var res = _db.Database.SqlQuery<SelectListItem>("exec BindDropDown @action , @val",
                   new SqlParameter("@action", action),
                    new SqlParameter("@val", val))
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
    }
}