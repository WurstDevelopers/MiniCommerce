Thomas First Notes
===

Goals
---
Have a solution that builds on Mal's computer so we can start programming this thing. 

Limitations
---
- The solution should have the same structure as the first MiniCommerce solution. 
    - Maybe we could rename the projects to MiniCommerce.ProductManager.* if we want this to be a seperate application. I am not passionate about this idea.
- It should have the dependency injection framework installed in the right projects. 
- The solution should have at least two unit testing projects. 
    - One for unit testing the domain. 
    - One that is an integration testing project. 
        - I'm thinking that we should make this test through the domain layer. Hooking it up to a console app is pretty stupid if we are just going to flip that out for a web UI at some point. 
    - Both of them should have a single failing unit test in them to make sure they work.
- The old projects should be deleted.
- Build artifacts like the /bin folder should not be checked into source control.
    - Maybe your git client is ignoring the .gitignore file. I use GitHub's client and it only checks in the right files.
- It should be checked into the main branch of source control.

