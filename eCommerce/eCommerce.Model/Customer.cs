using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using eCommerce.Contracts.Models;

namespace eCommerce.Model
{
    public class Customer: ICustomer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage ="Please enter your first name. ")]
        public string CustomerF_Name { get; set; }

        [Required(ErrorMessage = "Please enter your last name. ")]
        public string CustomerL_Name { get; set; }

        [Required(ErrorMessage = "Please enter your Email address. ")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
+ "@"
+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your User Name. ")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password you have entered is not the same! ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your address. ")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "Please enter your town. ")]
        public string Town { get; set; }
        [Required(ErrorMessage = "Please enter your PostCode. ")]
        public string PostCode { get; set; }
    }
}
