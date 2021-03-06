﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        [Required]
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
