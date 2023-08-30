# About
Update notes for the server made with the Unity project will be added here. This is not synchronized with the client's update notes.

## v0.1.1
### In-progress as of 08/30/2023
- Added the following packages:
  - NaughtyAttributes: Used for easier debugging in the editor.

## v0.1.0
### Completed on 08/30/2023
- Made changes to the project setup:
  - Removed tutorial assets
  - Renamed renderers/lighting settings and moved them into the Rendering folder.
- Created a basic scene for testing UDP networking.
- Created a basic prefab to represent a player instance.
  - Draws a handle containing some debug informtation.
  - Currently username and session token placeholders.
- Added Scripts and Packages folders.
- Added the following packages:
  - PrototypeGrids: for easier visualization of the map.
- Created PlayerData.cs with the PlayerData class.
  - PlayerData: A component which will be used to show information about individual players.
    - Current planned uses: position, rotation, username, and session token.
- Created DebugProperties.cs with the DebugProperties static class.
  - DebugProperties: A static class to retrieve styles for showing labels in the editor.
    - Will probably be expanded upon or merged with another class in the future.
- Created ListenOnUDPPort.cs with ListenOnUDPPort class.
  - ListenOnUDPPort: A component which will be used to test receiving UDP data on an arbitrary port from a local client project.
- Lowered the quality of many rendering settings such as lighting/post-processing since visualization should be much cheaper than on the client.
  - Normally the server app would run in headless mode instead.

## v0.0.0
### Completed on 08/21/2023
- Unity project creation.
- Added UpdateNotes.md and forced single-threaded rendering.