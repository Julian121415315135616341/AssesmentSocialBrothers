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
- I am currently handling the usage of the distancematrix API within my addresservice, there should be a seperate class for the usage of the API, I did not have the time to implement this so for now it is being done within the addresservice
- A second issue with the API is that the API key is currently not hidden, if I were not using a public free API with limited calls per month i would have to hide the key, but i did not take that into account when starting on the API functionality.
