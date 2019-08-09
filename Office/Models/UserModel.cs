using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace office.Models
{
    public class UserModel
    {
    }
    public class UserPermission
    {
        [Key]
        public int user_id { get; set; }
        public string fullname { get; set; }
        public string RoleName { get; set; }

    }

    public class AddUserPermission
    {
        [Key]
        public int ActionId { get; set; }
        public int? MenuId { get; set; }
        public int? SubMenuId { get; set; }
        public string submenuname { get; set; }
        public string FeatureName { get; set; }
        public int? UserId { get; set; }
        public int? Permission { get; set; } 
    }

    public partial class SaveUserAccess
    {
        [Key]
        public int Actionid { get; set; }
        public int Userid { get; set; }
        public int PermissionValue { get; set; }
    }
     
}