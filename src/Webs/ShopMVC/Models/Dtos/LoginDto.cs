﻿using System.ComponentModel.DataAnnotations;

namespace ShopMVC.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string SessionId { get; set; }
    }
}
