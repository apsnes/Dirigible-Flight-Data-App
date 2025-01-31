using FlightApp.Database;
using FlightApp.Entities;

namespace FlightApp.Repository
{
    public interface IUsersRepository
    {
        List<User> GetUsers();
        User AddUser(User user);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly FlightAppDbContext db;
        public UsersRepository(FlightAppDbContext _db)
        {
            db = _db;
        }

        // ------GET REQUESTS------
        public List<User> GetUsers()
        {
            try
            {
                return db.Users.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        // -----POST REQUESTS-----
        public User AddUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
