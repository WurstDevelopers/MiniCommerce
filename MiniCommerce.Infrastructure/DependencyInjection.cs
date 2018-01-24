using MiniCommerce.Data;
using MiniCommerce.Domain;
using MiniCommerce.Domain.Interfaces;
using SimpleInjector;

namespace MiniCommerce.Infrastructure
{
    public class DependencyInjection
    {
        public Container ApplicationContainer;

        public DependencyInjection()
        {
            ApplicationContainer = new Container();

            ApplicationContainer.Register<IDomainLayer, SuperDuperAwesomeLogic>(Lifestyle.Singleton);
            ApplicationContainer.Register<IDataContract, DataContract>(Lifestyle.Singleton);

            ApplicationContainer.Verify();
        }
    }
}
