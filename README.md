# ToDoAPI

## Checks
Please make sure you have the latest .NET 8 SDK installed on your machine.

## Inital Setup
Download the ZIP folder from this repo. Once you have extracted the contents go to the sln file and double click on that, it should open in Visual Studio. 
You should see 2 projects in the solution, one called ToDoAPI and the other ToDoData.

The ToDOAPI project is the .NET Web API Project 
The ToDoData project is a library project that houses all the necessary folders and .cs files, that talk to the db and control the inner workings of the API.

## DB Setup
Once you have the solution open go to tools -> Nuget Package Manager -> Package Manager Console.
Once you have the Package Manager Console open you're going to change the default project in the top dropdown to ToDoData.
From there you are going to run the command -> Update-Database

If the command and build was successful you should see a success message and a todo.db file in the ToDoAPI project.

## Final Step
You should now be able to run the project via https, I have set it up to use the https localhost url. The http variant won't work with the frontend app.

The API should open in the browser and you should see swagger if all was successful.
