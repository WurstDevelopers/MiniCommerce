using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniCommerce.Infrastructure.Interfaces;
using RandomTestValues;

namespace MiniCommerce.Infrastructure.Tests
{
    [TestClass]
    public class UserManagementTests
    {
        private MockHasher hasher;
        private MockUserRepository userRepository;
        private UserManagement userManager;

        [TestInitialize]
        public void RunBeforeBeforeTestMethods()
        {
            hasher = new MockHasher();
            userRepository = new MockUserRepository();
            userManager = new UserManagement(hasher, userRepository);
        }

        [TestMethod]
        public void UserManagementTestsCreateUserShouldCallThePasswordHasherWithTheCorrectParameters()
        {
            var password = "BlueIsMyFavoriteCat(ThisIsTrue)";
            var userName = "Thomas";

            userManager.CreateUser(userName, password);

            hasher.CreatePasswordHashInput.Should().Be(password);
        }

        [TestMethod]
        public void UserManagementTestsCreateUserShouldCallAddUserWithTheCorrectUserName()
        {
            var username = RandomValue.String();
            var password = RandomValue.String();

            userManager.CreateUser(username, password);

            userRepository.UserToAddInput.UserName.Should().Be(username);
        }
    }

    public class MockUserRepository : IUserRepository
    {
        public User UserToAddInput { get; private set; }

        public void AddUser(User userToAdd)
        {
            UserToAddInput = userToAdd;
        }
    }

    public class MockHasher : IHasher
    {
        public string CreatePasswordHashInput { get; private set; }

        public PasswordHash CreatePasswordHash(string password)
        {
            CreatePasswordHashInput = password;
            return null;
        }
    }
}
