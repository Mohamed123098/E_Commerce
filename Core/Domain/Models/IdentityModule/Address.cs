using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.IdentityModule
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}