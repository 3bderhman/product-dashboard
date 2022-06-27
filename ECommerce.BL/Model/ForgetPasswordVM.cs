﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Model
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid mail")]
        public string Email { get; set; }
    }
}
