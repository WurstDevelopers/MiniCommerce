using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniCommerce.Infrastructure.Interfaces;

namespace MiniCommerce.Infrastructure.Tests
{
    [TestClass]
    public class UserManagementTests
    {
        [TestMethod]
        public void UserManagementTestsCreateUserShouldCallThePasswordHasherWithTheCorrectParameters()
        {
            var hasher = new MockHasher();
            var userManager = new UserManagement(hasher);

            var password = "BlueIsMyFavoriteCat(ThisIsTrue)";
            var userName = "Thomas";

            userManager.CreateUser(userName, password);

            hasher.CreatePasswordHashInput.Should().Be(password);
        }
    }

    public class MockHasher : IHasher
    {
        public string CreatePasswordHashInput { get; set; }

        public Hash CreatePasswordHash(string password)
        {
            CreatePasswordHashInput = password;
            return null;
        }
    }
}
