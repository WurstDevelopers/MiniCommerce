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

Mal's thoughs and process
===