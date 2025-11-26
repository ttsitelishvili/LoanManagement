using Application.DTOs.Loans;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Application.Interfaces;
using Moq;
using Xunit;
namespace Tests
{
    public class LoanServiceTests
    {
        private readonly Mock<IGenericRepository<Loan>> _mockLoanRepo;
        private readonly Mock<IGenericRepository<User>> _mockUserRepo;
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _mockLoanRepo = new Mock<IGenericRepository<Loan>>();
            _mockUserRepo = new Mock<IGenericRepository<User>>();
            _loanService = new LoanService(_mockLoanRepo.Object, _mockUserRepo.Object);
        }

        [Fact]
        public async Task AddLoan_Should_Succeed_When_User_Is_Not_Blocked()
        {
            // Arrange
            int userId = 1;
            var user = new User { Id = userId, IsBlocked = false, FirstName = "Test", LastName = "User", UserName = "test", Password = "123", Email = "t@t.com", Salary = 100, Age = 42 };

            _mockUserRepo.Setup(r => r.GetById(userId)).Returns(user);

            var loanDto = new LoanCreateDto
            {
                Amount = 1000,
                Currency = Currency.USD,
                LoanPeriod = 12,
                LoanType = LoanType.QuickLoan
            };

            // Act
            await _loanService.AddLoan(loanDto, userId);

            // Assert
            _mockLoanRepo.Verify(r => r.Add(It.IsAny<Loan>()), Times.Once);
            _mockLoanRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddLoan_Should_Fail_When_User_Is_Blocked()
        {
            // Arrange
            int userId = 99;
            var blockedUser = new User { Id = userId, IsBlocked = true, FirstName = "Bad", LastName = "Guy", UserName = "bg", Password = "123", Email = "b@b.com", Salary = 100, Age = 21 };

            _mockUserRepo.Setup(r => r.GetById(userId)).Returns(blockedUser);

            var loanDto = new LoanCreateDto { Amount = 1000 };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _loanService.AddLoan(loanDto, userId));

            // Note: This message must match your LoanService.cs EXACTLY
            Assert.Equal("You are blocked and cannot apply for a loan.", ex.Message);
        }

        [Fact]
        public async Task UpdateLoan_Should_Fail_If_Status_Not_Processing()
        {
            // Arrange
            int loanId = 5;
            int userId = 1;

            var approvedLoan = new Loan
            {
                Id = loanId,
                UserId = userId,
                Status = LoanStatus.Approved, // It is NOT Processing
                Amount = 500
            };

            _mockLoanRepo.Setup(r => r.GetById(loanId)).Returns(approvedLoan);

            var updateDto = new LoanUpdateDto { Amount = 9000 };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _loanService.UpdateLoan(loanId, updateDto, userId));

            // Note: This message must match your LoanService.cs EXACTLY
            Assert.Equal("You can't update Loan after it's Processed.", ex.Message);
        }
    }
}