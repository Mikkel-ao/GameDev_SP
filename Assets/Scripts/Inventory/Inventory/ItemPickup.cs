using UnityEngine;

/// <summary>
/// Handles the pickup of inventory items (keys, gems, etc).
/// Items can only be picked up if the player has the bag.
/// When picked up, the item is destroyed and added to inventory.
/// </summary>
public class ItemPickup : MonoBehaviour
{
    public Sprite itemIcon; // drag your item sprite (key, gem, etc) here in inspector

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

            // Add item to inventory and destroy this object
            InventoryManager.instance.PickupItem(itemIcon);
            Destroy(gameObject);
        }
    }
}