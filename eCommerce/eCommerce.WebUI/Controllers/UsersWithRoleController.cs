using eCommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace eCommerce.WebUI.Controllers
{
    public class UsersWithRoleController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationDbContext context;

        public UsersWithRoleController(ApplicationUserManager _userManager, ApplicationDbContext context)
        {
            this._userManager = _userManager;
            this.context = context;
        }

        //public ActionResult GetUsersWithRoles(string roleName)
        //{
        //    var users = _userManager.GetUsersInRole(roleName);

        //    var viewModel = new UserListviewModel()
        //    {
        //        RoleName = roleName,
        //        Users = users,
        //    };

        //    return View(viewModel);
        //}

        public ActionResult GetUsersWithRoles()
        {
            var model = new List<UserListviewModel>();
            foreach (var user in _userManager.Users)
            {
                var model_user = new UserListviewModel
                {
                    Users = user.UserName
                };
                model.Add(model_user);
            }

            foreach (var user in model)
            {
                user.RoleName = _userManager.GetRoles(_userManager.Users.First(s => s.UserName == user.Users).Id);
            }

            return View(model);
        }



    }

    //public class GetUsersWithRolesViewModel
    //{
    //    public string RoleName { get; set; }
    //    public IEnumerable<ApplicationUser> Users { get; set; }
    //}
}