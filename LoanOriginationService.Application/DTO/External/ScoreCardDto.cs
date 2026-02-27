using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.External
{
    public class ScoreCardDto
    {
        public int customerId { get; set; }

        public int ScorecardId { get; set; }

        public decimal eligibleLoanAmount { get; set; }

        public int cibilScore { get; set; }
    }
}
