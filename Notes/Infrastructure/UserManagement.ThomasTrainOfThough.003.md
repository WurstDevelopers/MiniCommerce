Goal
---

I want to make sure that the user name is unique in the system and also assign admin permissions to the first user that signs up. 

I'm adding some permissions to the enum for user management. I'm not entirely sure that an emum is the way to go for this because it is just going to get longer and longer. Grumbles. 

Explanation
---

I just wanted to elaborate a little on a design choice in this section. I chose to create a method in the data access to check whether a username had been used before in the system. Alternatively, I could have got a list of all the usernames in the system and then did the check in this module. 

I chose to do the check in the data access layer because you tend to want list operations to happen close to the data. This is because that list of all users could become very large so we would have to be shuffling huge amounts of data around if we wanted to do the check in our application. Databases are optimized for dealing with sets so they tend to do these things quickly. 


Process
---

**Test Method UserManagementTestsCreateUserShouldCallTheAccountExistsWithCorrectParameters()**
1. So, in this test I'm going to be making sure that we call into AccountExists in the repo with the correct username. This method does not exist yet on the interface. So, I'm going to go add it. It's signature is ``bool AccountExists(string userName);``.
2. The compiler complains because the ``MockUserRepository`` doesn't have that method. I ctrl+. it into existance. 
3. We do the same thing as before and create an ``AccountExistsInput`` as a property within that mock object. We are just going to go ahead and wire up the return value as well with a ``AccountExistsReturnBool`` property. 

**"Final" MockUserRepository**
``` csharp
    public class MockUserRepository : IUserRepsository
    {
        public User UserToAddInput { get; private set; }
        public string AccountExistsInput { get; private set; }
        public bool AccountExistsReturnBool { private get; set; }

        public bool AccountExists(string userName)
        {
            AccountExistsInput = userName;
            return AccountExistsReturnBool;
        }

        public void AddUser(User userToAdd)
        {
            UserToAddInput = userToAdd;
        }
    }
```
4. Now that we are all set up we can write our assert. We just need to make sure that our username we pass into ``CreateUser`` is the same as the username that is passed into ``AccountExists`` on the ``IUserRepository``. The assert ends up looking like this: ``userRepository.AccountExistsInput.Should().Be(username);`` That test fails! 

**Test code**
``` csharp
        [TestMethod]
        public void UserManagementTestsCreateUserShouldCallTheAccountExistsWithCorrectParameters()
        {
            var username = RandomValue.String();
            var password = RandomValue.String();

            userManager.CreateUser(username, password);

            userRepository.AccountExistsInput.Should().Be(username);
        }
```
5. To pass it we just need to call into the ``AccountExists`` method with that username in our actual production code. It looks like so: ``userRepository.AccountExists(userName);`` This makes our test pass!

**Production code**
``` csharp
        public void CreateUser(string userName, string password)
        {
            //Arggg. We need to make sure that the username is unique to the system. That will be my next round of tests.

            var passwordHash = hasher.CreatePasswordHash(password);

            var user = new User(userName, passwordHash);

            userRepository.AddUser(user);

            userRepository.AccountExists(userName);
        }
```