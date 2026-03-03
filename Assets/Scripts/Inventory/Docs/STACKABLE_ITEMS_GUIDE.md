# Stackable Items System - Implementation Guide

## Overview
This guide explains how to use the stackable items system for your inventory.

## Key Components

### 1. **ItemData ScriptableObject**
- **Purpose**: Defines what an item is and its properties
- **Location**: `Assets/Scripts/Inventory/Items/ItemData.cs`
- **Properties**:
  - `itemName`: Display name of the item
  - `itemIcon`: Sprite/image of the item
  - `isStackable`: TRUE for stackable items (coins, ammo, potions), FALSE for unique items (keys, swords)
  - `maxStackSize`: Maximum quantity in one slot (only used if stackable = true)

### 2. **Updated InventorySlot**
- Now tracks **quantity** of items instead of just one item per slot
- Shows **quantity text** on UI (only for stackable items with quantity > 1)
- New methods:
  - `IncreaseQuantity()`: Add more items to stack
  - `DecreaseQuantity()`: Remove items from stack
  - `CanAddItem()`: Check if this slot can accept more of a certain item
  - `GetQuantity()`: Get current quantity
  - `GetItemData()`: Get the ItemData reference

### 3. **Updated InventoryManager**
- Now accepts `ItemData` instead of just `Sprite`
- Handles stacking logic intelligently:
  - If item is stackable → tries to add to existing stack first
  - If no room in stack or item not stackable → finds new empty slot
  - Recursively handles overflow when max stack is reached

### 4. **Updated ItemPickup**
- Now references `ItemData` asset instead of `Sprite`
- Has optional `quantity` field for picking up multiple items at once

## How to Use

### Step 1: Create ItemData Assets
1. Right-click in Project folder → Create → Inventory → Item Data
2. Create one for each unique item. Examples:
   - **Gold Coin** (Stackable, max 99)
   - **Health Potion** (Stackable, max 10)
   - **Rusty Key** (Not stackable)
   - **Ancient Sword** (Not stackable)

### Step 2: Configure Each ItemData
- Set `itemName` (for reference/debugging)
- Set `itemIcon` (drag your sprite here)
- Toggle `isStackable`:
  - **TRUE** for items that should stack (coins, potions, arrows)
  - **FALSE** for unique items (keys, quest items, weapons)
- If stackable, set `maxStackSize` (e.g., 99 for coins, 10 for potions)

### Step 3: Setup UI Slots
- Select each InventorySlot GameObject in hierarchy
- Assign the `quantityText` field to a TextMeshPro text component
  - This text should be a child of the slot and positioned in corner
  - Make sure it's set to white or visible color
  - Font size should be small (8-12pt recommended)

### Step 4: Update Item GameObjects
- Select each item in the scene (keys, coins, potions, etc.)
- Find the `ItemPickup` component
- Drag the corresponding **ItemData asset** into the `itemData` field
- Set `quantity` if you want to pick up multiple (e.g., picking up 5 coins at once)

## Example Setup

### Non-Stackable Item (Rusty Key)
```
ItemData Asset: "RustyKey"
- itemName: "Rusty Key"
- itemIcon: [key sprite]
- isStackable: FALSE
- maxStackSize: 1

In-game pickup shows: [Key Icon] with NO quantity text
```

### Stackable Item (Gold Coins)
```
ItemData Asset: "GoldCoin"
- itemName: "Gold Coin"
- itemIcon: [coin sprite]
- isStackable: TRUE
- maxStackSize: 99

Pick up 5 coins → Shows [Coin Icon] "5"
Pick up 10 more → Shows [Coin Icon] "15"
Pick up 90 more → Shows [Coin Icon] "15" + message "Inventory is full!"
```

## System Behavior

### Stackable Items
- Multiple instances go into ONE slot
- Display shows quantity text (e.g., "5", "10")
- When stack reaches max, tries to find another empty slot
- When inventory is full, item cannot be picked up

### Non-Stackable Items
- Each item takes its own slot, even if same item type
- No quantity text displayed
- Cannot combine in single slot
- Useful for unique/quest items

## Quantity Overflow Example
```
Player picks up: 150 coins (maxStackSize = 99)
- First slot: 99 coins
- Second slot: 51 coins
- Remaining slots: empty
```

## Important Notes
1. **TextMeshPro Required**: Make sure your inventory UI uses TextMeshPro for quantity text
2. **ItemData is Assets**: Create these as .asset files in your project, not runtime
3. **Backwards Compatibility**: Old sprite-based pickups are deprecated but won't break
4. **Customize Display**: You can modify `UpdateQuantityDisplay()` in InventorySlot to show quantity differently

## Debugging
Check console for these messages:
- "Item picked up! Added to inventory slot X." → Success
- "Item stacked! Added X to slot Y. Total: Z" → Stacking worked
- "Inventory is full!" → No room for item
- "itemData is not assigned!" → Missing ItemData on pickup object

