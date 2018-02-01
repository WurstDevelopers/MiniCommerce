namespace MiniCommerce.Infrastructure.Interfaces
{
    public class User
    {
        public User(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; private set; }
    }
}