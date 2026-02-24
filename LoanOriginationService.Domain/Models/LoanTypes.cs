using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Domain.Models
{
    public class LoanTypes
    {
        [Key]
        public int loanTypeId { get; set; }

        public string loanTypeName { get; set; }

        public bool isActive { get; set; } = true;

        public DateTime createdAt { get; set; } = DateTime.Now;

        public int createdBy { get; set; }

        public DateTime? modifiedAt { get; set; }

        public int? deletedBy { get; set; }

    }
}
