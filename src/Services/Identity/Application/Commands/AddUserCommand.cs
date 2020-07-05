using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class AddUserCommand : IRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
