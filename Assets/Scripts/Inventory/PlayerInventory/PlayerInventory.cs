using UnityEngine;

/// <summary>
/// Sits on the Player. Owns the ItemCollectedBag reference and keeps the old
/// coin/gem counter so existing GemCollect code still works unchanged.
/// </summary>
[RequireComponent(typeof(ItemCollectedBag))]
public class PlayerInventory : MonoBehaviour
{
    // ── Gem / coin counter (backward-compatible with GemCollect) ─────────────
    public int NumberOfCoins { get; private set; }

    /// <summary>Called by GemCollect when a gem is picked up.</summary>
    public void GemCollected()
    {
        NumberOfCoins++;
        Debug.Log($"[PlayerInventory] Coins: {NumberOfCoins}");
    }

    // ── Item bag ─────────────────────────────────────────────────────────────
    private ItemCollectedBag _bag;

    /// <summary>Access the full IInventory interface for item management.</summary>
    public IInventory Bag => _bag;

    private void Awake()
    {
        _bag = GetComponent<ItemCollectedBag>();
    }

    // ── Shortcut helpers ─────────────────────────────────────────────────────
    public bool AddItem(ItemData item)    => _bag.AddItem(item);
    public bool RemoveItem(ItemData item) => _bag.RemoveItem(item);
    public bool HasItem(ItemData item)    => _bag.HasItem(item);
}
