namespace MiniCommerce.Domain.Security
{
    public interface IAuthorizer
    {
        bool HasAuthorization(Permission permissionRequired);
    }
}
