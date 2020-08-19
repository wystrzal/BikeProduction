using System.ComponentModel.DataAnnotations;

namespace ShopMVC.Models.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must have minimum 6 signs.")]
        public string Password { get; set; }
    }
}
