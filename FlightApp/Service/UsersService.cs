using FlightApp.Entities;
using FlightApp.Repository;

namespace FlightApp.Service
{
    public interface IUsersService
    {
        List<User> GetUsers();
        User AddUser(User user);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // ------GET REQUESTS------
        public List<User> GetUsers()
        {
            return _usersRepository.GetUsers();
        }

        // -----POST REQUESTS-----
        public User AddUser(User user)
        {
            return _usersRepository.AddUser(user);
        }
    }
}
