﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Dtos
{
    public class SignUpResponseDTO
    {
        public bool IsRegistrationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
