using UnityEngine;

/// <summary>
/// Handles picking up a weapon and attaching it to a socket on the player.
/// Can be picked up once.
/// </summary>
public class WeaponPickup : MonoBehaviour
{
    [Header("Socket on the player where the weapon attaches")]
    [SerializeField] private Transform weaponSocket;

    [Header("Local offset inside the socket")]
    [SerializeField] private Vector3 attachPositionOffset = Vector3.zero;
    [SerializeField] private Vector3 attachRotationOffset = Vector3.zero;
    [SerializeField] private Vector3 attachScale = Vector3.one;

    private bool _hasBeenPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        // Prevent picking up the same weapon twice
        if (_hasBeenPickedUp) return;

        // Only the player can pick up the weapon
        if (!other.CompareTag("Player") && !other.CompareTag("Capsule")) return;

        _hasBeenPickedUp = true;

        // Disable colliders so it can't be picked up again
        foreach (Collider col in GetComponents<Collider>())
            col.enabled = false;

        // Optional: disable physics after pickup
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        // Attach weapon to player's designated socket
        transform.SetParent(weaponSocket);
        transform.localPosition = attachPositionOffset;
        transform.localEulerAngles = attachRotationOffset;
        transform.localScale = attachScale;

        Debug.Log("Weapon picked up and attached to player!");
    }
}