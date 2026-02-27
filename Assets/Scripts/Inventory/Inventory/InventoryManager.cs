using UnityEngine;

/// <summary>
/// Central manager for the entire inventory system.
/// Tracks if player has the bag, manages item pickups, and communicates with UI.
/// Uses singleton pattern to be easily accessible from other scripts.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton instance for easy access

    public InventorySlot[] slots; // All inventory slots - drag them here in inspector
    
    public InventoryUIToggle uiToggle; // Reference to UI toggle script - drag here in inspector

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
    /// Tries to add an item to the first available inventory slot.
    /// If no empty slots are available, logs "Inventory is full!".
    /// </summary>
    public void PickupItem(Sprite icon)
    {
        // Check if slots array is assigned
        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("InventoryManager: slots array is not assigned or empty! Assign slots in the Inspector.");
            return;
        }

        // Find the first empty slot and add the item there
        for (int i = 0; i < slots.Length; i++)
        {
            // Check if this slot reference is null
            if (slots[i] == null)
            {
                Debug.LogError($"InventoryManager: Slot at index {i} is NULL! You need to assign a GameObject with InventorySlot script to this slot in the Inspector.");
                continue;
            }

            if (slots[i].IsEmpty())
            {
                slots[i].AddItem(icon);
                Debug.Log($"Item picked up! Added to inventory slot {i}.");
                return;
            }
        }

        // All slots are full
        Debug.Log("Inventory is full!");
    }
}