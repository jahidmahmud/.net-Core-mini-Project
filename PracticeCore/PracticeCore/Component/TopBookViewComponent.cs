using Microsoft.AspNetCore.Mvc;
using PracticeCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Component
{
    public class TopBookViewComponent:ViewComponent
    {
        IBookRepository bookrepo;
        public TopBookViewComponent(IBookRepository br)
        {
            bookrepo = br;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await bookrepo.GetTopBookAsync();
            return View(books);
        }
    }
}
