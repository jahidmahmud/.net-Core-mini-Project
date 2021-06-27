using Microsoft.AspNetCore.Http;
using PracticeCore.Enums;
using PracticeCore.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Models
{
    public class Book
    {
        public int id { get; set; }
        [Display(Name ="Title")]
        //[StringLength(30,MinimumLength =5)]
        //[Required(ErrorMessage ="Title field is empty")]
        [MYCustomValidation]
        public string title { get; set; }
        [Required]
        [Display(Name = "Author")]
        public string author { get; set; }
        [Required(ErrorMessage ="Description field is empty")]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Required]
        public LanguageEnum LanguageEnum { get; set; }

        [Required]
        public int Totalpage { get; set; }
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverUrl { get; set; }
        [Required]
        public IFormFileCollection GalaryFiles { get; set; }
        public List<GalaryModel> Galary { get; set; }
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
}
