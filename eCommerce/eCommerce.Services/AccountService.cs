using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eCommerce.Contracts;
using eCommerce.Contracts.Modules;

namespace eCommerce.Services
{
    public class AccountService
    {
        IRepositoryBase<Customer> customers;

        public const string UserSessionName = "eCommerceUser";

        public AccountService(IRepositoryBase<Customer> customers)
        {
            this.customers = customers;
        }
    }
}
