How I'm going to do this
===
1. First I'm going to create a test class in MiniCommerce.Infrastructure.Tests.
2. It is called UserManagementTests.cs
3. Now I'm installing the fluentassertions nuget in all the test projects.
4. First test... The user manager must be able to create new users. So, I'm going to test an CreateUser method first. It will take a username and password. That test is `UserManagementTestsCreateUserShouldCallThePasswordHasherWithTheCorrectParameters()`.
5. I'm going to make a interface that does the hashing for me. Hashing is pretty complex and a different function than usermanagement so I would like to test it and keep it seperate. Otherwise this class would become too cumbersome.

**UserManagementTestsCreateUserShouldCallThePasswordHasherWithTheCorrectParameters Test Writing** 
1. First thing I need to do is create an instance of the userManager. Right now I can instanciate it without passing an interface. This isn't ideal since we want to have the hashing handled by some other class. So I'm going to create an IHasher interface in the infrastructure project. 
2. Now we get to the fun part. I'm unit testing this so I want to be able to control every aspect of IHasher's output. Therefore I need a Mock implementation. A mock implementation is simply an implementation that we create soley for the purposes of testing. Most people use Mocking frameworks but I want you to learn what is going on under the covers. So, I'm creating ``MockHasher`` at the bottom of the ``UserManagementTests`` file.
3. Now I need to create a method on the IHasher interface. It is called ``CreatePasswordHash``
4. Now the compiler is complaining because ``MockHasher`` doesn't have that implemented. So, I will tell it to implement it.
5. Ok, so now that I have that mock hasher I will instantiate it above my instance of userManager and pass it into it like ``new UserManagement(hasher)``
6. Now the compiler complains because there is no constructor that matches that for ``UserManagement()``. So I will go into ``UserManagement.cs`` and create a constructor that accepts an ``IHasher`` as a parameter. This solves the problem of the compiler. 
7. So, finally, we can test something. I want to test that I call ``CreatePasswordHash()`` with the password we supply it. The password I'm going to pass is ``"BlueIsMyFavoriteCat(ThisIsTrue)"``. Then I'm going to call ``userManager.CreatePassword(userName, password)``. Ooops. This doesn't exist so I'm going to ctrl+. it into existance.
8. Now we hit the tricky situation of how the hell are we going to test if ``CreatePasswordHash`` is called with the correct parameters. Well, we have complete control of the ``MockHasher``! So, we can just add a field willy nilly on mock hasher, and since UserManagementTests is using the implementation of MockHasher, not just IHasher, it will have access to all public fields, properties, and methods of the ``MockHasher``. So, I'm going to create a property called ``CreatePasswordHashInput`` in ``MockHasher``. Then I'm going to assign the parameter ``password`` to ``CreatePasswordHashInput`` in the mock ``CreatePasswordHash`` implementation. It doesn't like that I'm not returning anything... so I'm going to return null that this point.
9. OK, now we can actually assert something. This is the assertion that should fail at this point ``hasher.CreatePasswordHashInput.Should().Be("BlueIsMyFavoriteCat(ThisIsTrue)");``. This is because we want to assert that the password that is passed into the ``CreateUser`` function is passed, without alteration, into the ``CreatePasswordHash`` algorithm. 
10. Now we get to write production code! 

**Satisfying Our Test**

Now we look at ``UserManagement`` finally. So, we see this sad code:

``` CSharp
    public class UserManagement
    {
        public UserManagement(IHasher hasher)
        {

        }

        public void CreateUser(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
```

It doesn't do anything and it doesn't even have the hasher put into a field. Let's get it working. 

1. First, ctrl+. the hasher into a field. 
2. Next we just need to satisfy our test... which is to pass the password into the hasher. So we do that... 
3. The test goes green! Yes! It looks like this:

``` Csharp 
    public class UserManagement
    {
        private readonly IHasher hasher;

        public UserManagement(IHasher hasher)
        {
            this.hasher = hasher;
        }

        public void CreateUser(string userName, string password)
        {
            hasher.CreatePasswordHash(password);
        }
    }
```