using eCommerce.WebUI.App_Start;
using eCommerce.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Practices.Unity.Mvc;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(eCommerce.WebUI.Startup))]
namespace eCommerce.WebUI
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.GetConfiguredContainer()));
            //app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());
            DataProtectionProvider = app.GetDataProtectionProvider();

        }



    }
}
