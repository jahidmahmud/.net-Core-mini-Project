using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Data
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}
