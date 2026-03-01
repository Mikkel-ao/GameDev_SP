using UnityEngine;

/// <summary>
/// Central manager for the entire inventory system.
/// Tracks if player has the bag, manages item pickups (including stackable items), and communicates with UI.
/// Uses singleton pattern to be easily accessible from other scripts.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton instance for easy access

    [SerializeField] public InventorySlot[] slots; // All inventory slots - drag them here in inspector
    
    [SerializeField] public InventoryUIToggle uiToggle; // Reference to UI toggle script - drag here in inspector

    private bool _hasBag; // Flag: true when player has picked up the bag


    void OnEnable()
    {
        // Set up singleton
        instance = this;
    }

    /// <summary>
    /// Called when the player picks up the bag.
    /// Enables the inventory UI and item pickup functionality.
    /// </summary>
    public void PickedUpBag()
    {
        _hasBag = true;
        uiToggle.OnBagPickedUp();
        Debug.Log("Inventory bag obtained!");
    }

    /// <summary>
    /// Checks if the player currently has the bag.
    /// Items can only be picked up if this returns true.
    /// </summary>
    public bool HasBag()
    {
        return _hasBag;
    }

    /// <summary>
    /// Tries to add an item to the inventory.
    /// Handles both stackable and non-stackable items.
    /// If stackable, tries to add to existing stack first.
    /// If non-stackable or no room to stack, finds a new empty slot.
    /// Returns true if the item was successfully added, false if inventory is full.
    /// </summary>
    public bool PickupItem(ItemData itemData, int quantity)
    {
        // Validate inputs
        if (itemData == null)
        {
            Debug.LogError("InventoryManager: itemData is null!");
            return false;
        }

        // Check if slots array is assigned
        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("InventoryManager: slots array is not assigned or empty! Assign slots in the Inspector.");
            return false;
        }

        // If item is stackable, try to add to existing stack first
        if (itemData.isStackable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    Debug.LogError($"InventoryManager: Slot at index {i} is NULL! You need to assign a GameObject with InventorySlot script to this slot in the Inspector.");
                    continue;
                }

                // Check if this slot has the same item and can accept more
                if (slots[i].ContainsItem(itemData) && slots[i].CanAddItem(itemData))
                {
                    int amountToAdd = Mathf.Min(quantity, itemData.GetMaxStackSize() - slots[i].GetQuantity());
                    slots[i].IncreaseQuantity(amountToAdd);
                    Debug.Log($"Item stacked! Added {amountToAdd} to slot {i}. Total: {slots[i].GetQuantity()}");
                    
                    // If there's still quantity left, recursively add the rest
                    int remaining = quantity - amountToAdd;
                    if (remaining > 0)
                    {
                        return PickupItem(itemData, remaining);
                    }
                    return true;
                }
            }
        }

        // No existing stack found (or item is not stackable), find empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                Debug.LogError($"InventoryManager: Slot at index {i} is NULL! You need to assign a GameObject with InventorySlot script to this slot in the Inspector.");
                continue;
            }

            if (slots[i].IsEmpty())
            {
                slots[i].AddItem(itemData, quantity);
                Debug.Log($"Item picked up! Added to inventory slot {i}.");
                return true;
            }
        }

        // All slots are full
        Debug.Log("Inventory is full!");
        return false;
    }

    // Backward compatibility method (legacy for Sprite-based pickups)
    public void PickupItem(Sprite icon)
    {
        Debug.LogWarning("InventoryManager: Using legacy PickupItem(Sprite) method. Please use PickupItem(ItemData) instead!");
    }
}