# Quantity Text Not Showing - Troubleshooting Guide

## ✅ What I Fixed
1. **Changed quantity display threshold** - Now shows numbers starting from "1" instead of requiring 2+ items
2. **Added proper initialization** - Quantity text is now properly enabled/disabled on Start()

## 🔍 Checklist to Verify Your Setup

### For Each ItemData Asset (Right-click > Create > Inventory > Item Data)

- [ ] **Item Name**: Set a clear name (e.g., "Gold Coin", "Health Potion")
- [ ] **Item Icon**: Assigned a sprite
- [ ] **Is Stackable**: **TRUE** (for items you want to stack)
- [ ] **Max Stack Size**: Set a number like 99, 10, 64, etc.

### For Each Inventory Slot in the Hierarchy

**IMPORTANT**: Each slot needs BOTH the Image AND the TextMeshPro text assigned!

- [ ] **Slot has InventorySlot component**
- [ ] **itemImage field**: Assigned to the Image component (shows the item icon)
- [ ] **quantityText field**: Assigned to the TextMeshPro text child (shows the number)
  - This must be a TextMeshProUGUI component, not regular Text
  - Must be a child or sibling of the slot
  - Should be positioned in the corner
  - Make sure text color is visible (white, black, etc.)

### For Each Item in the Scene (that you pick up)

- [ ] **Item has ItemPickup component**
- [ ] **itemData field**: Assigned to your ItemData asset (e.g., "Gold Coin")
- [ ] **quantity field**: Set to 1 (or more if picking up multiple at once)

## 🐛 Quick Debugging

### To Check if Quantity Text is Assigned:

1. Play the game
2. Open the Console
3. Pick up a stackable item
4. Look for these messages:
   - ✅ "Item picked up! Added to inventory slot X." = Item was added
   - ✅ "Item stacked! Added X to slot Y. Total: Z" = Stacking worked
   - ❌ "itemData is not assigned!" = ItemPickup needs ItemData

### To See Complete Inventory Status:

1. Add the InventoryDebug script to your InventoryManager GameObject
2. In Start() or Update(), call: `GetComponent<InventoryDebug>().LogInventoryStatus();`
3. Play and check console output

## Common Issues

### Issue: No number appears at all

**Check**: Is the TextMeshPro text component visible on screen?
- Make sure it's not hidden behind the item image
- Check the text color isn't black on black background
- Check it's enabled in the hierarchy

**Check**: Is quantityText assigned in the Inspector?
- Select the slot in hierarchy
- Look at InventorySlot component
- Verify "Quantity Text" field has something assigned (not empty)

### Issue: Number shows but only when 2+ items

**Solution**: Already fixed! Make sure you have the latest InventorySlot.cs

### Issue: Number shows "1" but doesn't update to "2" when picking up more

**Check**: Is the item being set as Stackable?
- Select your ItemData asset
- Look for "Is Stackable" toggle
- It must be TRUE for quantity to update

### Issue: Each item takes a new slot instead of stacking

**Check**: ItemData properties
- "Is Stackable" must be TRUE
- "Max Stack Size" must be > 1
- These must match for all items of the same type

## Expected Behavior

```
Pick up 1 Gold Coin (Stackable: TRUE, Max: 99):
[Coin Icon] "1"

Pick up 4 more Gold Coins:
[Coin Icon] "5"

Pick up 10 more Gold Coins:
[Coin Icon] "15"

Pick up a Rusty Key (Stackable: FALSE):
[Key Icon]  (no number shown)

Pick up another Rusty Key:
[Key Icon]   [Key Icon]  (takes new slot, no stacking)
```

## Still Not Working?

Try these steps:

1. **Delete all the item pickups from the scene** (we'll remake them)
2. **Delete all ItemData assets** you created
3. **Follow this exact process**:
   ```
   Step 1: Create ItemData asset
   - Right-click > Create > Inventory > Item Data
   - Name it "TestCoin"
   - Set Is Stackable = TRUE
   - Set Max Stack Size = 99
   - Set Item Icon = any sprite
   
   Step 2: Create a test pickup in scene
   - Create new Cube
   - Add ItemPickup component
   - Drag "TestCoin" into itemData field
   - Set quantity = 1
   
   Step 3: Pick it up and check console
   ```

4. **If number still doesn't show**: Check if TextMeshPro text component is visible and has color

