using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Helpers
{
    public class MYCustomValidationAttribute:ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                string bookName = value.ToString();
                if (bookName.Contains("MVC") || bookName.Contains("mvc"))
                {
                    return ValidationResult.Success;
                }
            }
            //return base.IsValid(value, validationContext);
            return new ValidationResult(ErrorMessage??"This is error");
        }
    }
}
