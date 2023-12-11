# About
This is a log of update notes for the console server of the research project, which is meant for quick setup and testing of server prototypes before moving them to the Unity version of the server. This will primarily be used for testing authentication/data managament and easier interaction with the web client.

## v0.1.1
### In Progress as of October 16, 2023
- Renamed Server.cs to Main.cs.
- Added ConsoleServer.cs:
  - The ConsoleServer class creates a TCP Listener in Init() that awaits a client connection.

## v0.1.0 (Project Creation)
### Completed as of October 16th, 2023
- Created Visual Studio project.
- Added Server.cs:
  - Main runner class.