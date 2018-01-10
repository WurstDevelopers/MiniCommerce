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
            //ToDo: Create Registry for between IDataContract and your new implementation of IDataContract that is in the data project. 

            ApplicationContainer.Verify();
        }
    }
}
