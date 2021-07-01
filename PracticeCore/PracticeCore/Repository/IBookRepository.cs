using PracticeCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticeCore.Repository
{
    public interface IBookRepository
    {
        Task<int> Add(Book b);
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<List<Book>> GetTopBookAsync();
    }
}