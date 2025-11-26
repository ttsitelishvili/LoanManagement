using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class AccountantService : IAccountantService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Loan> _loanRepository;

        public AccountantService(IGenericRepository<User> userRepository, IGenericRepository<Loan> loanRepository)
        {
            _userRepository = userRepository;
            _loanRepository = loanRepository;
        }

        public async Task BlockUser(int userId, bool isBlocked)
        {
            var user = _userRepository.GetById(userId);
            if (user == null) throw new Exception("User not found");

            user.IsBlocked = isBlocked;
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateLoanStatus(int loanId, LoanStatus newStatus)
        {
            var loan = _loanRepository.GetById(loanId);
            if (loan == null) throw new Exception("Loan not found");

            loan.Status = newStatus;
            await _loanRepository.SaveChangesAsync();
        }
        public async Task DeleteLoan(int loanId)
        {
            var loan = _loanRepository.GetById(loanId);
            if (loan == null)
            {
                throw new Exception("Loan not found");
            }
            _loanRepository.Delete(loan);
            await _loanRepository.SaveChangesAsync();
        }
        public async Task<List<Loan>> GetAllLoans()
        {
            return await Task.FromResult(_loanRepository.GetAll());
        }
    }
}
