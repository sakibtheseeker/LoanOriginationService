using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.LoanDeals
{
    public class LoanDealsResponseDto
    {
        public int custId { get; set; }

        //public int scorecardId { get; set; }

        public int loanTypeId { get; set; }

        //public decimal eligibleAmount { get; set; }

        //public decimal approvedAmount { get; set; }

        public int riskRating { get; set; }

        //public int cibilScore { get; set; }
    }
}
