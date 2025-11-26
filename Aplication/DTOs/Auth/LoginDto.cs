using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class LoginDto
    {
        required public string UserName { get; set; }
        required public string Password { get; set; }
    }
}
