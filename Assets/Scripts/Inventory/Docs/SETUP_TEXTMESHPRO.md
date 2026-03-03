# TextMeshPro Quantity Text Setup Guide

## Quick Solution - 3 Steps

### Step 1: Create TextMeshPro Text on Your Slot

1. **Select your slot in the Hierarchy** (e.g., "Slot_0")
2. **Right-click on it** → **UI** → **TextMeshPro - Text**
3. A new child object will appear called **TextMeshProUGUI**

### Step 2: Configure the Text

1. **Select the new TextMeshProUGUI object**
2. In the Inspector, find the **TextMeshProUGUI** component and set:
   - **Text**: Leave it empty or type "1"
   - **Font Size**: 36 (or adjust to what looks good)
   - **Color**: White (255, 255, 255, 255)
   - **Alignment**: Bottom Right (so it appears in corner)

### Step 3: Assign it to Your Slot Script

1. **Select the Slot** (parent object)
2. Find the **InventorySlot** component
3. **Drag the TextMeshProUGUI object** into the "Quantity Text" field
4. Done! ✅

---

## If You Don't See TextMeshPro Option

If you don't see "TextMeshPro - Text" option:

1. **Window** → **TextMeshPro** → **Import TMP Essential Resources**
2. Click **Import**
3. Wait for import to complete
4. Now try again: Right-click slot → **UI** → **TextMeshPro - Text**

---

## Visual Guide

### Before:
```
Slot_0
├── Image (shows item icon)
└── (no text component)
```

### After:
```
Slot_0
├── Image (shows item icon)
└── TextMeshProUGUI (shows quantity like "5")
```

---

## Troubleshooting

### "I still don't see TextMeshPro option"
- Make sure you're right-clicking on a **UI** object in a **Canvas**
- TextMeshPro only works with UI elements on Canvas

### "Text doesn't show up"
- Check the **Color** is not transparent
- Check the **Font Size** is not 0
- Check the **Rect Transform** position isn't off-screen

### "Text shows but looks blurry"
- Select the TextMeshProUGUI object
- In Inspector, find **TextMeshProUGUI** component
- Set **Font Size** to 36-48
- Make sure **Material** is set to "default"

---

## Optional: Rename for Clarity

After creating, you can rename it:
1. Right-click TextMeshProUGUI
2. Click "Rename"
3. Type "AmountText" or "QuantityText"

This makes it easier to identify in your hierarchy!

