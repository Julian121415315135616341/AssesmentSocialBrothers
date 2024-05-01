# Assessment of Social Brothers
## Installation
- Ensure that you have the .NET SDK installed. (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Make sure you have SQLite installed (https://www.sqlite.org/download.html). For additional information on how to install SQLite, you can visit this page (https://dev.to/dendihandian/installing-sqlite3-in-windows-44eb).
- Clone or download the project.
- Open the project and navigate to the WebApplication1 folder using the terminal.
- Run the command "dotnet run".
- The WebAPI will run on "localhost:5196".
## Reflection
- One aspect that I'm very happy with is the sorting and ordering of the addresses because I think I am doing it using a very easy and changeable method.
- Another point I'm pleased with is the structure of the project; everything is well separated into layers, and communication between the layers is clear and clean.
- One aspect I'm less happy about is the structure; I ended up putting all of my endpoints within the program.cs file. I tried to use a separate controller file, but I did not manage to finish that in time.
- Another thing that I did not manage to do was to include JSON in my Swagger requests. In order to still be able to test the requests, I have included a Postman collection.
