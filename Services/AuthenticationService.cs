using System;
using System.Threading.Tasks;

namespace FilterDI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool Autheticate(string token)
        {
            return true;
        }
    }
}