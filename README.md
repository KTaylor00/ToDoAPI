# ToDoAPI Setup!

## Checks
Please make sure you have the latest .NET 8 SDK installed on your machine, you can use the command -> dotnet --list-sdks, this will show you what versions you have installed, if you don't have the latest version you can download it from here -> https://dotnet.microsoft.com/en-us/download/dotnet/8.0. 
Please use Visual Studio, as it will make the setup easier.

## Inital Setup
Download the ZIP folder from this repo. Once you have extracted the contents go to the .sln file and double click on that, it should open in Visual Studio. 
You should see 2 projects in the solution, one called ToDoAPI and the other ToDoData.

The ToDOAPI project is the .NET Web API Project 
The ToDoData project is a library project that houses all the necessary folders and .cs files, that talk to the db and control the inner workings of the API.

## Final Step
You should now be able to run the project via https, I have set it up to use the https localhost url. The http variant won't work with the frontend app.

The API should open in the browser and you should see swagger if all was successful.
