﻿using FlightApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace FlightApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Note> Notes { get; set; }
    }
}
