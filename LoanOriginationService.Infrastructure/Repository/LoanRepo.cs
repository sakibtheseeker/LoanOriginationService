using AutoMapper;
using LoanOriginationService.Application.DTO.LoanTypes;
using LoanOriginationService.Application.Helper;
using LoanOriginationService.Application.Interfaces;
using LoanOriginationService.Domain.Models;
using LoanOriginationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Infrastructure.Repository
{
    public class LoanRepo : ILoanRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public LoanRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public void AddLoanType(LoanTypesResponseDto dto)
        {
            var d = mapper.Map<LoanTypes>(dto);
            db.LoanTypes.Add(d);
            db.SaveChanges();
        }

        public async Task<ApiResponse<PagedResponse<LoanDto>>>GetAllLoanTypes(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalRecords = await db.LoanTypes.CountAsync();

            var loanTypes = await db.LoanTypes
                .OrderBy(x => x.loanTypeId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!loanTypes.Any())
            {
                return ApiResponse<PagedResponse<LoanDto>>.FailureResponse(
                    "Data Unavailable",
                    "No records found.",
                    "404");
            }

            var mappedData = mapper.Map<List<LoanDto>>(loanTypes);

            var pagedResponse = new PagedResponse<LoanDto>(
                mappedData,
                pageNumber,
                pageSize,
                (int)Math.Ceiling(totalRecords / (double)pageSize),
                totalRecords
            );

            return ApiResponse<PagedResponse<LoanDto>>.SuccessResponse(
                pagedResponse,
                "Loan Type Fetched Successfully");
        }




        public async Task<LoanDto> GetLoanTypeById(int id)
        {
            var d = await db.LoanTypes.FindAsync(id);
            var res = mapper.Map<LoanDto>(d);
            return res;

        }

    }
}
