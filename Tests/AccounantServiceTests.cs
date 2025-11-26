using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Loans;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace Tests
{
    public class AccountantServiceTests
    {
        private readonly Mock<IGenericRepository<Loan>> _mockLoanRepo;
        private readonly Mock<IGenericRepository<User>> _mockUserRepo;
        private readonly AccountantService _accountantService;

        public AccountantServiceTests()
        {
            _mockLoanRepo = new Mock<IGenericRepository<Loan>>();
            _mockUserRepo = new Mock<IGenericRepository<User>>();
            _accountantService = new AccountantService(_mockUserRepo.Object, _mockLoanRepo.Object);
        }

        [Fact]
        public async Task BlockUser_Should_Change_IsBlocked_To_True()
        {
            // Arrange
            int userId = 5;
            var user = new User { Id = userId, IsBlocked = false, FirstName = "Test", LastName = "User", UserName = "test", Password = "123", Email = "t@t.com", Salary = 100, Age = 30 };

            _mockUserRepo.Setup(r => r.GetById(userId)).Returns(user);

            // Act
            await _accountantService.BlockUser(userId, true);

            // Assert
            Assert.True(user.IsBlocked); // Did the property change?
            _mockUserRepo.Verify(r => r.SaveChangesAsync(), Times.Once); // Did we save it?
        }

        [Fact]
        public async Task UpdateLoanStatus_Should_Change_Status_To_Approved()
        {
            // Arrange
            int loanId = 10;
            var loan = new Loan { Id = loanId, Status = LoanStatus.Processing, Amount = 500 };

            _mockLoanRepo.Setup(r => r.GetById(loanId)).Returns(loan);

            // Act
            await _accountantService.UpdateLoanStatus(loanId, LoanStatus.Approved);

            // Assert
            Assert.Equal(LoanStatus.Approved, loan.Status); // Did it change?
            _mockLoanRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteLoan_Should_Delete_Without_Checking_Status()
        {
            // Arrange
            int loanId = 20;
            // Create a loan that is already APPROVED (Normally a user can't delete this, but Accountant can)
            var loan = new Loan { Id = loanId, Status = LoanStatus.Approved, Amount = 500 };

            _mockLoanRepo.Setup(r => r.GetById(loanId)).Returns(loan);

            // Act
            await _accountantService.DeleteLoan(loanId);

            // Assert
            _mockLoanRepo.Verify(r => r.Delete(loan), Times.Once);
            _mockLoanRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}