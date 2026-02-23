using System;
using UnityEngine;

/// <summary>
/// Handles coin collection when the player collides with individual gems.
/// Each gem child must have its own TRIGGER collider.
/// This script is placed on the PARENT empty GameObject.
/// 
/// Setup Instructions:
/// 1. Create a parent empty GameObject called "Gems"
/// 2. Attach this script to the parent
/// 3. Create child GameObjects for each gem (Gem1, Gem2, etc.)
/// 4. Add a TRIGGER collider to EACH child gem
/// 5. Do NOT add a collider to the parent
/// 6. The player should have a non-trigger collider
/// </summary>
public class GemCollect : MonoBehaviour
{
    /// <summary>
    /// Called when a gem child's collider enters a trigger.
    /// This is handled by physics callbacks on child colliders.
    /// We use a workaround by checking all child colliders.
    /// </summary>
    private void Start()
    {
        // Add OnTriggerEnter listeners to all child colliders
        // Since we can't directly listen to child events, we'll use a different approach
        
        // Create a trigger detector script for each child
        foreach (Transform child in transform)
        {
            Collider collider = child.GetComponent<Collider>();
            if (collider != null && collider.isTrigger)
            {
                // Add a GemTrigger component to each child to detect collisions
                if (child.GetComponent<GemTrigger>() == null)
                {
                    GemTrigger trigger = child.gameObject.AddComponent<GemTrigger>();
                    trigger.SetParentGemCollect(this);
                }
            }
        }
    }

    /// <summary>
    /// Called by individual gem trigger detectors when a gem is collected.
    /// </summary>
    public void OnGemCollected(Transform gemTransform, PlayerInventory playerInventory)
    {
        if (playerInventory != null)
        {
            // Notify the player that a gem has been collected
            playerInventory.GemCollected();
            
            // Debug log to verify collection is working
            Debug.Log("Gem collected! Total gems: " + playerInventory.NumberOfCoins);
            
            // Disable only the specific gem that was collected
            gemTransform.gameObject.SetActive(false);
        }
    }
}

/// <summary>
/// Helper class to detect trigger collisions on individual gem children.
/// This component is automatically added to each gem child by GemCollect.
/// </summary>
public class GemTrigger : MonoBehaviour
{
    private GemCollect _parentGemCollect;
    private bool _hasBeenCollected = false;

    public void SetParentGemCollect(GemCollect parentGemCollect)
    {
        _parentGemCollect = parentGemCollect;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prevent collecting the same gem twice
        if (_hasBeenCollected)
            return;

        // Attempt to get the PlayerInventory component from the colliding object
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        
        // If the colliding object has a PlayerInventory component, it's the player
        if (playerInventory != null)
        {
            _hasBeenCollected = true;
            
            // Call the parent's method to handle collection
            if (_parentGemCollect != null)
            {
                _parentGemCollect.OnGemCollected(transform, playerInventory);
            }
        }
    }
}
