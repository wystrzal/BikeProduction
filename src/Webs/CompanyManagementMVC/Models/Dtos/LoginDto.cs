﻿using System.ComponentModel.DataAnnotations;

namespace CompanyManagementMVC.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
