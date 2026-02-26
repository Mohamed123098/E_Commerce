using AutoMapper;
using Domain.Models.OrderModule;
using Microsoft.Extensions.Options;
using Shared.DTOs.Account;
using Shared.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Mapping
{
    public class AddressOrderProfile:Profile
    {
        public AddressOrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDTO>().ForMember(o => o.DeliveryMethod, o => o.MapFrom(o => o.DeliveryMethod.ShortName));
            CreateMap<OrderItems, ItemDTO>().ForMember(o => o.ProductName, o => o.MapFrom(o => o.OrderedItem.Name))
                .ForMember(o=>o.PictureURL,o=>o.MapFrom(o=>o.OrderedItem.PictureURL));
            
        }
    }
}
