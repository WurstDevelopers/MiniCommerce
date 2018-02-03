namespace MiniCommerce.Infrastructure.Interfaces
{
    public interface IUserRepsository
    {
        void AddUser(User userToAdd);
        bool AccountExists(string userName);
    }
}
