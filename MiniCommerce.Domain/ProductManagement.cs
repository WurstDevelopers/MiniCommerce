
using MiniCommerce.Domain.Security;

namespace MiniCommerce.Domain
{
    public class ProductManagement
    {
        private readonly IAuthorizer authorizer;

        public ProductManagement(IAuthorizer authorizer)
        {
            this.authorizer = authorizer;
        }

        public void Delete(Product product)
        {
            authorizer.HasAuthorization(Permission.Delete);
        }
    }
}
