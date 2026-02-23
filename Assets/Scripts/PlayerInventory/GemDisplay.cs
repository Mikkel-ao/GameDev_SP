using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays the number of gems collected in the UI.
/// Updates in real-time as the player collects gems.
/// </summary>
public class GemDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemCountText;
    [SerializeField] private PlayerInventory playerInventory;

    private void Start()
    {
        // If playerInventory is not assigned, try to find it
        if (playerInventory == null)
        {
            playerInventory = FindFirstObjectByType<PlayerInventory>();
        }

        // Make sure we have both references
        if (gemCountText == null)
        {
            Debug.LogError("GemDisplay: gemCountText is not assigned!");
            return;
        }

        if (playerInventory == null)
        {
            Debug.LogError("GemDisplay: playerInventory not found in scene!");
            return;
        }

        // Update the display immediately
        UpdateGemCount();
    }

    private void Update()
    {
        // Update the display every frame (simple approach)
        // This will update whenever the gem count changes
        UpdateGemCount();
    }

    /// <summary>
    /// Updates the gem count display text.
    /// </summary>
    private void UpdateGemCount()
    {
        if (gemCountText != null && playerInventory != null)
        {
            gemCountText.text = "Gems Collected: " + playerInventory.NumberOfCoins;
        }
    }
}
