using System.ComponentModel.DataAnnotations;

namespace SportClubWMS.Shared
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First Name is too long.")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50, ErrorMessage = "Second Name is too long.")]
        public string SecondName { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public ICollection<CustomerSportGood> CustomerSportGoods { get; set; }
            = new List<CustomerSportGood>();
        public ICollection<SportGood> SportGoods { get; set; }
            = new List<SportGood>();
        [MaxLength(400, ErrorMessage = "The address is too long.")]
        public string Address { get; set; } = string.Empty;
        public SubscribeStatus SubscribeStatus { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }


    }
}
