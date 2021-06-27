using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticeCore.Models;
using PracticeCore.Repository;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Controllers
{
    public class BookController : Controller
    {
        BookRepository bookrepo;
        LanguageRepository langrepo;
        IWebHostEnvironment webenvironment;
        public BookController(BookRepository br,LanguageRepository lr, IWebHostEnvironment webhostenvironment)
        {
            bookrepo =br;
            langrepo = lr;
            webenvironment = webhostenvironment;
        }
        public async Task<IActionResult> Index()
        {
            var data =await bookrepo.GetAll();
            return View(data);
        }
        [Route("GetBook/{id}",Name ="BookDetails")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var data =await bookrepo.GetById(id);
            return View(data);
        }
        //public List<Book> GetBooklist(string title,string author)
        //{
        //    return bookrepo.SearchBook(title,author);
        //}
        public async Task<IActionResult> AddNewBook(bool issuccess=false)
        {
            //ViewBag.list = new SelectList(new List<String>() { "Bangla","English","Urdu" });
            //ViewBag.list = new SelectList(GetLanguage(),"Id","Text");
            //ViewBag.list = GetLanguage().Select(x => new SelectListItem()
            //{
            //    Text = x.Text,
            //    Value = x.Id.ToString()
            //}).ToList();
            //var grp1 = new SelectListGroup() { Name = "Group1" };
            //var grp2 = new SelectListGroup() { Name = "Group2" };
            //ViewBag.list =new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text="Bangla",Value="1",Group=grp1},
            //    new SelectListItem(){Text="English",Value="2",Group=grp2},
            //    new SelectListItem(){Text="Tamil",Value="3",Group=grp1},
            //    new SelectListItem(){Text="Urdu",Value="4",Group=grp2}
            //};
            var languages = await langrepo.GetAll();
            ViewBag.list =new SelectList(languages,"Id","Name");
            ViewBag.success = issuccess;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(Book b)
        {
            if (ModelState.IsValid)
            {
                if (b.CoverPhoto != null)
                {
                    //UploadImage 
                    //string folder = "Book/Cover/";
                    //folder += Guid.NewGuid() + b.CoverPhoto.FileName.ToString();
                    //string serverFolder = Path.Combine(webenvironment.WebRootPath, folder);
                    //await b.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    //b.CoverUrl = folder;
                    string folder = "Book/Cover/";
                    b.CoverUrl=await UploadImage(folder,b.CoverPhoto);
                }
                if (b.GalaryFiles != null)
                { 
                    string folder = "Book/Galary/";
                    b.Galary = new List<GalaryModel>();
                    foreach(var file in b.GalaryFiles)
                    {
                        var galary = new GalaryModel()
                        {
                            Name = file.FileName,
                            Url = await UploadImage(folder, file)
                        };
                        b.Galary.Add(galary);
                    }
                }
                if (b.BookPdf != null)
                {
                    //UploadImage 
                    //string folder = "Book/Cover/";
                    //folder += Guid.NewGuid() + b.CoverPhoto.FileName.ToString();
                    //string serverFolder = Path.Combine(webenvironment.WebRootPath, folder);
                    //await b.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    //b.CoverUrl = folder;
                    string folder = "Book/pdf/";
                    b.BookPdfUrl = await UploadImage(folder, b.BookPdf);
                }
                var x = await bookrepo.Add(b);
                return RedirectToAction("AddNewBook", new { issuccess = true });
            }
            //ViewBag.list = new SelectList(new List<String>() { "Bangla", "English", "Urdu" });
            //ViewBag.list = new SelectList(GetLanguage(), "Id", "Text");
            //ViewBag.list = GetLanguage().Select(x => new SelectListItem() {
            //Text= x.Text,
            //Value=x.Id.ToString()
            //}).ToList();
            var languages = await langrepo.GetAll();
            ViewBag.list = new SelectList(languages, "Id", "Name");
            ViewBag.success = false;
            return View();

        }

        private async Task<string> UploadImage(string folderpath,IFormFile file)
        {

            folderpath += Guid.NewGuid() + file.FileName.ToString();
            string serverFolder = Path.Combine(webenvironment.WebRootPath, folderpath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderpath;
        }
        //private List<Language> GetLanguage()
        //{
        //    return new List<Language>()
        //    {
        //        new Language(){Id=1,Text="Bangla"},
        //        new Language(){Id=2,Text="English"},
        //        new Language(){Id=3,Text="Urdu"},
        //    };
        //}
    }
}
