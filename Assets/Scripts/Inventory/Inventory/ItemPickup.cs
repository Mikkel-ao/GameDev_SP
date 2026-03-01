using UnityEngine;

/// <summary>
/// Handles the pickup of inventory items (keys, gems, potions, etc).
/// Items can only be picked up if the player has the bag.
/// Supports both stackable and non-stackable items via ItemData.
/// When picked up, the item is destroyed and added to inventory.
/// Items are NOT destroyed if the inventory is full.
/// </summary>
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData; // Drag your ItemData asset here in inspector
    [SerializeField] private int quantity = 1; // How many of this item to pick up (for stackable items)

    void OnTriggerEnter(Collider other)
    {
        // Only the player can pick up items
        if (other.CompareTag("Player"))
        {
            // Check if player has the bag before allowing item pickup
            if (!InventoryManager.instance.HasBag())
            {
                Debug.Log("You need to pick up the bag first!");
                return;
            }

            // Validate that ItemData is assigned
            if (itemData == null)
            {
                Debug.LogError($"ItemPickup on {gameObject.name}: itemData is not assigned! Assign an ItemData asset in the Inspector.");
                return;
            }

            // Try to add item to inventory
            bool wasSuccessful = InventoryManager.instance.PickupItem(itemData, quantity);
            
            // Only destroy this object if the item was successfully added
            if (wasSuccessful)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"Could not pick up {itemData.itemName} - Inventory is full!");
            }
        }
    }
}

