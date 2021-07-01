using PracticeCore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticeCore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<Language>> GetAll();
    }
}