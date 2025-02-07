using FlightAppLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlightAppLibrary.Models.Dtos
{
    public class CommentTextDto
    {
        [NoProfanity(ErrorMessage = "Please refrain from using rude language")]
        public string Text { get; set; }
    }
}
