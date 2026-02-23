## 🎵 SIMPLIFIED SOUND SYSTEM SETUP GUIDE

### What We Have Now:
✅ **SoundManager.cs** - Simple, clean audio manager
✅ **SoundValues.cs** - Enum with sound types (WALK, JUMP, LAND, ATTACK, DAMAGE, DEATH, BACKGROUND)
✅ **PlaySoundEnter.cs** - Animation behavior to play sound when state enters
✅ **PlaySoundExit.cs** - Animation behavior to play sound when state exits

---

## 🔧 SETUP IN UNITY

### Step 1: Create a SoundManager GameObject
1. In your scene, create an empty GameObject named **"SoundManager"**
2. Add an **AudioSource** component to it
3. Add the **SoundManager** script from `Assets/Scripts/Audio/SoundManager.cs`

### Step 2: Configure Sounds in Inspector
1. Select the **SoundManager** GameObject
2. In the Inspector, find the **SoundManager** script
3. Expand the **Sounds** array
4. For each sound type (WALK, JUMP, LAND, ATTACK, DAMAGE, DEATH, BACKGROUND):
   - Set the **Type** dropdown
   - Drag audio clips into the **Clips** array (you can add multiple for random variation)
   - Set the **Volume** (0-1)

**Example:**
```
Sounds [7]
  [0] Type: WALK, Clips: [walk_1, walk_2], Volume: 0.7
  [1] Type: JUMP, Clips: [jump], Volume: 0.8
  [2] Type: LAND, Clips: [land], Volume: 0.8
  [3] Type: ATTACK, Clips: [attack], Volume: 0.9
  [4] Type: DAMAGE, Clips: [damage], Volume: 0.7
  [5] Type: DEATH, Clips: [death], Volume: 0.9
  [6] Type: BACKGROUND, Clips: [music], Volume: 0.5
```

---

## 🎮 HOW TO USE IN YOUR GAME

### Option 1: Play Sound from Code
```csharp
using SoundScripts.SoundManager_main;

// Play a jump sound
SoundManager.PlaySound(SoundType.JUMP);

// Play with custom volume multiplier
SoundManager.PlaySound(SoundType.ATTACK, 0.8f);
```

### Option 2: Play Sound from Animation (RECOMMENDED)
1. Open your **Animator Controller** (e.g., RobotController.controller)
2. Select an animation state (e.g., the "Jump" state)
3. In the Inspector, go to the bottom where it says **Behaviours**
4. Click **Add Behaviour** → **PlaySoundEnter**
5. Configure:
   - **Sound**: Select the sound type (JUMP)
   - **Volume**: Set volume multiplier (default 1)
6. Now whenever the Jump animation plays, the jump sound will automatically play!

**For state exit sounds:**
- Use **PlaySoundExit** behavior instead
- Example: Play a "land" sound when leaving the Jump state

---

## ✅ EXAMPLE: Add Jump Sound to Your Animation

1. Open **Assets → RobotController.controller**
2. Find the **Jump** state in the Animator
3. Click on the Jump state
4. In the Inspector at bottom, expand **Behaviours**
5. Click **Add Behaviour** → **PlaySoundEnter**
6. Set **Sound** to **JUMP**
7. Set **Volume** to **1**
8. Now jump and listen!

---

## 🎯 SUMMARY

The new system is much simpler:
- ✅ All sounds configured in one place (SoundManager inspector)
- ✅ Easy to add sounds to animations (PlaySoundEnter/Exit behaviors)
- ✅ Can play sounds from code anytime
- ✅ Supports multiple clips per sound type (random selection)
- ✅ Clean, maintainable code

---

This keeps everything simple and easy to use!

