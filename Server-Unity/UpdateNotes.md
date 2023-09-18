# About
Update notes for the server made with the Unity project will be added here. This is not synchronized with the client's update notes.

## v0.2.1
### Completed as of 09/18/2023
- Added the PlayerData folder:
  - Will be used to store non-realtime player data for authentication (username, password, etc).
    - Files in this folder will be unencrypted and stored as JSON and/or text files, as this folder is only for testing.
    - Added About.md for explanation of data in this folder.
- Some cleanup to ListenOnUDPPort.

## v0.2.0
### Completed as of 09/11/2023
![](https://raw.githubusercontent.com/rvishwajith/CS370-Research/main/Screenshots/Server/v0-2-0-multipleClients.png)
**Multiple clients being tracked and updated simultaneously.**
- Testing results: All tests for this version run successfully, with no known issues.
  - Tested using the following setup (macOS), all instances are running simultaneously:
    - Server (Unity Editor at 60 FPS to reduce extra CPU usage)
      - UDP 127.0.0.1/5000
      - TCP: N/A
    - 1 Editor Client
    - 1 Build Client
  - The server seems to work perfectly with multiple clients for the implemented features:
    - When a new client joins a session with a new ID:
      - Note: A session is currently anytime the server is running, this won't be changed for a while since the gameplay framework is not going to be implemented until Phase 2.
      - Client is instantiated using the first data packet.
      - A new registry is created in the dictionary so the client can be tracked for future data packets.
    - When a client sends a packet with an existing ID:
      - The corresponding client instance is looked up and updated successfully.

## v0.1.2
### Completed on 09/11/2023
- ListenOnUDPPort is now functional and successfully recieves data from the client.
  - ISSUE: Does not seem to work when the recieving port and IP address are not set to 127.0.0.1 (loopback IP) and 5000 (arbitrary testing port).
    - Appears to be an issue in Unity, where the editor only considers the local IP and ignores others.
    - Unsure of fix, will look into this in the future since testing is still possible for now.
- Added Constants.cs with the following classes:
  - Addresses: A static helper class storing IP address (System.Net) objects such as the Local IP (127.0.0.1) and Loopback IP. 
- Added LockFrameRate.cs with the following classes:
  - LockFrameRate: A component attached to any object used to reduce the server's frame rate, since a high frame rate is unnecessary for the server and reduces the performance of local clients.
- Changes to PlayerDataPacket:
  - Removed all unused properties.
  - Added identifier - a long to match with the client which will be used for a Dictionary lookup.
- Some rendering and material settings changed.

## v0.1.1
### Completed on 08/30/2023
- Added the following packages:
  - NaughtyAttributes: Used for easier debugging in the editor.
  - DOTween: No planned uses currently, will probably used for any tweening in the future.
- Rebuilt project because of a Unity glitch (fixed).

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
- Changed backend to IL2CPP.

## v0.0.0
### Completed on 08/21/2023
- Unity project creation.
- Added UpdateNotes.md and forced single-threaded rendering.