

using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Account
{
    public class RegisterDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? DisplayName { get; set; }
        public string UserName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
