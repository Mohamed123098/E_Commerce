using AutoMapper;
using Domain.Models.IdentityModule;
using Shared.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Mapping
{
    public class AccountProfile:Profile
    {
        public AccountProfile()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
