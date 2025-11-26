using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IAccountantService
    {
        Task BlockUser(int userId, bool isBlocked);
        Task UpdateLoanStatus(int loanId, LoanStatus newStatus);
        public Task DeleteLoan(int loanId);
        Task<List<Loan>> GetAllLoans();
    }
}
