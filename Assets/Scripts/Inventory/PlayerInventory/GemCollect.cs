using UnityEngine;

/// <summary>
/// Handles gem/item collection when the player collides with individual gems.
/// Unchanged logic — just updated to also optionally add an ItemData to the bag.
/// 
/// Setup: same as before.
/// Optionally assign a gemItemData to also add the gem as a tracked item.
/// </summary>
public class GemCollect : MonoBehaviour
{
    [Tooltip("Optional: the ItemData asset this gem represents. If assigned it will be added to the player's bag.")]
    [SerializeField] private ItemData gemItemData;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            Collider col = child.GetComponent<Collider>();
            if (col != null && col.isTrigger)
            {
                if (child.GetComponent<GemTrigger>() == null)
                {
                    GemTrigger trigger = child.gameObject.AddComponent<GemTrigger>();
                    trigger.SetParentGemCollect(this);
                }
            }
        }
    }

    public void OnGemCollected(Transform gemTransform, PlayerInventory playerInventory)
    {
        if (playerInventory == null) return;

        // Legacy coin counter
        playerInventory.GemCollected();

        // Add to the item bag if an ItemData is assigned
        if (gemItemData != null)
            playerInventory.AddItem(gemItemData);

        Debug.Log($"Gem collected! Total coins: {playerInventory.NumberOfCoins}");

        gemTransform.gameObject.SetActive(false);
    }
}

/// <summary>
/// Auto-added to each gem child. Detects trigger collisions and reports to GemCollect.
/// </summary>
public class GemTrigger : MonoBehaviour
{
    private GemCollect _parentGemCollect;
    private bool _hasBeenCollected = false;

    public void SetParentGemCollect(GemCollect parent) => _parentGemCollect = parent;

    private void OnTriggerEnter(Collider other)
    {
        if (_hasBeenCollected) return;

        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory == null) return;

        _hasBeenCollected = true;
        _parentGemCollect?.OnGemCollected(transform, playerInventory);
    }
}
