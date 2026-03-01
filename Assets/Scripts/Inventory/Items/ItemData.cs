using UnityEngine;

/// <summary>
/// Defines an item in the inventory system.
/// Contains basic item info and stackability settings.
/// Create instances of this using: Right-click > Create > Inventory > Item Data
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] public string itemName = "Item";
    [SerializeField] public Sprite itemIcon;
    [SerializeField] public bool isStackable = false;
    [SerializeField] public int maxStackSize = 1; // Only used if isStackable is true

    /// <summary>
    /// Returns the maximum stack size for this item.
    /// Non-stackable items always have a max of 1.
    /// </summary>
    public int GetMaxStackSize()
    {
        return isStackable ? maxStackSize : 1;
    }
}

