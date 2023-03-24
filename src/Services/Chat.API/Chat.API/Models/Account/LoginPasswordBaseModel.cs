using System.ComponentModel.DataAnnotations;

namespace Chat.API.Models.Account
{
    public class LoginPasswordBaseModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
