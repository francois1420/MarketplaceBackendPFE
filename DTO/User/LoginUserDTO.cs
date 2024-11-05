﻿using System.ComponentModel.DataAnnotations;

namespace marketplace_backend.DTO.User
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
