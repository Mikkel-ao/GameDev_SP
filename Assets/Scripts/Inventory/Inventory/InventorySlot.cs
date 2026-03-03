using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Represents a single slot in the inventory UI.
/// Each slot can hold one item type with a quantity (for stackable items).
/// Slots are hidden when empty and shown when an item is added.
/// </summary>
public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // UI Image component that displays the item sprite
    public TextMeshProUGUI quantityText; // Text component to show item count (for stackable items)

    private ItemData _currentItem; // The item data stored in this slot
    private int _quantity = 0; // How many of this item we have in this slot

    void Start()
    {
        // Hide empty slots at start
        gameObject.SetActive(false);
        
        // Initialize quantity text if not already set
        if (quantityText != null)
        {
            quantityText.enabled = false;
            quantityText.text = "1";
        }
    }

    /// <summary>
    /// Adds an item to this slot or increases quantity if already contains the same item.
    /// Makes the slot visible.
    /// </summary>
    public void AddItem(ItemData itemData, int quantity = 1)
    {
        // Validate inputs
        if (itemData == null)
        {
            Debug.LogError($"InventorySlot on {gameObject.name}: itemData is null!");
            return;
        }

        if (itemImage == null)
        {
            Debug.LogError($"InventorySlot on {gameObject.name}: itemImage is not assigned! Cannot add item.");
            return;
        }

        _currentItem = itemData;
        _quantity = quantity;

        itemImage.sprite = itemData.itemIcon;
        itemImage.enabled = true;

        // Update quantity display for stackable items
        UpdateQuantityDisplay();

        // Show the slot when an item is added
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Increases the quantity in this slot.
    /// </summary>
    public void IncreaseQuantity(int amount = 1)
    {
        _quantity += amount;
        UpdateQuantityDisplay();
    }

    /// <summary>
    /// Decreases the quantity in this slot.
    /// If quantity reaches 0, removes the item completely.
    /// </summary>
    public void DecreaseQuantity(int amount = 1)
    {
        _quantity -= amount;

        if (_quantity <= 0)
        {
            RemoveItem();
        }
        else
        {
            UpdateQuantityDisplay();
        }
    }

    /// <summary>
    /// Updates the quantity text display on the UI.
    /// Shows quantity for stackable items starting from 1.
    /// </summary>
    private void UpdateQuantityDisplay()
    {
        if (quantityText != null)
        {
            if (_currentItem != null && _currentItem.isStackable && _quantity > 0)
            {
                quantityText.text = _quantity.ToString();
                quantityText.enabled = true;
            }
            else
            {
                quantityText.enabled = false;
            }
        }
    }

    /// <summary>
    /// Gets the current quantity in this slot.
    /// </summary>
    public int GetQuantity()
    {
        return _quantity;
    }

    /// <summary>
    /// Gets the item data currently in this slot.
    /// </summary>
    public ItemData GetItemData()
    {
        return _currentItem;
    }

    /// <summary>
    /// Checks if this slot contains a specific item.
    /// </summary>
    public bool ContainsItem(ItemData itemData)
    {
        return _currentItem == itemData && _quantity > 0;
    }

    /// <summary>
    /// Checks if this slot can accept more of the given item.
    /// Returns false if the item is not stackable or stack is full.
    /// </summary>
    public bool CanAddItem(ItemData itemData)
    {
        if (_currentItem == null)
            return true; // Empty slot can accept any item

        if (_currentItem != itemData)
            return false; // Different item type

        if (!itemData.isStackable)
            return false; // Non-stackable items can't be added to occupied slot

        return _quantity < itemData.GetMaxStackSize(); // Check if we have room to stack
    }

    /// <summary>
    /// Removes the item from this slot completely.
    /// Makes the slot invisible.
    /// </summary>
    public void RemoveItem()
    {
        _currentItem = null;
        _quantity = 0;
        itemImage.sprite = null;
        itemImage.enabled = false;
        
        if (quantityText != null)
            quantityText.enabled = false;

        // Hide the slot when it's empty
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Checks if this slot is completely empty and available for a new item.
    /// </summary>
    public bool IsEmpty()
    {
        return _currentItem == null || _quantity == 0;
    }
}