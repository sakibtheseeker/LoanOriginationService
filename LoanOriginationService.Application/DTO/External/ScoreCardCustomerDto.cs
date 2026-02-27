using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.DTO.External
{
    public class ScorecardCustomerDto
    {
        public int customerId { get; set; }
        public string? AuthUserName { get; set; }
    }
}
