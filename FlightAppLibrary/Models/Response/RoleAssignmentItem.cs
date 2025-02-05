using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppLibrary.Models.Response
{
    public class RoleAssignmentItem
    {
        public string Email { get; set; }
        public string Role { get; set; }//1=admin, 2=customer
    }
}
