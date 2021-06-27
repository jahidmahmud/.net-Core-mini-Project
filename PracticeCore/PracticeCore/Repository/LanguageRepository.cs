using Microsoft.EntityFrameworkCore;
using PracticeCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Repository
{
    public class LanguageRepository
    {
        BookStoreContext context;
        public LanguageRepository(BookStoreContext context1)
        {
            context = context1;
        }

        public async Task<List<Language>> GetAll()
        {
            return await context.Language.Select(x => new Language()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
        }
    }
}
