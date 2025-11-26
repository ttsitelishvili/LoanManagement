using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class RegisterUserDto
    {
        required public string FirstName { get; set; }
        required public string LastName { get; set; }
        required public string UserName { get; set; }
        required public string Password { get; set; }
        required public string Email { get; set; }
        required public int Age { get; set; }
        required public decimal Salary { get; set; }
    }
}
