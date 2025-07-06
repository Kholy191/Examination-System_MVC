using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Authentication;

namespace ServicesAbstraction.CoreServices
{
    public interface IAuthenticationService
    {
        public Task<string> SignUpAsync(RegisterDto _user);
        public Task<string> SignInAsync(LoginDto _user);
        public Task SignOutAsync();
        public Task<ApplicationUserDataDto> GetCurrentUserAsync(string userName);
    }
}
