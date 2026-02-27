using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
   
    public class AccountController(IServiceManager _serviceManager):BaseApiController
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
          var user = await _serviceManager.AccountService.RegisterAsync(registerDTO);
            return Ok(user);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user =await _serviceManager.AccountService.LoginAsync(loginDTO);
            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<bool>> CheckEmailAsync(string email)
        => Ok(await _serviceManager.AccountService.CheckEmailAsync(email));
        [HttpGet("User")]
        public async Task<ActionResult<UserDTO>> GetCurrentUserAsync(string email)
            => Ok(await _serviceManager.AccountService.GetCurrentUserAsync(email));
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddressAsync(string email)
            => Ok(await _serviceManager.AccountService.GetCurrentUserAddressAsync(email));
        [HttpPut]
        public async Task<ActionResult<AddressDTO>> UpdateAddressAsync(string email, AddressDTO addressDTO)
            => Ok(await _serviceManager.AccountService.UpdateAddressAsync(email, addressDTO));
    }
}
