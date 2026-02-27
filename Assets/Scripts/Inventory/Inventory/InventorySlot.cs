using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a single slot in the inventory UI.
/// Each slot displays one item and can be filled or emptied.
/// Slots are hidden when empty and shown when an item is added.
/// </summary>
public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // UI Image component that displays the item sprite

    void Start()
    {
        // Hide empty slots at start
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Adds an item to this slot by displaying its sprite.
    /// Makes the slot visible.
    /// </summary>
    public void AddItem(Sprite icon)
    {
        // Check if itemImage is assigned
        if (itemImage == null)
        {
            Debug.LogError($"InventorySlot on {gameObject.name}: itemImage is not assigned! Cannot add item.");
            return;
        }

        itemImage.sprite = icon;
        itemImage.enabled = true;
        // Show the slot when an item is added
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Removes the item from this slot.
    /// Makes the slot invisible.
    /// </summary>
    public void RemoveItem()
    {
        itemImage.sprite = null;
        itemImage.enabled = false;
        // Hide the slot when it's empty
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks if this slot is empty and available for a new item.
    /// </summary>
    public bool IsEmpty()
    {
        // Check if itemImage is assigned
        if (itemImage == null)
        {
            Debug.LogError($"InventorySlot on {gameObject.name}: itemImage is not assigned! Assign it in the Inspector.");
            return false;
        }

        return !itemImage.enabled;
    }
}