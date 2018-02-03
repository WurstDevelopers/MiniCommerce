using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniCommerce.Domain.Security;

namespace MiniCommerce.Domain.Tests
{

    [TestClass]
    public class ProductManagementTests
    {
        [TestMethod]
        public void BadTestName1()
        {
            var authorizer = new MockAuthorizer();
            var productManager = new ProductManagement(authorizer);
            var product = new Product();

            productManager.Delete(product);

            authorizer.PermissionRequiredInput.Should().Be(Permission.ProductDelete);
        }

        //read
        
        //upsert maybe several

        //try to figure out next step for delete
    }


    public class MockAuthorizer : IAuthorizer
    {
        public Permission PermissionRequiredInput { get; set; }

        public bool HasAuthorization(Permission permissionRequired)
        {
            PermissionRequiredInput = permissionRequired;
            return false;
        }
    }
}
