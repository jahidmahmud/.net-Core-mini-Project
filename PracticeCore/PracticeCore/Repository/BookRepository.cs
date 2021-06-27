using Microsoft.EntityFrameworkCore;
using PracticeCore.Data;
using PracticeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Repository
{
    public class BookRepository
    {
        BookStoreContext context;
        public BookRepository(BookStoreContext context1)
        {
            context = context1;
        }
            public async Task<List<Book>> GetAll()
        {
            var book = new List<Book>();
            var allbooks = await context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
               foreach(var item in allbooks)
                {
                    book.Add(new Book()
                    {
                        id=item.id,
                        title=item.title,
                        author=item.author,
                        Description=item.Description,
                        Category=item.Category,
                        LanguageId=item.LanguageId,
                        Totalpage =item.Totalpage,
                        CoverUrl=item.Cover
                    });
                }
            }

            return book;
        }
        public async Task<int> Add(Book b)
        {
            var book = new Books()
            {
                title = b.title,
                author = b.author,
                Description = b.Description,
                Totalpage = b.Totalpage,
                LanguageId=b.LanguageId,
                Cover=b.CoverUrl,
                BookPdfUrl=b.BookPdfUrl,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            var galary = new List<BookGalary>();
            foreach (var file in b.Galary)
            {
                galary.Add(new BookGalary()
                {
                    Name=file.Name,
                    Url=file.Url
                });
            }
            book.bookGalary = galary;
            await context.AddAsync(book);
            await context.SaveChangesAsync();

            return book.id;
        }
        public async Task<Book> GetById(int id)
        {
            //var item=await context.Books.FindAsync(id);
            return await context.Books.Where(x => x.id == id).
                Select(book => new Book() {
                    id = book.id,
                    title = book.title,
                    author = book.author,
                    Description = book.Description,
                    Category = book.Category,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Totalpage = book.Totalpage,
                    CoverUrl = book.Cover,
                    BookPdfUrl=book.BookPdfUrl,
                    Galary = book.bookGalary.Select(x => new GalaryModel()
                    {
                        Id=x.Id,
                        Name=x.Name,
                        Url=x.Url
                    }).ToList()
                }).FirstOrDefaultAsync();
            //Book b = new Book();
            //if (item != null)
            //{
            //    b = new Book()
            //    {
            //        id = item.id,
            //        title = item.title,
            //        author = item.author,
            //        Description = item.Description,
            //        Category = item.Category,
            //        LanguageId = item.LanguageId,
            //        Language = item.Language.Name,
            //        Totalpage = item.Totalpage
            //    };
            //}
            //return b;
        }
        //public List<Book> SearchBook(string title,string author)
        //{
        //    return Data().Where(x=>x.title==title || x.author==author).ToList();
        //}
        //private List<Book> Data()
        //{
        //    return new List<Book>()
        //    {
        //        new Book() { id=1,title="jahid",author="Mahmud",Description="Invest in some me-time today, order your favourite beauty essentials and pamper yourself.",Category="Romantic",Language="Bangla",Totalpage=130},
        //        new Book() { id=2,title="Rakib",author="Hasan",Description="Sometimes, splurging on health & beauty essentials is an investment. Find premium skinsentials at foodpanda shops.",Category="Science Friction",Language="English",Totalpage=200}
        //    };
        //}
    }
}
