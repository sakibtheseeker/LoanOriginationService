using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.Customer
{
    public class CustomerDto
    {
        public int customerId { get; set; }
        public string? AuthUserName { get; set; }

        public int ScorecardId { get; set; }

        public decimal eligibleLoanAmount { get; set; }

        public int cibilScore { get; set; }
    }
}
