using LoanOriginationService.Application.DTO.LoanTypes;
using LoanOriginationService.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.Interfaces
{
    public interface ILoanRepo
    {
        void AddLoanType(LoanTypesResponseDto dto);

        public Task<LoanDto> GetLoanTypeById(int id);

        Task<ApiResponse<PagedResponse<LoanDto>>>GetAllLoanTypes(int pageNumber, int pageSize);

    }
}
