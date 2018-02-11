using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniCommerce.Domain.Security;

namespace MiniCommerce.Domain.Tests
{
    [TestClass]
    public class ProductManagementTests
    {
        [TestMethod]
        public void WhenProductIsDeletedPermissionShouldBeDelete()
        {
            var authorizer = new MockAuthorizer();
            var productManager = new ProductManagement(authorizer);
            var product = new Product();

            productManager.Delete(product);

            authorizer.PermissionRequiredInput.Should().Be(Permission.ProductDelete);
        }

        //try to figure out next step for delete
        
        [TestMethod]
        public void WhenProductIsUpdatedPermissionShouldBeWrite()
        {
            var authorizer = new MockAuthorizer();
            var productManager = new ProductManagement(authorizer);
            var product = new Product();

            productManager.Update(product);

            authorizer.PermissionRequiredInput.Should().Be(Permission.ProductWrite);
        }

        [TestMethod]
        public void WhenRetrievingProductPermissionShouldBeRead()
        {
            var authorizer = new MockAuthorizer();
            var productManager = new ProductManagement(authorizer);
            var product = new Product();

            authorizer.PermissionRequiredInput = Permission.ProductDelete;

            productManager.Read(product);
            authorizer.PermissionRequiredInput.Should().Be(Permission.ProductRead);
        }
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
