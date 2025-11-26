using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        required public string FirstName { get; set; }
        required public string LastName { get; set; }
        required public string UserName { get; set; }
        required public string Password { get; set; }
        required public string Email { get; set; }
        required public int Age { get; set; }
        required public decimal Salary { get; set; }
        public bool IsBlocked { get; set; } = false;
        public UserRole Role { get; set; } = UserRole.User;
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
