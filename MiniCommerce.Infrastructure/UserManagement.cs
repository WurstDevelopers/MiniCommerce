using MiniCommerce.Infrastructure.Interfaces;

namespace MiniCommerce.Infrastructure
{
    public class UserManagement
    {
        private readonly IHasher hasher;

        public UserManagement(IHasher hasher)
        {
            this.hasher = hasher;
        }

        public void CreateUser(string userName, string password)
        {
            hasher.CreatePasswordHash(password);
        }
    }
}
