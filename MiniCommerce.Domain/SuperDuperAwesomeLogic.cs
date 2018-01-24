using MiniCommerce.Domain.Interfaces;
using MiniCommerce.Domain.Security;

namespace MiniCommerce.Domain
{
    public class SuperDuperAwesomeLogic : IDomainLayer
    {
        private readonly IDataContract dataLayer;
        private readonly IAuthorizer authorizer;

        public SuperDuperAwesomeLogic(IDataContract dataContract, IAuthorizer authorizer)
        {
            dataLayer = dataContract;
            this.authorizer = authorizer;
        }

        public string DoSomethingLogicy()
        {
            var domainString = "This is from the domain! ";
            var dataString = dataLayer.DoSomethingDatay();

            return domainString + " " + dataString;
        }

        //Demo on how to use authorizer
        public void CreateProduct(Product product)
        {
            if (authorizer.IsAuthorized(Permission.Write))
            {
                // save product
            }
        }
    }

    public class Product
    {
    }
}
