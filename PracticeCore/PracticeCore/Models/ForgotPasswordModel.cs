using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Models
{
    public class ForgotPasswordModel
    {
        [Required,EmailAddress]
        public String Email { get; set; }
        public bool EmailSend { get; set; }
    }
}
