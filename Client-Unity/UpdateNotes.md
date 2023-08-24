Update notes for the client made with the Unity project will be added here. This may not be synchronized with the server's update notes. The server update notes can be found at /Server-Unity/UpdateNotes.md.

## v0.1.0
### In-progress as of 08/23/2023
- Added the file for documenting updates (this file).
- Removed all of the extra assets from the project (tutorial assets and Sample Scene) and reorganized the folders and rendering tools.
- Added folders for Scripts/ScriptableObjects (which will be used layer).
   - Scripts for Player and other objects may be moved under their own corresponding folders later.
- Set up the InputAndMovement scene.
  - Purpose: Getting basic player input/movement working, with no networking (as of 08/23/2023).
- Added PlayerControls.cs with these classes:
  - PlayerControls: Script component for movement, currenlty using Physics. Will be attached to the player GameObject.
  - PlayerInputSettings: A serializeable class for PlayerControls for controlling input sensitivity and limiting looking up/down.
    - Will probably be replaced with JSON later.
- Added CustomInput.cs with these classes:
  - CustomInput: A static class for getting abstracted input, using UnityEngine.Input  and then adjusting it.
    - Useful to account for normalization, sensitivity, etc, and keybind adjustability for the future.
- Added PlayerInputAsset.cs with these classes:
  - PlayerInputAsset: A scriptable object with read-only constants for player input/movement values. May be changed to a static class later.
    - Currently used for sensitivity and physics strength.

## v0.0.0
### Published 08/22/2023
- Created the repository and the Unity project.