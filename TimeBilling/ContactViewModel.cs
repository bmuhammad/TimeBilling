﻿using System.ComponentModel.DataAnnotations;

namespace TimeBilling
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Subject { get; set; } = "";

        [Required]
        [MinLength(25)]
        public string Body { get; set; } = ""; 
    }
}
