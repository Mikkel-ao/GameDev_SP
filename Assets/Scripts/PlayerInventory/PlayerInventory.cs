using UnityEngine;

/// <summary>
/// Manages the player's coin inventory.
/// Tracks the number of coins collected throughout the game.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    /// <summary>
    /// Gets the total number of coins currently collected by the player.
    /// </summary>
    public int NumberOfCoins { get; private set; }

    /// <summary>
    /// Called when the player collects a coin.
    /// Increments the coin counter by one.
    /// </summary>
    public void CoinCollected()
    {
        NumberOfCoins++;
    }
}