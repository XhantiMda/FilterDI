using System;
using System.Threading.Tasks;

namespace FilterDI.Services
{
    public interface IAuthenticationService
    {
        bool Autheticate(string token);
    }
}