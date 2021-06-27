using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Data
{
    public class Books
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Cover { get; set; }
        public string BookPdfUrl { get; set; }
        public int LanguageId { get; set; }
        public int Totalpage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Language Language { get; set; }
        public IEnumerable<BookGalary> bookGalary { get; set; }
    }
}
