# About
Update notes for the client made with the Unity project will be added here. This is synchronized with the server's update notes.

## v0.2.1
### In-progress as of 08/28/2023

## v0.2.0
### Completed on 08/28/2023
![](https://raw.githubusercontent.com/rvishwajith/CS370-Research/main/Screenshots/Client/v0-2-0-basicMap.png)
**The prototype map design.**
- Added CopyTransformData.cs with the following classes:
  - CopyTransformData: A component that can optionally copy the position and/or rotation of a target.
    - Used for the camera so it doesn't need to be a child of the capsule.
- Added a Decal object and material/texture.
  - Will be used in the future for displaying bullet holes onto to the wall.
- Added a simple UI system using UI toolkit/documents, which currently only has a crosshair using a preset texture and outline.
  - Crosshair color can be changed using TintColor property
- PlayerInputAsset was moved to "Player" folder, and now has additional properties:
  - maxHorizontalVelocity (not used yet) to limit a player's speed without slowing down the speed they fall.
    - clampHorizontalVelocity toggle limit.
- Created a prototype of a map design with much more types of interactions:
  - Ramps, which seem to work correctly.
    - They do slow down the player, which is intended behaviour for now.
  - Walls and ceilings for multiple-story buildings.
  - Pillars, whicb don't do anything currently.
    - Planned use: markers for ropes that players can climb up.
- Changed some internal project setttings: 
  - Changed game's Physics update rate (Time/fixedTimeStep) from 0.02s to 0.015625s (64 calls/second).
    - This is mainly for easier syncing with the server in the future, which will run at a fixed 64 or 128 tick rate.
  - Reduced some rendering quality settings in PlayerSettings that were unnecessarily high.
    - Lightmap and HDR cubemap encoding quality reduced from high quality to normal quality.
  - Project backend was changed from .NET to IL2CPP.
    - This will make the project run faster and possibly have a smaller build size.

## v0.1.2
### Completed on 08/26/2023
![](https://raw.githubusercontent.com/rvishwajith/CS370-Research/main/Screenshots/Client/v0-1-2-weapon.png)
**Weapon model on the first person controller object.**
- Added FirstPersonWeaponController.cs with these classes:
  - FirstPersonWeaponController: A component to handle client-side weapon interactions.
    - Shooting a weapon (either hold to shoot continuously or click to fire once)
    - Cooldown after shooting works.
    - Can spawn VFX (a line renderer and a particle system) when the weapon fires and destroy them after automatically.
      - This is ineifficient and may switch to use object pooling (enable/disable or replay a single particle system) in the future.
  - Added WeaponEnums.cs (only has enums):
    - Types of weapons (WeaponType) and the equipped weapon's state (WeaponState) 
- Added a weapon model for testing (CC model from SketchFab).
  - Also added a corresponding muzzle flash particle system and a line renderer for simple VFX.

## v0.1.1
### Completed on 08/25/2023
- PlayerControls.cs renamed to FirstPersonMovement.cs:
  - PlayerControls class renamed to FirstPersonMovement.
- Added features to FirstPersonMovement class:
  - Jumping using Physics.
  - Gravity now done manually instead of using Rigidbody gravity
    - Allows a higher force and adjustable direction, since jumps lasted too long.
- Added 2 packages: DOTween and NaughtyAttributes.
   - Useful utilities for timed functions and debugging.
- CustomInput now has a seperate MouseInput() function for easier usage.

## v0.1.0
### Completed on 08/24/2023
- Added UpdateNotes.md for documenting updates (this file).
- Removed all of the extra assets from the project (tutorial assets and Sample Scene) and reorganized the folders and rendering tools.
- Added folders for Scripts/ScriptableObjects (which will be used layer).
   - Scripts for Player and other objects may be moved under their own corresponding folders later.
- Set up a basic InputAndMovement scene.
  - Purpose: Getting basic player input/movement working, with no networking (as of 08/23/2023).
  - Added walls and a floor with colliders.
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