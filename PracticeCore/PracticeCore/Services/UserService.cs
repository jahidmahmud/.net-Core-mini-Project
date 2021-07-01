using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticeCore.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpcontext;

        public UserService(IHttpContextAccessor httpcontext)
        {
            _httpcontext = httpcontext;
        }
        public string getUserId()
        {
            return _httpcontext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()
        {
            return _httpcontext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
