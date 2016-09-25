using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//..----------------------------------------
using SendGrid;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using eCommerce.Model;
using eCommerce.Contracts;
using eCommerce.Contracts.Modules;
using eCommerce.Contracts.Repositories;

namespace eCommerce.Services
{
    public class EmailService: Customer
    {
        //IRepositoryBase<Customer> customers;

        //public async Task SendAsync(Customer message)
        //{
        //    await configSendGridasync(message);
        //}

        //private async Task configSendGridasync(Customer message)
        //{
        //    var myMessage = new SendGridMessage();
        //    myMessage.AddTo(message.de)
            
        //}
    }
}
