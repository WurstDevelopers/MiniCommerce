Thomas First Notes
===

Goals
---
Have a solution that builds on Mal's computer so we can start programming this thing. 

Limitations
---
- ~~The solution should have the same structure as the first MiniCommerce solution.~~ 
    - Maybe we could rename the projects to MiniCommerce.ProductManager.* if we want this to be a seperate application. I am not passionate about this idea.
- ~~It should have the dependency injection framework installed in the right projects. ~~
- The solution should have at least two unit testing projects. 
    - One for unit testing the domain. 
    - One that is an integration testing project. 
        - I'm thinking that we should make this test through the domain layer. Hooking it up to a console app is pretty stupid if we are just going to flip that out for a web UI at some point. 
    - Both of them should have a single failing unit test in them to make sure they work.
- The old projects should be deleted.
- Build artifacts like the /bin folder should not be checked into source control.
    - Maybe your git client is ignoring the .gitignore file. I use GitHub's client and it only checks in the right files.
- It should be checked into the main branch of source control.

##Mal's Notes
===
- Simple Injector nuget package is only referenced in UI and Infrastructure layers
- When programming do you creat the interface or the class first?
    - (Thomas) I personally think that I vary. I like to create interfaces first because it forces you to consider how a user of the class would use it. This happens a lot when you are testing a class and you decide you need to interact with some other class. You can have that interface grow naturally out of what you need... I'll try to document my thought process pretty thoroughly through this process. But, I'm sure there have been times when I just created an implementation and made the interface by what I required in the moment. (This doesn't have a spell checker so you are going to subjected to some horrible spelling. I'm an idiot.)


Thomas's 1/24 notes
===

Comments on your pull request.
- I deleted all the /bin /obj folders from your check-in and integrated the test projects. 
    - The /bin and /obj folders were messing with my build system. It is generally a good practice to only check in code files and csproj files. Basically just check in things that you manually edit. Let other people's build systems make the binaries. 

My thinking about programming.
- I think that our users should have restricted access based not on something like an admin position, but on what permissions they have. I think it is concievable that a user could have read access rights but not write access. So, I'm going to define an interface for autherization that basically looks like this ``bool IsAutherized(enumOfPermissions)``. 
    - The domain will have to be aware of this code because we are going to need to know if a user can use this information to read, write, etc. 
    - Should this code live in the domain since it won't have dependencies and it seems pretty important? I don't know.
- Authentication is going to be a seperate interface because authentication and autherization are two different things. One is asserting that someone is who they say they are, the other is saying that that person has the permissions to do something within the system. In fact, our autherization code will be completely independent of the authentication code and I bet the authentication code will have a much longer lifespace. The autherization code is built into the framework. 
    - I'm not entirely sure if the domain should have any awareness of passing passwords, username, or anything. Should this code be solely in infrastructure? 
- I'm going to just create some junk code in infrastructure for personas for permissions to be used in integration testing. 
    - Write Access Persona - Walter
    - Read Access Persona - Rhonda
    - Read/Write Access Persona - Wren
    - Delete Access Persona - Dominic
- Blarg, got to go. 
    - Checking in only .csproj, .cs, and .md files. 
- Hopefully I'll get to programming soon. 



