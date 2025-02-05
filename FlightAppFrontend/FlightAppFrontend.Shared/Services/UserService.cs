
namespace FlightAppFrontend.Shared.Services
{
    public static class UserService
    {
        public static string UserRank(int karma)
        {
            if(karma > 10)
            {
                return "Cleared for Takeoff";
            }
            else if(karma > 20)
            {
                return "Frequent Flyer";
            }
            else if(karma > 30)
            {
                return "Aero Enthusiast";
            }
            else if(karma > 40)
            {
                return "Jet Setter";
            }
            else if(karma > 50)
            {
                return "Top Gun";
            }
            else if(karma > 60)
            {
                return "Cloud Rider";
            }
            return "Cleared for Takeoff";
        }
    }
}
