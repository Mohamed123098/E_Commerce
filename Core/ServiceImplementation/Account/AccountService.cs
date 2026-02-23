using AutoMapper;
using Domain.Exceptions.AccountException;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTOs.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.Account
{
    public class AccountService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAccountService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<AddressDTO> GetCurrentUserAddressAsync(string email)
        {
            ApplicationUser? user = await GetUserByEmailAsync(email);
                if(user.Address is not null)
                {
                    return new() { FirstName = user.Address.FirstName, LastName = user.Address.LastName, City = user.Address.City,Street=user.Address.Street };
                }   
                throw new AddressNotFoundException(email);
        }

        public async Task<UserDTO> GetCurrentUserAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);
            return new()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = user.Token
            };
           //return _mapper.Map<ApplicationUser,UserDTO>();

        }

        public async Task<AddressDTO> UpdateAddressAsync(string email, AddressDTO addressDTO)
        {
           var user= await GetUserByEmailAsync(email);
            if (user.Address is not null)
            {
                user.Address.FirstName = addressDTO.FirstName;
                user.Address.LastName = addressDTO.LastName;
                user.Address.Street = addressDTO.Street;
                user.Address.City = addressDTO.City;
            }
            else
            { 
                user.Address = _mapper.Map<AddressDTO, Address>(addressDTO);  
            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDTO>(user.Address);
        }

        private async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            ApplicationUser? user = await _userManager.Users.
               Include(u => u.Address).
               FirstOrDefaultAsync(u => u.Email == email)
               ?? throw new AccountNotFoundException(email);
            return user;
        }

        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user =await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user is not null)
            {
                var IsVaild =await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (IsVaild)
                {
                    user.Token = await CreateToken(user);
                    return new() { DisplayName = user.DisplayName, Email = user.Email, Token = await CreateToken(user) };
                }
            }
            throw new AccountNotFoundException(loginDTO.Email);
           
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            ApplicationUser appUser = new()
            {
                DisplayName = registerDTO.DisplayName,
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                

            };
            appUser.Token = await CreateToken(appUser);
            var user = await _userManager.CreateAsync(appUser,registerDTO.Password);
            if (user.Succeeded)
            {
                appUser.Token =await CreateToken(appUser);
                return new() { DisplayName = appUser.DisplayName, Email = appUser.Email, Token = await CreateToken(appUser) };
            }
            else
                throw new BadRequestException(registerDTO.Email);
        }


        private async Task<string> CreateToken(ApplicationUser appUser)
        {

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,appUser.Email),
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier,appUser.Id)
            };
            var Roles = await _userManager.GetRolesAsync(appUser);
            foreach (var role in Roles)
            {
                Claims.Add(new(ClaimTypes.Role, role));
            }
            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecreteKey"]!));
            var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audiance"],
                signingCredentials:Credentials,
                claims:Claims,
                expires:DateTime.Now.AddHours(1)
                
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        
    }
}
