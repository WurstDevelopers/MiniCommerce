using System;
using MiniCommerce.Domain.Interfaces;
using MiniCommerce.Infrastructure;

namespace MiniCommerce
{
    public class Program
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
