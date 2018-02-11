Implementing Product Management
===

So, we want the system to be able to do CRUD operations on products. This module will handle that. The only real logic I can think of is making sure the user has permissions and assigning unique IDs to each product... but I'm sure it is more complex than that. 

Thomas's Goal
---
So, I want you to understand mocked objects more fully at the end of this section. 

You should have 2 failing tests checked in at the end of this section and one passing.

The 2 failing should be asserting that the user has permissions to DELETE and UPDATE products. The passing one should assert that the user has permissions to READ products.

You can read my UserManagement.ThomasTrainOfThought.md in the infrastructure folder for maybe a model of how to do this. 

Limitations
--- 
- You cannot use a mocking framework. 
- You should use the ``IAuthorizer`` interface to determine if a user has permission to do that interaction with the system. 
- You should create a new class for these behaviors. 

Steps
---
See UserManagement.ThomasTrainOfThought.md and feel free to ask me questions. 

Interesting Observation - Hint
---
It is going to be difficult to test the read permission. Enums default to the first option of the enum. So, if make sure that `Permission.Read` is passed in you need to overwrite the default to something else in order to get a failing test. 

Mal's thoughts and process
===
~~1/31 - Troubleshoot why unit tests aren't running on my machine; write two tests for update and read (keep one failing)~~

Thoughts: I fixed the test per your suggestion of setting the permission to something other than read before making the call. I also started looking into enums and I think Permissions.cs is a simple enum that [should probably have a 'None' or 'Undefined' attribute](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/enum) as its default? This got me wondering if we want user and product permissions in the same class? I figure there would be a case were we'd give a user permissions to do some things but not with products, so we'd probably want a UserPermissionsNone and a ProductPermissionsNone and at that point we could probably just split them up into their own classes. What do you think?

**Thomas Response**

1. I like the idea of having a default. Maybe we can just make that ``Permission.None``. This is a **good** idea. You are awesome.

Sidebar: YES YES YES I just got a Code Spell Checker for VS Code. It is awesome. 

2. I think there is definitely going to be the case where we have users don't have product permissions. But I think that we will be OK with only having affirmative permissions. I can't imagine a time when we would be checking in code to see if someone lacks the permissions to do something before we do it. I'm thinking that we would always ask if that user *can* do something. So, the lack of product permissions associated with the user would be the same as a ``Permission.ProductNone`` without having the complexities of figuring out whether we have to assign that person the ProductNone permission. 
3. I don't really like the Product and User permissions in the same class. It is horrible. Every time we add something to it it will I just for the life of me can't figure out how we could get around it. 

I was thinking that a user would look like this:

``` csharp
// My current idea
public class User
{
    public List<Permission> Permissions { get; set; }
}

// Option #2
public class User
{
    public List<UserPermission> UserPermissions { get; set; }
    public List<ProductPermission> ProductPermissions { get; set; }
}

```
The problem with separating them like in option #2 is that we would have to have multiple interfaces to account for the multiple Enum types. 

``` csharp
public interface IAuthorizer
{
    bool HasAuthorization(UserPermission permissionRequired);
    bool HasAuthorization(ProductPermission permissionRequired);
}
```

Normally you could get around this issue by inheriting from a base class. But, alas, enums can't inherit. 

So, maybe we could create a permission class? I'm not sure. 

*A little time passes*

Ok, I've experimented a bit with making the enum a class. The code is a little crazy and I'd have to test it but I think it would work. It is in Permission.cs. Tell me what you think. If we did that we could separate out the two. 

I'll experiment a bit tomorrow to see if the class version is good. I do like it a lot... I think. We could have simple interfaces but also have the joy of having a small number of options for each set of permissions. Also, default behavior would be a null permission instead of the top one. 


**End Thomas Response**


Sidebar: I'm imagining that for a user the permissions would be none and for the product it would be undefined?

