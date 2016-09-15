using System;


namespace eCommerce.Contracts.Models
{
    public interface ICustomer
    {
        
         int CustomerId { get; set; }
        
         string CustomerF_Name { get; set; }

        
         string CustomerL_Name { get; set; }

        string UserName { get; set; }


        string Email { get; set; }

         string Password { get; set; }

       
         string ConfirmPassword { get; set; }

        
         string Address1 { get; set; }

       
         string Town { get; set; }
        
         string PostCode { get; set; }
    }
}
