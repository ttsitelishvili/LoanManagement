using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Loans;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IGenericRepository<Loan> _loanRepository;
        private readonly IGenericRepository<User> _userRepository;

        public LoanService(IGenericRepository<Loan> loanRepository, IGenericRepository<User> userRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
        }
        public async Task AddLoan(LoanCreateDto model, int userId)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (user.IsBlocked)
            {
                throw new Exception("You are blocked and cannot apply for a loan.");
            }
            var newLoan = new Loan
            {
                LoanType = model.LoanType,
                Amount = model.Amount,
                Currency = model.Currency,
                LoanPeriod = model.LoanPeriod,
                UserId = userId,
                Status = LoanStatus.Processing
            };
            _loanRepository.Add(newLoan);
            await _loanRepository.SaveChangesAsync();
        }
        public async Task UpdateLoan(int loanId, LoanUpdateDto model, int userId)
        {
            var loan = _loanRepository.GetById(loanId);
            if(loan == null)
            {
                throw new Exception("Loan not found");
            }
            if (loan.UserId != userId)
            {
                throw new Exception("Doesn't have access.");
            }
            if (loan.Status != LoanStatus.Processing)
            {
                throw new Exception("You can't update Loan after it's Processed.");
            }
            loan.LoanType = model.LoanType;
            loan.Amount = model.Amount;
            loan.Currency = model.Currency;
            loan.LoanPeriod = model.LoanPeriod;
            await _loanRepository.SaveChangesAsync();
        }
        public async Task DeleteLoan(int loanId, int userId)
        {
            var loan = _loanRepository.GetById(loanId);
            if (loan == null)
            {
                throw new Exception("Loan not found");
            }
            if (loan.UserId != userId)
            {
                throw new Exception("Doesn't have access.");
            }
            if (loan.Status != LoanStatus.Processing)
            {
                throw new Exception("You can't Delete Loan after it's Processed.");
            }
            _loanRepository.Delete(loan);
            await _loanRepository.SaveChangesAsync();
        }
        public async Task<List<Loan>> GetUserLoans(int userId)
        {
            var allLoans = _loanRepository.GetAll();

            var myLoans = allLoans.Where(x => x.UserId == userId).ToList();
            return await Task.FromResult(myLoans);
        }
    }
}
