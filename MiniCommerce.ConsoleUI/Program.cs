using System;

namespace MiniCommerce.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var dependencyInjector = new DependencyInjection().ApplicationContainer;

            var domain = dependencyInjector.GetInstance<IDomainLayer>();

            var output = "This is from the UI! " + domain.DoSomethingLogicy();

            Console.WriteLine("Hello World!");
            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}
