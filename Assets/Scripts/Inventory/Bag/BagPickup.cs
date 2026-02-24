using UnityEngine;

/// <summary>
/// Place this on the Bag_4 prefab in the world.
/// When the player walks into the trigger the bag:
///   1. Calls a method on the player so they "own" it.
///   2. Attaches itself visually to a socket on the player (e.g. a back bone).
///   3. Disables its own collider so it can't be re-collected.
///
/// Setup:
///  - Set the MeshCollider on Bag_4 to IsTrigger = true (or add a separate
///    trigger collider — a small SphereCollider works great).
///  - On the Player GameObject, add an empty child called "BagSocket" and
///    position it on the player's back.
///  - Assign that Transform to the bagSocket field below.
/// </summary>
public class BagPickup : MonoBehaviour
{
    [Header("Socket on the player where the bag attaches")]
    [Tooltip("Drag the 'BagSocket' child Transform from your Player here.")]
    [SerializeField] private Transform bagSocket;

    [Header("Local offset inside the socket (fine-tune position/rotation)")]
    [SerializeField] private Vector3 attachPositionOffset = Vector3.zero;
    [SerializeField] private Vector3 attachRotationOffset = Vector3.zero;
    [SerializeField] private Vector3 attachScale = Vector3.one;

    private bool _hasBeenPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_hasBeenPickedUp) return;

        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory == null) return;

        _hasBeenPickedUp = true;

        Debug.Log("[BagPickup] Player picked up the bag!");

        // Disable collider(s) so it can't be triggered again
        foreach (Collider col in GetComponents<Collider>())
            col.enabled = false;

        // Attach visually to the player
        AttachToPlayer(playerInventory);
    }

    private void AttachToPlayer(PlayerInventory playerInventory)
    {
        // Prefer the serialized socket; fall back to the player root
        Transform socket = bagSocket != null
            ? bagSocket
            : playerInventory.transform;

        transform.SetParent(socket);
        transform.localPosition = attachPositionOffset;
        transform.localEulerAngles = attachRotationOffset;
        transform.localScale = attachScale;

        Debug.Log($"[BagPickup] Bag attached to '{socket.name}'.");
    }
}
