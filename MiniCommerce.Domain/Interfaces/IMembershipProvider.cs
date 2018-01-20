namespace MiniCommerce.Domain.Interfaces
{
    public interface IMembershipProvider
    {
        void AddUser(User user);
        bool Login(string username, string password);
        bool IsSignedInUserAdmin();
        User GetSignedInUser();
    }
}
