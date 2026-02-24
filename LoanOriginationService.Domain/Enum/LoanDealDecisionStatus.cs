using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Domain.Enum
{
    public enum LoanDealDecisionStatus
    {
        Review=1,
        Approved=2, 
        Rejected=3,
        Sanctioned=4
    }
}
