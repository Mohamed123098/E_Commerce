using Shared.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAccountService
    {
        Task<UserDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<bool> CheckEmailAsync(string email);
        Task<AddressDTO> GetCurrentUserAddressAsync(string email);
        
        Task<AddressDTO> UpdateAddressAsync(string email,AddressDTO addressDTO);
        Task<UserDTO> GetCurrentUserAsync(string email);

    }
}
