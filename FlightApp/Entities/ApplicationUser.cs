using Microsoft.AspNetCore.Identity;

namespace FlightApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Karma {  get; set; }
        public string DisplayName { get; set; }
        
    }
}
