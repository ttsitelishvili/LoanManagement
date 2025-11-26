using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Accountant")]
    public class AccountantController : ControllerBase
    {
        private readonly IAccountantService _accountantService;

        public AccountantController(IAccountantService accountantService)
        {
            _accountantService = accountantService;
        }
        [HttpPost("block-user/{userId}")]
        public async Task<IActionResult> BlockUser(int userId, bool isBlocked)
        {
            try
            {
                await _accountantService.BlockUser(userId, isBlocked);
                return Ok($"User {userId} block status set to: {isBlocked}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-loan/{loanId}")]
        public async Task<IActionResult> UpdateLoan(int loanId, LoanStatus newStatus)
        {
            try
            {
                await _accountantService.UpdateLoanStatus(loanId, newStatus);
                return Ok($"Loan {loanId} status updated to: {newStatus}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-loan/{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            try
            {
                await _accountantService.DeleteLoan(id);
                return Ok($"Loan {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("loans")]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _accountantService.GetAllLoans();
            return Ok(loans);
        }
    }
}
