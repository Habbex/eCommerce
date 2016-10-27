using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using eCommerce.WebUI.Models;
using eCommerce.WebUI.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eCommerce.WebUI.Controllers
{
    public class RolesController : ApplicationBaseController
    {
        private readonly ApplicationUserManager _userManager;

        //ApplicationDbContext context = new ApplicationDbContext();
        private readonly ApplicationDbContext context;

        public RolesController(ApplicationDbContext context, ApplicationUserManager _userManager)
        {
            //var userList = new List<ApplicationUser>();
            this.context = context;
            this._userManager = _userManager;
        }


        // GET: /Roles
        public ActionResult Index()
        {
            //var roles = context.Roles.ToList();
            //return View(roles);

            GroupedUsersandIdentityView GroupedView= new GroupedUsersandIdentityView();
            GroupedView.Roles = GetIdentityRole();
            GroupedView.UserwithRoles = GetUsersWithRoles();
            return View(GroupedView);

        }

        // get: users with roles
        public IEnumerable<IdentityRole> GetIdentityRole()
        {
            var roles = context.Roles.ToList();
            return roles;

        }

        public List<UserListviewModel> GetUsersWithRoles()
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

            return model;
        }


        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole =
                context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                context.Entry(role).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Delete/5
        public ActionResult Delete(string RoleName)
        {
            var thisRole =
                context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageUserRoles()
        {
            var list =
                context.Roles.OrderBy(r => r.Name)
                    .ToList()
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                    .ToList();
            ViewBag.Roles = list;

            var userlist =
                context.Users.OrderBy(u => u.UserName)
                    .ToList()
                    .Select(uu => new SelectListItem {Value = uu.UserName.ToString(), Text = uu.UserName})
                    .ToList();
            ViewBag.Users = userlist;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            var user =
                context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();
            //var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            _userManager.AddToRole(user.Id, RoleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list =
                context.Roles.OrderBy(r => r.Name)
                    .ToList()
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                    .ToList();
            ViewBag.Roles = list;

            var userlist =
                context.Users.OrderBy(u => u.UserName)
                    .ToList()
                    .Select(uu => new SelectListItem {Value = uu.UserName.ToString(), Text = uu.UserName})
                    .ToList();
            ViewBag.Users = userlist;

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                var user =
                    context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase))
                        .FirstOrDefault();

                //var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ViewBag.RolesForThisUser = _userManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list =
                    context.Roles.OrderBy(r => r.Name)
                        .ToList()
                        .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                        .ToList();
                ViewBag.Roles = list;

                var userlist =
                    context.Users.OrderBy(u => u.UserName)
                        .ToList()
                        .Select(uu => new SelectListItem {Value = uu.UserName.ToString(), Text = uu.UserName})
                        .ToList();
                ViewBag.Users = userlist;
            }

            return View("ManageUserRoles");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var user =
                context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();
            //var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (_userManager.IsInRole(user.Id, RoleName))
            {
                _userManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list =
                context.Roles.OrderBy(r => r.Name)
                    .ToList()
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                    .ToList();
            ViewBag.Roles = list;

            var userlist =
                context.Users.OrderBy(u => u.UserName)
                    .ToList()
                    .Select(uu => new SelectListItem {Value = uu.UserName.ToString(), Text = uu.UserName})
                    .ToList();
            ViewBag.Users = userlist;

            return View("ManageUserRoles");
        }
    }
}