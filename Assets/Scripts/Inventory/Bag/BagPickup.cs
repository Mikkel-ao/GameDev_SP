using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Handles the pickup of the inventory bag.
/// Attaches the bag to the player and enables inventory functionality.
/// Can only be picked up once.
/// </summary>
public class BagPickup : MonoBehaviour
{
    [Header("Socket on the player where the bag attaches")]
    [SerializeField] private Transform bagSocket;

    [Header("Local offset inside the socket")]
    [SerializeField] private Vector3 attachPositionOffset = Vector3.zero;
    [SerializeField] private Vector3 attachRotationOffset = Vector3.zero;
    [SerializeField] private Vector3 attachScale = Vector3.one;

    private bool _hasBeenPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        // Prevent picking up the same bag twice
        if (_hasBeenPickedUp) return;

        // Only the player can pick up the bag
        if (!other.CompareTag("Player") && !other.CompareTag("Capsule")) return;

        _hasBeenPickedUp = true;
        
        // Disable collider so it can't be picked up again
        foreach (Collider col in GetComponents<Collider>())
            col.enabled = false;

        // Attach bag to player's designated socket
        transform.SetParent(bagSocket);
        transform.localPosition = attachPositionOffset;
        transform.localEulerAngles = attachRotationOffset;
        transform.localScale = attachScale;
        
        // Notify InventoryManager that player now has a bag
        InventoryManager.instance.PickedUpBag();

        Debug.Log("Bag picked up and attached to player!");
    }
}