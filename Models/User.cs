using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace ShoppingCart_9b.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Username!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password!")]
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = Crypto.HashPassword(value);
            }
        }

        public Boolean IsPWValid(String plainPW)
        {
           
            return Crypto.VerifyHashedPassword(Password, plainPW);
        }
    }
}
