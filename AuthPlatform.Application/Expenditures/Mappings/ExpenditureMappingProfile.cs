using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Expenditures.DTOs;
using AuthPlatform.Domain.Expenditures.Entities;
using AutoMapper;

namespace AuthPlatform.Application.Expenditures.Mappings
{
    public class ExpenditureMappingProfile : Profile
    {
        public ExpenditureMappingProfile()
        {
            CreateMap<ExpenditureHead, ExpenditureHeadDto>().ReverseMap();

            CreateMap<ExpenditureInvoice, ExpenditureInvoiceDto>()
                .ForMember(dest => dest.ExpenditureHeadName,
                    opt => opt.MapFrom(src => src.ExpenditureHead.ExpenditureHeadName))
                .ReverseMap();

            CreateMap<ExpenditureInvoiceDetail, ExpenditureInvoiceDetailDto>()
                .ForMember(dest => dest.LineTotal,
                    opt => opt.MapFrom(src => src.Quantity * src.UnitPrice))
                .ReverseMap();
        }
    }
}
