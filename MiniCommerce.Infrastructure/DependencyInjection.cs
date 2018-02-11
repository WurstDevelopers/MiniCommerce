using SimpleInjector;

namespace MiniCommerce.Infrastructure
{
    public class DependencyInjection
    {
        public Container ApplicationContainer;

        public DependencyInjection()
        {
            ApplicationContainer = new Container();


            ApplicationContainer.Verify();
        }
    }
}
