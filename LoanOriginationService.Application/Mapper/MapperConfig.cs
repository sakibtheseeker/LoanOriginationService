using AutoMapper;
using LoanOriginationService.Application.DTO.LoanDeals;
using LoanOriginationService.Application.DTO.LoanTypes;
using LoanOriginationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Application.Mapper
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<LoanDto, LoanTypes>().ReverseMap();
            CreateMap<LoanTypesResponseDto, LoanTypes>().ReverseMap();
            CreateMap<LoanDealsDto, LoanDeals>().ReverseMap();
            CreateMap<LoanDealsResponseDto, LoanDeals>().ReverseMap();
            CreateMap<LoanDeals, LoanDealsDto>()
            .ForMember(dest => dest.loanTypeName,
                opt => opt.MapFrom(src => src.LoanType.loanTypeName));


        }


    }
}
