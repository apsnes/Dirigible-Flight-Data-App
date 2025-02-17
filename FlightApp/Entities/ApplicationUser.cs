﻿using FlightApp.Entities;
using FlightAppLibrary.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace FlightApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Pronouns { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Karma {  get; set; }
        public string? DisplayName { get; set; }
        public List<Note> Notes { get; set; }
        public string Avatar { get; set; } = "_content/FlightAppFrontend.Shared/Images/Avatars/3.png";
    }
}