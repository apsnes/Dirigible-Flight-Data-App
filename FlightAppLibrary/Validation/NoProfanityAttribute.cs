using FlightAppLibrary.Models.Dtos;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppLibrary.Validation
{
    sealed public class NoProfanityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string)
            {
                string commentText = (string)value;
                commentText = commentText.ToLower();
                List<string> perscribed = File.ReadAllLines("../../FlightApp/Resources/enHash.txt").ToList();

                string commentHash = "";
                foreach (char c in commentText)
                {
                    char d = (char)(((int)c / 2 + 32));
                    commentHash += (char)((d + 7));
                }

                foreach (string phrase in perscribed)
                {
                    if (commentHash.Contains(phrase))
                    {
                        return new ValidationResult("Please refrain from using rude language");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
