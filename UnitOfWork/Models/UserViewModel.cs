﻿using System.ComponentModel.DataAnnotations;

namespace UnitOfWork.Models
{
    public class UserViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}
