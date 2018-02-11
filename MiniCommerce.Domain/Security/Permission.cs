namespace MiniCommerce.Domain.Security
{
    public enum Permission  
    {
        ProductNone,
        ProductRead,
        ProductWrite, 
        ProductDelete,
        UserAdminRead,
        UserAdminWrite,
        UserAdminDelete
    }
}
