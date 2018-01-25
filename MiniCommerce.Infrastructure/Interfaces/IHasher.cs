namespace MiniCommerce.Infrastructure.Interfaces
{
    public interface IHasher
    {
        Hash CreatePasswordHash(string password);
    }

    public class Hash
    {
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public uint Iterations { get; set; }
    }
}
