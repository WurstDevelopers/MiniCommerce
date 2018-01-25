using MiniCommerce.Domain.Interfaces;
using MiniCommerce.Domain.Security;

namespace MiniCommerce.Domain
{
    public class SuperDuperAwesomeLogic : IDomainLayer
    {
        private readonly IDataContract dataLayer;

        public SuperDuperAwesomeLogic(IDataContract dataContract)
        {
            dataLayer = dataContract;
        }

        public string DoSomethingLogicy()
        {
            var domainString = "This is from the domain! ";
            var dataString = dataLayer.DoSomethingDatay();

            return domainString + " " + dataString;
        }

    }
}
