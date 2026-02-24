using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A bag that stores collected items.
/// Attach this to the Player, an Enemy, a Chest — anything that needs an inventory.
/// Implements IInventory so it can be referenced through the interface.
/// </summary>
public class ItemCollectedBag : MonoBehaviour, IInventory
{
    // ── Events ──────────────────────────────────────────────────────────────
    /// <summary>Fired whenever an item is added or removed.</summary>
    public event Action OnInventoryChanged;

    // ── Internal state ───────────────────────────────────────────────────────
    private readonly List<ItemData> _items = new List<ItemData>();

    // ── IInventory ───────────────────────────────────────────────────────────
    public IReadOnlyList<ItemData> Items => _items;

    public int ItemCount => _items.Count;

    public bool AddItem(ItemData item)
    {
        if (item == null) return false;

        _items.Add(item);
        Debug.Log($"[Bag] Added '{item.ItemName}'. Total items: {_items.Count}");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool RemoveItem(ItemData item)
    {
        bool removed = _items.Remove(item);
        if (removed)
        {
            Debug.Log($"[Bag] Removed '{item.ItemName}'. Total items: {_items.Count}");
            OnInventoryChanged?.Invoke();
        }
        return removed;
    }

    public bool HasItem(ItemData item) => _items.Contains(item);

    public IEnumerable<ItemData> GetItemsByCategory(ItemCategory category)
        => _items.Where(i => i.Category == category);

    // ── Convenience ──────────────────────────────────────────────────────────
    /// <summary>Sums a specific stat across all items in the bag.</summary>
    public int GetTotalStrength()     => _items.Sum(i => i.Stats.Strength);
    public int GetTotalIntelligence() => _items.Sum(i => i.Stats.Intelligence);
    public int GetTotalWillpower()    => _items.Sum(i => i.Stats.Willpower);
}
