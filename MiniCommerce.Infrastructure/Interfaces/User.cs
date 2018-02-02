namespace MiniCommerce.Infrastructure.Interfaces
{
    public class User
    {
        public User(string userName, PasswordHash passwordHash)
        {
            UserName = userName;
            PasswordHash = passwordHash;
        }

        public string UserName { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
    }
}