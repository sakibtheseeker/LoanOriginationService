using LoanOriginationService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Domain.Models
{
    public class LoanDeals
    {
        [Key]
        public int dealId { get; set; }

        public int custId { get; set; }

        public int scorecardId { get; set; }

        public int loanTypeId { get; set; }

        public decimal eligibleAmount { get; set; }

        public decimal approvedAmount { get; set; }

        public int riskRating { get; set; }

        public int cibilScore { get; set; }

        public LoanDealDecisionStatus currentStatus { get; set; } = LoanDealDecisionStatus.Review;

        public bool isActive { get; set; } = true;

        public DateTime createdAt { get; set; } = DateTime.Now;

        public int createdBy { get; set; }
        public DateTime? modifiedAt { get; set; }

        public int? deletedBy { get; set; }

        public LoanTypes LoanType { get; set; }

    }
}
