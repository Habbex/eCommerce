using eCommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace eCommerce.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AreaRegistration.RegisterAllAreas();

            var context = new ApplicationDbContext();
            if (!context.Users.Any(user => user.UserName == "ihabokal@hotmail.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var applicationUser = new ApplicationUser() { UserName = "ihabokal@hotmail.com" };
                userManager.Create(applicationUser, "Habbe89-");

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole("Admin"));

                userManager.AddToRole(applicationUser.Id, "Admin");

            }
        }

    }
}
