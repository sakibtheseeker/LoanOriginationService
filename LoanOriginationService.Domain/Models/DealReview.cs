using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Domain.Models
{
    public class DealReview
    {
        [Key]
        public int reviewId { get; set; }

        [ForeignKey(nameof(LoanDeals))]
        public int dealId { get; set; }

        public int officerId { get; set; }

        public string decision { get; set; }

        public string decisionReason { get; set; }

        public DateTime reviewDate { get; set; }

        public bool isActive { get; set; } = true;

        public DateTime createdAt { get; set; } = DateTime.Now;

        public int createdBy { get; set; }

        public DateTime? modifiedAt { get; set; }

        public int? deletedBy { get; set; }

        public LoanDeals LoanDeals { get; set; }

    }
}
