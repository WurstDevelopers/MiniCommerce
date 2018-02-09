using MiniCommerce.Infrastructure.Interfaces;

namespace MiniCommerce.Infrastructure
{
    public class UserManagement
    {
        private readonly IHasher hasher;
        private readonly IUserRepsository userRepository;

        public UserManagement(IHasher hasher, IUserRepsository userRepository)
        {
            this.hasher = hasher;
            this.userRepository = userRepository;
        }

        public void CreateUser(string userName, string password)
        {
            //Arggg. We need to make sure that the username is unique to the system. That will be my next round of tests.
            // if one user default to admin permissioins

            var passwordHash = hasher.CreatePasswordHash(password);

            var user = new User(userName, passwordHash);

            userRepository.AddUser(user);

            userRepository.AccountExists(userName);
        }
    }
}
