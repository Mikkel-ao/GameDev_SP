using System.Collections.Generic;

/// <summary>
/// Interface for any entity that can hold an inventory (player, enemy, NPC, chest…).
/// </summary>
public interface IInventory
{
    /// <summary>All items currently in this inventory.</summary>
    IReadOnlyList<ItemData> Items { get; }

    /// <summary>Add an item. Returns true if successful.</summary>
    bool AddItem(ItemData item);

    /// <summary>Remove one instance of an item. Returns true if it was present.</summary>
    bool RemoveItem(ItemData item);

    /// <summary>Returns true if the inventory contains at least one of this item.</summary>
    bool HasItem(ItemData item);

    /// <summary>All items that belong to the given category.</summary>
    IEnumerable<ItemData> GetItemsByCategory(ItemCategory category);

    /// <summary>Total number of items currently held.</summary>
    int ItemCount { get; }
}
