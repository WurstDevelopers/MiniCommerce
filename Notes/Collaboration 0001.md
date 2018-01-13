Initial Setup and instructions by Thomas 
====

Goal
---
My goal in this exercise is to expose you to the onion architecture which is essentially a special subset of layered architectures. I was first exposed to the idea of the onion architecture in this [blog](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/). It is a great way to make sure that the core of your app remains focused on what matters to the business and you don't accedentally couple your business logic to a database or some other implementation detail. If that coupling happens it will make your application brittle.  

This Bob [video](https://devu.com/courses/applied-architecture-architecting-the-domain-layer/lessons/aa-ad-10-using-dependency-injection-to-break-layer-dependencies) is also helpful in explaining why you would want to depend on interfaces instead of actual implementations. Later on in the course of this project I will show you how easy it is to flip out databases or UI layers when you've implemented the onion architecture. 

The goal for you in this exercise is to get the console app to say **"This is from the UI! This is from the domain! This is from the data layer!"** when you run the application. 

Limitations
---

- The domain layer cannot have any dependecies outside of core c# things.
- The UI layer cannot have any dependencies on the data layer. 
- Any layer can depend on the domain layer since it should be at the core of the application. 

Steps
---

- Create an implementation of MiniCommerce.Domain.Interfaces.IDataContract in the data layer
- Use SimpleInjector to register that new DataContract class so that your application will know what to inject when it encounters IDataContract in a constructor.  
- Figure out how to get the app to compile by adding the right dependencies. 
- Run the application
- Analyse why the infrastructure layer was necessary or even if it is necessary. Could you get the app to compile without the infrastructure layer? If so, how? What limitations would be violated, if any? 


Mal's Thoughts and Process
====



Feedback for Thomas
---
