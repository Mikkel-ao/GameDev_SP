using System;
using UnityEngine;

/// <summary>
/// Handles coin collection when the player collides with a coin object.
/// Detects trigger collisions and notifies the player inventory of coin collection.
/// </summary>
public class CoinCollect : MonoBehaviour
{
    /// <summary>
    /// Called when another collider enters this object's trigger collider.
    /// Checks if the colliding object is the player, collects the coin, and deactivates it.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Attempt to get the PlayerInventory component from the colliding object
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        
        // If the colliding object has a PlayerInventory component, it's the player
        if (playerInventory != null)
        {
            // Notify the player that a coin has been collected
            playerInventory.CoinCollected();    
            
            // Disable the coin to remove it from the game world
            gameObject.SetActive(false);
        }
    }
}
