# I Can Haz Dad Jokes Challenge

>ASP.NET MVC application that shows dad jokes from https://icanhazdadjokes.com
 
### Requirements to run the code:

  - Visual Studio Express 2017
  - Windows with IIS installed
  - ASP.NET runtime 4.0.30319 or newer
 
To run the application open the solution with Visual Studio, right-click on IHazDadJokes.MVC, set it as *StartUp Project* and run the project pressing F5.

### Tech

This project uses:

* .NET Framework 4.6.1
* C#
* ASP.NET
* MVC 5
* Razor
* Bootstrap
* jQuery
* NUnit 3
* Moq

Also, for the live demo, the project have been deployed to:

* Azure App Services

### Structure

- IHazDadJokes.Infrastructure.HttpLib
    - Infrastructure class library used to make HTTP requests to through HttpClientWrapper
- IHazDadJokes.API.Lib
    - Service class library used to expose services to get random jokes from ICanHazDadJokes API and search for jokes by a given search term. Makes GET requests to the external API through HttpClientWrapper, deserializes the response, processes and returns a model or view-model to the caller.
- IHazDadJokes.MVC
    - ASP.NET MVC web application containing the controllers that expose actions for the view logic to retrieve data and fill the views to be rendered in the browser when requested by a user.
- Simple test projects have been created with the names of the project being tested followed by the suffix *.Tests*

### Remarks on logics and reasoning
- Rendering and text formatting logic was kept whenever possible in the views. 
- Controllers have only the necessary logic to call the services, except for some dependency injection logic that was kept in JokesController and would be, in a production scenario, inserted in a dependency injection structure.
- Services have the actual logic to deal with fetching and processing the data that will then be returned to the controllers and sent back to be rendered by the views.
