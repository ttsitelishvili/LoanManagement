using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Auth;

namespace Aplication.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Register(RegisterUserDto userDto);
        public Task<string> Login(LoginDto loginDto);
    }
}
