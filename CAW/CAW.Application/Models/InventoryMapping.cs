using AutoMapper;
using CAW.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAW.Application.Models
{
    public class InventoryMapping : Profile
    {
        public InventoryMapping()
        {
            CreateMap<InventoryModel, Inventory>()
                .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));


            CreateMap<Inventory, InventoryModel>();
        }
    }
}
