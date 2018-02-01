namespace MiniCommerce.Infrastructure.Interfaces
{
    public interface IHasher
    {
        PasswordHash CreatePasswordHash(string password);
    }

    public class PasswordHash
    {
        public string Salt { get; set; }
        public string Hash { get; set; }
        public uint Iterations { get; set; }
    }
}
