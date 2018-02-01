Goal
---
I want to have test that insures that we pass a User to the data layer that has the correct properities. 

A User needs to have a PasswordHash that has been generated and returned from IHasher. It will also need a field with the Username. We then need to make sure that this object is passed to the data layer. So, the two asserts I can think of are that the PasswordHash that is returned from the IHasher is passed on the User object to the data layer and the username is also passed. Easy peasy. 

Process
===


Refactoring
---

I'm going to refactor this code. Currently we have this:

``` csharp
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
```

The instantiation of the hasher and userManager are going to have to happen on every test method. So, I'm going to refactor that out into its own method called ``RunBeforeBeforeTestMethods()`` and decorate it with the ``[TestInitialize]`` attribute. This will make that code run before the code in the test methods. After I do that we have this:

``` csharp
    [TestClass]
    public class UserManagementTests
    {
        private MockHasher hasher;
        private UserManagement userManager;

        [TestInitialize]
        public void RunBeforeBeforeTestMethods()
        {
            hasher = new MockHasher();
            userManager = new UserManagement(hasher);
        }

        [TestMethod]
        public void UserManagementTestsCreateUserShouldCallThePasswordHasherWithTheCorrectParameters()
        {
            var password = "BlueIsMyFavoriteCat(ThisIsTrue)";
            var userName = "Thomas";

            userManager.CreateUser(userName, password);

            hasher.CreatePasswordHashInput.Should().Be(password);
        }
    }
```

Great. The test is still passing. Now on to the the tests.

**The Test Named ``UserManagementTestsCreateUserShouldCallAddUserWithTheCorrectUserName()``**

1. I installed RandomTestValues so I don't have to think about clever usernames and password. 
2. I'm going to be testing whether the correct username is passed to the data layer. But we don't have an interface defined for that so I'm going to go ahead and do it now. It is going to be called ``IUserRepository`` and I'm going to put it in the Infrastructure layer. I'm not sure if this is the correct place to put it... but we can always move it. 
3. Ok. So, now UserManagement is going to need knowledge of IUserRepository. So, I'm going to add that to the constructor of the real ``UserManagment`` class. 
4. Now our tests are complaining because there isn't a repo being passed in. So, I'm going to create a MockUserRepository and pass it in. 

**Test Code So Far**

``` csharp
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
        }
    }

    public class MockUserRepository : IUserRepository
    {

    }

    public class MockHasher : IHasher
    {
        public string CreatePasswordHashInput { get; set; }

        public PasswordHash CreatePasswordHash(string password)
        {
            CreatePasswordHashInput = password;
            return null;
        }
    }
}
```
**Real Code so far**
``` csharp
using MiniCommerce.Infrastructure.Interfaces;

namespace MiniCommerce.Infrastructure
{
    public class UserManagement
    {
        private readonly IHasher hasher;
        private readonly IUserRepository userRepository;

        public UserManagement(IHasher hasher, IUserRepository userRepository)
        {
            this.hasher = hasher;
            this.userRepository = userRepository;
        }

        public void CreateUser(string userName, string password)
        {
            //Arggg. We need to make sure that the username is unique to the system. That will be my next round of tests.

            hasher.CreatePasswordHash(password);
        }
    }
}
```

5. Ok, now we are going to be testing that the MockRepository's AddUser method is being called with a User object that has the correct username. So, we need to create a method on the IUserRepository that accepts a User and is called ``AddUser``.
6. I needed to create a User class as well. 
7. Ok, that is all done. Now the compiler is complaining because our MockUserRepo doesn't have that method. So, we will ctrl+. it into existance. And we need to add a property to expose userToAdd that is passed into the AddUser method to our tests. So we add a prop called AddUserInput to the class.

**Our Mock Repo looks like this now**

``` csharp
    public class MockUserRepository : IUserRepository
    {
        public User UserToAddInput { get; private set; }

        public void AddUser(User userToAdd)
        {
            UserToAddInput = userToAdd;
        }
    }
```
8. OK, now we have enough setup to write our next test. We need to ensure that the User we pass into that method has a Username property that matches what we passed into ``userManager.CreateUser``. So we write the assert: ``userRepository.UserToAddInput.UserName.Should().Be(username);`` This is asserting that we've passed the correct username value to our repository. This test fails miserably like me at life because UserToAddInput is currently null. Looks good.  
9. Passing the test... all I need to do is create a User object and assign it the username that we pass in and then pass that object to the MockUserRepo. I got it passing! Yeah!


``` csharp

//Implemenation
using MiniCommerce.Infrastructure.Interfaces;

namespace MiniCommerce.Infrastructure
{
    public class UserManagement
    {
        private readonly IHasher hasher;
        private readonly IUserRepository userRepository;

        public UserManagement(IHasher hasher, IUserRepository userRepository)
        {
            this.hasher = hasher;
            this.userRepository = userRepository;
        }

        public void CreateUser(string userName, string password)
        {
            //Arggg. We need to make sure that the username is unique to the system. That will be my next round of tests.

            hasher.CreatePasswordHash(password);

            var user = new User(userName);

            userRepository.AddUser(user);
        }
    }
}
//User
//I'm doing it this way because a User should always have a username and be immutable.
namespace MiniCommerce.Infrastructure.Interfaces
{
    public class User
    {
        public User(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; private set; }
    }
}

// Tests
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




```