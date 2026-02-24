using LoanOriginationService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.LoanDeals
{
    public class LoanDealsDto
    {
        public int dealId { get; set; }

        public int custId { get; set; }

        public int scorecardId { get; set; }

        public string loanTypeName { get; set; }
        public double eligibleAmount { get; set; }

        public double approvedAmount { get; set; }

        public int riskRating { get; set; }

        public int cibilScore { get; set; }

        public LoanDealDecisionStatus currentStatus { get; set; }

    }
}
