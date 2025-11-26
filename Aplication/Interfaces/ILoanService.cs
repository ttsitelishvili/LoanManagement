using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Loans;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface ILoanService
    {
        public Task AddLoan(LoanCreateDto loanCreateDto, int userId);
        public Task UpdateLoan(int loanId, LoanUpdateDto model, int userId);
        public Task DeleteLoan(int loanId, int userId);
        public Task<List<Loan>> GetUserLoans(int userId);
    }
}
