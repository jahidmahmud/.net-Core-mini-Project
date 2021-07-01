using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Models
{
    public class ChangePasswordModel
    {
        [Required,Display(Name ="Current Password"),DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required, Display(Name = "New Password"), DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required, Display(Name = "Confirm New Password"), DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
