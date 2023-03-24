
using System.ComponentModel.DataAnnotations;

namespace Chat.API.Models.Account
{
    public class RegisterModel: LoginPasswordBaseModel
    {
        [Required]
        public string AccountName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
