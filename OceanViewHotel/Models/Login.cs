﻿using System.ComponentModel.DataAnnotations;

namespace OceanViewHotel.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
