using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

using System.ComponentModel.DataAnnotations;

namespace eCommerce.WebUI.Models
{
    public class GroupedUsersandIdentityView
    {
        public IEnumerable <UserListviewModel> UserwithRoles { get; set; }
        public IEnumerable <IdentityRole> Roles { get; set; }
    }

    public class UserListviewModel
    {


        //public string RoleName { get; set; }
        //public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<string>RoleName { get; set; }
        public string Users { get; set; }

        //public IList<string> RoleName { get; set; }
        //public IList<ApplicationUser> Users { get; set; }
    }
}
