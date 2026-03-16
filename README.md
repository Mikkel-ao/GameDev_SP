# GameDev_SP

A 3D Unity game where a robot wakes up after 100 years of deep sleep and must find a way out of the place it is trapped in.

## Developers

- Mikkel Andreas Olsen
- Valdemar Poulsen
- Philip Sune Nilausen

## Game Concept

The player controls a robot that comes back to life and explores the environment to escape.
To progress, the player collects items and keys, uses a bag-based inventory, and follows in-game messages that provide directions and instructions.

## Game Areas

1. Starting Area
2. Outdoor
3. Dungeon

## Core Features

- Item and key collection
- Inventory system with bag unlock and slots
- Stackable and non-stackable inventory items
- Pause menu and UI flow
- Health and health bar UI
- Guidance messages shown to the player during gameplay

## Game Systems

- Sound manager (centralized audio handling)
- Inventory system
- Lighting and shadows
- Camera system with Cinemachine

## Tech Stack

- Engine: Unity
- Language: C#
- Camera: Cinemachine
- Level design plugin: ProBuilder

## How To Run

1. Open the project in Unity Hub.
2. Open with Unity editor version `6000.3.9f1`.
3. Open the scene `Assets/Scenes/StartingArea.unity`.
4. Press Play.

## Controls

- `Tab` - Toggle inventory (after bag pickup)
- `Esc` - Toggle pause menu
- Movement/combat inputs are configured through the Input System actions in the project

## Script Overview

- `Assets/Scripts/Inventory/Bag/BagPickup.cs` - Handles bag pickup and attach flow
- `Assets/Scripts/Inventory/Inventory/InventoryManager.cs` - Inventory logic, slots, stacking, bag state
- `Assets/Scripts/Inventory/Inventory/ItemPickup.cs` - World item pickup behavior
- `Assets/Scripts/Inventory/InventoryUI/InventoryUIToggle.cs` - Inventory UI toggle behavior
- `Assets/Scripts/PauseMenu/PauseMenu.cs` - Pause menu behavior and UI control
- `Assets/Scripts/CombatScripts/Health.cs` - Shared health logic for player/enemy
- `Assets/Scripts/Health/HealthBar.cs` - Health bar display updates
- `Assets/Scripts/Raycast/SignUI.cs` - Triggered player message UI
- `Assets/Scripts/Audio/SoundManager.cs` - Centralized sound playback

## Notes / TODO

- Keep Inspector references assigned (inventory slots, UI panels, script references), or systems may fail at runtime.
- Add screenshots/GIFs for inventory, pause menu, and health bar for portfolio/exam presentation.
- Expand this README with a short "Known Issues" list when needed.
