using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands
{
    public abstract class BaseCommand
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
