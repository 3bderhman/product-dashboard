using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Model
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Password Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
