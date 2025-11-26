using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public int LoanPeriod { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Processing;
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
