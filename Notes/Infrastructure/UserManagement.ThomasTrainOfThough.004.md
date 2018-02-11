Goal
---

At the end of this set of tests I want to make sure that:
1. If the username is unique, the account is in fact created. 
2. If the username is not unique, the account is not made and that is communicated to the calling method. 

Thoughts
---
I've been thinking about how to do this for about the past week. My initial inclination was to just throw a UserAlreadyExistsException and have the calling method catch that exception. I've decided I do not like this **at all**.  The problems I see with that:

1. It isn't explicit. How am I as a user of this method supposed to know that if User already exists that I will need to catch that exception? This is my main problem. I don't want this to be something that a programmer discovers after they have written the code and run the application. It should be obvious how to use the method by just looking at the method signature. 
2. Is this really exceptional behaviour? I would think that trying to sign up for an account with a username is pretty common. 

I think I want the calling code to look something like this:

``` csharp

var createUserResult = userManagement.CreateUser(username, password);

if(createUserResult.Success)
{
    // tell user they have a new account.
}
else
{
    // tell the user there was a problem.
}

```