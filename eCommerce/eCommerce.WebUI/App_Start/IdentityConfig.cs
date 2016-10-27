using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using eCommerce.WebUI.Models;
using Microsoft.Owin.Security.DataProtection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace eCommerce.WebUI
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly ApplicationDbContext context;

        public ApplicationUserManager(ApplicationDbContext context, IUserStore<ApplicationUser> store)
            : base(store)
        {

            this.context = context;
            //var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });
            this.EmailService = new EmailService();
            this.SmsService = new SmsService();
            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                //manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
                IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");
                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtector);
            }
            //return manager;



        }

        public IEnumerable<ApplicationUser> GetUsersInRole(string RoleName)
        {


            if (string.IsNullOrWhiteSpace(RoleName))
            {
                throw new ArgumentNullException(nameof(RoleName));
            }

            var role = context.Roles.FirstOrDefault(r => r.Name == RoleName);


            if (role == null)
            {
                throw new Exception($"Role with this name not found: {RoleName}");
            }

            var users = context.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).ToList();

            return users;
        }


        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //{
        //    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
        //    // Configure validation logic for usernames
        //    manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        //    {
        //        AllowOnlyAlphanumericUserNames = false,
        //        RequireUniqueEmail = true
        //    };
        //    // Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = true,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireUppercase = true,
        //    };
        //    // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        //    // You can write your own provider and plug in here.
        //    manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
        //    {
        //        MessageFormat = "Your security code is: {0}"
        //    });
        //    manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
        //    {
        //        Subject = "Security Code",
        //        BodyFormat = "Your security code is: {0}"
        //    });
        //    manager.EmailService = new EmailService();
        //    manager.SmsService = new SmsService();
        //    var dataProtectionProvider = options.DataProtectionProvider;
        //    if (dataProtectionProvider != null)
        //    {
        //        manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }
        //    return manager;
        //}
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
