# Inventory System Documentation

This folder contains all documentation for the inventory system.

## 📚 Documentation Files

### **INVENTORY_SYSTEM_COMPLETE.md**
Complete overview of the entire inventory system implementation.
- Core features
- File structure
- Key components
- How to add new items
- Player controls
- System behavior examples
- Advanced features
- Troubleshooting reference

**Read this first** for a comprehensive understanding of the system.

---

### **STACKABLE_ITEMS_GUIDE.md**
How to implement and use the stackable items system.
- ItemData ScriptableObject overview
- InventorySlot updates
- InventoryManager stacking logic
- ItemPickup configuration
- Step-by-step setup process
- Configuration examples
- System behavior for stackable vs non-stackable items
- Debugging tips

**Read this if:** You want to create items that stack (coins, potions, ammo).

---

### **QUANTITY_TEXT_TROUBLESHOOTING.md**
Troubleshooting guide for quantity text display issues.
- Verification checklist
- Quick debugging steps
- Common issues and solutions
- Expected behavior reference
- Debug procedures

**Read this if:** Quantity numbers aren't showing in inventory slots.

---

### **SETUP_TEXTMESHPRO.md**
Guide to adding TextMeshPro text components to inventory slots.
- 3-step setup process
- Configuration instructions
- Visual hierarchy guide
- Troubleshooting tips
- Naming recommendations

**Read this if:** You need to add quantity text display to your slots.

---

### **INVENTORY_FULL_FIX.md**
Explanation of the inventory full protection system.
- How items are preserved when inventory is full
- PickupItem() return value system
- Destruction logic for ItemPickup
- Expected behavior when inventory full
- Component changes summary

**Read this if:** You want to understand how items stay in the world when inventory is full.

---

### **PAUSE_INVENTORY_FIX.md**
Integration between pause menu and inventory system.
- Input management system
- Communication between PauseMenu and InventoryUIToggle
- Setup instructions (auto-detection or manual)
- Expected behavior when pausing/unpausing
- Troubleshooting tips

**Read this if:** You need to fix inventory showing when pause menu opens.

---

## 🎯 Quick Start Paths

### I want to add a new item to my game
1. Read: **INVENTORY_SYSTEM_COMPLETE.md** (section "How to Add New Items")
2. Reference: **STACKABLE_ITEMS_GUIDE.md** (for stackability setup)

### Quantity text isn't showing
1. Read: **QUANTITY_TEXT_TROUBLESHOOTING.md**
2. Check: **SETUP_TEXTMESHPRO.md** if you need to add text components

### Items disappear when inventory is full
1. Read: **INVENTORY_FULL_FIX.md**
2. This is expected behavior - items should stay in the world!

### Inventory shows when pause menu opens
1. Read: **PAUSE_INVENTORY_FIX.md**
2. Verify your setup matches the configuration instructions

### I need to understand the entire system
1. Start with: **INVENTORY_SYSTEM_COMPLETE.md**
2. Then read specific guides as needed

---

## 📁 Inventory System File Structure

```
Assets/Scripts/Inventory/
├── Bag/
│   └── BagPickup.cs
├── Inventory/
│   ├── InventoryManager.cs
│   ├── InventorySlot.cs
│   └── ItemPickup.cs
├── InventoryUI/
│   └── InventoryUIToggle.cs
├── Items/
│   └── ItemData.cs
└── Docs/ (This folder)
    ├── README.md (you are here)
    ├── INVENTORY_SYSTEM_COMPLETE.md
    ├── STACKABLE_ITEMS_GUIDE.md
    ├── QUANTITY_TEXT_TROUBLESHOOTING.md
    ├── SETUP_TEXTMESHPRO.md
    ├── INVENTORY_FULL_FIX.md
    └── PAUSE_INVENTORY_FIX.md
```

---

## 🎮 System Features at a Glance

| Feature | Documentation | Script |
|---------|---------------|--------|
| Bag requirement | INVENTORY_SYSTEM_COMPLETE | BagPickup.cs |
| Stackable items | STACKABLE_ITEMS_GUIDE | ItemData.cs, InventorySlot.cs |
| Quantity display | QUANTITY_TEXT_TROUBLESHOOTING | InventorySlot.cs |
| Item pickup | INVENTORY_SYSTEM_COMPLETE | ItemPickup.cs |
| Inventory full | INVENTORY_FULL_FIX | InventoryManager.cs |
| TAB toggle | INVENTORY_SYSTEM_COMPLETE | InventoryUIToggle.cs |
| Pause integration | PAUSE_INVENTORY_FIX | PauseMenu.cs, InventoryUIToggle.cs |

---

## 💡 Tips

- Always create ItemData assets for new items
- Set `isStackable = TRUE` for consumables
- Set `isStackable = FALSE` for unique items
- TextMeshPro is required for quantity display
- Quantity text only shows for stackable items
- Items are preserved when inventory is full
- Inventory closes automatically when pausing

---

## 🐛 Need Help?

1. Check the troubleshooting section in the relevant guide
2. Review the system behavior examples
3. Check console for debug messages
4. Verify setup matches the configuration instructions

Happy inventory building! 🎮✨

