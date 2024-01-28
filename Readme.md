## Introduction
This project is created to demonstrate a way to deal with situations where there is a requirement to support different types of databases within the same application. This also handles the additional complexity where different and possibly incompatible data-types are used in different databases for the same column.
In this example, Oracle 19c Standard edition and Microsoft SQL Server 2022 is used as the database.

Key concepts in use:
- Use of dependency-injection to allow development of a single business-logic layer calling different data-access layers depending on setup.
- Simple method of separating data-access table-models by database type.
- Use of Json web tokens (JWT) for authentication.
- Use of service-extensions / middleware code to create a standardized exception message for user, and provide easy reference for developers/support-staff in log files when users report an issue.
- Unit-tests for data access layers. Can be used in continuous-integration (CI) pipelines as a prerequisite for completing a build, before being sent to the release pipeline (continuous-deployment, CD).
-- This serves as a development team checkpoint before it passes to the testing team.
- Documentation within API application which is available for end-users (via Swagger).

## Built with
- Microsoft .Net Core
- Microsoft Entity Framework
-- Oracle Entity Framework (.Net Core)
-- SQL Server Entity Framework (.Net Core)
- Serilog
- NSwag
- NUnit

## Getting Started
- Clone this repository to your local folder.
- Download and install Oracle and MS Sql server database (preferably both, or choose one only).
- Go to "\dbscripts" directory, choose "oracle" or "sql server"
- Execute the script files in the chosen directory for the database of your choice.
-- This creates the necessary tables and sequence objects in the database.
-- Do this for one or both databases with their respective set of files.
- Compile the source code
-- Go to "\src\CompleteDevNet\" directory.
-- Use MS Visual studio to open and compile "CompleteDevNet.sln". Or,
-- Use command prompt and change directory to "\src\CompleteDevNet\", and run the following "dotnet build --output "[`name of directory for output files`]".
- Edit "appsettings.json" file to use your local machine's environment. Key values to change in "appsettings.json":
-- DatabaseSettings --> DatabaseProvider : choose "oracle" or "sql" for different database types.
-- DatabaseSettings --> DatabaseSchema : set schema name if your database uses a non-standard schema. Especially relevant for sql server. Leave this blank if it is not required.
-- DatabaseSettings --> TnsAdmin : (For oracle only) directory location of your local machine's "tnsnames.ora" file.
-- ConnectionStrings --> SqlConnectionString : connection string for MS Sql Server.
-- ConnectionStrings --> OracleConnectionString : connection string for Oracle.

## Usage
Run the executable "CompleteDevNet.API.exe" in the compiled/output directory, or host this application on IIS.
- Running the executable: by default this will start the API application on port 5000. If you're unable to start it on port 5000 (example: port already used) or would like to start it on a different port, edit "appsettings.json", uncomment the section named "Kestrel" and modify the relevant URL accordingly.
- Hosting on IIS: create a new website, point the folder location to the compiled/output folder for this application.
-- You might be required to download and install [Microsoft ASP.Net Core Runtime Hosting Bundle](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Once the API application is successfully started, use a browser and go to "http(s)://[`localhost or machine name`]:[`port number`]/swagger/index.html"

All application logs will be recorded in "[`application root`]\Logs\" directory.

All endpoints requires authentication, except "(get)/Developers" and "(post)/Account/GetToken".
"(post)/Account/GetToken" will provide a JWT string that serves as the authentication method for all other endpoints.
To get a token, do the following steps:
- Call "(post)/Account/GetToken" and provide the username/password value.
-- following usernames will be accepted: "admin", "user1", "user2". Password is the same as username.
- Once API responds with the token-object, look for the property "token" and copy the entire string.
- at swagger page, click on "Authorize" button.
- Enter the word "bearer", followed by a space, followed by the whole string copied in the step earlier.
- You may now perform further actions on all endpoints.
- Endpoints usage:
-- GetToken: get JWT string to authenticate user.
-- (get)Developers : get listing of all developers. Has optional paging methods.
-- (put)Developers : edit/update single developer's details.
-- (post)Developers : add developer details to database.
-- (delete)Developers : delete single developer from database.

## Application use-case / design assumptions
- This application uses a simple user-authentication method. Complex in-application authentication can be built further by modifying the function "AuthenticateUser" found in AccountService.
-- There may be scenarios where 3rd party authentication is done outside of this API application, and this application only authenticates the JWT string given by the client. In this scenario, "GetToken" can be removed or replace with some other machine-to-machine endpoint.
- Design assumption for this example:
-- List of freelancers/developers can be viewed by public.
-- Any add/update/delete of freelancers will be vetted and performed by the application's administrators.

## License
Distributed under the MIT License. See LICENSE.md for more information.