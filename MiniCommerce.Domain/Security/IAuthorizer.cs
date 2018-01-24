namespace MiniCommerce.Domain.Security
{
    public interface IAuthorizer
    {
        bool IsAuthorized(Permission permissionRequired);
    }
}
