using MiniCommerce.Infrastructure.Interfaces;

namespace MiniCommerce.Infrastructure
{
    public class UserManagement
    {
        private readonly IHasher hasher;
        private readonly IUserRepository userRepository;

        public UserManagement(IHasher hasher, IUserRepository userRepository)
        {
            this.hasher = hasher;
            this.userRepository = userRepository;
        }

        public void CreateUser(string userName, string password)
        {
            //Arggg. We need to make sure that the username is unique to the system. That will be my next round of tests.

            hasher.CreatePasswordHash(password);

            var user = new User(userName);

            userRepository.AddUser(user);
        }
    }
}
